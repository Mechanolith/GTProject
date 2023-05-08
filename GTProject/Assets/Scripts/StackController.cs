using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] private GameObject stackParent;
    [SerializeField] private float stackOffset;
    [SerializeField] private int maxStacks = 3; //The data does have a 4th "stack" of one block, but the spec says there's only 3.
    List<Stack> stacks = new List<Stack>();

    int currentStack;

    public void InitialiseStacks(List<BlockData> _blockData)
    {
        Debug.Log($"[StackController] Initialising stacks with {_blockData.Count} blocks.");

        List<string> grades = _blockData.Select(b => b.grade).Distinct().ToList();

        foreach(string grade in grades)
        {
            List<BlockData> blocks = _blockData.Where(b => b.grade == grade).ToList();
            CreateStack(blocks);
        }

        currentStack = 0;
        FocusOnCurrentStack();
    }

    void CreateStack(List<BlockData> _blockData)
    {
        if(stacks.Count >= maxStacks) 
        {
            return;
        }

        float offset = stackOffset * stacks.Count;
        Vector3 spawnPoint = Vector3.right * offset;

        Stack stack = Instantiate(stackParent, spawnPoint, Quaternion.identity).GetComponent<Stack>();
        stack.Initialise(_blockData);

        stacks.Add(stack);
    }

    void FocusOnCurrentStack()
    {

    }

    private void Update()
    {
        //[Testing]
        //Todo: Remove this once there's proper controls.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach(var stack in stacks)
            {
                stack.StartTest();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var stack in stacks)
            {
                stack.Reset();
            }
        }
    }
}
