using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "New Quest/quest")]
public class Quest:ScriptableObject
{
    // 퀘스트 이름
    public string questTitle = "";
    //퀘스트 ID
    public int questID = -1;
    //퀘스트 수락
    public bool accept = false;
    //퀘스트 진행중
    public bool isProceeding = false;
    //퀘스트 클리어
    public bool isClear = false;
    // 몬스터
    public string targetName = "";
    public int targetCount = 0;
    public int targetKill = 0;
    //아이템
    public Item needitem = null;
    public int needCount = 0;
    public int haveCount = 0;
    // 보상
    public Item item = null;
    public int exp = 0;
    public int coin = 0;

    //퀘스트 리셋
    public void NewQuest()
    {
        accept = false;
        isProceeding = false;
        isClear = false;
        targetKill = 0;
        haveCount = 0;
    }
}
