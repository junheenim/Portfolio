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

    //����ġ ǥ��
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
            QuestDetail.text = "���� �� ���� �� �Ա� ������ ��ġ�� �Ƿ� �޾Ҵ�.";
        }
        else if (questnum == 1)
        {
            QuestDetail.text = "�̹��� ���� ���� ��ġ �Ƿڴ�. ���Ұ� ������ �غ� �ܴ��� �ؼ� ����!";
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
            QuestDetail.text = "�������� ������ ��ġ�Ƿڸ� �޾Ҵ�. �������� ������ �Ա��� ��������!";
            Target.text = detailQuest.quests[count].targetName + " : " + detailQuest.quests[count].targetKill.ToString() + " / " + detailQuest.quests[count].targetCount.ToString();
            Itemimage.sprite = coin;
            reword.text = detailQuest.quests[count].coin.ToString();
            exptext.text = detailQuest.quests[count].exp.ToString();
            exp.SetActive(true);
        }
        else if (n == 1)
        {
            QuestDetail.text = "�������̰� ö�� ��Ź�ߴ�. ������ ����Ѱ��� �ִ� ���� �������� ö�� �԰� ��ٴµ�...";
            Target.text = detailQuest.quests[count].needitem.name + " : " + detailQuest.quests[count].haveCount.ToString() + " / " + detailQuest.quests[count].needCount.ToString();
            Itemimage.sprite = detailQuest.quests[count].item.itemImage;
            reword.text = detailQuest.quests[count].item.name;
            exp.SetActive(false);
        }
    }
    
}
