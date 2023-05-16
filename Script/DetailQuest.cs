using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetailQuest : MonoBehaviour
{
    public int questNum;
    public GameObject notAccept;
    public GameObject beforeAccept;
    public GameObject questTip;
    public List<Quest> quests;
    public Quest[] quest;
    public Inventory inventory;
    public GameObject slotBackGround;
    Slot[] slots;

    public AudioSource audioSource;
    public AudioClip complet;

    private void Awake()
    {
        slots = slotBackGround.GetComponentsInChildren<Slot>();
        for (int i = 0; i < quest.Length; i++)
        {
            quest[i].NewQuest();
        }
    }
    private void Update()
    {
        if (!quest[questNum].accept)
        {
            beforeAccept.SetActive(true);
            notAccept.SetActive(false);
        }
        else if(quest[questNum].accept)
        {
            notAccept.SetActive(true);
            beforeAccept.SetActive(false);
        }
    }
    public void OnClickAccept()
    {
        quest[questNum].accept = true;
        quest[questNum].isProceeding = true;
        quest[questNum].questID = questNum;
        quests.Add(quest[questNum]);
    }
    public void OnClickcomplete()
    {
        if(quest[questNum].isClear)
        {
            audioSource.clip = complet;
            audioSource.Play();
            quest[questNum].NewQuest();
            quests.Remove(quest[questNum]);
            switch(questNum)
            {
                case 1:
                    for(int i=0;i<slots.Length;i++)
                    {
                        if (slots[i].item == quest[questNum].needitem)
                        {
                            slots[i].SetSlotCount(-5);
                            inventory.AcquirelItem(quest[questNum].item);
                        }
                    }
                    break;
            }
            PlayerManager.instance.nowPlayer.coin += quest[questNum].coin;
            PlayerManager.instance.nowPlayer.curEXP += quest[questNum].exp;
            questTip.SetActive(false);
        }
    }
    public void OnClickIgnore()
    {
        quest[questNum].NewQuest();
        quests.Remove(quest[questNum]);
    }

}
