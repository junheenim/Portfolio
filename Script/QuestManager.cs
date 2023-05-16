using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestManager : MonoBehaviour
{
    public DetailQuest detailQuest;
    public Slot[] slots;
    public GameObject inventory;
    public Weapon weapon;
    public Text[] text;
    public Quest[] mainQuest;
    public int mainStoryCount = 0;
    public bool mainQuestClear = false;
    private void Awake()
    {
        slots = inventory.GetComponentsInChildren<Slot>();
    }
    private void Update()
    {
        MainQuest(mainStoryCount);

        for (int i = 0; i < detailQuest.quests.Count; i++)
        {
            text[i].text = detailQuest.quests[i].questTitle;
            if (detailQuest.quests[i].questID == 0)
            {
                Quest1start(detailQuest.quests[i]);
            }
            else if (detailQuest.quests[i].questID == 1)
            {
                Quest2Start(detailQuest.quests[i]);
            }
        }
        for (int i = detailQuest.quests.Count; i < text.Length; i++)
        {
            text[i].text = " ";
        }
    }
    void MainQuest(int mainStoryCount)
    {
        if (mainQuest[mainStoryCount].accept)
        {
            if (mainQuest[mainStoryCount].isProceeding)
            {
                if (mainStoryCount == 0)
                {
                    if (weapon.target != null && weapon.target.name == mainQuest[mainStoryCount].targetName)
                    {
                        if (weapon.target.kill)
                        {
                            mainQuest[mainStoryCount].targetKill++;
                            weapon.target.kill = false;
                        }
                    }
                }
                else if (mainStoryCount == 1)
                {
                    if (weapon.boss != null && weapon.boss.name == mainQuest[mainStoryCount].targetName)
                    {
                        if (weapon.boss.kill)
                        {
                            mainQuest[mainStoryCount].targetKill++;
                        }
                    }
                }
                if (mainQuest[mainStoryCount].targetKill >= mainQuest[mainStoryCount].targetCount)
                {
                    mainQuest[mainStoryCount].targetKill = mainQuest[mainStoryCount].targetCount;
                    mainQuest[mainStoryCount].isClear = true;
                    mainQuest[mainStoryCount].accept = false;
                    mainQuest[mainStoryCount].isProceeding = false;
                    mainQuestClear = true;
                }
            }
        }
    }


    public void Quest1start(Quest quest)
    {
        if (quest.isProceeding)
        {
            if (weapon.target != null && weapon.target.name == quest.targetName)
            {
                if (weapon.target.subKill)
                {
                    quest.targetKill++;
                    weapon.target.subKill = false;
                }
            }
        }
        if (quest.targetKill >= quest.targetCount)
        {
            quest.targetKill = quest.targetCount;
            quest.isClear = true;
        }
    }
    public void Quest2Start(Quest quest)
    {
        if (quest.isProceeding)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (quest.needitem == slots[i].item)
                {
                    quest.haveCount = slots[i].itemCount;
                    break;
                }
            }
        }
        if (quest.haveCount >= quest.needCount)
        {
            quest.isClear = true;
        }
    }
}
