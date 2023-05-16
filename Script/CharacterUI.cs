using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    // 상점 메뉴
    public GameObject WeaponStore;
    public GameObject itemStore;
    public GameObject guildManager;

    // 캐릭터 UI
    public GameObject skill;
    public GameObject inventory;
    public GameObject equipment;
    public GameObject request;
    public GameObject questNote;
    public GameObject option;
    public ToolTip theToolTip;

    bool kdown;
    bool idown;
    bool edown;

    bool odown;
    bool jdown;

    AudioSource audioSource;
    public AudioClip openUI;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Skill();
        InventoryCont();
        EquipmentCont();
        OptionCont();
        QuestCont();
    }
    
    public void OnClickOption()
    {
        inventory.SetActive(false);
        equipment.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        option.SetActive(true);
    }
    public void OnClickSkill()
    {
        audioSource.clip = openUI;
        audioSource.Play();
        if (!skill.activeSelf)
        {
            skill.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else if(skill.activeSelf)
        {
            theToolTip.HideToolTip();
            skill.SetActive(false);
            MouseControl();
        }
    }
    public void OnClickInventory()
    {
        audioSource.clip = openUI;
        audioSource.Play();
        if (!inventory.activeSelf)
        {
            inventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else if (inventory.activeSelf)
        {
            theToolTip.HideToolTip();
            inventory.SetActive(false);
            MouseControl();
        }
    }
    public void OnClickEquipment()
    {
        audioSource.clip = openUI;
        audioSource.Play();
        if (!equipment.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            equipment.SetActive(true);
        }
        else if (equipment.activeSelf)
        {
            theToolTip.HideToolTip();
            equipment.SetActive(false);
            MouseControl();
        }
    }
    public void OnClickReQuest()
    {
        audioSource.clip = openUI;
        audioSource.Play();
        if (!request.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            request.SetActive(true);
            questNote.SetActive(false);
        }
        else if (request.activeSelf)
        {
            questNote.SetActive(false);
            request.SetActive(false);
            MouseControl();
        }
    }

    void OptionCont()
    {
        odown = Input.GetButtonDown("Option");
        if(odown)
        {
            inventory.SetActive(false);
            equipment.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            option.SetActive(true);
        }
    }
    void Skill()
    {
        
        kdown = Input.GetKeyDown(KeyCode.K);
        if (kdown && !skill.activeSelf)
        {
            audioSource.clip = openUI;
            audioSource.Play();
            Cursor.lockState = CursorLockMode.None;
            skill.SetActive(true);
        }
        else if(kdown && skill.activeSelf)
        {
            audioSource.clip = openUI;
            audioSource.Play();
            theToolTip.HideToolTip();
            skill.SetActive(false);
            MouseControl();
        }
    }
    void InventoryCont()
    {
        idown = Input.GetButtonDown("Inventory");
        
        if (idown && !inventory.activeSelf)
        {
            audioSource.clip = openUI;
            audioSource.Play();
            Cursor.lockState = CursorLockMode.None;
            inventory.SetActive(true);
        }
        else if (idown && inventory.activeSelf)
        {
            audioSource.clip = openUI;
            audioSource.Play();
            theToolTip.HideToolTip();
            inventory.SetActive(false);
            MouseControl();
        }
    }
    void EquipmentCont()
    {
        edown = Input.GetButtonDown("Equipment");
        
        if (edown && !equipment.activeSelf)
        {
            audioSource.clip = openUI;
            audioSource.Play();
            Cursor.lockState = CursorLockMode.None;
            equipment.SetActive(true);
        }
        else if (edown && equipment.activeSelf)
        {
            audioSource.clip = openUI;
            audioSource.Play();
            theToolTip.HideToolTip();
            equipment.SetActive(false);
            MouseControl();
        }
    }
    void QuestCont()
    {
        jdown = Input.GetKeyDown(KeyCode.J);
        if(jdown && !request.activeSelf)
        {
            audioSource.clip = openUI;
            audioSource.Play();
            Cursor.lockState = CursorLockMode.None;
            request.SetActive(true);
            questNote.SetActive(false);
        }
        else if(jdown && request.activeSelf)
        {
            audioSource.clip = openUI;
            audioSource.Play();
            questNote.SetActive(false);
            request.SetActive(false);
            MouseControl();
        }
    }

    public void MouseControl()
    {
        if (!inventory.activeSelf && !equipment.activeSelf && !option.activeSelf && !WeaponStore.activeSelf && !itemStore.activeSelf && !guildManager.activeSelf && !request.activeSelf && !skill.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
