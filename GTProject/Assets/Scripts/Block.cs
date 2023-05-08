using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //[Blocks] [Code Quality]
    //Todo: Move this to a more central place, rather than storing it on every block.
    [SerializeField] private Material glassMaterial;
    [SerializeField] private Material woodMaterial;
    [SerializeField] private Material metalMaterial;

    Vector3 spawnPoint;
    Quaternion spawnRotation;
    BlockData data;
    Rigidbody rigidbody;

    public void Initialise(BlockData _data)
    {
        data = _data;

        spawnPoint = transform.localPosition;
        spawnRotation = transform.localRotation;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        SetMaterialType();
    }

    void SetMaterialType()
    {
        Material selectedMaterial;

        switch (data.mastery)
        {
            case MasteryLevel.Learned:
                selectedMaterial = woodMaterial;
                break;

            case MasteryLevel.Mastered:
                selectedMaterial = metalMaterial;
                break;

            case MasteryLevel.None:
            default:
                selectedMaterial = glassMaterial;
                break;
        }

        GetComponent<MeshRenderer>().material = selectedMaterial;
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
            $"<b>{data.standardID}:</b>\n{data.standardDescription}";
    }
}
