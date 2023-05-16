using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Vector3 originPos;

    public Inventory inventory;

    public Weapon changeWeapon;
    public Weapon lookchangeWeapon;
    public Shield changeShield;
    public Shield lookchangeShield;
    public Item item;
    public int itemCount;
    public Image itemImage;
    public Slot weponSlot;
    public Slot shieldSlot;

    public Slot[] qSlot;
    public bool lookSlot = true;
    [SerializeField]
    private Text textCount;
    [SerializeField]
    private GameObject text;

    [SerializeField]
    private ToolTip theToolTip;

    public GameObject store;
    public GameObject selctUI;

    public GameObject weaponSellUI;
    public WeaponSellUI weaponSellUIcont;

    public GameObject itemSellUI;
    public ItemSellUI itemSellUIcont;

    public bool isCooltime;
    float cool;

    AudioSource audioSource;
    public AudioClip selectSound;
    public AudioClip weaponSound;
    public AudioClip shieldSound;
    public AudioClip skillSound;
    public AudioClip potionSound;
    public AudioClip ingredientSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item, int count = 1)
    {
        item = _item;
        itemCount = count;
        itemImage.sprite = _item.itemImage;

        if (item.type == Item.Type.potion || item.type == Item.Type.ingredient)
        {
            text.SetActive(true);
            textCount.text = itemCount.ToString();
        }
        else if (_item.type == Item.Type.Weapon || _item.type == Item.Type.shield || _item.type == Item.Type.skill)
        {
            textCount.text = "0";
            text.SetActive(false);
        }

        SetColor(1.0f);
    }
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        textCount.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        textCount.text = "0";
        text.SetActive(false);
    }
    // 슬롯 사용
    public void OnPointerClick(PointerEventData eventData)
    {
        if (lookSlot)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (item != null)
                {
                    if (gameObject.name == "WeaponSlot")
                    {
                        audioSource.clip = weaponSound;
                        audioSource.Play();
                        theToolTip.HideToolTip();
                        PlayerManager.instance.nowPlayer.maxattack -= item.Atk;
                        PlayerManager.instance.nowPlayer.minattack -= item.Atk;
                        inventory.AcquirelItem(weponSlot.item);
                        ClearSlot();
                        changeWeapon.ChaingeWeapon(-1);
                        lookchangeWeapon.ChaingeWeapon(-1);
                    }
                    else if (gameObject.name == "ShieldSlot")
                    {
                        audioSource.clip = shieldSound;
                        audioSource.Play();
                        theToolTip.HideToolTip();
                        PlayerManager.instance.nowPlayer.def -= item.Def;
                        inventory.AcquirelItem(shieldSlot.item);
                        changeShield.ChaingeShield(-1);
                        lookchangeShield.ChaingeShield(-1);
                        ClearSlot();
                    }
                    else if (gameObject.name == "QuickSlot")
                    {
                        if (item.type == Item.Type.potion)
                        {
                            audioSource.clip = potionSound;
                            audioSource.Play();
                            if (item.itemName == "마나포션")
                            {
                                PlayerManager.instance.nowPlayer.curMP += 3;
                                if (PlayerManager.instance.nowPlayer.curMP > PlayerManager.instance.nowPlayer.maxMP)
                                {
                                    PlayerManager.instance.nowPlayer.curMP = PlayerManager.instance.nowPlayer.maxMP;
                                }
                            }
                            else if (item.itemName == "체력포션")
                            {
                                PlayerManager.instance.nowPlayer.curHP += (int)(PlayerManager.instance.nowPlayer.maxHP * 0.3);
                                if (PlayerManager.instance.nowPlayer.curHP > PlayerManager.instance.nowPlayer.maxHP)
                                {
                                    PlayerManager.instance.nowPlayer.curHP = PlayerManager.instance.nowPlayer.maxHP;
                                }
                            }
                            SetSlotCount(-1);
                            if (itemCount == 0)
                            {
                                theToolTip.HideToolTip();
                            }
                        }
                        else
                        {
                            ClearSlot();
                        }
                    }
                    else
                    {
                        // 판매
                        if (store.activeSelf)
                        {
                            if (item.type == Item.Type.Weapon || item.type == Item.Type.shield)
                            {
                                selctUI.SetActive(false);
                                itemSellUI.SetActive(false);
                                weaponSellUI.SetActive(true);
                                weaponSellUI.transform.position = Input.mousePosition;
                                weaponSellUIcont = weaponSellUI.GetComponent<WeaponSellUI>();
                                weaponSellUIcont.slot = this;
                                weaponSellUIcont.coin.text = item.price.ToString();
                                weaponSellUIcont.itemImage.sprite = item.itemImage;
                            }
                            else
                            {
                                selctUI.SetActive(false);
                                weaponSellUI.SetActive(false);
                                itemSellUI.SetActive(true);
                                itemSellUI.transform.position = Input.mousePosition;
                                itemSellUIcont = itemSellUI.GetComponent<ItemSellUI>();
                                itemSellUIcont.slot = this;
                                itemSellUIcont.itemImage.sprite = item.itemImage;
                                itemSellUIcont.coin.text = "0";
                                itemSellUIcont.countText.text = "0";
                                itemSellUIcont.count = 0;

                            }
                        }
                        else
                        {
                            //장비 장착 해제
                            if (item.type == Item.Type.Weapon)
                            {
                                audioSource.clip = weaponSound;
                                audioSource.Play();
                                if (weponSlot.item == null)
                                {
                                    theToolTip.HideToolTip();
                                    weponSlot.AddItem(item);
                                    PlayerManager.instance.nowPlayer.maxattack += item.Atk;
                                    PlayerManager.instance.nowPlayer.minattack += item.Atk;
                                    changeWeapon.ChaingeWeapon(item.itemCode);
                                    lookchangeWeapon.ChaingeWeapon(item.itemCode);
                                    ClearSlot();
                                }
                                else if (weponSlot.item != null)
                                {
                                    PlayerManager.instance.nowPlayer.maxattack += item.Atk;
                                    PlayerManager.instance.nowPlayer.minattack += item.Atk;
                                    changeWeapon.ChaingeWeapon(item.itemCode);
                                    lookchangeWeapon.ChaingeWeapon(item.itemCode);
                                    Item tempItem = weponSlot.item;
                                    weponSlot.AddItem(item);
                                    AddItem(tempItem);
                                    PlayerManager.instance.nowPlayer.maxattack -= item.Atk;
                                    PlayerManager.instance.nowPlayer.minattack -= item.Atk;
                                    theToolTip.HideToolTip();
                                    theToolTip.transform.position = Input.mousePosition;
                                    theToolTip.ShowToolTip(item, true);
                                }
                            }
                            else if (item.type == Item.Type.shield)
                            {
                                audioSource.clip = shieldSound;
                                audioSource.Play();
                                if (shieldSlot.item == null)
                                {
                                    theToolTip.HideToolTip();
                                    shieldSlot.AddItem(item);
                                    PlayerManager.instance.nowPlayer.def += item.Def;
                                    changeShield.ChaingeShield(item.itemCode);
                                    lookchangeShield.ChaingeShield(item.itemCode);
                                    ClearSlot();
                                }
                                else if (shieldSlot.item != null)
                                {
                                    PlayerManager.instance.nowPlayer.def += item.Def;
                                    changeShield.ChaingeShield(item.itemCode);
                                    lookchangeShield.ChaingeShield(item.itemCode);
                                    Item tempItem = shieldSlot.item;
                                    shieldSlot.AddItem(item);
                                    AddItem(tempItem);
                                    PlayerManager.instance.nowPlayer.def -= item.Def;
                                    theToolTip.HideToolTip();
                                    theToolTip.transform.position = Input.mousePosition;
                                    theToolTip.ShowToolTip(item, true);
                                }
                            }
                            // 아이템 사용
                            else if (item.type == Item.Type.potion)
                            {
                                audioSource.clip = potionSound;
                                audioSource.Play();
                                if (item.itemName == "마나포션")
                                {
                                    PlayerManager.instance.nowPlayer.curMP += 3;
                                    if (PlayerManager.instance.nowPlayer.curMP > PlayerManager.instance.nowPlayer.maxMP)
                                    {
                                        PlayerManager.instance.nowPlayer.curMP = PlayerManager.instance.nowPlayer.maxMP;
                                    }
                                }
                                else if (item.itemName == "체력포션")
                                {
                                    PlayerManager.instance.nowPlayer.curHP += (int)(PlayerManager.instance.nowPlayer.maxHP * 0.3);
                                    if (PlayerManager.instance.nowPlayer.curHP > PlayerManager.instance.nowPlayer.maxHP)
                                    {
                                        PlayerManager.instance.nowPlayer.curHP = PlayerManager.instance.nowPlayer.maxHP;
                                    }

                                }
                                SetSlotCount(-1);
                                if (itemCount == 0)
                                {
                                    theToolTip.HideToolTip();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    //tooltip
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            if (gameObject.name == "WeaponSlot" || gameObject.name == "ShieldSlot")
            {
                theToolTip.transform.position = Input.mousePosition;
                theToolTip.ShowToolTip(item, false);
            }
            else
            {
                theToolTip.transform.position = Input.mousePosition;
                theToolTip.ShowToolTip(item, true);
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        theToolTip.HideToolTip();
    }
    //Drag 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (lookSlot)
        {
            audioSource.clip = selectSound;
            audioSource.Play();
            if (!isCooltime && eventData.button == PointerEventData.InputButton.Left)
            {
                if (item != null)
                {
                    originPos = transform.position;
                    DragItem.instance.dragSlot = this;
                    DragItem.instance.DragSetIamge(itemImage);
                    DragItem.instance.transform.position = eventData.position;
                }
            }
        }
    }
    // Drag 중
    public void OnDrag(PointerEventData eventData)
    {
        if (lookSlot)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (item != null)
                {
                    DragItem.instance.transform.position = eventData.position;
                }
            }
        }
    }
    // Drag 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        DragItem.instance.SetImageColor(0);
        DragItem.instance.dragSlot = null;
    }
    // 놓는지점 
    public void OnDrop(PointerEventData eventData)
    {
        if (!isCooltime&&lookSlot)
        {
            if (DragItem.instance.dragSlot != null)
            {
                Item tempItem = item;
                int tempItemCount = itemCount;
                if (gameObject.name == "WeaponSlot")
                {
                    if (DragItem.instance.dragSlot.item.type == Item.Type.Weapon)
                    {
                        audioSource.clip = weaponSound;
                        audioSource.Play();
                        AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);
                        PlayerManager.instance.nowPlayer.maxattack += item.Atk;
                        PlayerManager.instance.nowPlayer.minattack += item.Atk;
                        changeWeapon.ChaingeWeapon(item.itemCode);
                        lookchangeWeapon.ChaingeWeapon(item.itemCode);
                        if (tempItem != null)
                        {
                            PlayerManager.instance.nowPlayer.maxattack -= tempItem.Atk;
                            PlayerManager.instance.nowPlayer.minattack -= tempItem.Atk;
                            DragItem.instance.dragSlot.AddItem(tempItem, tempItemCount);
                            theToolTip.HideToolTip();
                            theToolTip.transform.position = Input.mousePosition;
                            theToolTip.ShowToolTip(item, false);
                        }
                        else if (tempItem == null)
                        {
                            DragItem.instance.dragSlot.ClearSlot();
                        }
                    }
                }
                else if (gameObject.name == "ShieldSlot")
                {
                    if (DragItem.instance.dragSlot.item.type == Item.Type.shield)
                    {
                        audioSource.clip = shieldSound;
                        audioSource.Play();
                        AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);
                        PlayerManager.instance.nowPlayer.def += item.Def;
                        changeShield.ChaingeShield(item.itemCode);
                        lookchangeShield.ChaingeShield(item.itemCode);
                        if (tempItem != null)
                        {
                            PlayerManager.instance.nowPlayer.def -= tempItem.Def;
                            DragItem.instance.dragSlot.AddItem(tempItem, tempItemCount);
                            theToolTip.HideToolTip();
                            theToolTip.transform.position = Input.mousePosition;
                            theToolTip.ShowToolTip(item, false);
                        }
                        else if (tempItem == null)
                        {
                            DragItem.instance.dragSlot.ClearSlot();
                        }
                    }
                }
                // 퀵슬롯 교환
                else if (gameObject.name == "QuickSlot")
                {
                    if (DragItem.instance.dragSlot.item.type == Item.Type.potion)
                    {
                        audioSource.clip = potionSound;
                        audioSource.Play();
                        AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);
                        if (tempItem != null && tempItem.type == Item.Type.potion)
                        {
                            DragItem.instance.dragSlot.AddItem(tempItem, tempItemCount);
                            theToolTip.HideToolTip();
                            theToolTip.transform.position = Input.mousePosition;
                            theToolTip.ShowToolTip(item, false);
                        }
                        else if (tempItem != null && !isCooltime&&tempItem.type == Item.Type.skill)
                        {
                            if (DragItem.instance.dragSlot.name == "QuickSlot")
                            {
                                DragItem.instance.dragSlot.AddItem(tempItem, tempItemCount);
                                theToolTip.HideToolTip();
                                theToolTip.transform.position = Input.mousePosition;
                                theToolTip.ShowToolTip(item, false);
                            }
                            else
                            {
                                DragItem.instance.dragSlot.ClearSlot();
                            }
                        }
                        else if (tempItem == null)
                        {
                            DragItem.instance.dragSlot.ClearSlot();
                        }
                    }
                    else if (DragItem.instance.dragSlot.item.type == Item.Type.skill)
                    {
                        audioSource.clip = skillSound;
                        audioSource.Play();
                        if (tempItem != null && tempItem.type == Item.Type.potion)
                        {
                            if (DragItem.instance.dragSlot.name == "QuickSlot")
                            {
                                AddItem(DragItem.instance.dragSlot.item);
                                DragItem.instance.dragSlot.AddItem(tempItem, tempItemCount);
                                theToolTip.HideToolTip();
                                theToolTip.transform.position = Input.mousePosition;
                                theToolTip.ShowToolTip(item, false);
                            }
                            else
                            {
                                for (int i = 0; i < qSlot.Length; i++)
                                {
                                    
                                    if (qSlot[i].item == DragItem.instance.dragSlot.item)
                                    {
                                        if (qSlot[i].isCooltime)
                                        {
                                            return;
                                        }
                                        qSlot[i].ClearSlot();
                                    }
                                }
                                AddItem(DragItem.instance.dragSlot.item);
                                inventory.AcquirelItem(tempItem, tempItemCount);
                                if (DragItem.instance.dragSlot.name != "SkillSlot")
                                    DragItem.instance.dragSlot.ClearSlot();
                            }
                        }
                        else if (tempItem != null && tempItem.type == Item.Type.skill)
                        {
                            if (DragItem.instance.dragSlot.name == "QuickSlot")
                            {
                                AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);
                                DragItem.instance.dragSlot.AddItem(tempItem);
                                theToolTip.HideToolTip();
                                theToolTip.transform.position = Input.mousePosition;
                                theToolTip.ShowToolTip(item, false);
                            }
                            else
                            {
                                for (int i = 0; i < qSlot.Length; i++)
                                {
                                    if (qSlot[i].item == DragItem.instance.dragSlot.item)
                                    {
                                        if (qSlot[i].isCooltime)
                                        {
                                            return;
                                        }
                                        qSlot[i].ClearSlot();
                                    }
                                }
                                AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);
                            }
                        }
                        else if (tempItem == null)
                        {
                            if (DragItem.instance.dragSlot.name == "QuickSlot")
                            {
                                AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);
                                DragItem.instance.dragSlot.ClearSlot();
                            }
                            else
                            {
                                for (int i = 0; i < qSlot.Length; i++)
                                {
                                    if (qSlot[i].item == DragItem.instance.dragSlot.item)
                                    {
                                        if(qSlot[i].isCooltime)
                                        {
                                            return;
                                        }
                                        qSlot[i].ClearSlot();
                                    }
                                }
                                AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);
                                if (DragItem.instance.dragSlot.name != "SkillSlot")
                                {
                                    DragItem.instance.dragSlot.ClearSlot();
                                }
                            }
                            
                        }
                    }
                }
                
                else if (gameObject.name == "SkillSlot")
                {
                    return;
                }
                //인벤토리 교환
                else
                {
                    if (DragItem.instance.dragSlot.name == "WeaponSlot")
                    {
                        audioSource.clip = weaponSound;
                        audioSource.Play();
                        if (item != null && item.type == Item.Type.Weapon)
                        {
                            PlayerManager.instance.nowPlayer.maxattack -= DragItem.instance.dragSlot.item.Atk;
                            PlayerManager.instance.nowPlayer.minattack -= DragItem.instance.dragSlot.item.Atk;
                            changeWeapon.ChaingeWeapon(-1);
                            lookchangeWeapon.ChaingeWeapon(-1);
                            AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);

                            DragItem.instance.dragSlot.AddItem(tempItem, tempItemCount);
                            PlayerManager.instance.nowPlayer.maxattack += tempItem.Atk;
                            PlayerManager.instance.nowPlayer.minattack += tempItem.Atk;
                            changeWeapon.ChaingeWeapon(item.itemCode);
                            lookchangeWeapon.ChaingeWeapon(item.itemCode);
                        }
                        else if (item == null)
                        {
                            PlayerManager.instance.nowPlayer.maxattack -= DragItem.instance.dragSlot.item.Atk;
                            PlayerManager.instance.nowPlayer.minattack -= DragItem.instance.dragSlot.item.Atk;
                            changeWeapon.ChaingeWeapon(-1);
                            lookchangeWeapon.ChaingeWeapon(-1);
                            AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);

                            DragItem.instance.dragSlot.ClearSlot();
                        }

                    }
                    else if (DragItem.instance.dragSlot.name == "ShieldSlot")
                    {
                        audioSource.clip = shieldSound;
                        audioSource.Play();
                        if (item != null && item.type == Item.Type.shield)
                        {
                            PlayerManager.instance.nowPlayer.def -= DragItem.instance.dragSlot.item.Def;
                            changeShield.ChaingeShield(-1);
                            lookchangeShield.ChaingeShield(-1);
                            AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);

                            DragItem.instance.dragSlot.AddItem(tempItem, tempItemCount);
                            PlayerManager.instance.nowPlayer.def += tempItem.Def;
                            changeShield.ChaingeShield(item.itemCode);
                            lookchangeShield.ChaingeShield(item.itemCode);
                        }
                        else if (item == null)
                        {
                            PlayerManager.instance.nowPlayer.def -= DragItem.instance.dragSlot.item.Def;
                            changeShield.ChaingeShield(-1);
                            lookchangeShield.ChaingeShield(-1);
                            AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);

                            DragItem.instance.dragSlot.ClearSlot();
                        }
                    }
                    else if (DragItem.instance.dragSlot.name == "SkillSlot")
                    {
                        return;
                    }
                    else if (DragItem.instance.dragSlot.name == "QuickSlot")
                    {
                        if (tempItem != null && tempItem.type == Item.Type.potion)
                        {
                            audioSource.clip = potionSound;
                            audioSource.Play();
                            AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);
                            DragItem.instance.dragSlot.AddItem(tempItem, tempItemCount);
                        }
                        else if (tempItem == null)
                        {
                            AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);
                            DragItem.instance.dragSlot.ClearSlot();
                        }
                    }
                    else
                    {
                        AddItem(DragItem.instance.dragSlot.item, DragItem.instance.dragSlot.itemCount);
                        if (tempItem != null && tempItem.type != Item.Type.skill)
                        {
                            if(DragItem.instance.dragSlot.item.type== Item.Type.Weapon)
                            {
                                audioSource.clip = weaponSound;
                                audioSource.Play();
                            }
                            else if(DragItem.instance.dragSlot.item.type == Item.Type.shield)
                            {
                                audioSource.clip = shieldSound;
                                audioSource.Play();
                            }
                            else if (DragItem.instance.dragSlot.item.type == Item.Type.potion)
                            {
                                audioSource.clip = potionSound;
                                audioSource.Play();
                            }
                            else if (DragItem.instance.dragSlot.item.type == Item.Type.ingredient)
                            {
                                audioSource.clip = ingredientSound;
                                audioSource.Play();
                            }

                            DragItem.instance.dragSlot.AddItem(tempItem, tempItemCount);
                            theToolTip.HideToolTip();
                            theToolTip.transform.position = Input.mousePosition;
                            theToolTip.ShowToolTip(item, true);
                        }
                        else if (tempItem == null)
                        {
                            if (DragItem.instance.dragSlot.item.type == Item.Type.Weapon)
                            {
                                audioSource.clip = weaponSound;
                                audioSource.Play();
                            }
                            else if (DragItem.instance.dragSlot.item.type == Item.Type.shield)
                            {
                                audioSource.clip = shieldSound;
                                audioSource.Play();
                            }
                            else if (DragItem.instance.dragSlot.item.type == Item.Type.potion)
                            {
                                audioSource.clip = potionSound;
                                audioSource.Play();
                            }
                            else if (DragItem.instance.dragSlot.item.type == Item.Type.ingredient)
                            {
                                audioSource.clip = ingredientSound;
                                audioSource.Play();
                            }
                            DragItem.instance.dragSlot.ClearSlot();
                        }
                    }

                }
            }
        }
    }
    public void CoolTime(float time)
    {
        isCooltime = true;
        cool = time;
        StartCoroutine("CoolTimeCheck");
    }
    IEnumerator CoolTimeCheck()
    {
        yield return new WaitForSeconds(cool);
        isCooltime = false;
    }
}
