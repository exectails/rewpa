﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MabiWorld;
using MabiWorld.Data;
using MackLib;
using rewpa.Properties;

namespace rewpa
{
	/// <summary>
	/// Application's main form.
	/// </summary>
	public partial class FrmMain : Form
	{
		/// <summary>
		/// Creates new instance.
		/// </summary>
		public FrmMain()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Initializes form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_Load(object sender, EventArgs e)
		{
			this.TxtSources.Text = Settings.Default.Folders;
			this.TxtSources.SelectionStart = this.TxtSources.TextLength;
			this.BtnCreate.Select();
		}

		/// <summary>
		/// Saves settings.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.Folders = this.TxtSources.Text;
			Settings.Default.Save();
		}

		/// <summary>
		/// Updates progressbar.
		/// </summary>
		/// <param name="value">
		/// -1 will display marquee, while anything above 0 will display
		/// that value.
		/// </param>
		private void UpdateProgressBar(int value)
		{
			this.Invoke((MethodInvoker)delegate
			{
				if (value == -1)
				{
					this.ProgressBar.Style = ProgressBarStyle.Marquee;
					this.ProgressBar.Value = 0;
				}
				else
				{
					this.ProgressBar.Style = ProgressBarStyle.Blocks;
					this.ProgressBar.Value = value;
				}
			});
		}

		/// <summary>
		/// Updates status label.
		/// </summary>
		/// <param name="statusText"></param>
		private void UpdateStatus(string statusText)
		{
			this.Invoke((MethodInvoker)delegate { this.LblStatus.Text = statusText; });
		}

		/// <summary>
		/// Opens help dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnHelp_Click(object sender, EventArgs e)
		{
			new FrmAbout().ShowDialog();
		}

