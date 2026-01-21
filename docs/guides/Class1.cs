using AstroModIntegrator;
using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;

namespace AMLCustomRoutines
{
    // This routine modifies the Floodlight to require organic instead of tungsten
    // You can name the class whatever you like, as long as it inherits from AstroModIntegrator.CustomRoutine
    public class Class1 : CustomRoutine
    {
        // RoutineID can be any string, it is used for display and identification purposes
        public override string RoutineID => "ExampleCustomRoutine1";
        // Set Enabled to true
        public override bool Enabled => true;
        // Set APIVersion to 1
        public override int APIVersion => 1;

        // This method will be executed by the mod integrator, after all main routines have finished
        public override void Execute(ICustomRoutineAPI api)
        {
            // we fetch the file with the package name "/Game/Items/ItemTypes/FloodLight_IT"
            // you can also pass a raw path here, e.g. "Astro/Content/Items/ItemTypes/FloodLight_IT.uasset"
            UAsset floodlightAsset = api.FindFile("/Game/Items/ItemTypes/FloodLight_IT");

            // we now use UAssetAPI to find ConstructionRecipe.Ingredients[0].ItemType in the Class Default Object export and modify it
            // you can try following along in UAssetGUI; for ItemType assets like this one, the Class Default Object is typically Export 2

            // we fetch the CDO and cast it to a NormalExport
            // a NormalExport is any export with typical tagged property data, like you might see in UAssetGUI (containing properties like ObjectProperty, FloatProperty, etc.)
            NormalExport exp = (NormalExport)(floodlightAsset.GetClassExport().ClassDefaultObject.ToExport(floodlightAsset));
            StructPropertyData constructionRecipe = exp["ConstructionRecipe"] as StructPropertyData; // ConstructionRecipe
            ArrayPropertyData ingredients = constructionRecipe["Ingredients"] as ArrayPropertyData; // ConstructionRecipe.Ingredients
            StructPropertyData ingredient0 = ingredients.Value[0] as StructPropertyData; // ConstructionRecipe.Ingredients[0]
            ObjectPropertyData ingredient0type = ingredient0["ItemType"] as ObjectPropertyData; // ConstructionRecipe.Ingredients[0].ItemType
            // we now set ConstructionRecipe.Ingredients[0].ItemType to a new resource
            // UAsset.AddItemTypeImport adds a new import into the asset, pointing to the item type at "/Game/Items/ItemTypes/Minables/Organic"
            // this will still work even if the resource already is imported by the asset
            ingredient0type.Value = floodlightAsset.AddItemTypeImport("/Game/Items/ItemTypes/Minables/Organic");

            // Save the asset to the final integrator pak
            api.AddFile("/Game/Items/ItemTypes/FloodLight_IT", floodlightAsset);

            // Log some text to the integrator log file (optional, but may help debugging)
            api.LogToDisk("Completed " + RoutineID);
        }
    }
}
