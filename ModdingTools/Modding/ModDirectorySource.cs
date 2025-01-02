﻿using ModdingTools.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModdingTools.Modding
{
    public class ModDirectorySource
    {
        public string Root             { get; private set; }
        public string Name             { get; private set; }

        public bool IsReadOnly         { get; private set; }
        public bool Enabled            { get; set; }
        public bool AutoLoad           { get; set; }

        public ModObject FindModByScriptPath(string scriptPath)
        {
            var path = Path.GetFullPath(scriptPath).ToLower();
            var mods = GetMods();
            Logger.Log(CUFramework.Shared.LogLevel.Verbose, path);
            foreach (var mod in mods)
            {
                var x = Path.GetFullPath(mod.GetClassesDir()).ToLower();
                Logger.Log(CUFramework.Shared.LogLevel.Verbose, x);

                // idk I'm skeptical about this method... but for testing it should be enough
                if (path.StartsWith(x))
                    return mod;
            }
            return null;
        }

        public ModObject[] GetMods(string modName = null)
        {
            List<ModObject> mods = new List<ModObject>();
            if (!AutoLoad)
                return mods.ToArray();

            if (!Directory.Exists(Root))
                return mods.ToArray();

            var paths = Directory.GetDirectories(Root);
            foreach (var path in paths)
            {
                if (modName != null)
                {
                    if (!modName.Equals(Path.GetFileName(path), StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }
                }

                var modIniPath = Path.Combine(path, "modinfo.ini");
                if (File.Exists(modIniPath))
                {
                    try
                    {
                        var mod = new ModObject(path, this);
                        mods.Add(mod);
                    }
                    catch (Exception ex)
                    {
                        CUFramework.Dialogs.CUMessageBox.Show($"Mod parser failed while reading mod {path}!\n" + ex.Message + "\n" + ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Debug.WriteLine(":sealnyon: Oopsie woopsie!\n" + ex.Message + "\n" + ex.ToString());
                    }
                }
            }
            return mods.ToArray();
        }

        public ModDirectorySource(string name, string path, bool autoLoad, bool defaultEnabled = true, bool isReadOnly = false)
        {
            this.Name = name;
            this.Root = path;
            this.AutoLoad = autoLoad;

            if (!Directory.Exists(path) && !isReadOnly)
            {
                Directory.CreateDirectory(path);
            }

            this.Enabled = defaultEnabled;
            this.IsReadOnly = isReadOnly;
        }
    }
}
