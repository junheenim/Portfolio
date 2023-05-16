using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerInpt : MonoBehaviour
{
    [SerializeField]
    GameObject characterBody;

    #region 성별
    //외형 컨트롤
    [SerializeField]
    private GameObject faceMile;
    [SerializeField]
    private GameObject bodyMile;
    [SerializeField]
    private GameObject cloackMile;
    [SerializeField]
    private GameObject faceFemile;
    [SerializeField]
    private GameObject bodyFemile;
    [SerializeField]
    private GameObject cloackFemile;
    // 카메라 외형 컨트롤
    [SerializeField]
    private GameObject cafaceMile;
    [SerializeField]
    private GameObject cabodyMile;
    [SerializeField]
    private GameObject cacloackMile;
    [SerializeField]
    private GameObject cafaceFemile;
    [SerializeField]
    private GameObject cabodyFemile;
    [SerializeField]
    private GameObject cacloackFemile;
    #endregion
    Rigidbody rigid;
    Animator animator;

    #region 카메라
    public Camera maincam;
    #endregion

    #region 공격
    public NomalAttackSlot nomalAtkCT; //노말어택 쿨타임
    public Slot weaponSlot;
    bool onAttack;
    public Weapon weapon;
    bool attacking = false;
    float attackingTime = 0.8f;
    float curAttacking;
    float AttackDelay = 0.6f;
    float AtcTime;
    float combo;

    //스턴 공격
    public StunAttackSlot stunAtkCT; //스턴어택 쿨타임
    bool stunAttack;
    bool stunAttackTime;
    bool stunAttackCoolTime = false;
    //어택 태그
    public GameObject stunTag;

    //스킬 동작시 이동 위치
    Vector3 pos;
    //스킬
    public Skill skill;
    bool isSkill;
    //돌진
    bool rushCoolTime = true;

    //파이어볼
    public GameObject fireBallOBJ;
    public FireBall fireBall;
    bool fireballCoolTime = true;

    //일섬
    bool slashCoolTime = true;
    public Slash slash;
    public GameObject skillattackPos;
    public ParticleSystem slashEffect;
    Collider skillAtkArea;
    #endregion

    #region 방어
    public DefPush defCT;// 방어 쿨타임
    public ParticleSystem guard;
    public ParticleSystem counter;
    public Slot shieldSlot;
    public GameObject playerLayer;
    public Transform sheildPos;
    bool onDefend = true;
    bool onCounter = true;
    bool hit = true;
    bool defendTime = true;

    bool down = true;
    bool isDown;

    Vector3 back;
    #endregion

    #region 이동
    bool runDown;
    public float moveSpeed = 5f;
    bool isMove;
    bool isJump;
    bool JumpDown;
    public int JumpPower = 20;
    bool lendingTime = false;
    #endregion

    #region 큇슬롯
    public Slot[] quickSlot;
    public CoolTime[] coolTimeImage;
    bool num1;
    bool num2;
    bool num3;
    bool num4;
    bool num5;
    #endregion

    #region 상호작용
    bool interDown;
    GameObject nearObject;

    public GameObject AdditemUI;
    [SerializeField]
    Text notice;

    bool visivleCursor;
    public GameObject soundActive;
    public GameObject optionActive;
    public GameObject inventoryActive;
    public GameObject EequipmentActive;
    public GameObject reQuestActive;
    public GameObject OptionActive;
    public GameObject skillActive;
    public Inventory inventory;

    public GameObject WeaponStore;
    public GameObject itemStore;
    public GameObject guildManager;
    public MainQuestData mainQuest;
    #endregion

    #region 쥬금
    public bool die;
    public bool resurrection;
    public GameObject dieUI;
    public Text dieText;
    public Image image;
    #endregion

    #region 사운드
    public AudioSource audioSource;
    public AudioSource audioSourceSlot;
    public AudioClip vilige;
    //공격
    public AudioClip[] attack;

    //걷기
    public AudioClip walk;
    public AudioClip run;
    //점프
    public AudioClip landing;
    //포션
    public AudioClip potion;
    //스킬
    public AudioClip rushSound;
    public AudioClip slashSound;
    //쥬금
    public AudioClip dieSound;
    //아이템
    public AudioClip moneyAcquisition;
    public AudioClip itemAcquisition;
    //상점
    public AudioClip openStore;
    #endregion
    //레벨 업
    public AudioClip levelUpSound;
    public GameObject levelUpEffect;

    private void Awake()
    {
        //캐릭터 외형 변경
        if (PlayerManager.instance.SelectCharacter == Character.Male)
        {
            faceMile.SetActive(true);
            bodyMile.SetActive(true);
            cloackMile.SetActive(true);
            faceFemile.SetActive(false);
            bodyFemile.SetActive(false);
            cloackFemile.SetActive(false);

            cafaceMile.SetActive(true);
            cabodyMile.SetActive(true);
            cacloackMile.SetActive(true);
            cafaceFemile.SetActive(false);
            cabodyFemile.SetActive(false);
            cacloackFemile.SetActive(false);
        }
        else if (PlayerManager.instance.SelectCharacter == Character.Female)
        {
            faceMile.SetActive(false);
            bodyMile.SetActive(false);
            cloackMile.SetActive(false);
            faceFemile.SetActive(true);
            bodyFemile.SetActive(true);
            cloackFemile.SetActive(true);

            cafaceMile.SetActive(false);
            cabodyMile.SetActive(false);
            cacloackMile.SetActive(false);
            cafaceFemile.SetActive(true);
            cabodyFemile.SetActive(true);
            cacloackFemile.SetActive(true);
        }

        rigid = GetComponent<Rigidbody>();
        animator = characterBody.GetComponent<Animator>();
        skillAtkArea = skillattackPos.GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        //마우스커서 락
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Start()
    {
        levelUpEffect.SetActive(false);
        skillAtkArea.enabled = false;
        dieUI.SetActive(false);
    }
    void Update()
    {
        if (!inventoryActive.activeSelf && !EequipmentActive.activeSelf && !OptionActive.activeSelf && !visivleCursor && !WeaponStore.activeSelf && !itemStore.activeSelf && !guildManager.activeSelf && !reQuestActive.activeSelf && !skillActive.activeSelf && !die&&!soundActive.activeSelf&&!optionActive.activeSelf)
        {
            if (!PlayerManager.instance.moveStop && !isJump && !isSkill)
            {
                if (weaponSlot.item != null && !stunAttackTime && !isDown)
                {
                    OnAttack();

                }
                if (weaponSlot.item != null && !onDefend && !isDown)
                {
                    OnStunAttack();
                }
                if (shieldSlot.item != null && !stunAttackTime && !isDown)
                {
                    OnDefend();
                }
            }
            if (!PlayerManager.instance.moveStop && !attacking && !lendingTime && !stunAttackTime && !isDown && !isSkill)
            {
                Move();
                Jump();
            }
        }

        if (!attacking && !isJump && !onDefend && !stunAttackTime && !isSkill && !die)
        {
            QuickSlotUse();
        }

        if (PlayerManager.instance.moveStop || inventoryActive.activeSelf || EequipmentActive.activeSelf || reQuestActive.activeSelf || skillActive.activeSelf)
        {
            animator.SetBool("IsRun", false);
            animator.SetBool("IsMove", false);
        }
        StandUp();
        Interaction();
        VisibleCursor();
    }
    public void Move()
    {
        runDown = Input.GetButton("Run");
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isMove = moveInput.magnitude != 0;

        animator.SetBool("IsMove", isMove);
        animator.SetFloat("X", moveInput.x);
        animator.SetFloat("Y", moveInput.y);
        animator.SetBool("IsRun", runDown);
        if (!isMove)
        {
            audioSource.Stop();
            animator.SetBool("IsRun", false);
        }

        if (isMove && (playerLayer.tag != "PlayerCounter" && playerLayer.tag != "ShiledPlayer") && !attacking)
        {
            Vector3 lookFoward = new Vector3(maincam.transform.forward.x, 0f, maincam.transform.forward.z).normalized;
            Vector3 lookRight = new Vector3(maincam.transform.right.x, 0f, maincam.transform.right.z).normalized;
            Vector3 moveDir = lookFoward * moveInput.y + lookRight * moveInput.x;

            characterBody.transform.forward = lookFoward;
            transform.position += moveDir * Time.deltaTime * moveSpeed * (runDown ? 1.5f : 1.0f);

            audioSource.clip = (runDown ? run : walk);
            if (!audioSource.isPlaying && !isJump)
            {
                audioSource.Play();
            }
        }
    }
    public void OnAttack()
    {
        onAttack = Input.GetButtonDown("Attack");

        curAttacking += Time.deltaTime;
        AtcTime += Time.deltaTime;
        if (onAttack && !isJump && !onDefend)
        {
            if (AtcTime > AttackDelay)
            {
                audioSource.clip = attack[(int)combo];
                audioSource.Play();
                nomalAtkCT.NormalAttack();
                attacking = true;
                animator.SetBool("IsMove", false);
                animator.SetBool("IsRun", false);
                animator.SetTrigger("Attack");
                if (attacking)
                {
                    animator.SetFloat("Combo", combo);
                    weapon.Use();
                    curAttacking = 0f;
                    AtcTime = 0f;
                    combo++;
                    if (combo > 2)
                    {
                        combo = 0;
                    }
                }
            }
        }
        if (curAttacking > attackingTime)
        {
            combo = 0;
            attacking = false;
        }
    }
    public void OnStunAttack()
    {
        stunAttack = Input.GetButtonDown("StunAttack");
        if (stunAttack && weapon.enTarget != null)
        {
            if (weapon.enTarget.tag == "TutorialEnemy")
            {
                if (weapon.teTarget.groggy && weapon.teTarget.groggyHit && !stunAttackTime && !stunAttackCoolTime)
                {
                    stunAttackCoolTime = true;
                    stunAtkCT.StunAttack();
                    StartCoroutine("StunAttackCoolTime");
                    stunAttackTime = true;
                    StunAttack();
                }
            }
            else if (weapon.enTarget.tag == "Enemy")
            {
                if (weapon.target.groggy && weapon.target.groggyHit && !stunAttackTime&& !stunAttackCoolTime)
                {
                    stunAttackCoolTime = true;
                    stunAtkCT.StunAttack();
                    StartCoroutine("StunAttackCoolTime");
                    stunAttackTime = true;
                    StunAttack();
                }
            }
            else if(weapon.enTarget.tag == "WildEnemy")
            {
                if (weapon.wildTarget.groggy && weapon.wildTarget.groggyHit && !stunAttackTime && !stunAttackCoolTime)
                {
                    stunAttackCoolTime = true;
                    stunAtkCT.StunAttack();
                    StartCoroutine("StunAttackCoolTime");
                    stunAttackTime = true;
                    StunAttack();
                }
            }
            else if(weapon.enTarget.tag == "Boss")
            {
                if (weapon.boss.isStun && weapon.boss.groggyHit && !stunAttackTime && !stunAttackCoolTime)
                {
                    stunAttackCoolTime = true;
                    stunAtkCT.StunAttack();
                    StartCoroutine("StunAttackCoolTime");
                    stunAttackTime = true;
                    StunAttack();
                }
            }
        }
    }
    void StunAttack()
    {
        animator.SetTrigger("StunAttack");
        StartCoroutine("StunAttackHit");
    }
    IEnumerator StunAttackHit()
    {
        yield return new WaitForSeconds(1.2f);
        stunTag.tag = "StunAttack";
        weapon.area.enabled = true;
        yield return new WaitForSeconds(0.3f);
        weapon.area.enabled = false;
        stunTag.tag = "Melee";
        animator.SetBool("StunAttackHit", false);
        stunAttackTime = false;
    }
    IEnumerator StunAttackCoolTime()
    {
        yield return new WaitForSeconds(5f);
        stunAttackCoolTime = false;
    }
    public void OnDefend()
    {
        onDefend = Input.GetButton("Defend");

        if (onDefend && defendTime)
        {
            defendTime = false;
            animator.SetTrigger("DefendSet");
            animator.SetBool("OnDefend", true);

            if (onCounter)
            {
                onCounter = false;
                StartCoroutine("Counter");
            }
        }

        if (!onDefend || defendTime)
        {
            if (playerLayer.tag == "PlayerCounter" || playerLayer.tag == "ShiledPlayer")
            {
                defCT.DefPushOn();
                StartCoroutine("DefendCoolTime");
                onCounter = true;
                StopCoroutine("Counter");
                playerLayer.tag = "Player";
                animator.SetBool("OnDefend", false);
            }
        }
    }
    IEnumerator DefendCoolTime()
    {
        yield return new WaitForSeconds(1.0f);
        defendTime = true;
    }
    IEnumerator Counter()
    {
        playerLayer.tag = "PlayerCounter";
        yield return new WaitForSeconds(0.2f);
        playerLayer.tag = "ShiledPlayer";
    }
    void Jump()
    {
        JumpDown = Input.GetButtonDown("Jump");
        if (JumpDown && !isJump && !onDefend && !attacking)
        {
            audioSource.Stop();
            isJump = true;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            animator.SetBool("IsJump", true);
        }
    }
    void Interaction()
    {
        interDown = Input.GetButtonDown("Interaction");
        if (interDown && nearObject != null && !isJump && !attacking)
        {
            if (nearObject.tag == "Item")
            {
                audioSourceSlot.clip = itemAcquisition;
                audioSourceSlot.Play();
                inventory.AcquirelItem(nearObject.transform.GetComponent<ItemPickUP>().item);
                Destroy(nearObject);
                AdditemUI.SetActive(false);
            }
            else if (nearObject.tag == "Coin")
            {
                audioSourceSlot.clip = moneyAcquisition;
                audioSourceSlot.Play();
                Destroy(nearObject);
                PlayerManager.instance.nowPlayer.coin += nearObject.GetComponent<Coin>().coins;
                AdditemUI.SetActive(false);
            }
            else if(nearObject.tag == "WeaponStore")
            {
                audioSourceSlot.clip = openStore;
                audioSourceSlot.Play();
                Cursor.lockState = CursorLockMode.None;
                animator.SetBool("IsRun", false);
                animator.SetBool("IsMove", false);
                WeaponStore.SetActive(true);
            }
            else if(nearObject.tag == "PotionStore")
            {
                audioSourceSlot.clip = openStore;
                audioSourceSlot.Play();
                Cursor.lockState = CursorLockMode.None;
                animator.SetBool("IsRun", false);
                animator.SetBool("IsMove", false);
                itemStore.SetActive(true);
            }
            else if(nearObject.tag == "QuestStore")
            {
                audioSourceSlot.clip = openStore;
                audioSourceSlot.Play();
                Cursor.lockState = CursorLockMode.None;
                animator.SetBool("IsRun", false);
                animator.SetBool("IsMove", false);
                guildManager.SetActive(true);
            }
        }
    }
    void QuickSlotUse()
    {
        num1 = Input.GetButtonDown("QuickSlot1");
        num2 = Input.GetButtonDown("QuickSlot2");
        num3 = Input.GetButtonDown("QuickSlot3");
        num4 = Input.GetButtonDown("QuickSlot4");
        num5 = Input.GetButtonDown("QuickSlot5");

        if (num1)
        {
            int num = 0;
            SlotUse(quickSlot[num].item, num);
        }
        else if (num2)
        {
            int num = 1;
            SlotUse(quickSlot[num].item, num);
        }
        else if (num3)
        {
            int num = 2;
            SlotUse(quickSlot[num].item, num);
        }
        else if (num4)
        {
            int num = 3;
            SlotUse(quickSlot[num].item, num);
        }
        else if (num5)
        {
            int num = 4;
            SlotUse(quickSlot[num].item, num);
        }
    }
    void SlotUse(Item item, int num)
    {
        if (item == null)
        {
            return;
        }

        if (item.type == Item.Type.potion)
        {
            if (item.itemName == "마나포션")
            {
                audioSourceSlot.clip = potion;
                audioSourceSlot.Play();
                PlayerManager.instance.nowPlayer.curMP += 3;
                if (PlayerManager.instance.nowPlayer.curMP > PlayerManager.instance.nowPlayer.maxMP)
                {
                    PlayerManager.instance.nowPlayer.curMP = PlayerManager.instance.nowPlayer.maxMP;
                }
            }
            else if (item.itemName == "체력포션")
            {
                audioSourceSlot.clip = potion;
                audioSourceSlot.Play();
                PlayerManager.instance.nowPlayer.curHP += (int)(PlayerManager.instance.nowPlayer.maxHP * 0.3);
                if (PlayerManager.instance.nowPlayer.curHP > PlayerManager.instance.nowPlayer.maxHP)
                {
                    PlayerManager.instance.nowPlayer.curHP = PlayerManager.instance.nowPlayer.maxHP;
                }
            }
            quickSlot[num].SetSlotCount(-1);
        }
        else if(item.type == Item.Type.skill)
        {
            SkillOn(item.itemName);
        }
    }
    void VisibleCursor()
    {
        visivleCursor = Input.GetButton("VisibleCursor");
        if (visivleCursor)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetButtonUp("VisibleCursor"))
        {
            if (!OptionActive.activeSelf && !inventoryActive.activeSelf && !EequipmentActive.activeSelf && !skillActive.activeSelf && !WeaponStore.activeSelf && !itemStore.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    //점프 확인
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && isJump)
        {
            audioSource.clip = landing;
            audioSource.Play();
            animator.SetBool("IsJump", false);
            isJump = false;
            lendingTime = true;
            StartCoroutine("LendingTime");
        }
    }
    IEnumerator LendingTime()
    {
        yield return new WaitForSeconds(0.3f);
        lendingTime = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Player" && other.tag == "EnemyAttack" && !isDown)
        {
            if (!isJump)
            {
                characterBody.transform.LookAt(other.transform.parent);
            }
            if (hit)
            {
                hit = false;
                StartCoroutine("HitTime");
                animator.SetTrigger("OnHit");
            }
        }
        else if (gameObject.tag == "ShiledPlayer" && other.tag == "EnemyAttack" && !isDown)
        {
            characterBody.transform.LookAt(other.transform.parent);
            animator.SetTrigger("DefendOnHit");
            Instantiate(guard, sheildPos);
        }
        else if (gameObject.tag == "PlayerCounter" && other.tag == "EnemyAttack" && !isDown)
        {
            characterBody.transform.LookAt(other.transform.parent);
            Instantiate(counter, sheildPos);
        }
        else if (other.tag == "BossAttack" && !isDown)
        {
            animator.SetTrigger("StrongHit");
            back = transform.position - other.transform.position;
            isDown = true;
            hit = false;
        }
    }
    
    public void LevelUPEvent()
    {
        audioSource.clip = levelUpSound;
        audioSource.Play();
        levelUpEffect.transform.position = transform.position;
        levelUpEffect.SetActive(true);
        StartCoroutine("LevelUplifeTime");
    }
    IEnumerator LevelUplifeTime()
    {
        yield return new WaitForSeconds(1f);
        levelUpEffect.SetActive(false);
    }
    public void Die()
    {
        if (PlayerManager.instance.nowPlayer.curHP <= 0 && !die)
        {
            audioSource.clip = dieSound;
            audioSource.Play();
            die = true;
            hit = false;
            isDown = true;
            gameObject.layer = 20;
            weapon.enTarget = null;
            weapon.target = null;
            weapon.wildTarget = null;
            weapon.boss = null;
            weapon.LockOff();
            animator.SetTrigger("Die");
            animator.SetBool("DieCont", true);
            
            StartCoroutine("Resurrection");
        }
    }
    IEnumerator Resurrection()
    {
        dieUI.SetActive(true);
        //fade
        float ia = 0;
        image.color = new Color(0, 0, 0, 0);
        dieText.color = new Color(255, 0, 0, 0);
        for (int i = 0; i <= 100; i++)
        {
            image.color = new Color(0, 0, 0, ia);
            ia += 0.01f;
            yield return new WaitForSeconds(0.02f);
        }
        float ta = 0;
        for (int i = 0; i < 10; i++)
        {
            dieText.color = new Color(255, 0, 0, ta);
            ta += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.layer = 3;
        yield return new WaitForSeconds(1f);
        dieUI.SetActive(false);
        SoundManager.instance.bgmAudio.clip = vilige;

        Loading.LoadScene("MainWorld",gameObject, new Vector3(-1, 7f, 415.3f));
        //로딩
        
        PlayerManager.instance.nowPlayer.curHP = 50;

        yield return new WaitForSeconds(3f);
        resurrection = true;
    }
    IEnumerator DieGetUp()
    {
        animator.SetBool("DieCont", false);
        yield return new WaitForSeconds(1.2f);
        die = false;
        resurrection = false;
    }
    void StandUp()
    {
        if (die && resurrection)
        {
            StartCoroutine("DieGetUp");
        }
        if (isDown)
        {
            if (down)
            {
                down = false;
                back = back.normalized;
                back += Vector3.up;
                rigid.AddForce(back * 20, ForceMode.Impulse);
                StartCoroutine("OnDown");
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine("GetUP");
            }
        }
    }
    IEnumerator OnDown()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine("GetUP");
    }
    IEnumerator GetUP()
    {
        animator.SetTrigger("StandUp");
        yield return new WaitForSeconds(1.2f);
        isDown = false;
        hit = true;
        down = true;
        StopCoroutine("OnDown");
    }
    IEnumerator HitTime()
    {
        yield return new WaitForSeconds(0.2f);
        hit = true;
    }
    //아이템 흭득
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item")
        {
            nearObject = other.gameObject;
            AdditemUI.SetActive(true);
            notice.text = nearObject.transform.GetComponent<ItemPickUP>().item.itemName + "을(를) " + "<color=yellow>" + "(E)" + "</color>" + "키를 눌러 획득";
        }
        else if(other.tag == "Coin")
        {
            nearObject = other.gameObject;
            AdditemUI.SetActive(true);
            notice.text = "돈을 " + "<color=yellow>" + "(E)" + "</color>" + "키를 눌러 획득";
        }
        else if(other.tag == "WeaponStore"|| other.tag == "PotionStore"||other.tag=="NPC"|| other.tag == "QuestStore")
        {
            nearObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AdditemUI.SetActive(false);
        nearObject = null;
    }
    void SkillOn(string skillname)
    {
        if (skillname == "파이어 볼" && fireballCoolTime)
        {
            if(PlayerManager.instance.nowPlayer.curMP<3)
            {
                return;
            }
            PlayerManager.instance.nowPlayer.curMP -= 3;
            StartCoroutine("FireBallSkill");
            StartCoroutine("FireBallCoolTime");
            for (int i = 0; i < quickSlot.Length; i++)
            {
                if (quickSlot[i].item != null && quickSlot[i].item.itemName == skillname)
                {
                    quickSlot[i].isCooltime = true;
                    quickSlot[i].CoolTime(8f);
                    coolTimeImage[i].LookCoolTime(8f);
                }
            }
        }
        else if (skillname == "돌진" && rushCoolTime)
        {
            if (PlayerManager.instance.nowPlayer.curMP < 2)
            {
                return;
            }
            PlayerManager.instance.nowPlayer.curMP -= 2;
            StartCoroutine("RushSkill");
            StartCoroutine("RushCoolTime");
            for (int i = 0; i < quickSlot.Length; i++)
            {
                if (quickSlot[i].item != null && quickSlot[i].item.itemName == skillname)
                {
                    quickSlot[i].isCooltime = true;
                    quickSlot[i].CoolTime(5f);
                    coolTimeImage[i].LookCoolTime(5f);
                }
            }
        }
        else if (skillname == "일섬" && slashCoolTime)
        {
            if (PlayerManager.instance.nowPlayer.curMP < 4)
            {
                return;
            }
            if (weapon.enTarget == null || Vector3.Distance(transform.position, weapon.enTarget.transform.position) >= 4)
            {
                return;
            }
            PlayerManager.instance.nowPlayer.curMP -= 4;
            StartCoroutine("SlashSkill");
            StartCoroutine("SlashCoolTime");
            for (int i = 0; i < quickSlot.Length; i++)
            {
                if (quickSlot[i].item != null && quickSlot[i].item.itemName == skillname)
                {
                    quickSlot[i].isCooltime = true;
                    quickSlot[i].CoolTime(10f);
                    coolTimeImage[i].LookCoolTime(10f);
                }
            }
            
        }
    }
    //rush
    IEnumerator RushSkill()
    {
        isSkill = true;
        weapon.trailEffect.startColor = Color.yellow;
        animator.SetTrigger("Rush");
        animator.SetBool("RushActive", true);
        animator.SetFloat("Speed", 0.2f);
        yield return new WaitForSeconds(0.5f);
        weapon.trailEffect.enabled = true;
        weapon.area.enabled = true;
        animator.SetFloat("Speed", 1f);
        hit = false;
        yield return new WaitForSeconds(0.1f);
        animator.SetFloat("Speed", 0f);
        float curtime = 0;
        int rayMask = LayerMask.GetMask("Enemy") | LayerMask.GetMask("Map") | LayerMask.GetMask("Boss");
        RaycastHit rayHit;
        audioSourceSlot.clip = rushSound;
        audioSourceSlot.Play();
        // raycast 포착
        if (Physics.Raycast(characterBody.transform.position + Vector3.up * 0.8f, characterBody.transform.forward, out rayHit, 15f, rayMask))
        {
            pos = new Vector3(rayHit.point.x, characterBody.transform.position.y+0.2f, rayHit.point.z) + -characterBody.transform.forward;
            while (curtime < 0.05f)
            {
                curtime += Time.deltaTime;
                characterBody.transform.position = Vector3.Lerp(characterBody.transform.position, pos, curtime / 0.05f);
                yield return null;
            }
        }
        // 아무것도 없을시
        else
        {
            pos = characterBody.transform.forward * 15;
            while (curtime < 0.05f)
            {
                curtime += Time.deltaTime;
                characterBody.transform.localPosition = Vector3.Lerp(characterBody.transform.localPosition, pos, curtime / 0.05f);
                yield return null;
            }
        }
        
        yield return new WaitForSeconds(0.2f);
        pos = characterBody.transform.position;
        transform.position = pos;
        characterBody.transform.localPosition = new Vector3(0, 0, 0); 
        weapon.area.enabled = false;
        weapon.trailEffect.startColor = Color.gray;
        weapon.trailEffect.enabled = false;
        hit = true;
        
        animator.SetFloat("Speed", 1f);
        animator.SetBool("RushActive", false);
        isSkill = false;
    }
    IEnumerator RushCoolTime()
    {
        rushCoolTime = false;
        yield return new WaitForSeconds(5f);
        rushCoolTime = true;
    }

    //fire ball
    IEnumerator FireBallSkill()
    {
        isSkill = true;
        animator.SetTrigger("FireBall");
        yield return new WaitForSeconds(0.1f);
        GameObject Fireball = Instantiate(fireBallOBJ, characterBody.transform.position + transform.up, characterBody.transform.rotation);
        fireBall = Fireball.GetComponent<FireBall>();
        fireBall.weapon = weapon;
        fireBall.player = gameObject;
        int damage = Random.Range(PlayerManager.instance.nowPlayer.maxattack, PlayerManager.instance.nowPlayer.minattack);
        fireBall.damage = (int)(damage * 2 + damage * skill.fireLevel * 0.2f);
        yield return new WaitForSeconds(0.2f);
        isSkill = false;
    }
    IEnumerator FireBallCoolTime()
    {
        fireballCoolTime = false;
        yield return new WaitForSeconds(8f);
        fireballCoolTime = true;
    }

    //surprise attack
    IEnumerator SlashSkill()
    {
        isSkill = true;
        hit = false;
        audioSourceSlot.clip = slashSound;
        audioSourceSlot.Play();
        weapon.trailEffect.startColor = Color.red;
        weapon.trailEffect.enabled = true;
        float curtime = 0;
        pos = -weapon.enTarget.transform.forward * 4;
        animator.SetTrigger("SkillAttack");
        while (curtime < 0.05f)
        {
            curtime += Time.deltaTime;
            characterBody.transform.localPosition = Vector3.Lerp(characterBody.transform.localPosition, pos, curtime / 0.05f);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        pos = characterBody.transform.position;
        transform.position = pos;
        characterBody.transform.localPosition = new Vector3(0, 0, 0);
        weapon.trailEffect.startColor = Color.gray;
        weapon.trailEffect.enabled = false;
        isSkill = false;
        hit = true;
        for (int i = 0; i < 5; i++)
        {
            int damage = Random.Range(PlayerManager.instance.nowPlayer.maxattack, PlayerManager.instance.nowPlayer.minattack);
            slash.damage = (int)(damage * 0.5f + damage * skill.slashLevel * 0.1f);
            skillattackPos.transform.position = weapon.enTarget.transform.position;
            skillAtkArea.enabled = true;
            yield return new WaitForSeconds(0.1f);
            skillAtkArea.enabled = false;
            yield return new WaitForSeconds(0.11f);
        }
    }
    IEnumerator SlashCoolTime()
    {
        slashCoolTime = false;
        yield return new WaitForSeconds(10f);
        slashCoolTime = true;
    }

}
