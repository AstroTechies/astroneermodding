using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

// change to location of unpacked game files, with trailing backslash
string contentDirectory = @"D:\Games\steamapps\common\ASTRONEER\Astro\Content\Paks\pakchunk0-WindowsNoEditor\Astro\Content\";
// change to output file paths
string outputPath1 = @"D:\amoddocs\docs\guides\LookupTableIT.json";
string outputPath2 = @"D:\amoddocs\docs\guides\LookupTableBP.json";
// banned directories
List<string> bannedDirectories = new List<string>() { "Animations", "Models", "Textures", "Intangibles", "Developers", "OrbitalPlatformMiniPlanet" };
// banned files
HashSet<string> bannedFiles = new HashSet<string>() { "/Game/Items/ItemTypes/Components/MediumPrinter" };
// banned names
HashSet<string> bannedNames = new HashSet<string>() { "Packaged Item", "Research Sample", "RESEARCH SAMPLE", "Unknown", "Detritus", "Aeoluz", "Asteroid", "MODULE" };

string FormatPath(string rawPath)
{
    return rawPath.Replace(contentDirectory, "/Game/").Replace("\\", "/").Replace(".uasset", "");
}

Console.WriteLine("Begin");

// collect string tables
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

Console.WriteLine("Parsed all string tables\n");

int i = 0;
bool needStatusUpdate = false;
Dictionary<string, dynamic> itemNameToPath1 = new Dictionary<string, dynamic>();
Dictionary<string, dynamic> itemNameToPath2 = new Dictionary<string, dynamic>();

string[] allPotentialAssets = Directory.GetFiles(contentDirectory, "*.uasset", SearchOption.AllDirectories);
double numPotentialAssets = allPotentialAssets.Length;
foreach (string assetPath in allPotentialAssets)
{
    ++i;
    if (i % 500 == 0) needStatusUpdate = true;

    string formattedAssetPath = FormatPath(assetPath);
    if (bannedFiles.Contains(formattedAssetPath)) continue;

    bool isBanned = false;
    foreach (string bannedDirectory in bannedDirectories)
    {
        if (formattedAssetPath.Contains("/" + bannedDirectory + "/")) isBanned = true;
    }
    if (isBanned) continue;

    try
    {
        UAsset y = new UAsset(assetPath, EngineVersion.VER_UE4_27, null, CustomSerializationFlags.SkipPreloadDependencyLoading | CustomSerializationFlags.SkipParsingBytecode | CustomSerializationFlags.SkipParsingExports);
        if (y.Exports.Count > 10) continue; // all item types should have 3 exports
        y = new UAsset(assetPath, EngineVersion.VER_UE4_27, null, CustomSerializationFlags.SkipPreloadDependencyLoading | CustomSerializationFlags.SkipParsingBytecode);

        string? objectName = y.GetClassExport()?.SuperStruct?.ToImport(y)?.ObjectName?.ToString();
        if (objectName != "ItemType" && objectName != "BoxedResource_C" && objectName != "CanisterResource_C") continue;

        string? finalName = null;
        string? pickupActorPath = null;
        foreach (var exp in y.Exports)
        {
            if (exp is NormalExport nExp && nExp["Name"] is TextPropertyData texProp && texProp != null && texProp.Value != null)
            {
                switch (texProp.HistoryType)
                {
                    case TextHistoryType.StringTableEntry:
                        finalName = allStringTableEntries[texProp.Value.Value];
                        break;
                    case TextHistoryType.Base:
                    case TextHistoryType.None:
                        finalName = texProp.CultureInvariantString.ToString();
                        break;
                    default:
                        finalName = texProp.Value.ToString();
                        break;
                }
            }
            if (exp is NormalExport nExp2 && nExp2["PickupActor"] is ObjectPropertyData objProp && objProp != null && objProp.Value != null && objProp.IsImport())
            {
                pickupActorPath = objProp.ToImport(y)?.OuterIndex?.ToImport(y)?.ObjectName?.ToString();
            }
        }

        if (finalName != null && !bannedNames.Contains(finalName))
        {
            if (needStatusUpdate)
            {
                needStatusUpdate = false;
                double progress = i / numPotentialAssets * 100;
                Console.WriteLine($"{progress:F2}%");
                Console.WriteLine(formattedAssetPath);
                Console.WriteLine();
            }

            if (itemNameToPath1.ContainsKey(finalName))
            {
                dynamic curVal = itemNameToPath1[finalName];
                if (curVal == null || (curVal is string && curVal == formattedAssetPath)) itemNameToPath1[finalName] = formattedAssetPath;
                else if (curVal is List<string>) itemNameToPath1[finalName].Add(formattedAssetPath);
                else if (curVal is string) itemNameToPath1[finalName] = new List<string>() { curVal, formattedAssetPath };
            }
            else
            {
                itemNameToPath1[finalName] = formattedAssetPath;
            }

            if (pickupActorPath != null)
            {
                if (itemNameToPath2.ContainsKey(finalName))
                {
                    dynamic curVal = itemNameToPath2[finalName];
                    if (curVal == null || (curVal is string && curVal == pickupActorPath)) itemNameToPath2[finalName] = pickupActorPath;
                    else if (curVal is List<string>) itemNameToPath2[finalName].Add(pickupActorPath);
                    else if (curVal is string) itemNameToPath2[finalName] = new List<string>() { curVal, pickupActorPath };
                }
                else
                {
                    itemNameToPath2[finalName] = pickupActorPath;
                }
            }
        }
    }
    catch
    {
        continue;
    }
}

File.WriteAllText(outputPath1, NormalizeJsonString(JsonConvert.SerializeObject(itemNameToPath1)));
Console.WriteLine("Wrote output to " + outputPath1);
File.WriteAllText(outputPath2, NormalizeJsonString(JsonConvert.SerializeObject(itemNameToPath2)));
Console.WriteLine("Wrote output to " + outputPath2);
Console.WriteLine("All done");

// BEGIN CC BY-SA 3.0 CODE //

// CC BY-SA 3.0 (https://creativecommons.org/licenses/by-sa/3.0/deed.en)
// This code is copyrighted by StackOverflow user "g45rg34d" https://stackoverflow.com/users/111438/g45rg34d
// Minor changes were made to this source code from the original. No warranties are given. See the original license text for more information.
// https://stackoverflow.com/a/28557035
string NormalizeJsonString(string json)
{
    // Parse json string into JObject.
    var parsedObject = JObject.Parse(json);

    // Sort properties of JObject.
    var normalizedObject = SortPropertiesAlphabetically(parsedObject);

    // Serialize JObject .
    return JsonConvert.SerializeObject(normalizedObject, Formatting.Indented);
}

JObject SortPropertiesAlphabetically(JObject original)
{
    var result = new JObject();

    foreach (var property in original.Properties().ToList().OrderBy(p => p.Name))
    {
        var value = property.Value as JObject;

        if (value != null)
        {
            value = SortPropertiesAlphabetically(value);
            result.Add(property.Name, value);
        }
        else
        {
            result.Add(property.Name, property.Value);
        }
    }

    return result;
}

// END CC BY-SA 3.0 CODE //
