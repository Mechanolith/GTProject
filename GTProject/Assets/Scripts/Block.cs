using UnityEngine;

public class Block : MonoBehaviour
{
    private Vector3 spawnPoint;
    private Quaternion spawnRotation;
    private BlockData data;
    private Rigidbody rigidbody;
    private MeshRenderer renderer;

    public void Initialise(BlockData _data)
    {
        data = _data;

        spawnPoint = transform.localPosition;
        spawnRotation = transform.localRotation;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        renderer = GetComponent<MeshRenderer>();

        SetMaterialType();
    }

    void SetMaterialType()
    {
        renderer.material = BlockMaterials.GetMasteryMaterial(data.Mastery);
    }

    public void StartTest()
    {
        if(data.Mastery == MasteryLevel.None)
        {
            gameObject.SetActive(false);
            return;
        }

        rigidbody.isKinematic = false;
    }

    public void Reset()
    {
        rigidbody.isKinematic = true;
        gameObject.SetActive(true);
        transform.localPosition = spawnPoint;
        transform.localRotation = spawnRotation;
    }

    public string GetInspectString()
    {
        return $"<b>{data.Grade}:</b> {data.Domain}\n" +
            $"{data.Cluster}\n\n" +
            $"<b>Mastery:</b> {data.Mastery}\n\n" +
            $"<b>{data.StandardID}:</b>\n{data.StandardDescription}";
    }

    public void Select()
    {
        renderer.material.EnableKeyword("_EMISSION");
    }

    public void Deselect()
    {
        renderer.material.DisableKeyword("_EMISSION");
    }
}
