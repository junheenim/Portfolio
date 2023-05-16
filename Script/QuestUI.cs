using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public CharacterUI UI;
    public DetailQuest detailQuest;
    // questTip
    public GameObject QuestNote;

    //경험치 표시
    public GameObject exp;
    public Text QuestDetail;
    public Text Target;
    public Image Itemimage;
    public Text reword;
    public Text exptext;
    public Sprite coin;
    public PlayerManager playerManager;

    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnClickClose()
    {
        gameObject.SetActive(false);
        QuestNote.SetActive(false);
        UI.MouseControl();
    }
    public void Onclick1But()
    {
        if (detailQuest.quests.Count >= 1)
        {
            audioSource.Play();
            QuestNote.SetActive(true);
            QuestType(detailQuest.quests[0].questID,0);
        }
    }
    public void Onclick2But()
    {
        if (detailQuest.quests.Count >= 2)
        {
            audioSource.Play();
            QuestNote.SetActive(true);
            QuestType(detailQuest.quests[1].questID,1);
        }
    }
    public void OnClickmainStory()
    {
        int questnum = playerManager.questManager.mainStoryCount;
        QuestNote.SetActive(true);
        if (questnum == 0)
        {
            QuestDetail.text = "마을 앞 엘폰 숲 입구 슬라임 퇴치를 의뢰 받았다.";
        }
        else if (questnum == 1)
        {
            QuestDetail.text = "이번엔 숲의 주인 퇴치 의뢰다. 강할거 같은니 준비 단단히 해서 들어가자!";
        }
        Target.text = playerManager.questManager.mainQuest[questnum].targetName + " : " + playerManager.questManager.mainQuest[questnum].targetKill.ToString() +
            " / " + playerManager.questManager.mainQuest[questnum].targetCount;
        reword.text = playerManager.questManager.mainQuest[questnum].coin.ToString();
        exptext.text = playerManager.questManager.mainQuest[questnum].exp.ToString();
    }
    void QuestType(int n,int count)
    {
        if (n == 0)
        {
            QuestDetail.text = "마을에서 슬라임 퇴치의뢰를 받았다. 슬라임은 엘폰숲 입구에 있을꺼야!";
            Target.text = detailQuest.quests[count].targetName + " : " + detailQuest.quests[count].targetKill.ToString() + " / " + detailQuest.quests[count].targetCount.ToString();
            Itemimage.sprite = coin;
            reword.text = detailQuest.quests[count].coin.ToString();
            exptext.text = detailQuest.quests[count].exp.ToString();
            exp.SetActive(true);
        }
        else if (n == 1)
        {
            QuestDetail.text = "대장장이가 철을 부탁했다. 엘폰숲 깊숙한곳에 있는 가시 슬라임이 철을 먹고 산다는데...";
            Target.text = detailQuest.quests[count].needitem.name + " : " + detailQuest.quests[count].haveCount.ToString() + " / " + detailQuest.quests[count].needCount.ToString();
            Itemimage.sprite = detailQuest.quests[count].item.itemImage;
            reword.text = detailQuest.quests[count].item.name;
            exp.SetActive(false);
        }
    }
    
}
