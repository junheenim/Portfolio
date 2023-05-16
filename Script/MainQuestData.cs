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
        qeustdata.Add(1, "�ٸ� �ǳʱ�");
        qeustdata.Add(2, "��� ã�ư���");
        qeustdata.Add(3, "���� �Ծ��");
        qeustdata.Add(4, "���� ������ ��ġ");
        qeustdata.Add(5, "������ ����");
        qeustdata.Add(6, "���� ��ȭ�ϱ�");
        qeustdata.Add(7, "������ ��ġ");
        qeustdata.Add(8, "���� ���� ���");
        qeustdata.Add(9, "�Ƿ� Ű���");
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
