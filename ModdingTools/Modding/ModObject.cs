﻿using IniParser.Model;
using ModdingTools.GUI;
using ModdingTools.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using UELib;
using UELib.Core;
using UELib.Engine;
using static ModdingTools.Engine.ModClass;
using ModdingTools.Windows.Tools;
using System.Web;
using ModdingTools.Settings;
using System.Net;
// JLINT-ADD: No idea why these were added.
using ModdingTools.Windows;
using Steamworks;
using CUFramework.Dialogs;
// JLINT-END

namespace ModdingTools.Modding
{
    public class ModObject
    {
        public string Name              { get; set; }
        // changed from String to Array for compatibility with CDLC
        public string[] Author          { get; set; } 
        public string Description       { get; set; }
        public string Version           { get; set; }
        public bool   IsCheat           { get; set; }
        public string Icon              { get; set; }
        public string ChapterInfoName   { get; set; }
        public string Map               { get; set; }

        public int CuratedSteamId       { get; set; }
        public int PlayableSteamId      { get; set; }

        public bool IsOnlineParty       { get; set; }
        public bool HasSkin             { get; set; }
        public bool HasHat              { get; set; } // JLINT-CHANGE: Fixed support for hats(ambiguity between hats and hat flairs?)
        public bool HasWeapon           { get; set; }
        public bool HasPlayableCharacter { get; set; }
        public bool HasDeathWish        { get; set; } // JLINT-ADD: Support for Death Wish tag
        public bool AutoGiveItems       { get; set; }
        
        public bool IsLanguagePack      { get; set; }
        public string MapType           { get; set; }
        public string GameMod           { get; set; }
        public string Coop              { get; set; }
        public string ModClass          { get; set; }


        // CDLC-related tags
        public string[] SpecialThanks { get; set; }
        public string Logo { get; set; }
        public string SplashArt { get; set; }
        public string Background { get; set; }
        public string Titlecard { get; set; }
        public string IntroductionMap { get; set; }

        public List<ModConfigItem> Config  { get; set; }

        public Dictionary<string, string> AssetReplacements;

        // JLINT-ADD: Used for the always loaded workaround
        public ModClass[] AlwaysLoadedClasses { get; set; }
        public ModObject AlwaysLoadedReference { get; set; }

        public string[] AllowedMapTypes = new[] {
            "TimeRift", 
            "SingleTimePiece", 
            "MultiTimePiece"
        };

        public static readonly Dictionary<string, string> IniTagToSteamMapping = new Dictionary<string, string>
        {
            { "TimeRift",                "Time Rift"                 },
            { "SingleTimePiece",         "Single TimePiece Level"    },
            { "MultiTimePiece",          "Multi TimePiece Level"     }
        };

        IniParser.Parser.IniDataParser Parser;

        public string RootPath { get; private set; }
        public ModDirectorySource RootSource { get; private set; }

        public void UpdateVSCodeRunTasks()
        {
            if (!OMMSettings.Instance.VSCIntegration) return;

            var vscodepath = Path.Combine(RootPath, ".vscode");
            if (!Directory.Exists(vscodepath))
                Directory.CreateDirectory(Path.Combine(RootPath, ".vscode"));

            // OMM path may change... so better just update workspace file every time
            File.WriteAllText(Path.Combine(RootPath, "vsc-modworkspace.code-workspace"), Properties.Resources.VSCodeWorkspaceTemplate.Replace("##AHIT:SRC_ROOT##", HttpUtility.JavaScriptStringEncode(Engine.GameFinder.GetSrcDir())));
            File.WriteAllText(Path.Combine(vscodepath, "tasks.json"), Properties.Resources.VSCodeTaskTemplate.Replace("##OMM:OMM_EXE_PATH##", HttpUtility.JavaScriptStringEncode(Program.GetCLIPath())));
        }

        public string[] GetIniTags()
        {
            List<string> tmp = new List<string>();
            if (IsOnlineParty)
                tmp.Add("Online Party");
            if (HasSkin)
                tmp.Add("Dye");
            if (HasHat)
                tmp.Add("Hat");
            if (AutoGiveItems)
                tmp.Add("Available Instantly");
            if (HasPlayableCharacter)
                tmp.Add("Playable Character");
            if (IsLanguagePack)
                tmp.Add("Language Pack");
            if (HasWeapon)
                tmp.Add("Weapon");
            if (AllowedMapTypes.Contains(MapType))
                tmp.Add(IniTagToSteamMapping[MapType]);

            return tmp.ToArray();
        }

        public string GetDescription()
        {
            return this.Description.Replace("[br]", Environment.NewLine);
        }

        public void SetDescription(string desc)
        {
            this.Description = desc.Replace(Environment.NewLine, "[br]");
        }

        public string GetIniPath()
        {
            return Path.Combine(RootPath, "modinfo.ini");
        }

        public string GetDirectoryName()
        {
            return Path.GetFileName(RootPath);
        }

