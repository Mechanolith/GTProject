using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackController : MonoBehaviour
{
    Stack CurrentStack => stacks[currentStackIndex];

    [SerializeField] private GameObject stackParent;
    [SerializeField] private float stackOffset;
    [SerializeField] private int maxStacks = 3; //The data does have a 4th "stack" of one block, but the spec says there's only 3.
    List<Stack> stacks = new List<Stack>();
    
    int currentStackIndex;

    CameraController cameraController;
    StackUI stackUI;

    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
        stackUI = FindObjectOfType<StackUI>();
    }

    public void InitialiseStacks(List<BlockData> _blockData)
    {
        Debug.Log($"[StackController] Initialising stacks with {_blockData.Count} blocks.");

        List<string> grades = _blockData.Select(b => b.grade).Distinct().ToList();

        foreach(string grade in grades)
        {
            List<BlockData> blocks = _blockData.Where(b => b.grade == grade).ToList();
            CreateStack(blocks);
        }

        currentStackIndex = 0;
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
        cameraController.OrbitStack(CurrentStack);
        stackUI.SetStackTitle(CurrentStack.StackName);
        stackUI.SetTestButtons(CurrentStack.IsTesting);
    }

    private void Update()
    {
        //[Testing] [Input]
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            FocusPreviousStack();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            FocusNextStack();
        }
    }

    public void FocusNextStack()
    {
        ++currentStackIndex;
        currentStackIndex = currentStackIndex >= stacks.Count ? 0 : currentStackIndex;
        FocusOnCurrentStack();
    }

    public void FocusPreviousStack()
    {
        --currentStackIndex;
        currentStackIndex = currentStackIndex < 0 ? stacks.Count - 1 : currentStackIndex;
        FocusOnCurrentStack();
    }

    public void TestCurrentStack()
    {
        CurrentStack.StartTest();
        stackUI.SetTestButtons(true);
    }

    public void ResetCurrentStack()
    {
        CurrentStack.Reset();
        stackUI.SetTestButtons(false);
    }

}
