﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModdingTools.Modding;
using ModdingTools.Engine;
using ModdingTools.Windows;
using System.Diagnostics;
using CUFramework.Controls;
using CUFramework.Dialogs;
using System.Threading.Tasks;
using System.IO;
using ModdingTools.Settings;
using System.Globalization;
using static ModdingTools.Windows.ModProperties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ModdingTools.GUI
{
    public partial class ModTile : UserControl
    {
        public ModObject Mod { get; private set; }
        private Color OriginalColor;

        public ModTile(ModObject mod)
        {
            InitializeComponent();

            contextMenuStrip1.Renderer = new ToolStripProfessionalRenderer(new CUMenuColorTable());
            contextMenuStrip1.BackColor = ThemeConstants.BackgroundColor;
            contextMenuStrip1.ForeColor = ThemeConstants.ForegroundColor; 

            OriginalColor = this.BackColor;

            this.Mod = mod;
            this.panel1.BackgroundImage = mod.GetIcon();
            this.label1.Text = mod.Name + "\n(" + mod.GetDirectoryName() + ")";

            this.panel2.Visible = mod.IsReleased;

            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
            CheckBox1_CheckedChanged(null, null);

            this.MouseEnter += ModTile_MouseEnter;
            this.MouseLeave += ModTile_MouseLeave;

            this.panel1.MouseEnter += ModTile_MouseEnter;
            this.panel1.MouseLeave += ModTile_MouseLeave;

            this.label1.MouseEnter += ModTile_MouseEnter;
            this.label1.MouseLeave += ModTile_MouseLeave;

            checkBox1.Cursor = Cursors.Arrow;

            this.Click += ModTile_Click;
            this.panel1.Click += ModTile_Click;
            this.label1.Click += ModTile_Click;

            scriptWatcherToolStripMenuItem2.Checked = ScriptWatcherManager.IsWatcherAttached(Mod);

            RevalidateBG();
        }

        private void RevalidateBG(ToolStripItemCollection col = null)
        {
            if (col == null)
                col = contextMenuStrip1.Items;
            foreach (var item in col)
            {
                if (item is ToolStripMenuItem)
                {
                    var it = ((ToolStripMenuItem)item);
                    it.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    it.BackColor = ThemeConstants.BackgroundColor;
                    it.ForeColor = ThemeConstants.ForegroundColor;

                    if (it.HasDropDownItems)
                    {
                        RevalidateBG(it.DropDownItems);
                    }
                }
            }
        }

        private void ModTile_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = checkBox1.Checked ? ThemeConstants.BorderColor : ThemeConstants.TileUnselected;
        }

        private void ModTile_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = ControlPaint.Light(checkBox1.Checked ? ThemeConstants.BorderColor : ThemeConstants.TileUnselected);
        }

        private void ModTile_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                this.Checked = !this.Checked;
            }
            else
            {
                if (!this.Checked)
                {
                    this.Mod.Refresh();
                    var mp = ModProperties.GetPropertiesWindowForMod(this.Mod);
                    if (mp != null) {  
                        mp.Show();
                        mp.WindowState = FormWindowState.Normal;
                        mp.Focus();
                    }
                    else
                    {
                        var props = new ModProperties(this.Mod);
                        props.StartPosition = FormStartPosition.CenterParent;
                        props.ToCenterParent = this.ParentForm;
                        props.Show();
                    }
                }
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = checkBox1.Checked ? ThemeConstants.BorderColor : ThemeConstants.TileUnselected;
            this.Cursor = checkBox1.Checked ? Cursors.Arrow : Cursors.Hand;
        }

        public bool Checked
        {
            get => checkBox1.Checked;
            set => checkBox1.Checked = value;
        }

        private void compileScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mod.CompileScripts(MainWindow.Instance.Runner);
        }

        private void cookModToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Mod.UnCookMod();
            Mod.CookMod(MainWindow.Instance.Runner);
        }

        private void titleScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mod.TestMod(MainWindow.Instance.Runner);
        }

        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.OpenInExplorer(Mod.RootPath);
        }

        private void moveoToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var menu = moveToToolStripMenuItem;
            menu.DropDownItems.Clear();
            foreach (var modSource in MainWindow.Instance.GetModSources())
            {
                if ((modSource.IsReadOnly && modSource.Name != "Mods directory (disabled)") || Mod.RootSource == modSource)
                    continue;

                var item = new ToolStripMenuItem(modSource.Name, null, (obj, args) => {
                    var o = (ToolStripMenuItem)obj;
                    var t = (ModDirectorySource)o.Tag;
                    this.Mod.ChangeModSource(t);

                    MainWindow.Instance.ReloadModList();
                });

                item.Name = modSource.Name;
                item.Text = modSource.Name;
                item.Tag = modSource;

                menu.DropDownItems.Add(item);
            }
            RevalidateBG();
        }

        private void mafiaTownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mod.TestMod(MainWindow.Instance.Runner, "mafia_town");
        }

        private void spaceshipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mod.TestMod(MainWindow.Instance.Runner, "hub_spaceship");
        }

        private void scriptWatcherToolStripMenuItem2_Click(object sender, EventArgs e)
        { 
            if (ScriptWatcherManager.IsWatcherAttached(Mod))
            {
                ScriptWatcherManager.DetachWatcher(Mod);
            }
            else
            {
                ScriptWatcherManager.AttachWatcher(Mod);
            }

            scriptWatcherToolStripMenuItem2.Checked = ScriptWatcherManager.IsWatcherAttached(Mod);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            CleanupTests();

            openInVSC.Visible = OMMSettings.Instance.VSCIntegration;
            cookModToolStripMenuItem.Enabled = !Mod.IsReadOnly;
            testModToolStripMenuItem.Enabled = !Mod.IsReadOnly;
            scriptingToolStripMenuItem.Enabled = !Mod.IsReadOnly;

            var test = new ContentBrowser();
            test.LoadMod(Mod);

            var result = test.HasContentError;
            test.Dispose();

            bool flag5 = Utils.DirContainsKey(Mod.GetCookedDir(), "*.u") || Utils.DirContainsKey(Mod.GetCookedDir(), "*.umap");
            var cooked = (flag5 || Mod.IsLanguagePack) && !result;

            compileScriptsToolStripMenuItem.Enabled = !Mod.IsReadOnly && !result && Mod.HasAnyScripts();
            cookModToolStripMenuItem1.Enabled = !Mod.IsReadOnly && !result && !(!Mod.HasCompiledScripts() && Mod.HasAnyScripts());
            testModToolStripMenuItem.Enabled = cooked && !Mod.IsReadOnly;

            if (Mod.HasAnyMaps() && testModToolStripMenuItem.Enabled)
            {
                testModToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator() {Tag = "toDelete", BackColor = ThemeConstants.BackgroundColor });
                foreach (var map in Mod.GetCookedMaps())
                {
                    var item = new ToolStripMenuItem() { Tag = "toDelete", Text = map };
                    item.Click += (s, a) =>
                    {
                        var y = (ToolStripMenuItem)s;
                        Mod.TestMod(MainWindow.Instance.Runner, y.Text);
                    };
                    testModToolStripMenuItem.DropDownItems.Add(item);
                }
            }
            RevalidateBG();
        }

        private void CleanupTests()
        {
            List<object> pendingRemoval = new List<object>();
            foreach (var i in testModToolStripMenuItem.DropDownItems)
            {
                if (((ToolStripItem)i).Tag == null) continue;
                if (((ToolStripItem)i).Tag.ToString().Equals("toDelete"))
                {
                    pendingRemoval.Add(i);
                }
            }
            foreach (var i in pendingRemoval)
            {
                testModToolStripMenuItem.DropDownItems.Remove((ToolStripItem)i);
            }
        }

        private void deleteModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = CUMessageBox.Show(null, "Do you REALLY want to delete '" + Mod.Name + "'?\nThis CANNOT BE UNDONE!", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning );
            if (result == DialogResult.Yes)
            {
                Mod.Delete();
                MainWindow.Instance.ReloadModList();
            }
        }

        private void hatInTimeEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mod.TestMod(MainWindow.Instance.Runner, "hatintimeentry");
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Process.Start("steam://openurl/http://steamcommunity.com/sharedfiles/filedetails/?id=" + Mod.GetUploadedId());
        }

        private void compileCookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompileAndCookWithAction(false, false, false);
        }

        private void CompileAndCookWithAction(bool launchGame, bool withWorkshopMods, bool lastMap)
        {
            if (OMMSettings.Instance.KillEditorBeforeCooking)
                Utils.KillEditor();
            if (OMMSettings.Instance.KillGameBeforeCooking)
                Utils.KillGame();

            if (OMMSettings.Instance.FastCook)
            {
                Task.Factory.StartNew(() =>
                {
                    var startTime = DateTime.Now;

                    var cookedStatus = Mod.DoesModNeedToBeCooked();
                    var fast = cookedStatus != null && !cookedStatus.Contains("[0x0]") && !cookedStatus.Contains("[0x3]") && !cookedStatus.Contains("[0x4]");
                    var scriptNeedCooking = cookedStatus != null && (cookedStatus.Contains("[0x1]") || cookedStatus.Contains("[0x2]") || cookedStatus.Contains("[0x0]"));

                    MainWindow.Instance.Runner.Log("[Experimental] Fast script cooking is enabled! Please report any issues!", CUFramework.Shared.LogLevel.Warn);
                    MainWindow.Instance.Runner.Log("Current state:", CUFramework.Shared.LogLevel.Info);
                    MainWindow.Instance.Runner.Log(cookedStatus, CUFramework.Shared.LogLevel.Info);
                    if (cookedStatus != null)
                    {
                        if (scriptNeedCooking)
                        {
                            var compileResult = Mod.CompileScripts(MainWindow.Instance.Runner, false);
                            if (compileResult)
                            {
                                var cookResult = Mod.CookMod(MainWindow.Instance.Runner, false, false, fast);
                                if (!cookResult)
                                {
                                    CUMessageBox.Show("Cooking failed! Look at the console output for more info!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    Utils.UpdateFileDates(Mod.GetCookedDir());
                                    RunPostCookTask(launchGame, withWorkshopMods, lastMap);
                                }
                            }
                            else
                            {
                                CUMessageBox.Show("Script compile failed! Look at the console output for more info!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MainWindow.Instance.Runner.Log("Scripts are up-to-date! Cooking...", CUFramework.Shared.LogLevel.Success);
                            var cookResult = Mod.CookMod(MainWindow.Instance.Runner, false, false, fast);
                            if (!cookResult)
                            {
                                CUMessageBox.Show("Cooking failed! Look at the console output for more info!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                Utils.UpdateFileDates(Mod.GetCookedDir());
                                RunPostCookTask(launchGame, withWorkshopMods, lastMap);
                            }
                        }
                    }
                    else
                    {
                        MainWindow.Instance.Runner.Log("Skipping cooking, everything is up-to-date!", CUFramework.Shared.LogLevel.Success);
                        RunPostCookTask(launchGame, withWorkshopMods, lastMap);
                    }
                    var endTime = DateTime.Now;
                    var taskTime = Math.Round((endTime - startTime).TotalMilliseconds / 1000, 2).ToString(CultureInfo.InvariantCulture);
                    MainWindow.Instance.Runner.Log($"Task finished in {taskTime}s", CUFramework.Shared.LogLevel.Verbose);
                });
            }
            else
            {
                Mod.UnCookMod();
                var startTime = DateTime.Now;

                var cookedStatus = Mod.DoesModNeedToBeCooked();
                var scriptNeedCooking = cookedStatus != null && (cookedStatus.Contains("[0x1]") || cookedStatus.Contains("[0x2]") || cookedStatus.Contains("[0x0]"));

                var compileResult = false;

                if (scriptNeedCooking)
                {
                    compileResult = Mod.CompileScripts(MainWindow.Instance.Runner, false, false, false);
                }
                else
                {
                    MainWindow.Instance.Runner.Log("Scripts are up-to-date! Cooking...", CUFramework.Shared.LogLevel.Success);
                    compileResult = true;
                }

                if (compileResult)
                {
                    var cookResult = Mod.CookMod(MainWindow.Instance.Runner, false, false);
                    if (!cookResult)
                    {
                        CUMessageBox.Show("Cooking failed! Look at the console output for more info!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Utils.UpdateFileDates(Mod.GetCookedDir());
                        RunPostCookTask(launchGame, withWorkshopMods, lastMap);
                    }
                }
                else
                {
                    CUMessageBox.Show("Script compile failed! Look at the console output for more info!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                var endTime = DateTime.Now;
                var taskTime = Math.Round((endTime - startTime).TotalMilliseconds / 1000, 2).ToString(CultureInfo.InvariantCulture);
                MainWindow.Instance.Runner.Log($"Task finished in {taskTime}s", CUFramework.Shared.LogLevel.Verbose);
            }
        }

        private void RunPostCookTask(bool launchGame, bool withWorkshopMods, bool useLastMap)
        {

            if (!launchGame)
                return;

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => {
                    RunPostCookTask(launchGame, withWorkshopMods, useLastMap);
                }));
                return;
            }

            string mapName;
            if (useLastMap)
            {
                var lastMap = File.Exists(Path.Combine(Mod.RootPath, ".lastMap")) ? File.ReadAllText(Path.Combine(Mod.RootPath, ".lastMap")) : null;
                if (!Mod.GetCookedMaps().Contains(lastMap, StringComparer.InvariantCultureIgnoreCase))
                {
                    var mapChooser = new MapChooser(Mod);
                    var result = mapChooser.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        mapName = mapChooser.GetSelectedMap();
                    }
                    else return;
                }
                else mapName = lastMap;
            }
            else
            {
                var mapChooser = new MapChooser(Mod);
                var result = mapChooser.ShowDialog(this.ParentForm);
                if (result == DialogResult.OK)
                {
                    mapName = mapChooser.GetSelectedMap();
                }
                else return;
            }
            
            if (withWorkshopMods)
                Mod.TestModAllMods(MainWindow.Instance.Runner, mapName == "??menu" ? null : mapName);
            else
                Mod.TestMod(MainWindow.Instance.Runner, mapName == "??menu" ? null : mapName);
        }

        private void openInVSC_Click(object sender, EventArgs e)
        {
            var vsCodePath = Utils.FindVSCodeExecutable();
            if (vsCodePath != null)
            {
                Mod.UpdateVSCodeRunTasks();
                Process.Start(vsCodePath, "\"" + Path.Combine(Path.GetFullPath(Mod.RootPath), "vsc-modworkspace.code-workspace") + "\"");
            }
            else
            {
                CUMessageBox.Show("Failed to find the VS:Code installation directory! Please specify the path to the code.exe manually!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OpenFileDialog ov = new OpenFileDialog();
                ov.CheckFileExists = true;
                ov.FileName = "code.exe";
                ov.Filter = "code.exe|code.exe";

                if (ov.ShowDialog() == DialogResult.OK)
                {
                    OMMSettings.Instance.VSCCustomPath = ov.FileName;
                    OMMSettings.Instance.Save();

                    vsCodePath = Utils.FindVSCodeExecutable();
                    if (vsCodePath != null)
                    {
                        Process.Start(vsCodePath, "\"" + Path.Combine(Path.GetFullPath(Mod.RootPath), "vsc-modworkspace.code-workspace") + "\"");
                    }
                }
            }
        }

        private void compileLaunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                if (OMMSettings.Instance.KillEditorBeforeCooking)
                    Utils.KillEditor();
                if (OMMSettings.Instance.KillGameBeforeCooking)
                    Utils.KillGame();

                Mod.UnCookMod();

                if (Mod.CompileScripts(MainWindow.Instance.Runner, false, false))
                {
                    this.Invoke(new MethodInvoker(() => {
                        MainWindow.Instance.Runner.RunWithoutWait(Program.ProcFactory.LaunchEditor(Mod.GetDirectoryName()));
                    }));
                }
            });
        }

        private void launchGameSteamModsLastMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //(bool launchGame, bool withWorkshopMods, bool lastMap)
            CompileAndCookWithAction(true, false, true);
        }

        private void launchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //(bool launchGame, bool withWorkshopMods, bool lastMap)
            CompileAndCookWithAction(true, true, true);
        }

        private void launchGameNoWorkshopModsSpecifyMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //(bool launchGame, bool withWorkshopMods, bool lastMap)
            CompileAndCookWithAction(true, false, false);
        }

        private void launchGameWorkshopModsSpecifyMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //(bool launchGame, bool withWorkshopMods, bool lastMap)
            CompileAndCookWithAction(true, true, false);
        }
    }
}
