using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private Vector3 tooltipOffset;
    [SerializeField] private float screenMargins;
    [SerializeField] private float maxRayDistance = 1000f;

    private RectTransform tooltipRect;
    private int dirtyPositionFrames;
    private int framesUntilReposition = 2;

    private void Start()
    {
        tooltipRect = GetComponent<RectTransform>();
        HideTooltip();
    }

    private void Update()
    {
        //Because we dynamically position the tooltip relative to the mouse and screen borders
        //We need to wait a frame (or two) for the layout to fully rebuild before we attempt to position it.
        if (dirtyPositionFrames > 0)
        {
            --dirtyPositionFrames;
            if (dirtyPositionFrames == 0)
            {
                PositionTooltip();
            }
        }
    }

    public void TryInspectBlock()
    {
        //Raycast only on the Block layer.
        LayerMask mask = 1 << 6;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, maxRayDistance, mask);

        if(hit.transform != null)
        {
            string blockInfo = hit.transform.GetComponent<Block>().GetInspectString();
            ShowTooltip(blockInfo);
        }
        else 
        {
            HideTooltip();
        }
    }

    void ShowTooltip(string _contents)
    {
        gameObject.SetActive(true);
        contentText.text = _contents;

        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipRect);

        dirtyPositionFrames = framesUntilReposition;
    }

    public void PositionTooltip()
    {   
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, Camera.main, out Vector2 mousePos))
        {
            Vector3 targetPosition = new Vector3(mousePos.x, mousePos.y) + tooltipOffset + new Vector3(tooltipRect.sizeDelta.x * 0.5f, 0f, 0f);

            //Check if we go over the right edge of the screen, then flip the tooltip if we do.
            bool overScreenEdgeHorizontal = !canvasRect.rect.Contains(new Vector3(mousePos.x + tooltipOffset.x + tooltipRect.sizeDelta.x, mousePos.y));

            if (overScreenEdgeHorizontal)
            {
                targetPosition.x = mousePos.x - tooltipOffset.x - (tooltipRect.sizeDelta.x * 0.5f);
            }

            //Offset any overflow at the top/bottom of the screen.
            float tooltipTop = mousePos.y + tooltipOffset.y + (tooltipRect.sizeDelta.y * 0.5f);
            float tooltipBottom = mousePos.y - tooltipOffset.y - (tooltipRect.sizeDelta.y * 0.5f);

            float topOverflow = tooltipTop - (canvasRect.anchoredPosition.y + (canvasRect.sizeDelta.y * 0.5f) - screenMargins);
            float bottomOverflow = (canvasRect.anchoredPosition.y - (canvasRect.sizeDelta.y * 0.5f) + screenMargins) - tooltipBottom;

            topOverflow = topOverflow < 0f ? 0f : topOverflow;
            bottomOverflow = bottomOverflow < 0f ? 0f : bottomOverflow;

            targetPosition.y -= topOverflow;
            targetPosition.y += bottomOverflow;

            //This is a bit of hack.
            //I originally created this bounding code for a 2D game, and it turns out that an angled 3D camera offsets the math a bit.
            //For the purposes of this little project though, this adjustment makes it look and feel nice again, even if it starts to fail at more extreme values.
            targetPosition.y -= Camera.main.transform.position.y;

            tooltipRect.anchoredPosition = targetPosition;
        }
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
