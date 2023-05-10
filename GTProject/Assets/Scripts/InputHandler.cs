using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    CameraController cameraController;
    StackController stackController;
    Tooltip tooltip;

    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
        stackController = FindObjectOfType<StackController>();
        tooltip = FindObjectOfType<Tooltip>();
    }

    private void Update()
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
