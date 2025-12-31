using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

// change to location of unpacked game files
string contentDirectory = @"D:\Games\steamapps\common\ASTRONEER\Astro\Content\Paks\pakchunk0-WindowsNoEditor\Astro\Content\";
// change to output file path
string outputPath = @"D:\amoddocs\docs\guides\ITLookupTable.json";

// collect string table
Dictionary<string, string> allStringTableEntries = new Dictionary<string, string>();
List<string> allStringTableAssets = Directory.GetFiles(contentDirectory + @"Globals\StringTables", "*.uasset", SearchOption.AllDirectories).ToList();
allStringTableAssets.AddRange(Directory.GetFiles(contentDirectory + "U36_Expansion", "*.uasset", SearchOption.AllDirectories));
allStringTableAssets.AddRange(Directory.GetFiles(contentDirectory + "U32_Expansion", "*.uasset", SearchOption.AllDirectories));
foreach (string assetPath in allStringTableAssets)
{
    try
    {
        UAsset y = new UAsset(assetPath, EngineVersion.VER_UE4_27);

        if (y.Exports[0] is StringTableExport exp)
        {
            foreach (KeyValuePair<FString, FString> thing in exp.Table)
            {
                if (thing.Key?.Value == null || thing.Value?.Value == null) continue;
                allStringTableEntries[thing.Key.Value] = thing.Value.Value;
            }
        }
    }
    catch
    {
        continue;
    }
}

Console.WriteLine("Parsed all string tables");

int i = 0;
Dictionary<string, string> itemNameToPath = new Dictionary<string, string>();
string[] allPotentialAssets = Directory.GetFiles(contentDirectory, "*.uasset", SearchOption.AllDirectories);
double numPotentialAssets = allPotentialAssets.Length;
foreach (string assetPath in allPotentialAssets)
{
    ++i;
    if (assetPath.Contains("Animations") || assetPath.Contains("Models")) continue;
    try
    {
        if (i % 500 == 0)
        {
            Console.WriteLine((i / numPotentialAssets) * 100 + "%");
            Console.WriteLine(assetPath);
        }
        UAsset y = new UAsset(assetPath, EngineVersion.VER_UE4_27, null, CustomSerializationFlags.SkipPreloadDependencyLoading | CustomSerializationFlags.SkipParsingBytecode | CustomSerializationFlags.SkipParsingExports);
        if (y.Exports.Count > 5) continue; // all item types should have 3 exports
        y = new UAsset(assetPath, EngineVersion.VER_UE4_27, null, CustomSerializationFlags.SkipPreloadDependencyLoading | CustomSerializationFlags.SkipParsingBytecode);
        if (y.GetClassExport()?.SuperStruct?.ToImport(y)?.ObjectName?.ToString() != "ItemType") continue;

        foreach (var exp in y.Exports)
        {
            if (exp is NormalExport nExp && nExp["Name"] is TextPropertyData texProp && texProp != null)
            {
                string formattedAssetPath = assetPath.Replace(contentDirectory, "/Game/").Replace("\\", "/").Replace(".uasset", "");
                switch (texProp.HistoryType)
                {
                    case TextHistoryType.StringTableEntry:
                        if (texProp.Value != null) itemNameToPath[allStringTableEntries[texProp.Value.Value]] = formattedAssetPath;
                        break;
                    case TextHistoryType.Base:
                    case TextHistoryType.None:
                        if (texProp.Value != null) itemNameToPath[allStringTableEntries[texProp.CultureInvariantString.Value]] = formattedAssetPath;
                        break;
                    default:
                        if (texProp.Value != null) itemNameToPath[texProp.Value.Value] = formattedAssetPath;
                        break;
                }
                break;
            }
        }
    }
    catch
    {
        continue;
    }
}

File.WriteAllText(outputPath, JsonConvert.SerializeObject(itemNameToPath, Formatting.Indented));
