using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SkillToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject toolTip;
    public ToolTip theToolTip;
    public string skillName; 
    [TextArea]
    public string skillTip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.transform.position = Input.mousePosition;
        theToolTip.itemName.text = skillName;
        theToolTip.itemtip.text = skillTip;
        theToolTip.itemUseTip.SetActive(false);
        theToolTip.priceToolTop.SetActive(false);
        toolTip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        theToolTip.itemUseTip.SetActive(true);
        theToolTip.itemUseTip.SetActive(true);
        theToolTip.HideToolTip();
    }
}
