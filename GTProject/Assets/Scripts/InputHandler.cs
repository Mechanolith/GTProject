using UnityEngine;
using UnityEngine.EventSystems;

//[Input] [Code Quality]
//Todo: This should probably be rewritten as an event based system that the various controllers subscribe to,
//instead of calling the functions directly in response to inputs. But for this scale of project it gets the job done.
public class InputHandler : MonoBehaviour
{
    private CameraController cameraController;
    private StackController stackController;
    private Tooltip tooltip;

    void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
        stackController = FindObjectOfType<StackController>();
        tooltip = FindObjectOfType<Tooltip>();
    }

    void Update()
    {
        //Always hide the tooltip if we click away.
        if (Input.GetMouseButtonDown(0))
        {
            tooltip.HideTooltip();
        }

        //Check that we're not over an interactable UI element
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                cameraController.EnableRotation();
            }   

            if (Input.GetMouseButtonDown(1))
            {
                tooltip.TryInspectBlock();
            }
        }

        //If we start rotatating then stop with the mouse over a UI element we should still stop rotating.
        if (Input.GetMouseButtonUp(0))
        {
            cameraController.DisableRotation();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stackController.ToggleCurrentStackTest();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            stackController.FocusPreviousStack();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            stackController.FocusNextStack();
        }
    }
}
