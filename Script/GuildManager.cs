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
        questList.Add(0, "�ٷ� ������ �Ա��� �������� ���� �������� ���� �ֹε��� �Ƿڰ� ���Խ��ϴ�. \n" + "<color=red>" + "������ 5����" + "</color>" + "�� ��ġ�� �ּ���\n"+"<color=green>"+"���� : ���� 100, ����ġ : 10" + "</color>");
        questList.Add(1, "�������̰� ö������ �ʿ��ϴٰ� �մϴ�. \n" + "<color=red>" + "ö���� 5��" + "</color>" + "�� �����ֽø�\n"+ "<color=green>" + "�ɽõ��"+"</color>"+ "���� ��ȯ���شٰ� �մϴ�.");
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
