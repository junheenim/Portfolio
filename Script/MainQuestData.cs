using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainQuestData : MonoBehaviour
{
    public Dictionary<int, string> qeustdata;
    public int curStory = -1;
    public Text mainTip;
    public GameObject mainQuestTip;
    private void Awake()
    {
        qeustdata = new Dictionary<int, string>();
    }
    private void Start()
    {
        qeustdata.Add(1, "다리 건너기");
        qeustdata.Add(2, "루루 찾아가기");
        qeustdata.Add(3, "포션 먹어보기");
        qeustdata.Add(4, "약한 슬라임 퇴치");
        qeustdata.Add(5, "마을로 가기");
        qeustdata.Add(6, "루루와 대화하기");
        qeustdata.Add(7, "슬라임 퇴치");
        qeustdata.Add(8, "숲의 주인 토벌");
        qeustdata.Add(9, "실력 키우기");
    }
    public void QuestUpdate()
    {
        curStory++;
        if (curStory == 7)
        {
            mainQuestTip.SetActive(true);
        }
        if (curStory == 9)
        {
            mainQuestTip.SetActive(false);
        }
        mainTip.text = qeustdata[curStory];
    }
}
