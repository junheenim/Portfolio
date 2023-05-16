using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "New Quest/quest")]
public class Quest:ScriptableObject
{
    // ����Ʈ �̸�
    public string questTitle = "";
    //����Ʈ ID
    public int questID = -1;
    //����Ʈ ����
    public bool accept = false;
    //����Ʈ ������
    public bool isProceeding = false;
    //����Ʈ Ŭ����
    public bool isClear = false;
    // ����
    public string targetName = "";
    public int targetCount = 0;
    public int targetKill = 0;
    //������
    public Item needitem = null;
    public int needCount = 0;
    public int haveCount = 0;
    // ����
    public Item item = null;
    public int exp = 0;
    public int coin = 0;

    //����Ʈ ����
    public void NewQuest()
    {
        accept = false;
        isProceeding = false;
        isClear = false;
        targetKill = 0;
        haveCount = 0;
    }
}
