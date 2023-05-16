using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    Vector3 curPos;
    bool victory;
    public bool inbattle;
    // 데미지 표시
    public DamageSign dsText;
    // 데미지 좌표 조정
    public float DamageY = 1f;
    // 이펙트
    public ParticleSystem onHitEf;
    public ParticleSystem stunHitEf;
    public ParticleSystem slash;
    // 공격 이펙트
    public GameObject farAtkEf;
    public Transform farAtkEfPos;

    Rigidbody rb;
    NavMeshAgent nav;
    public GameObject player;
    public Animator anim;

    public new string name = "숲의 주인";
    public int maxHP = 1000;
    public int curHP = 1000;
    public int stunCount = 0;
    int exp = 30;
    
    public bool attacking;

    //스턴패턴
    public StunCount stun_Count;
    public GameObject stunStar;
    public bool stunPettern = true;
    public bool isStun;
    public bool groggyHit;

    //쥬금
    public bool isDie;
    public GameObject Potal;
    public GameObject wall;
    PlayerManager pm;
    // 공격
    public Collider NormalAtk1;
    public Collider NormalAtk2;

    public Collider farAtk;
    public float farAtkCollTime = 0f;
    public bool farAtkReady = true;
    Vector3 lockPos;
    public bool lockOn;
    public GameObject atkRock;

    // 특수패턴
    public Collider specialAtk1;
    public Collider specialAtk2;
    public bool specialAtkOn = false;
    public bool tracking = false;
    public Transform RockPos;
    public GameObject specialRock;
    public StrongAttack strongAtk;
    int specialCount1 = 0;
    int specialCount2 = 0;

    //피격
    Vector3 pos;
    int hpHit = 0;
    bool isCritical = false;
    bool hitOn = true;

    //퀘스트 용
    public bool kill;

    //드랍 아이템
    public GameObject money1;
    public GameObject money2;
    public GameObject[] item;

    public AudioSource bossSound;
    public AudioSource otherSound;
    public AudioClip hit;
    public AudioClip boom;
    public AudioClip attackSound1;
    public AudioClip attackSound2;
    public AudioClip attackSound3;
    public AudioClip attackSound4;
    public AudioClip specialAttackSound;
    public AudioClip trackingSound;
    public AudioClip stiffnessSound;
    public AudioClip dieSound;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        curPos = transform.position;
        curHP = maxHP;
        atkRock.SetActive(false);
        anim.SetTrigger("Sleep");
    }
    private void Update()
    {
        if (player == null && !inbattle && victory)
        {
            if(!attacking)
            {
                nav.enabled = true;
                anim.SetBool("Victory", true);
                nav.SetDestination(curPos);
                nav.speed = 3;
                if (Vector3.Distance(transform.position, curPos) <= 1)
                {
                    nav.speed = 0;
                    anim.SetTrigger("Sleep");
                }
            }
        }
        if (player != null && inbattle)
        {
            // 적 바라보기
            if (!attacking && !isStun && !specialAtkOn && !isDie)
            {
                Vector3 dir = player.transform.position - transform.position;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 20);
            }
            //원거리 적 추적
            if (lockOn && !isDie)
            {
                lockPos = player.transform.position;
            }
            if (nav.enabled && !isStun && !specialAtkOn && !isDie)
            {
                nav.SetDestination(player.transform.position);
                if (nav.stoppingDistance < nav.remainingDistance && !attacking)
                {
                    anim.SetBool("Move", true);

                    if (Vector3.Distance(player.transform.position, transform.position) >= 10f && farAtkReady)
                    {
                        anim.SetBool("Move", false);
                        StartCoroutine("FarAtk");
                    }
                }
            }

            if (hpHit >= 100)
            {
                if (!isStun && !specialAtkOn)
                {
                    StopAllCoroutines();
                    StartCoroutine("Stiffness");
                }
            }

            if (stunCount >= 5 && stunPettern && !isDie)
            {
                OnStun();
            }
            if (specialCount1 == 1)
            {
                if (!specialAtkOn && !isStun && !attacking && !isDie)
                {
                    specialCount1 = 2;
                    StopAllCoroutines();
                    StartCoroutine("SpecialAttack");
                }
            }
            if (specialCount2 == 1)
            {
                if (!specialAtkOn && !isStun && !attacking && !isDie)
                {
                    specialCount2 = 2;
                    StopAllCoroutines();
                    StartCoroutine("SpecialAttack");
                }
            }

            if (specialAtkOn && tracking && !isDie)
            {
                anim.SetBool("AttackRun", true);
                nav.SetDestination(player.transform.position);
                Vector3 dir = player.transform.position - transform.position;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 20);
            }
        }
        if (PlayerManager.instance.nowPlayer.curHP <= 0 && !victory)
        {
            PlayerManager.instance.nowPlayer.curHP = 0;
            player.GetComponent<PlayerInpt>().Die();
            victory = true;
            player = null;
            nav.enabled = true;
            inbattle = false;
            anim.SetFloat("AttackSpeed", 1f);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if(!victory)
        {
            GetTargeting();
        }
    }
    void GetTargeting()
    {
        RaycastHit[] rayHits
            = Physics.SphereCastAll(transform.position,
            0.7f, transform.forward, 2.5f, LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !attacking && !isStun && !isDie && !specialAtkOn)
        {
            nav.enabled = false;
            anim.SetBool("Move", false);
            int atkNum = Random.Range(0, 10);
            switch (atkNum)
            {
                case 5:
                case 6:
                case 7:
                    StartCoroutine("Attack2");
                    break;

                case 8:
                case 9:
                    StartCoroutine("Attack3");
                    break;

                case 10:
                    StartCoroutine("FarAtk");
                    break;

                default:
                    StartCoroutine("Attack1");
                    break;
            }
        }
        else if (!attacking && rayHits.Length <= 0 && !isStun && !isDie && !specialAtkOn)
        {
            nav.enabled = true;
        }
    }
    // 근접공격 1
    IEnumerator Attack1()
    {
        attacking = true;
        nav.enabled = false; 
        NormalAtk1.tag = "EnemyAttack";
        bossSound.clip = attackSound1;
        bossSound.Play();
        anim.SetFloat("AttackSpeed", 0.5f); 
        anim.SetTrigger("Attack1");
        yield return new WaitForSeconds(0.5f);
        anim.SetFloat("AttackSpeed", 2f);
        NormalAtk1.enabled = true;
        yield return new WaitForSeconds(0.3f);
        NormalAtk1.enabled = false;
        yield return new WaitForSeconds(1f);
        attacking = false;
    }
    // 근접공격 2
    IEnumerator Attack2()
    {
        attacking = true;
        nav.enabled = false;
        anim.SetFloat("AttackSpeed", 0.3f);
        anim.SetTrigger("Attack2");
        bossSound.clip = attackSound2;
        bossSound.Play();
        yield return new WaitForSeconds(0.5f);
        anim.SetFloat("AttackSpeed", 1f);
        NormalAtk1.enabled = true;
        yield return new WaitForSeconds(0.3f);
        NormalAtk1.enabled = false;
        yield return new WaitForSeconds(0.8f);
        NormalAtk1.enabled = true;
        yield return new WaitForSeconds(0.3f);
        NormalAtk1.enabled = false;
        yield return new WaitForSeconds(1f);
        attacking = false;
    }
    // 근접공격 3
    IEnumerator Attack3()
    {
        attacking = true;
        nav.enabled = false;
        anim.SetFloat("AttackSpeed", 0.1f);
        anim.SetTrigger("Attack3");
        bossSound.clip = attackSound3;
        bossSound.Play();
        yield return new WaitForSeconds(1.5f);
        anim.SetFloat("AttackSpeed", 2f);
        NormalAtk2.enabled = true;
        yield return new WaitForSeconds(0.3f);
        NormalAtk2.enabled = false;
        yield return new WaitForSeconds(1f);
        attacking = false;
    }
    //원거리 공격
    IEnumerator FarAtk()
    {
        lockOn = true;
        attacking = true;
        farAtkReady = false;
        nav.enabled = false;
        bossSound.clip = attackSound4;
        bossSound.Play();
        anim.SetFloat("AttackSpeed", 0.5f);
        anim.SetTrigger("FarAttack");
        yield return new WaitForSeconds(1.2f);
        anim.SetFloat("AttackSpeed", 0.3f);
        yield return new WaitForSeconds(0.5f);
        anim.SetFloat("AttackSpeed", 1.5f);
        lockOn = false;
        yield return new WaitForSeconds(0.6f);
        farAtk.enabled = true;
        atkRock.transform.position = new Vector3(lockPos.x, -28f, lockPos.z);
        bossSound.clip = boom;
        bossSound.Play();
        Instantiate(farAtkEf, farAtkEfPos.position, transform.rotation);
        atkRock.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        farAtk.enabled = false;
        yield return new WaitForSeconds(1f);
        attacking = false;
        nav.speed = 13f;
        yield return new WaitForSeconds(5f);
        nav.speed = 6f;
        farAtkReady = true;
    }
    // 특수 패턴
    IEnumerator SpecialAttack()
    {
        hitOn = true;
        float ranx = Random.Range(-10, 10);
        float ranz = Random.Range(-10, 10);
        strongAtk.up = true;
        specialRock.transform.position = new Vector3(lockPos.x + ranx, -28f, lockPos.z + ranz);
        specialAtkOn = true;
        nav.enabled = false;
        attacking = true;
        anim.SetTrigger("special pattern");
        bossSound.clip = specialAttackSound;
        bossSound.Play();
        yield return new WaitForSeconds(2.4f);

        bossSound.clip = boom;
        bossSound.Play();
        Instantiate(farAtkEf, farAtkEfPos.position, transform.rotation);
        specialRock.SetActive(true);
        specialAtk1.enabled = true;
        yield return new WaitForSeconds(0.1f);
        specialAtk1.enabled = false;
        
        //추적 시작
        yield return new WaitForSeconds(1.5f);
        bossSound.clip = trackingSound;
        bossSound.Play();
        nav.enabled = true;
        tracking = true;
        nav.speed = 10;
        specialAtk2.enabled = true;
        yield return new WaitForSeconds(10f);
        nav.speed = 15;
    }
    //경직
    IEnumerator Stiffness()
    {
        if (!isDie)
        {
            attacking = true;
            hpHit = 0;
            bossSound.clip = stiffnessSound;
            bossSound.Play();
            anim.SetTrigger("OnHit");
            nav.speed = 0;
            yield return new WaitForSeconds(0.5f);
            nav.speed = 6;
            attacking = false;
            hitOn = true;
        }
    }
    //스턴
    public void OnStun()
    {
        StopAllCoroutines();
        StartCoroutine("Stun");
    }
    IEnumerator Stun()
    {
        nav.enabled = false;
        attacking = true;
        stunPettern = false;
        isStun = true;
        groggyHit = true;
        stunStar.SetActive(true);
        anim.SetTrigger("Onstun");
        anim.SetBool("Stun", true);
        yield return new WaitForSeconds(5f);
        stunCount = 0;
        stun_Count.CountReset();
        stunStar.SetActive(false);
        anim.SetBool("Stun", false);
        isStun = false;
        stunPettern = true;
        attacking = false;
        nav.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Slash")
        {
            otherSound.clip = hit;
            otherSound.Play();
            float rx = Random.Range(-3f, 3f);
            float ry = Random.Range(0f, 3f);
            float rz = Random.Range(-3f, 3f);
            Instantiate(slash, transform.position + new Vector3(rx, ry, rz), transform.rotation);
            int cri = Random.Range(0, 9);
            if (cri == 0)
            {
                isCritical = true;
            }
            int hitDamage = other.GetComponent<Slash>().damage;
            if (isCritical)
            {
                hitDamage *= 2;
            }
            dsText.DamageOn(hitDamage, gameObject.transform.position + new Vector3(0, DamageY, 0), isCritical); //데미지 표시
            groggyHit = false;
            curHP -= hitDamage;
            hpHit += hitDamage;
            isCritical = false;
            if (curHP <= 0)
            {
                kill = true;
                gameObject.layer = 7;
                isDie = true;
                PlayerManager.instance.nowPlayer.curEXP += exp;
                PlayerManager.instance.LevelUp();
                StopAllCoroutines();
                curHP = 0;
                StartCoroutine("Die");
            }
            if (curHP <= 700 && specialCount1 == 0)
            {
                specialCount1 = 1;
            }
            if (curHP <= 400 && specialCount2 == 0)
            {
                specialCount2 = 1;
            }
        }
        if (other.tag == "Magic")
        {
            otherSound.clip = boom;
            otherSound.Play();
            FireBall fireball = other.GetComponent<FireBall>();
            if (hitOn)
            {
                groggyHit = false;
                int cri = Random.Range(0, 9);
                if (cri == 0)
                {
                    isCritical = true;
                }
                int hitDamage = fireball.damage;
                if (isCritical)
                {
                    hitDamage *= 2;
                }
                StartCoroutine("HitOn");
                curHP -= hitDamage;
                hpHit += hitDamage;
                dsText.DamageOn(hitDamage, gameObject.transform.position + new Vector3(0, DamageY, 0), isCritical); //데미지 표시
                isCritical = false;
                if (curHP <= 0)
                {
                    kill = true;
                    gameObject.layer = 7;
                    isDie = true;
                    PlayerManager.instance.nowPlayer.curEXP += exp;
                    PlayerManager.instance.LevelUp();
                    StopAllCoroutines();
                    curHP = 0;
                    StartCoroutine("Die");
                }
                if (curHP <= 700 && specialCount1 == 0)
                {
                    specialCount1 = 1;
                }
                if (curHP <= 400 && specialCount2 == 0)
                {
                    specialCount2 = 1;
                }
            }
        }
        if (other.tag == "Melee")
        {
            pos = other.transform.position + new Vector3(0, 1, 0);

            if (hitOn)
            {
                otherSound.clip = hit;
                otherSound.Play();
                groggyHit = false;
                int hitDamage = Damage();
                StartCoroutine("HitOn");
                Instantiate(onHitEf, pos, transform.rotation); //이펙트 생성
                curHP -= hitDamage;
                hpHit += hitDamage;
                dsText.DamageOn(hitDamage, gameObject.transform.position + new Vector3(0, DamageY, 0), isCritical); //데미지 표시
                isCritical = false;
                if (curHP <= 0)
                {
                    kill = true;
                    gameObject.layer = 7;
                    isDie = true;
                    PlayerManager.instance.nowPlayer.curEXP += exp;
                    PlayerManager.instance.LevelUp();
                    StopAllCoroutines();
                    curHP = 0;
                    StartCoroutine("Die");
                }
                if (curHP <= 700 && specialCount1 == 0)
                {
                    specialCount1 = 1;
                }
                if (curHP <= 400 && specialCount2 == 0)
                {
                    specialCount2 = 1;
                }
            }
        }
        else if (other.tag == "StunAttack")
        {
            anim.SetFloat("AttackSpeed", 1f);
            pos = other.transform.position + new Vector3(0, 1, 0);
            if (hitOn)
            {
                otherSound.clip = hit;
                otherSound.Play();
                int hitDamage = Damage();
                StartCoroutine("HitOn");
                groggyHit = false;
                Instantiate(stunHitEf, pos, transform.rotation);
                curHP -= hitDamage * 3;
                hpHit += hitDamage * 3;
                dsText.DamageOn(hitDamage * 3, gameObject.transform.position + new Vector3(0, DamageY, 0), isCritical);
                isCritical = false;
                if (curHP <= 0)
                {
                    kill = true;
                    gameObject.layer = 7;
                    isDie = true;
                    PlayerManager.instance.nowPlayer.curEXP += exp;
                    PlayerManager.instance.LevelUp();
                    StopAllCoroutines();
                    curHP = 0;
                    StartCoroutine("Die");
                }
            }
            if (curHP <= 700 && specialCount1 == 0)
            {
                specialCount1 = 1;
            }
            if (curHP <= 400 && specialCount2 == 0)
            {
                specialCount2 = 1;
            }
        }
    }
    IEnumerator HitOn()
    {
        hitOn = false;
        yield return new WaitForSeconds(0.3f);
        hitOn = true;
    }
    private int Damage()
    {
        int criticalHit = Random.Range(0, 9);
        int damage = Random.Range(PlayerManager.instance.nowPlayer.maxattack, PlayerManager.instance.nowPlayer.minattack);
        if (criticalHit == 0)
        {
            isCritical = true;
            damage *= 2;
        }
        return damage;
    }
    //쥬금
    IEnumerator Die()
    {
        stunStar.SetActive(false);
        bossSound.clip = dieSound;
        bossSound.Play();
        anim.SetTrigger("Die");
        gameObject.layer = 7;
        wall.SetActive(false);
        Potal.SetActive(true);
        money1.transform.position = transform.position + new Vector3(0, 2, 0);
        money2.transform.position = transform.position + new Vector3(0, 2, 0);
        money1 = Instantiate(money1);
        money2 = Instantiate(money2);
        money1.GetComponent<Coin>().coins = Random.Range(100, 200);
        money2.GetComponent<Coin>().coins = Random.Range(100, 200);
        for (int i = 0; i < item.Length; i++)
        {
            item[i].transform.position = transform.position + new Vector3(0, 2, 0);
            item[i] = Instantiate(item[i]);
            RandomPop(item[i]);
        }
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void RandomPop(GameObject item)
    {
        float x, y, z, force;
        Rigidbody rb;
        rb = item.GetComponent<Rigidbody>();
        x = Random.Range(-5, 5);
        y = Random.Range(-5, 5);
        z = Random.Range(-5, 5);
        force = Random.Range(0, 50);
        rb.AddForce(transform.up * 300);
        rb.AddForce(new Vector3(x, y, z) * force);
    }
}
