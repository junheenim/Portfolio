using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalkUI : MonoBehaviour
{
    public TalkOn talk;
    public Text talkText;
    public TalkBox talkBox;
    public int talkCount = 0;
    public int talkIndex = 0;
    bool GetEnter, eDown;
    PlayerManager playerManager;

    AudioSource audioSource;
    private void Awake()
    {
        playerManager = GameObject.Find("Player(Clone)").GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if(gameObject.activeSelf)
        {
            playerManager.moveStop = true;
            NextTalk();
        }
    }
    public void SetTalk()
    {
        talkText.text = talkBox.GetTalk(talkCount, talkIndex);
    }
    void NextTalk()
    {
        GetEnter = Input.GetKeyDown(KeyCode.Return);
        eDown = Input.GetButtonDown("Interaction");
        if (eDown || GetEnter)
        {
            audioSource.Play();
            talkIndex++;
            if (talkBox.GetTalk(talkCount, talkIndex) == null)
            {
                talkIndex = 0;
                gameObject.SetActive(false);
                playerManager.moveStop = false;
                switch (talkCount)
                {
                    case 0:
                        playerManager.luluID++;
                        playerManager.mainQuestData.QuestUpdate();
                        playerManager.questManager.mainQuest[0].NewQuest();
                        playerManager.questManager.mainQuest[0].accept = true;
                        playerManager.questManager.mainQuest[0].isProceeding = true;
                        break;
                    case 2:
                        playerManager.luluID++;
                        playerManager.mainQuestData.QuestUpdate();
                        playerManager.questManager.mainStoryCount++;
                        playerManager.questManager.mainQuest[0].NewQuest();
                        playerManager.questManager.mainQuest[1].NewQuest();
                        playerManager.questManager.mainQuest[1].accept = true;
                        playerManager.questManager.mainQuest[1].isProceeding = true;
                        break;
                    case 4:
                        playerManager.luluID++;
                        playerManager.mainQuestData.QuestUpdate();
                        playerManager.questManager.mainQuest[1].NewQuest();
                        break;
                }
            }
            else
            {
                SetTalk();
                if (talkCount == 2 && talkIndex == 1)
                {
                    PlayerManager.instance.nowPlayer.coin += 300;
                    PlayerManager.instance.nowPlayer.curEXP += 20;
                    PlayerManager.instance.LevelUp();
                }
                else if (talkCount == 4 && talkIndex == 1)
                {
                    PlayerManager.instance.nowPlayer.coin += 500;
                    PlayerManager.instance.nowPlayer.curEXP += 30;
                    PlayerManager.instance.LevelUp();
                }
            }
        }
    }
}