		/// <summary>
		/// Reads given folders and generates regioninfo.dat.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void BtnCreate_Click(object sender, EventArgs e)
		{
			this.UpdateProgressBar(-1);
			this.BtnCreate.Enabled = false;
			this.TxtSources.Enabled = false;
			this.LblStatus.Text = "Reading files...";

			var regions = new Dictionary<int, MabiWorld.Region>();
			var cancel = false;

			// Run in task to not block UI
			await Task.Run(() =>
			{
				// Read regions from folders
				foreach (var line in this.TxtSources.Lines)
				{
					var trimmedLine = line.Trim();

					if (trimmedLine == "" || trimmedLine.StartsWith("//"))
						continue;

					bool success;
					if (Directory.Exists(line))
					{
						if (Directory.EnumerateFiles(line, "*.pack", SearchOption.TopDirectoryOnly).Any())
						{
							success = this.GetRegionsFromPacks(line, ref regions);
						}
						else
						{
							success = this.GetRegionsFromFolder(line, ref regions);
						}
					}
					else if (File.Exists(line) && string.Compare(Path.GetFileName(line), "world.trn") == 0)
					{
						success = this.GetRegionsFromFolder(line, ref regions);
					}
					else
					{
						this.Invoke((MethodInvoker)delegate
						{
							MessageBox.Show(this, $"Invalid directory or file: '{line}'.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
						});
						continue;
					}

					if (!success)
					{
						cancel = true;
						return;
					}
				}

				// Remove props from areas that are only enabled during
				// certain events or when features are enabled that are
				// currently not.
				// TODO: Include all props and let the server handle this.
				foreach (var area in regions.Values.SelectMany(a => a.Areas))
				{
					area.Props.RemoveAll(prop =>
					{
						if (!PropDb.TryGetEntry(prop.Id, out var data))
							return false;

						if (data.StringID.Value.Contains("/event/") && data.UsedServer)
							return true;

						// Some invisible prop at Dunbarton's Unicorn statue
						if (data.StringID.Value.Contains("/weekly_concert/"))
							return true;

						// Invisible Emain cooking contest props
						if (data.StringID.Value.Contains("/contest/"))
							return true;

						// Invisible Emain concert hall barricade
						if (data.ClassName == "scene_emain_concert_hall_barricade01")
							return true;

						// Invisible Gairech Halloween event prop
						if (data.ClassName == "field_gairech_halloween2018_portal_place01")
						{
							// Controlled by the locale ExtraXML property,
							// where "usa" is not negated while the event is
							// active <_> WHY DO YOU HATE SERVER SIDED PROPS
							// DEVCAT!? >__>
							// ExtraXML="&lt;xml locale=&quot;!korea|usa|japan|!taiwan|!china&quot;/&gt;"
							if (data.ExtraXML.Contains("!usa"))
								return true;
						}

						if (!string.IsNullOrWhiteSpace(data.Feature) && !Features.IsEnabled(data.Feature))
							return true;

						return false;
					});
				}
			});

			if (cancel)
			{
				regions.Clear();
			}

			// Update UI and move region info
			this.Invoke((MethodInvoker)delegate
			{
				this.UpdateProgressBar(0);
				this.UpdateStatus($"Found {regions.Count} regions.");
				this.BtnCreate.Enabled = true;
				this.TxtSources.Enabled = true;

				if (regions.Count == 0)
				{
					MessageBox.Show(this, "No regions found.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				var result = this.SfdRegionInfo.ShowDialog();
				if (result == DialogResult.OK)
				{
					this.BtnCreate.Select();

					var filePath = this.SfdRegionInfo.FileName;
					var folderPath = Path.GetDirectoryName(filePath);

					Settings.Default.SaveFolder = folderPath;
					this.Export(filePath, regions);

					this.UpdateStatus($"Saved region info at '{filePath}'.");
				}
				else
				{
					this.UpdateStatus($"Saving cancelled.");
				}
			});
		}

		/// <summary>
		/// Reads world.trn in packs and all regions that are listed within.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="regions"></param>
		private bool GetRegionsFromPacks(string path, ref Dictionary<int, MabiWorld.Region> regions)
		{
			using (var pack = new PackReader(path))
			{
				var worldEntry = pack.GetEntry(@"data\world\world.trn");
				if (worldEntry == null)
				{
					this.Invoke((MethodInvoker)delegate
					{
						MessageBox.Show(this, $"File 'world.trn' not found in pack folder '{path}'.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
					});
					return false;
				}

				var propEntry = pack.GetEntry(@"data\db\propdb.xml");
				if (propEntry != null)
				{
					PropDb.Clear();
					PropDb.Load(propEntry.GetDataAsStream());
				}

				var featureEntry = pack.GetEntry(@"data\features.xml.compiled");
				if (featureEntry != null)
				{
					Features.Clear();
					Features.Load(featureEntry.GetDataAsStream());
					Features.SelectSetting("USA", false, false);
				}

				using (var ms = worldEntry.GetDataAsStream())
				using (var trnReader = new XmlTextReader(ms))
				{
					if (!trnReader.ReadToDescendant("regions"))
						throw new FormatException("Tag 'regions' not found in world.trn.");

					using (var trnRegionsReader = trnReader.ReadSubtree())
					{
						while (trnRegionsReader.ReadToFollowing("region"))
						{
							var workDir = trnRegionsReader.GetAttribute("workdir");
							var fileName = trnReader.GetAttribute("name");
							var regionFilePath = Path.Combine("data", "world", workDir, fileName + ".rgn");
							var regionEntry = pack.GetEntry(regionFilePath);

							if (regionEntry == null)
								continue;

							try
							{
								using (var regionStream = regionEntry.GetDataAsStream())
								{
									this.UpdateStatus($"Reading {fileName}...");

									var region = MabiWorld.Region.ReadFrom(regionStream);
									regions[region.Id] = region;

									for (var i = 0; i < region.AreaFileNames.Count; ++i)
									{
										var areaFileName = region.AreaFileNames[i];
										var areaFilePath = Path.Combine("data", "world", workDir, areaFileName + ".area");

										using (var areaStream = pack.GetEntry(areaFilePath).GetDataAsStream())
										{
											var area = Area.ReadFrom(areaStream);
											area.AreaPlanes.Clear();
											region.Areas.Add(area);
										}
									}
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show($"Failed to parse region '{regionFilePath}', error: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
								return false;
							}
						}
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Loads all .rgn files in given folder and all its subfolders.
		/// Returns whether the process was successful.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="regions"></param>
		private bool GetRegionsFromFolder(string path, ref Dictionary<int, MabiWorld.Region> regions)
		{
			var propPath = Path.Combine(path, @"db\propdb.xml");
			if (File.Exists(propPath))
			{
				PropDb.Clear();
				PropDb.Load(propPath);
			}

			var featurePath = Path.Combine(path, @"features.xml.compiled");
			if (File.Exists(featurePath))
			{
				Features.Clear();
				Features.Load(featurePath);
				Features.SelectSetting("USA", false, false);
			}

			var regionFilePaths = new List<string>();

			if (File.Exists(path))
			{
				var worldFolderPath = Path.GetDirectoryName(path);

				using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
				using (var trnReader = new XmlTextReader(fs))
				{
					if (!trnReader.ReadToDescendant("regions"))
						throw new FormatException("Tag 'regions' not found in world.trn.");

					using (var trnRegionsReader = trnReader.ReadSubtree())
					{
						while (trnRegionsReader.ReadToFollowing("region"))
						{
							var workDir = trnRegionsReader.GetAttribute("workdir");
							var fileName = trnReader.GetAttribute("name");
							var regionFilePath = Path.Combine(worldFolderPath, workDir, fileName + ".rgn");

							regionFilePaths.Add(regionFilePath);
						}
					}
				}
			}
			else
			{
				regionFilePaths.AddRange(Directory.EnumerateFiles(path, "*.rgn", SearchOption.AllDirectories));
			}

			foreach (var regionFilePath in regionFilePaths)
			{
				try
				{
					using (var regionStream = new FileStream(regionFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						var fileName = Path.GetFileNameWithoutExtension(regionFilePath);

						if (string.Compare(fileName, "DungeonProp_Temp", true) == 0)
						{
							this.UpdateStatus($"Skipping {fileName}...");
							continue;
						}

						this.UpdateStatus($"Reading {fileName}...");

						var region = MabiWorld.Region.ReadFrom(regionStream);
						regions[region.Id] = region;

						var regionDirPath = Path.GetDirectoryName(regionFilePath);

						for (var i = 0; i < region.AreaFileNames.Count; ++i)
						{
							var areaFileName = region.AreaFileNames[i];
							var areaFilePath = Path.Combine(regionDirPath, areaFileName + ".area");

							using (var areaStream = new FileStream(areaFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
							{
								var area = Area.ReadFrom(areaStream);
								area.AreaPlanes.Clear();
								region.Areas.Add(area);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Failed to parse region '{regionFilePath}', error: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Writes region info to given path.
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="regions"></param>
		private void Export(string filePath, Dictionary<int, MabiWorld.Region> regions)
		{
			using (var ms = new MemoryStream())
			using (var bw = new BinaryWriter(ms))
			{
				// Regions
				bw.Write(regions.Count);
				foreach (var region in regions.Values.OrderBy(a => a.Id))
				{
					bw.Write(region.Id);
					bw.Write(region.Name);
					bw.Write(region.GroupId);

					// Bounds
					var x1 = region.Areas.Min(a => a.BottomLeft.X);
					var y1 = region.Areas.Min(a => a.BottomLeft.Y);
					var x2 = region.Areas.Max(a => a.TopRight.X);
					var y2 = region.Areas.Max(a => a.TopRight.Y);
					bw.Write((int)x1);
					bw.Write((int)y1);
					bw.Write((int)x2);
					bw.Write((int)y2);

					// Areas
					bw.Write(region.Areas.Count);
					foreach (var area in region.Areas)
					{
						bw.Write((int)area.Id);
						bw.Write(area.Name);
						bw.Write((int)area.BottomLeft.X);
						bw.Write((int)area.BottomLeft.Y);
						bw.Write((int)area.TopRight.X);
						bw.Write((int)area.TopRight.Y);

						// Props
						bw.Write(area.Props.Count);
						foreach (var prop in area.Props.OrderBy(a => a.EntityId))
						{
							bw.Write(prop.EntityId);
							bw.Write(prop.Id);
							bw.Write(prop.Name);
							bw.Write(prop.Position.X);
							bw.Write(prop.Position.Y);
							bw.Write(prop.Rotation);
							bw.Write(prop.Scale);
							bw.Write(prop.Title);
							bw.Write(prop.State);

							// Shape
							bw.Write(prop.Shapes.Count);
							foreach (var shape in prop.Shapes)
							{
								bw.Write(shape.DirX1);
								bw.Write(shape.DirX2);
								bw.Write(shape.DirY1);
								bw.Write(shape.DirY2);
								bw.Write(shape.LenX);
								bw.Write(shape.LenY);
								bw.Write(shape.Position.X);
								bw.Write(shape.Position.Y);
							}

							// Parameters
							bw.Write(prop.Parameters.Count);
							foreach (var parameter in prop.Parameters)
							{
								bw.Write((int)parameter.Type);
								bw.Write((int)parameter.SignalType);
								bw.Write(parameter.Name);
								bw.Write(parameter.Xml);
							}
						}

						// Events
						bw.Write(area.Events.Count);
						foreach (var evnt in area.Events.OrderBy(a => a.EntityId))
						{
							bw.Write(evnt.EntityId);
							bw.Write(evnt.Name);
							bw.Write(evnt.Position.X);
							bw.Write(evnt.Position.Y);
							bw.Write((int)evnt.Type);

							// Shape
							bw.Write(evnt.Shapes.Count);
							foreach (var shape in evnt.Shapes)
							{
								bw.Write(shape.DirX1);
								bw.Write(shape.DirX2);
								bw.Write(shape.DirY1);
								bw.Write(shape.DirY2);
								bw.Write(shape.LenX);
								bw.Write(shape.LenY);
								bw.Write(shape.Position.X);
								bw.Write(shape.Position.Y);
							}

							// Parameters
							bw.Write(evnt.Parameters.Count);
							foreach (var parameter in evnt.Parameters)
							{
								bw.Write((int)parameter.Type);
								bw.Write((int)parameter.SignalType);
								bw.Write(parameter.Name);
								bw.Write(parameter.Xml);
							}
						}
					}
				}

				// zip it
				using (var min = new MemoryStream(ms.ToArray()))
				using (var mout = new MemoryStream())
				{
					using (var gzip = new GZipStream(mout, CompressionMode.Compress))
						min.CopyTo(gzip);

					File.WriteAllBytes(filePath, mout.ToArray());
				}
			}
		}
	}
}
