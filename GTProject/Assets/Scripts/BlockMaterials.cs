using UnityEngine;

public static class BlockMaterials
{
    private static BlockMaterialsData materialData;

    public static void Initialise()
    {
        materialData = Resources.Load("BlockMaterialsData") as BlockMaterialsData;
    }

    public static Material GetMasteryMaterial(MasteryLevel _masteryLevel)
    {
        switch (_masteryLevel)
        {
            case MasteryLevel.Learned:
                return materialData.WoodMaterial;

            case MasteryLevel.Mastered:
                return materialData.MetalMaterial;

            case MasteryLevel.None:
            default:
                return materialData.GlassMaterial;
        }
    }
}
