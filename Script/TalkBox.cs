using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkBox: MonoBehaviour
{
    public Dictionary<int, string[]> talkData;
    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    void GenerateData()
    {
        //��� ��ȭ
        talkData.Add(0, new string[] { "��? �� �ű⼭ ����? ��Ż�� �ⱸ�� �� �ڿ� �ִ� �м��� ���ε�...?", "�ƹ�ư ���� ������Ȳ�� ������...", 
            "��� ���������� ���͵��� ���� ����������", "���� �� �Ա��� �������� 3���� ���� ��ġ����","���� ���͵��� �������� ������ ã�� ������" });
        talkData.Add(1, new string[] { "������! ���� ������� �����ϰ� �Ѽ� ����!" });
        talkData.Add(2, new string[] { "����! �������� ����ġ�� ���� �� �ٲ�","�ʰ� �������� ��ġ�ϴ� �߿� �˾ƺôµ�","���� �� �������� ���� ������ ������ �θ��� �ִ����",
            "������ �Ѽ� ���� ���� ������ ��ġ����!","����ġ ���� ����ϲ��� �غ� �ܴ��� �ϰ� �����!"});
        talkData.Add(3, new string[] { "�غ� �ܴ��� ��!" });
        talkData.Add(4, new string[] { "��! ���� ������ ����ġ�ٴ�...","���� ����! �����",
            "�ٵ� ���� ������ ���µ��� ������ ���Ͱ� ������...","�� �� �����غ��״� �ʴ� �� �Ƿ��� Ű�쵵����!","�׷� ���߿���!"});
        talkData.Add(5, new string[] { "���߿� ��!" });

        //������ ��ȭ
        talkData.Add(100, new string[] { "����... �� �����̿��� ���� �����ΰ�?" });

        //���� ��ȭ
        talkData.Add(200, new string[] { "���! ���� �ȳ��ϼ���?" });

        //���� ��ȭ
        talkData.Add(300, new string[] { "���� �Ǻΰ� ��ĥ��..." });

        //ī�� ��ȭ
        talkData.Add(400, new string[] { "�ǵ帮�� ������! �������̿���!" });

        //���� ��ȭ
        talkData.Add(500, new string[] { "��... ��� �ؾ�����?" });

        //���� ������
        talkData.Add(600, new string[] { "�̰��� ���� �� �Դϴ�!" });
    }

    public string GetTalk(int count, int talkIndex)
    {
        if (talkIndex == talkData[count].Length)
        {
            return null;
        }
        else
            return talkData[count][talkIndex];
    }
}
