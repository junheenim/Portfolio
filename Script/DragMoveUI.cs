using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMoveUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private Transform moveTargetUI;
    [SerializeField]
    private Image workingUI;

    private Vector2 startPoint;
    private Vector2 moveBegin;
    private Vector2 moveOffset;

    private void Awake()
    {
        if(moveTargetUI==null)
        {
            moveTargetUI = transform.parent;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData PEData)
    {
        workingUI.rectTransform.SetAsLastSibling();
        startPoint = moveTargetUI.position;
        moveBegin = PEData.position;
    }
    void IDragHandler.OnDrag(PointerEventData PEData)
    {
        moveOffset = PEData.position - moveBegin;
        moveTargetUI.position = startPoint + moveOffset;
    }
}
