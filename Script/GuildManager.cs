using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildManager : MonoBehaviour
{
    public GameObject detailQuest;
    public DetailQuest questNum;
    public Text questText;
    public Dictionary<int, string> questList;
    private void Awake()
    {
        questList = new Dictionary<int, string>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
        MakeData();
    }

    void MakeData()
    {
        questList.Add(0, "근래 엘폰숲 입구에 슬라임이 많이 난폭해져 마을 주민들의 의뢰가 들어왔습니다. \n" + "<color=red>" + "슬라임 5마리" + "</color>" + "만 퇴치해 주세요\n"+"<color=green>"+"보상 : 코인 100, 경험치 : 10" + "</color>");
        questList.Add(1, "대장장이가 철광석이 필요하다고 합니다. \n" + "<color=red>" + "철광석 5개" + "</color>" + "만 구해주시면\n"+ "<color=green>" + "옵시디언"+"</color>"+ "으로 교환해준다고 합니다.");
    }
    public void OnClickSlimeQuest()
    {
        detailQuest.SetActive(true);
        questNum.questNum = 0;
        questText.text = questList[0];
    }
    public void OnClickIronQuest()
    {
        detailQuest.SetActive(true);
        questNum.questNum = 1;
        questText.text = questList[1];
    }
    public void OnclickClose()
    {
        questText.text = "";
        gameObject.SetActive(false);
        detailQuest.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