        public void RenameDirectory(string newName)
        {
            Utils.CleanupAttrib(RootPath);
            Utils.MoveDirDos(RootPath, Path.Combine(RootSource.Root, newName));
            this.RootPath = Path.Combine(RootSource.Root, newName);
        }

        public void SetImageResource(string key, string val)
        {
            switch (key)
            {
                case "background":
                    this.Background = val;
                    break;
                case "splash":
                    this.SplashArt = val;
                    break;
                case "titlecard":
                    this.Titlecard = val;
                    break;
                case "logo":
                    this.Logo = val;
                    break;
                case "icon":
                    this.Icon = val;
                    break;
                default:
                    throw new Exception("Not implemented!");
            }
        }

        public string GetImageResource(string key)
        {
            switch (key)
            {
                case "background":
                    return this.Background;
                case "splash":
                    return this.SplashArt;
                case "titlecard":
                    return this.Titlecard;
                case "logo":
                    return this.Logo;
                case "icon":
                    return this.Icon;
                default:
                    throw new Exception("Not implemented!");
            }
        }

        // returns null if not
        public string DoesModNeedToBeCooked()
        {
            var lines = new StringBuilder();
 
            if (!Utils.DirectoryHasFiles(GetCookedDir(), new[] { "*.umap", "*.u" }))
            {
                return "- entire mod needs to be recooked! [0x0]";
            }

            var cookedPCData = Utils.OldestInDir(GetCookedDir(), new[] { "*.umap", "*.upk", "*.u" });
            if (Utils.DirectoryHasFiles(GetClassesDir(), new[] { "*.uc" }))
            {
                if (!Utils.DirectoryHasFiles(GetCookedDir(), new[] { "*.u" }))
                    lines.AppendLine("- scripts needs to be recooked [0x1]");

                if (Utils.YoungestInDir(GetClassesDir(), new[] { "*.uc" }) > cookedPCData)
                    lines.AppendLine("- scripts needs to be recooked [0x1]");
            }

            if (Utils.DirectoryHasFiles(GetCompiledScriptsDir(), new[] { "*.u" }))
            {
                if (!Utils.DirectoryHasFiles(GetCookedDir(), new[] { "*.u" }))
                    lines.AppendLine("- compiled scripts needs to be recooked [0x1]");

                if (Utils.YoungestInDir(GetCompiledScriptsDir(), new[] { "*.u" }) > cookedPCData)
                    lines.AppendLine("- compiled scripts needs to be recooked [0x2]");
            }

            if (Utils.DirectoryHasFiles(GetMapsDir(), new[] { "*.umap" }))
            {
                if (!Utils.DirectoryHasFiles(GetCookedDir(), new[] { "*.umap" }))
                    lines.AppendLine("- maps needs to be recooked [0x3]");

                if (Utils.YoungestInDir(GetMapsDir(), new[] { "*.umap" }) > cookedPCData)
                    lines.AppendLine("- maps needs to be recooked [0x3]");
            }

            if (Utils.DirectoryHasFiles(GetContentDir(), new[] { "*.upk" }))
            {
                if (Utils.YoungestInDir(GetContentDir(), new[] { "*.upk" }) > cookedPCData)
                    lines.AppendLine("- content needs to be recooked [0x4]");
            }

            // Jessie, we need to cook!
            return lines.Length > 0 ? lines.ToString() : null;
        }

        public void ChangeModSource(ModDirectorySource source)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            GC.Collect();

            var newRoot = Path.Combine(source.Root, Path.GetFileName(RootPath));
            Utils.CleanupAttrib(RootPath);
            Utils.MoveDir(RootPath, newRoot);
            this.RootPath = newRoot;
            this.RootSource = source;
        }

