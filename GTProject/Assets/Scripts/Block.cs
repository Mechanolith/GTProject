using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Vector3 spawnPoint;
    Quaternion spawnRotation;
    BlockData data;
    Rigidbody rigidbody;
    MeshRenderer renderer;

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
        renderer.material = BlockMaterials.GetMasteryMaterial(data.mastery);
    }

    public void StartTest()
    {
        if(data.mastery == MasteryLevel.None)
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
        return $"<b>{data.grade}:</b> {data.domain}\n" +
            $"{data.cluster}\n\n" +
            $"<b>Mastery:</b> {data.mastery}\n\n" +
            $"<b>{data.standardID}:</b>\n{data.standardDescription}";
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
