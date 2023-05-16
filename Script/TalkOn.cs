using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalkOn : MonoBehaviour
{
    public int npcID;
    public int questID;
    public GameObject talkobj;
    public TalkUI talkUI;
    bool eDown;
    public GameObject nearObj;
    public PlayerManager playerManager;
    private void Update()
    {
        eDown = Input.GetButtonDown("Interaction");
        if (nearObj != null && nearObj.tag == "Player")
        {
            if (eDown)
            {
                if(npcID==0)
                {
                    questID = playerManager.luluID;
                    if (playerManager.mainQuestData.curStory == 7 || playerManager.mainQuestData.curStory == 8)
                    {
                        if (playerManager.questManager.mainQuestClear)
                        {
                            playerManager.questManager.mainQuestClear = false;
                            playerManager.luluID++;
                            questID = playerManager.luluID;
                        }
                    }
                }
                talkUI.talkCount = npcID + questID;
                talkUI.SetTalk();
                talkobj.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            nearObj = other.gameObject;
            playerManager = nearObj.GetComponent<PlayerManager>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nearObj = null;
        }
    }

}
