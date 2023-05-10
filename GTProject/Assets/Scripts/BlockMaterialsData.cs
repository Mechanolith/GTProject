using UnityEngine;

[CreateAssetMenu(fileName = "BlockMaterialsData", menuName = "Data Classes/Block Materials")]
public class BlockMaterialsData : ScriptableObject
{
    public Material GlassMaterial;
    public Material WoodMaterial;
    public Material MetalMaterial;
}