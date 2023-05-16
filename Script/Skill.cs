using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill : MonoBehaviour
{
    public CharacterUI UI;

    public GameObject skillSloat1;
    public Text rushText;
    public int rushLevel = 0;

    public GameObject skillSloat2;
    public Text fireText;
    public int fireLevel = 0;

    public GameObject skillSloat3upBut;
    public GameObject skillSloat3;
    public Text slashText;
    public int slashLevel = 0;

    public GameObject tooltip;
    public Text skillPoint;
    public Text toolTip;
    private void Update()
    {
        skillPoint.text = PlayerManager.instance.nowPlayer.skillPoint.ToString();
    }
    private void Start()
    {
        skillSloat3.SetActive(false);

        rushText.text = rushLevel.ToString();
        fireText.text = fireLevel.ToString();
        slashText.text = slashLevel.ToString();

        skillSloat1.GetComponent<Slot>().item.itemInfo = "빠른속도로 앞으로 돌진한다.\r\n공격력 : " + ((rushLevel-1)  * 10 + 100).ToString() +
                "%\nMP : -2\n재사용 : 5초";
        skillSloat2.GetComponent<Slot>().item.itemInfo = "적에게 불꽃구체를 날린다.\r\n공격력 : " + ((fireLevel - 1) * 20 + 200).ToString() +
                "%\nMP : -3\n재사용 : 8초";
        skillSloat3.GetComponent<Slot>().item.itemInfo = "표적을 관통하며 베어낸다.\r\n공격력 : " + ((slashLevel - 1) * 10 + 50).ToString() +
                "% X 5\nMP : -4\n재사용 : 10초";
        skillSloat3upBut.SetActive(false);
        SkillSlotActive();
        gameObject.SetActive(false);
    }
    public void OnClickClose()
    {
        gameObject.SetActive(false);
        UI.MouseControl();
    }
    public void OnClickRushUP()
    {
        if (PlayerManager.instance.nowPlayer.skillPoint >= 1)
        {
            rushLevel++;
            PlayerManager.instance.nowPlayer.skillPoint--;
            rushText.text = rushLevel.ToString();
            skillPoint.text = PlayerManager.instance.nowPlayer.skillPoint.ToString();
            skillSloat1.GetComponent<Slot>().item.itemInfo = "빠른속도로 앞으로 돌진한다.\r\n공격력 : " + ((rushLevel-1) * 10 + 100).ToString() +
                "%\nMP : -2\n재사용 : 8초";
            toolTip.text = skillSloat1.GetComponent<Slot>().item.itemInfo;
            if (rushLevel >= 1)
            {
                skillSloat3upBut.SetActive(true);
                slashText.text = slashLevel.ToString();
                skillSloat3.SetActive(true);
            }
            SkillSlotActive();
        }
    }
    public void OnClickFireUP()
    {
        if (PlayerManager.instance.nowPlayer.skillPoint >= 1)
        {
            fireLevel++;
            PlayerManager.instance.nowPlayer.skillPoint--;
            fireText.text = fireLevel.ToString();
            skillPoint.text = PlayerManager.instance.nowPlayer.skillPoint.ToString();
            skillSloat2.GetComponent<Slot>().item.itemInfo = "적에게 불꽃구체를 날린다.\r\n공격력 : " + ((fireLevel-1) * 10 + 200).ToString() +
                "%\nMP : -3\n재사용 : 8초";
            toolTip.text = skillSloat2.GetComponent<Slot>().item.itemInfo;
            SkillSlotActive();
        }

    }
    public void OnClickAttckUP()
    {
        if (PlayerManager.instance.nowPlayer.skillPoint >= 1)
        {
            slashLevel++;
            PlayerManager.instance.nowPlayer.skillPoint--;
            slashText.text = slashLevel.ToString();
            skillPoint.text = PlayerManager.instance.nowPlayer.skillPoint.ToString();
            skillSloat3.GetComponent<Slot>().item.itemInfo = "표적을 관통하며 베어낸다.\r\n공격력 : " + ((slashLevel - 1) * 10 + 50).ToString() +
                "% X 5\nMP : -4\n재사용 : 10초";
            toolTip.text = skillSloat3.GetComponent<Slot>().item.itemInfo;
            SkillSlotActive();
        }
    }

    void SkillSlotActive()
    {
        if (rushLevel <= 0)
        {
            skillSloat1.SetActive(false);
        }
        else
        {
            skillSloat1.SetActive(true);
        }

        if (fireLevel <= 0)
        {
            skillSloat2.SetActive(false);
        }
        else
        {
            skillSloat2.SetActive(true);
        }

        if (slashLevel <= 0)
        {
            skillSloat3.SetActive(false);
        }
        else
        {
            skillSloat3.SetActive(true);
        }
    }
}