        public void UnCookMod()
        {
            var path = GetCookedDir();
            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path, "*.u"))
                {
                    File.Delete(file);
                }
                foreach (var file in Directory.GetFiles(path, "*.upk"))
                {
                    File.Delete(file);
                }
                foreach (var file in Directory.GetFiles(path, "*.umap"))
                {
                    File.Delete(file);
                }
            }

            if (OMMSettings.Instance.RmShaderOnCook)
            {
                var shaders = GetShaderCacheDir();
                if (Directory.Exists(shaders))
                {
                    foreach (var file in Directory.GetFiles(shaders, "*.upk"))
                    {
                        if (new FileInfo(file).Length > 10485760)
                            File.Delete(file);
                    }
                }
            }   
        }

        public static ModClass.ModClassType[] CombineTags(ModClass[] source)
        {
            List<ModClass.ModClassType> mc = new List<ModClass.ModClassType>();
            foreach (var s in source)
            {
                if (!mc.Contains(s.ClassType))
                {
                    mc.Add(s.ClassType);
                }
            }
            return mc.ToArray();
        }

        public string GetClassesDir()
        {
            return Path.Combine(RootPath, "Classes");
        }

        public string GetMapsDir()
        {
            return Path.Combine(RootPath, "Maps");
        }

        public string GetContentDir()
        {
            return Path.Combine(RootPath, "Content");
        }

        public string GetCookedDir()
        {
            return Path.Combine(RootPath, "CookedPC");
        }

        public string GetCompiledScriptsDir()
        {
            return Path.Combine(RootPath, "CompiledScripts");
        }

        public string GetShaderCacheDir()
        {
            return Path.Combine(RootPath, "Shadercache");
        }

        public string GetLocsDir()
        {
            return Path.Combine(RootPath, "Localization");
        }

        ModClass[] ModClassCache = null;
        public ModClass[] GetModClasses(bool skipCache = false)
        {
            List<ModClass> mlc = new List<ModClass>();
            if (ModClassCache != null && !skipCache)
            {
                return ModClassCache;
            }

            var path = GetClassesDir();

            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path, "*.uc", SearchOption.AllDirectories))
                {
                    try
                    {
                        mlc.Add(new ModClass(file));
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Failed to read class file: " + file + "\nException message: " + e.Message + "\nStack trace:\n" + e.StackTrace);
                    }
                }
            }
            ModClassCache = mlc.ToArray();
            return ModClassCache;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ModObject)) return false;
            var c = (ModObject)obj;
            return (c.GetDirectoryName().ToLower() == this.GetDirectoryName().ToLower());
        }

        public override int GetHashCode()
        {
            return this.GetDirectoryName().ToLower().GetHashCode();
        }

        public bool CookMod(AbstractProcessRunner runner, bool async = true, bool cleanConsole = true, bool fast = false)
        {
            if (OMMSettings.Instance.AlwaysloadedWorkaround && OMMSettings.Instance.FastCook)
            {
                // JLINT-ADD: Throwing an exception causes a crash, so show an error message instead(no one will see this though)
                CUMessageBox.Show("Idk how do you enabled FastCooking and AlwaysLoadedWorkaround at the same time, but please, choose only one lol", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (OMMSettings.Instance.AlwaysloadedWorkaround)
            {
                runner.Log("[Experimental] Alwaysloaded workaround is enabled! Please report any issues!", CUFramework.Shared.LogLevel.Warn);

                // JLINT-ADD: Ask the user about which classes they want AlwaysLoaded.
                if (AlwaysLoadedReference == null || !GetDirectoryName().Contains(AlwaysLoadedReference.GetDirectoryName()))
                {
                    AlwaysLoadedClasses = ArrayCheckboxWindow.Ask(Name.ToUpper() + " - ALWAYSLOADED CLASSES", "Which classes should be AlwaysLoaded?\n(NOTE: Any classes that are directly or INDIRECTLY referenced by an AlwaysLoaded class will become AlwaysLoaded too!)", GetModClasses(), "ClassName", this.GetDirectoryName()).Cast<ModClass>().ToArray();

                    // JLINT-ADD: This creates a new mod that references our AlwaysLoaded classes, then compiles it
                    // We need this if we want some of our other classes to be non-AlwaysLoaded.
                    // More information in this guide from UnDrew: https://steamcommunity.com/sharedfiles/filedetails/?id=2888211200
                    if (AlwaysLoadedClasses.Length > 0 && AlwaysLoadedClasses.Length < GetModClasses().Length)
                    {
                        runner.Log("Creating and compiling AlwaysLoaded reference mod...", CUFramework.Shared.LogLevel.Verbose);
                        var NewModName = this.GetDirectoryName() + "_AlwaysLoaded";
                        CreateNewMod(NewModName);
                        AlwaysLoadedReference = RootSource.GetMods(NewModName)[0];
                        AlwaysLoadedReference.AlwaysLoadedReference = this;

                        var ClassContents = new StringBuilder();
                        ClassContents.AppendLine("class " + this.GetDirectoryName() + "_ALRef extends Object;");
                        ClassContents.AppendLine("var Array< class<Object> > RefClasses;");
                        ClassContents.AppendLine("defaultproperties");
                        ClassContents.AppendLine("{");
                        foreach (var item in AlwaysLoadedClasses)
                        {
                            ClassContents.AppendLine("  RefClasses.Add( class'" + this.GetDirectoryName() + "." + item.ClassName + "' )");
                        }
                        ClassContents.AppendLine("}");
                        File.WriteAllText(Path.Combine(AlwaysLoadedReference.GetClassesDir(), this.GetDirectoryName() + "_ALRef.uc"), ClassContents.ToString());

                        AlwaysLoadedReference.CompileScripts(runner, false, false, false);
                    }
                }
                else
                {
                    AlwaysLoadedClasses = GetModClasses();
                }
                // JLINT-END

                if (!Directory.Exists(this.GetMapsDir()))
                    Directory.CreateDirectory(this.GetMapsDir());
                var csc = Path.Combine(this.GetCompiledScriptsDir(), this.GetDirectoryName() + ".u");

                if (File.Exists(csc))
                {
                    // JLINT-ADD: Support for non-AlwaysLoaded mod classes
                    var target = "";
                    if (AlwaysLoadedClasses.Length >= GetModClasses().Length)
                        target = Path.Combine(this.GetMapsDir(), this.GetDirectoryName() + ".umap");
                    else
                        target = Path.Combine(this.GetCompiledScriptsDir(), this.GetDirectoryName() + ".upk");
                    // JLINT-END

                    if (File.Exists(target))
                        File.Delete(target);
                    File.Move(csc, target);
                }
            }

            if (async)
            {
                runner.RunAppAsync(Program.ProcFactory.GetCookMod(this, fast), cleanConsole, OnCookingFinish);
                return true; // async task always return true
            }
            else
            {
                return runner.RunApp(Program.ProcFactory.GetCookMod(this, fast, OnCookingFinish), cleanConsole);
            }
        }

        private void OnCookingFinish()
        {
            if (OMMSettings.Instance.AlwaysloadedWorkaround)
            {
                Debug.WriteLine("OnCookingFinish called");

                // JLINT-ADD: Uhh... There's alot to unpack here, but basically, organization of class files and stuff, more cooking, destroying.
                var scr = "";
                var target = "";
                if (AlwaysLoadedReference == null)
                {
                    if (AlwaysLoadedClasses.Length >= GetModClasses().Length)
                        scr = Path.Combine(this.GetMapsDir(), this.GetDirectoryName() + ".umap");
                    else
                        scr = Path.Combine(this.GetCompiledScriptsDir(), this.GetDirectoryName() + ".upk");

                    if (File.Exists(scr))
                    {
                        target = Path.Combine(this.GetCompiledScriptsDir(), this.GetDirectoryName() + ".u");
                        if (File.Exists(target))
                            File.Delete(target);
                        File.Move(scr, target);
                    }
                }

                var cooked = Path.Combine(this.GetCookedDir(), this.GetDirectoryName() + ".umap");
                target = Path.Combine(this.GetCookedDir(), this.GetDirectoryName() + ".u");

                if (File.Exists(target))
                    File.Delete(target);

                if (File.Exists(cooked))
                    File.Move(cooked, target);

                if (AlwaysLoadedReference != null)
                {
                    if (GetDirectoryName().Contains(AlwaysLoadedReference.GetDirectoryName()))
                    {
                        cooked = Path.Combine(this.GetCookedDir(), this.GetDirectoryName() + ".u");
                        target = Path.Combine(AlwaysLoadedReference.GetCookedDir(), AlwaysLoadedReference.GetDirectoryName() + ".u");
                        if (File.Exists(target))
                            File.Delete(target);

                        if (File.Exists(cooked))
                            File.Move(cooked, target);

                        scr = Path.Combine(AlwaysLoadedReference.GetCompiledScriptsDir(), AlwaysLoadedReference.GetDirectoryName() + ".upk");
                        if (File.Exists(scr))
                        {
                            target = Path.Combine(AlwaysLoadedReference.GetCompiledScriptsDir(), AlwaysLoadedReference.GetDirectoryName() + ".u");
                            if (File.Exists(target))
                                File.Delete(target);
                            File.Move(scr, target);
                        }

                        // We no longer need our AlwaysLoaded reference mod, destroy it!
                        AlwaysLoadedReference.AlwaysLoadedReference = null;
                        AlwaysLoadedReference = null;
                        this.Delete();
                    }
                    else
                    {
                        var runner = ModProperties.GetPropertiesWindowForMod(this).processRunner1.Runner;
                        runner.Log("Cooking AlwaysLoaded reference mod...", CUFramework.Shared.LogLevel.Verbose);
                        AlwaysLoadedReference.CookMod(runner, false, false, false);
                    }
                }
                // JLINT-END
            }
        }

        public void TestMod(AbstractProcessRunner runner, string mapName = null)
        {
            runner.RunWithoutWait(Program.ProcFactory.StartMap(mapName));
        }

        public void TestModAllMods(AbstractProcessRunner runner, string mapName = null)
        {
            runner.RunWithoutWait(Program.ProcFactory.StartMapWithAllMods(mapName));
        }

        public bool CompileScripts(AbstractProcessRunner runner, bool async = true, bool watcher = false, bool cleanConsole = false)
        {
            if (async)
            {
                runner.RunAppAsync(Program.ProcFactory.GetCompileScript(this, watcher), cleanConsole);
                return true; // async task always return true
            }
            else
            {
                return runner.RunApp(Program.ProcFactory.GetCompileScript(this, watcher), cleanConsole);
            }
        }

        public ModObject(string rootPath, ModDirectorySource parent)
        {
            this.RootPath = rootPath;
            this.RootSource = parent;
            Parser = new IniParser.Parser.IniDataParser();
            Parser.Configuration.AllowDuplicateKeys = true;
            Refresh();
        }

        public bool IsReleased => GetUploadedId() > 0;

        public void Refresh()
        {
            Debug.WriteLine("Refreshing mod...");
            var ini = Utils.ReadStringFromFile(GetIniPath());
            IniData info = Parser.Parse(ini);

            AssetReplacements = new Dictionary<string, string>();
            Config = new List<ModConfigItem>();

            var i = info["Info"];
            // Parse "Info" section
            this.Name = TryGet(i, "name", "New mod");
            this.Author = TryGet(i, "author", "Me").Split(';');
            this.Description = TryGet(i, "description", "Hello this is my all new mod!");
            this.Version = TryGet(i, "version", "1.0.0");
            this.Map = TryGet(i, "Map");

            this.IsCheat = bool.Parse(TryGet(i, "is_cheat", "false"));
            this.Icon = TryGet(i, "icon");
            this.ChapterInfoName = TryGet(i, "ChapterInfoName", "");
            this.ModClass = TryGet(i, "modclass", "");

            // Parse "Tags" section
            var t = info["Tags"];
            this.MapType              = TryGet(t, "MapType"); // TimeRift, SingleTimePiece, MultiTimePiece
            this.IsOnlineParty        = TryGet(t, "OnlineParty", "0").Equals("1");
            this.HasSkin              = TryGet(t, "HasSkin", "0").Equals("1");
            this.HasHat               = TryGet(t, "HasHat", "0").Equals("1"); // JLINT-ADD: Extra tags, important for getting sorted into the right mod list category
            this.HasWeapon            = TryGet(t, "HasWeapon", "0").Equals("1");
            this.HasPlayableCharacter = TryGet(t, "HasPlayableCharacter", "0").Equals("1");
            this.HasDeathWish         = TryGet(t, "HasDeathWish", "0").Equals("1"); // JLINT-ADD: Extra tag
            this.AutoGiveItems        = TryGet(t, "AutoGiveItems", "0").Equals("1");
            this.IsLanguagePack       = TryGet(t, "IsLanguagePack", "0").Equals("1");
            this.Coop                 = TryGet(t, "Coop", "");

            this.SpecialThanks = TryGet(i, "specialthanks", "").Split(';');

            this.Logo       = TryGet(i, "Logo");
            this.SplashArt  = TryGet(i, "SplashArt");
            this.Background = TryGet(i, "Background");
            this.Titlecard  = TryGet(i, "Titlecard");

            this.IntroductionMap = TryGet(i, "IntroductionMap");

            if (!this.IsReadOnly)
            {
                GetModClasses(true);
                UpdateVSCodeRunTasks();
            }    

            // ok, let's the fun begin... I hate this
            bool parse = false;
            int lastMode = -1;
            string lastArVal = null;

            // parse ARs
            foreach (var ln in ini.Split('\n'))
            {
                if (!parse && ln.Contains("[AssetReplace]"))
                {
                    parse = true;
                    continue;
                }
                else if (parse && ln.Trim().StartsWith("[") && ln.Trim().EndsWith("]"))
                {
                    parse = false;
                    break;
                }

                if (parse)
                {
                    var lns = ln.Trim(); // remove extra characters
                    if (string.IsNullOrEmpty(lns)) continue; // skip empty lines
                    if (lns[0] == '#' || lns[0] == ';') continue; // skip comments

                    var kv = lns.Split('=');
                    if (kv.Count() == 2)
                    {
                        var key = kv[0].Trim();
                        var val = kv[1].Trim();

                        int mode = (key == "+Asset") ? 0 : (key == "NewAsset") ? 1 : 2;
                        // 0 - Asset, 1 - NewAsset, 2 - Invalid, -1 - None

                        if (mode == 1 && lastMode == 0)
                        {
                            if (lastArVal == null)
                            {
                                throw new Exception("Invalid AssetReplacement section [code: 2]! Did you edit it by hand?");
                            }
                            AssetReplacements.Add(lastArVal, val);
                        }
                        else if (mode == 0)
                        {
                            lastArVal = val;
                        }
                        else if (mode == 2)
                        {
                            throw new Exception("Invalid AssetReplacement section [code: 1]! Did you edit it by hand?");
                        }
                        lastMode = mode;
                    }
                    else
                    {
                        throw new Exception("AssetReplacement parse error!");
                    }
                }
            }

            parse = false;
            lastMode = -1;
            bool configStart = false;
            ModConfigItem tmp = null;
            // parse Config
            foreach (var ln in ini.Split('\n'))
            {
                if (!parse && ln.Contains("[Configs]"))
                {
                    parse = true;
                    configStart = false;
                    continue;
                }
                else if (parse && ln.Trim().StartsWith("[") && ln.Trim().EndsWith("]"))
                {
                    parse = false;
                    configStart = false;
                    if (tmp != null)
                    {
                        Config.Add(tmp);
                    }
                    tmp = null;
                    break;
                }

                if (parse)
                {
                    var lns = ln.Trim(); // remove extra characters
                    if (string.IsNullOrEmpty(lns)) continue; // skip empty lines
                    if (lns[0] == '#' || lns[0] == ';') continue; // skip comments

                    var kv = lns.Split('=');
                    if (kv.Count() == 2)
                    {
                        var key = kv[0].Trim().ToLower();
                        var val = IniUnQuote(kv[1].Trim()).TrimStart('"').TrimEnd('"');

                        Debug.WriteLine($" KEY: {key}, VALUE: {val}");

                        int mode = (key == "+config") ? 0 : 1;
                        // 0 - Asset, 1 - NewAsset, 2 - Invalid, -1 - None

                        if (mode == 0)
                        {
                            configStart = true;
                            if (tmp != null)
                            {
                                Config.Add(tmp);
                            }
                            tmp = new ModConfigItem();
                            tmp.PropertyName = val;
                        }
                        if (configStart)
                        {
                            if (tmp == null)
                            {
                                throw new Exception("Invalid Config section [code: 2]! Did you edit it by hand?");
                            }
                            if (key.StartsWith("option[") && key.EndsWith("]"))
                            {
                                tmp.Options.Add(int.Parse(key.Split('[')[1].Split(']')[0]), val);
                            }
                            else
                            {
                                switch (key)
                                {
                                    case "name":
                                        tmp.Name = val;
                                        break;
                                    case "description":
                                        tmp.Description = val;
                                        break;
                                    /*case "default":
                                        tmp.DefaultIndex = int.Parse(val);
                                        break;*/
                                }
                            }
                        }
                        lastMode = mode;
                    }
                    else
                    {
                        throw new Exception("Config parse error!");
                    }
                }
            }
            if (tmp != null)
            {
                Config.Add(tmp);
            }
        }

        public bool HasClass(ModClassType @type)
        {
            return GetModClasses().Where(x => x.ClassType == @type).Count() > 0;
        }

        public bool HasAnyMaps()
        {
            if (!Directory.Exists(GetMapsDir())) return false;
            return Directory.GetFiles(GetMapsDir(), "*.umap").Length > 0;
        }

        public bool HasAnyCookedMaps()
        {
            if (!Directory.Exists(GetCookedDir())) return false;
            return Directory.GetFiles(GetCookedDir(), "*.umap").Length > 0;
        }

        public string GetLastMap()
        {
            var flagFile = Path.Combine(RootPath, ".lastMap");
            if (File.Exists(flagFile))
            {
                var data = File.ReadAllText(flagFile);
                if (!string.IsNullOrEmpty(data)) 
                    return data;
            }
            return null;
        }

        public string[] GetAllMaps()
        {
            var a = GetUncookedMaps();
            var b = GetCookedMaps();
            var x = new List<string>();
            if (a != null)
            {
                foreach (var _a in a)
                    x.Add(_a);
            }
            if (b != null)
            {
                foreach (var _b in b)
                    if (!x.Contains(_b))
                        x.Add(_b);
            }

            x.Sort();
            return x.ToArray();
        }

        public string[] GetCookedMaps()
        {
            if (!Directory.Exists(GetCookedDir())) return null;
            List<string> maps = new List<string>();
            foreach (var x in Directory.GetFiles(GetCookedDir(), "*.umap"))
            {
                maps.Add(Path.GetFileNameWithoutExtension(x));
            }
            return maps.ToArray();
        }

        public string[] GetUncookedMaps()
        {
            if (!Directory.Exists(GetMapsDir())) return null;
            List<string> maps = new List<string>();
            foreach (var x in Directory.GetFiles(GetMapsDir(), "*.umap", SearchOption.TopDirectoryOnly))
            {
                maps.Add(Path.GetFileNameWithoutExtension(x));
            }
            return maps.ToArray();
        }

        public string TryDetectCIInContent()
        {
            if (!Directory.Exists(GetContentDir())) return null;
            foreach (var c in Directory.GetFiles(GetContentDir(), "*.upk"))
            {
                var d = TryDetectCI(Path.GetFileNameWithoutExtension(c));
                if (d != null) return d;
            }
            return null;
        }

        public bool HasCompiledScripts()
        {
            return Utils.DirContainsKey(GetCompiledScriptsDir(), "*.u");
        }

        public bool HasAnyScripts()
        {
            return Utils.DirContainsKey(GetClassesDir(), "*.uc");
        }

        public string TryDetectCI(string packageName)
        {
            try
            {
                using (var pkg = UnrealLoader.LoadFullPackage(Path.Combine(GetContentDir(), packageName + ".upk"), FileAccess.Read))
                {
                    foreach (var obj in pkg.Objects)
                    {
                        if (obj.Class != null && obj.Class.Name == "Hat_ChapterInfo")
                        {
                            if (obj.Outer == null)
                            {
                                return $"{obj.Package.PackageName}.{obj.Name}";
                            }
                            else
                            {
                                return $"{obj.Package.PackageName}.{obj.Outer.Name}.{obj.Name}";
                            }
                        }
                    }
                }            
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + Environment.NewLine + e.ToString());
            }
            return null;
        }

        public string MakeARClass()
        {
            var classDir = GetClassesDir();
            var className = $"{GetDirectoryName()}_AutoGenGameModClass";
            var classContent = $"class {className} extends GameMod;";

            var cp = Path.Combine(classDir, $"{className}.uc");
            
            if (File.Exists(cp)) return className; // don't edit existing class
            if (!Directory.Exists(classDir))
            {
                Directory.CreateDirectory(classDir);
            }

            File.WriteAllText(cp, classContent);
            return className;
        }

        public bool IsReadOnly => RootSource.IsReadOnly;

        public bool ValidateIcon()
        {
            if (string.IsNullOrEmpty(Icon)) return false;
            return ValidateIcon(Path.Combine(RootPath, Icon));
        }

        public static bool ValidateIcon(string path)
        {
            FileInfo f = new FileInfo(path);
            if (f.Exists)
            {
                if (f.Length > 1048576)
                {
                    return false;
                }
                else
                {
                    var img = Image.FromFile(f.FullName);
                    if (img.Width / img.Height == 1)
                    {
                        if (img.Width < 100)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public string GetIconLocation()
        {
            return Icon == null ? null : Path.Combine(RootPath, Icon);
        }

        public string GetBackgroundLocation()
        {
            return Background == null ? null : Path.Combine(RootPath, Background);
        }

        public string GetLogoLocation()
        {
            return Logo == null ? null : Path.Combine(RootPath, Logo);
        }

        public string GetSplashArtLocation()
        {
            return SplashArt == null ? null : Path.Combine(RootPath, SplashArt);
        }

        public string GetTitleCardLocation()
        {
            return Titlecard == null ? null : Path.Combine(RootPath, Titlecard);
        }

        public Image GetIcon()
        {
            var noIconImage = Properties.Resources.noimage;
            if (Icon == null)
                return noIconImage;

            var path = GetIconLocation();

            try
            {
                if (!ValidateIcon())
                    return noIconImage;
                Image img;
                using (var bmpTemp = new Bitmap(path))
                {
                    img = FlipbookGenerator.ResizeImage(new Bitmap(bmpTemp), 200, 200);
                }
                return img;
            }
            catch (Exception)
            {
                return noIconImage;
            }
        }

        // JLINT-ADD: Moved from MainWindow to ModObject to have a common function to create a new mod.
        // I should probably give this a return value like a bool to verify a sucessful action, but oh well.
        public static void CreateNewMod(string modName)
        {
            string modsRoot = Path.Combine(Program.ProcFactory.GetGamePath(), @"HatinTimeGame\Mods");
            string modPath = Path.Combine(modsRoot, modName);
            string modInfoPath = Path.Combine(modPath, "modinfo.ini");

            Directory.CreateDirectory(modPath);
            Directory.CreateDirectory(Path.Combine(modPath, "Classes"));
            Directory.CreateDirectory(Path.Combine(modPath, "Content"));
            Directory.CreateDirectory(Path.Combine(modPath, "Maps"));
            Directory.CreateDirectory(Path.Combine(modPath, "Localization"));
            Directory.CreateDirectory(Path.Combine(modPath, "Localization", "INT"));

            using (StreamWriter sW = File.CreateText(modInfoPath))
            {
                sW.WriteLine("[Info]");
                sW.WriteLine("name=" + modName);
                sW.WriteLine("author=\"" + SteamFriends.GetPersonaName().Replace("\"", "\\\"") + "\"");
                sW.WriteLine("description=\"Hello this is my all new mod!\"");
                sW.WriteLine("version=\"1.0.0\"");
                sW.WriteLine("is_cheat=false");
                sW.WriteLine("icon=icon.jpg");
            }
        }

        public void Delete()
        {
            Utils.CleanupAttrib(RootPath);
            Directory.Delete(RootPath, true);   
        }

        private string TryGet(KeyDataCollection context, string key, string def = null)
        {
            if (context.ContainsKey(key))
                return CleanString(context[key]);
            return CleanString(def);
        }

        private string CleanString(string data)
        {
            if (data == null) return null;
            if (data.StartsWith("\"") && data.EndsWith("\""))
                data = data.Substring(1, data.Length - 2);
            return data.Replace("\\\"", "\"");
        }

        private void AppendIni(KeyDataCollection context, string key, string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            var data = new KeyData(key);
            data.Value = value;
            context.AddKey(data);
        }

        public long GetUploadedId()
        {
            return Program.SWS.GetIdForMod(this);
        }

        public void SetUploadedId(long id)
        {
            Program.SWS.SetIdForMod(this, id);
        }


        public bool DetectIsLanguagePack()
        {
            if (!Directory.Exists(GetLocsDir())) return false;
            if (HasAnyMaps() || HasAnyScripts()) return false;

            return true;
        }

        public bool TagsCompleted()
        {
            if (HasAnyMaps() && string.IsNullOrEmpty(MapType)) return false;
            return true;
        }

        // here we PECKIN GO!
        public void Save()
        {
            IniData info = new IniData();
            info.Configuration.AssigmentSpacer = "";

            var i = info["Info"];
   
            // Store "Info" section
            AppendIni(i, "name", IniQuote(this.Name));

            if (this.Author != null && this.Author.Length > 0)
                AppendIni(i, "author", IniQuote(string.Join(";", this.Author)));

            AppendIni(i, "description", IniQuote(this.Description));
            AppendIni(i, "version", IniQuote(this.Version));

            AppendIni(i, "is_cheat", this.IsCheat ? "true" : "false");
            AppendIni(i, "icon", this.Icon);
            AppendIni(i, "ChapterInfoName", this.ChapterInfoName);

            AppendIni(i, "Map", this.Map);

            ApplyTag(info, "isLanguagePack", DetectIsLanguagePack() ? "1" : "");

            if (HasAnyMaps())
            {
                ApplyTag(info, "MapType", this.MapType);
            }

            ApplyTag(info, "OnlineParty", this.IsOnlineParty ? "1" : "");
            ApplyTag(info, "Coop", this.Coop);


            if (this.SpecialThanks != null && this.SpecialThanks.Length > 0)
                AppendIni(i, "specialthanks", string.Join(";", this.SpecialThanks));

            AppendIni(i, "SplashArt", this.SplashArt);
            AppendIni(i, "Background", this.Background);
            AppendIni(i, "Titlecard", this.Titlecard);
            AppendIni(i, "Logo", this.Logo);

            AppendIni(i, "IntroductionMap", this.IntroductionMap);

            bool autoEquip = false;
            // JLINT-ADD: See the Recursive GameMod search comment below.
            int gameModState = 0; // 0=No GameMod, 1=Has a GameMod, 2=Found final GameMod
            string gameModClass = "";
            
            foreach (var c in GetModClasses(true))
            {
                if (!c.IsIniAccessible)
                {
                    if (c.IsGameModClass)
                    {
                        gameModClass = c.ClassName;
                        gameModState = 1;
                    }
                    if (c.IsAutoAwardItem)
                    {
                        autoEquip = true;
                    }
                    continue;
                }
                ApplyTag(info, c.IniKey, "1");
            }

            // JLINT-ADD: Recursive GameMod search, allowing for a GameMod to extend from another one.
            // Might seem useless, but this allows for compartmentalization of functions(e.g. one
            // GameMod for cosmetics, another for level functions)
            // I don't think modders actually do this lol.
            if (gameModState > 0)
            {
                while (gameModState < 2)
                {
                    foreach (var c in GetModClasses())
                    {
                        if (gameModClass.Equals(c.ExtendsClass, StringComparison.InvariantCultureIgnoreCase))
                        {
                            // New GameMod that extends from the current one, so update it
                            gameModClass = c.ClassName; 
                            break;
                        }

                        gameModState = 2; // No other classes that extend from the current GameMod
                    }
                }
            }
            else if (AssetReplacements.Count() > 0)
            {
                gameModClass = MakeARClass();
            }
            AppendIni(i, "modclass", gameModClass);

            if (autoEquip)
            {
                ApplyTag(info, "AutoGiveItems", "1");
            }

            // asset replacement storage
            var builder = new StringBuilder();
            if (AssetReplacements.Count > 0)
            {
                ApplyTag(info, "AssetReplace", "1");

                builder.AppendLine("[AssetReplace]");

                foreach (var a in AssetReplacements)
                {
                    builder.AppendLine($"+Asset={a.Key}");
                    builder.AppendLine($"NewAsset={a.Value}");
                }
            }

            // config storage
            if (Config.Count > 0)
            {
                builder.AppendLine("[Configs]");

                foreach (var a in Config)
                {
                    builder.AppendLine(a.ToString());
                }
            }

            var iniContent = $"{info}{Environment.NewLine}{builder}";

            Parser.Parse(iniContent); // just for a check, it should throw the exception if something weird happens
            File.WriteAllText(Path.Combine(RootPath, "modinfo.ini"), iniContent, Encoding.Unicode);
        }

        private void ApplyTag(IniData ini, string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (!string.IsNullOrEmpty(value))
            {
                ini["Tags"].AddKey(key, value);
            }
        }

        public static string IniQuote(string data)
        {
            return $"\"{data.Replace("\"", "\\\"")}\"";
        }

        public static string IniUnQuote(string data)
        {
            return $"\"{data.Replace("\\\"", "\"")}\"";
        }


        public class ModConfigItem
        {
            public string PropertyName;
            public string Name;
            public string Description;
            /* public int DefaultIndex; */
            public SortedDictionary<int, string> Options;

            public ModConfigItem()
            {
                PropertyName = "";
                Name = "";
                Description = "";
                /* DefaultIndex = 0; */
                Options = new SortedDictionary<int, string>();
            }

            public override string ToString()
            {
                var build = new StringBuilder();
                build.AppendLine($"+Config={PropertyName}");
                build.AppendLine($"Name={IniQuote(Name)}");
                build.AppendLine($"Description={IniQuote(Description)}");
                /* build.AppendLine($"Default={DefaultIndex}"); */
                foreach (var opt in Options)
                {
                    build.AppendLine($"Option[{opt.Key}]={IniQuote(opt.Value)}");
                }
                return build.ToString();
            }
        }
    }
}
