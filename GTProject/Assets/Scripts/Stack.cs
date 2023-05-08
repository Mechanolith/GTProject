using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stack : MonoBehaviour
{
    public float StackHeight => Mathf.Ceil(blocks.Count / 3) * blockSizeY;
    public string StackName => stackName;
    public bool IsTesting { get; private set; }

    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private float blockSpacing;
    [SerializeField] List<Block> blocks;

    float blockSizeY = -1;
    float blockSizeZ = -1;
    string stackName = "The Great Stack Of Learning";

    public void Initialise(List<BlockData> _blockData) 
    {
        //Sort the blocks by Domain > Cluster > Standard ID.
        _blockData = _blockData.OrderBy(b => b.domain)
                .ThenBy(b => b.cluster)
                .ThenBy(b => b.standardID)
                .ToList();

        if (_blockData.Any())
        {
            stackName = _blockData[0].grade;
            name = $"{stackName} Stack";
        }

        SpawnBlocks(_blockData);
    }

    void SpawnBlocks(List<BlockData> _blockData)
    {
        //Spawn all the blocks in a stack and initialise them.
        for (int i = 0; i < _blockData.Count; ++i)
        {
            GameObject blockObject = Instantiate(blockPrefab, transform);

            GetBlockSize(blockObject);
            SetBlockPositionAndRotation(blockObject, i);

            Block blockScript = blockObject.GetComponent<Block>();
            blockScript.Initialise(_blockData[i]);

            blocks.Add(blockScript);
        }
    }

    void GetBlockSize(GameObject _blockObject)
    {
        //Grab the size of the collider after the first spawn.
        if (blockSizeY >= 0 && blockSizeZ >= 0)
        {
            return;
        }

        BoxCollider collider = _blockObject.GetComponent<BoxCollider>();
        blockSizeY = collider.bounds.size.y;
        blockSizeZ = collider.bounds.size.z;
    }

    void SetBlockPositionAndRotation(GameObject _blockObject, int _iteration)
    {
        Vector3 blockPosition = Vector3.zero;

        int rowNumber = Mathf.FloorToInt(_iteration / 3);
        float verticalOffset = (rowNumber * blockSizeY) + (blockSizeY * 0.5f);  //Add half the block size so the bottom is flat on 0.

        //(i % 3) - 1 will always resolve to -1, 0, 1 in order, allowing us to easily place blocks as left, center, or right.
        float horizontalOffset = ((_iteration % 3) - 1) * (blockSizeZ + blockSpacing);

        bool rotateRow = rowNumber % 2 == 0;
        float rotation = rotateRow ? 90 : 0;

        blockPosition.x = rotateRow ? horizontalOffset : 0;
        blockPosition.y = verticalOffset;
        blockPosition.z = rotateRow ? 0 : horizontalOffset;

        _blockObject.transform.localPosition = blockPosition;
        _blockObject.transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    public void StartTest()
    {
        foreach (Block block in blocks)
        {
            block.StartTest();
        }

        IsTesting = true;
    }

    public void Reset()
    {
        foreach(Block block in blocks) 
        {
            block.Reset();
        }

        IsTesting = false;
    }
}
