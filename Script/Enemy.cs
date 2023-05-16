using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public DamageSign dsText;
    //이펙트
    public ParticleSystem onHit;
    public ParticleSystem stunHit;
    public ParticleSystem slash;
    //스턴 머리 별
    public GameObject stun;

    public GameObject dieobj;
    public Rigidbody rigid;
    public GameObject enemy;
    public new string name = "슬라임";
    public int maxHP = 100;
    public int curHP;
    public int exp = 11;
    // 원래위치와 현재위치의 거리 차이
    public float targetDis = 20f;
    // 랜덤위치 이동좌표 생성 범위
    public float maxDis = 10f;
    public float minDis = -10f;
    // 공격종류 고르기
    public int attack;
    public bool strongAtk = false;

    #region 공격 시간 설정
    // 뒤에 S 붙은건 강력한 공격
    // 공격 시작 전 대기시간
    public float beforeAtkDelay = 1f;
    public float beforeAtkDelayS = 1f;
    // 공격 시작
    public float startAtkTime = 0.2f;
    public float startAtkTimeS = 0.4f;
    // 콜라이더 생성시간
    public float attackingTime = 0.2f;
    public float attackingTimeS = 0.3f;
    // 공격 후 딜레이
    public float attackingDelayTime = 0.2f;
    public float attackingDelayTimeS = 0.2f;
    // 공격 후 대기시간
    public float afterAtkDelay = 1f;
    public float afterAtkDelayS = 1f;
    #endregion
    // 데미지표시 좌표 조정
    public float DamageY = 1f;
    // 크리티컬 표시
    bool isCritical = false;

    Vector3 thisPos;
    bool moveAround = true;
    public Animator anim;
    public NavMeshAgent nav;
    public GameObject target;

    // 타겟 가져오기용
    bool hitOn = true;
    public bool isDie = false;
    public Collider attackArea;
    bool isAttack = false;
    bool hitAction = true;
    public bool groggy = false;
    public bool groggyHit = false;
    public bool groggyOut = false;
    public DetectPlayer detect;

    // 드랍 아이템
    public GameObject money;

    //퀘스트
    public bool kill;
    public bool subKill;

    //사운드
    public AudioSource dieAudio;
    AudioSource audioSource;
    public AudioClip hit;
    public AudioClip boom;
    public AudioClip attackSound;
    public AudioClip dieSound;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        kill = false;
        curHP = maxHP;
        thisPos = transform.position;
    }
    private void Update()
    {
        if (nav.enabled && !groggy)
        {
            // 평화상태 이동
            if (detect.target == null && target == null && nav.remainingDistance < nav.stoppingDistance)
            {
                nav.speed = 2.0f;
                detect.detect.enabled = true;
                anim.SetBool("MoveWalk", false);
            }
            // 평화상태 마구 돌아다님
            if (detect.target == null && target == null && moveAround)
            {
                moveAround = false;

                StartCoroutine("MoveAround");
                nav.isStopped = false;
                
            }
            // 목표 포착 추적
            if (target != null)
            {
                nav.speed = 3.0f;
                nav.SetDestination(target.transform.position);
            }
            // 목표와 거리 멀어짐 or 원래위치에서 너무 멀어짐
            if (target != null && Vector3.Distance(target.transform.position, transform.position) >= 20f || Vector3.Distance(thisPos, transform.position) >= 20)
            {
                RetrunToCurPos();
            }
            // 적대세력 근처에 있을시 전투 아님 
            if (detect.target != null && target == null)
            {
                StopCoroutine("MoveAround");
                nav.SetDestination(transform.position);
                anim.SetBool("MoveWalk", false);
                nav.isStopped = true;
                moveAround = true;
            }
        }
    }
    // 평화상태 마구 돌아다니기
    IEnumerator MoveAround()
    {
        float second = Random.Range(10, 20);
        yield return new WaitForSeconds(1f);
        anim.SetBool("MoveWalk", true);
        float ranX = Random.Range(minDis, maxDis);
        float ranZ = Random.Range(minDis, maxDis);
        nav.SetDestination(new Vector3(thisPos.x + ranX, thisPos.y, thisPos.z + ranZ));
        yield return new WaitForSeconds(second);
        moveAround = true;
    }

    // 레이케스트 공격
    private void FixedUpdate()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        if (target != null && !groggy && !isDie)
        {
            GetTargeting();
        }
    }
    void GetTargeting()
    {
        RaycastHit[] rayHits
            = Physics.SphereCastAll(transform.position,
            0.8f, transform.forward, 0.6f, LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !groggy && !isDie)
        {
            nav.isStopped = true;
            anim.SetBool("MoveRun", false);
        }
        if (rayHits.Length > 0 && !isAttack && !groggy && !isDie)
        {
            isAttack = true;
            attack = Random.Range(0, 3);
            if (attack == 0)
            {
                StartCoroutine("OnAttack2");
            }
            else
            {
                StartCoroutine("OnAttack1");
            }
        }
        if (rayHits.Length == 0 && !isDie && !groggy)
        {
            nav.isStopped = false;
            anim.SetBool("MoveRun", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {   
        if (other.tag == "Slash")
        {
            audioSource.clip = hit;
            audioSource.Play();
            float rx = Random.Range(-1f, 1f);
            float ry = Random.Range(0f, 1.5f);
            float rz = Random.Range(-1f, 1f);
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
            isCritical = false;
            if (hitAction && !groggy)
            {
                anim.SetTrigger("GetHit");
            }
            if (curHP <= 0)
            {
                gameObject.layer = 7;
                isDie = true;
                PlayerManager.instance.nowPlayer.curEXP += exp;
                PlayerManager.instance.LevelUp();
                StopAllCoroutines();
                curHP = 0;
                StartCoroutine("Die");
            }
        }
        if (other.tag == "Magic")
        {
            audioSource.clip = boom;
            audioSource.Play();
            FireBall fireball = other.GetComponent<FireBall>();
            StopCoroutine("MoveAround");
            if (!groggy)
            {
                enemy.transform.LookAt(fireball.player.transform.root);
            }
            groggyHit = false;
            nav.speed = 3f;
            if (hitOn)
            {
                int cri = Random.Range(0, 9);
                if(cri==0)
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
                dsText.DamageOn(hitDamage, gameObject.transform.position + new Vector3(0, DamageY, 0), isCritical); //데미지 표시
                isCritical = false;
                if (curHP <= 0)
                {
                    gameObject.layer = 7;
                    isDie = true;
                    PlayerManager.instance.nowPlayer.curEXP += exp;
                    PlayerManager.instance.LevelUp();
                    StopAllCoroutines();
                    curHP = 0;
                    StartCoroutine("Die");
                }
                else
                {
                    if (hitAction && !groggy)
                    {
                        anim.SetTrigger("GetHit");
                    }
                    target = fireball.player;
                    anim.SetBool("InBattle", true);
                    StartCoroutine("HitOn");
                }
            }
        }
        if (other.tag == "Melee")
        {
            StopCoroutine("MoveAround");
            if (!groggy)
            {
                enemy.transform.LookAt(other.transform.root);
            }
            groggyHit = false;
            nav.speed = 3f;
            if (hitOn)
            {
                audioSource.clip = hit;
                audioSource.Play();
                int hitDamage = Damage();
                StartCoroutine("HitOn");
                Instantiate(onHit, transform.position + Vector3.forward * 0.1f, transform.rotation); //이펙트 생성
                curHP -= hitDamage;
                dsText.DamageOn(hitDamage, gameObject.transform.position + new Vector3(0, DamageY, 0), isCritical); //데미지 표시
                isCritical = false;
                if (curHP <= 0)
                {
                    gameObject.layer = 7;
                    isDie = true;
                    PlayerManager.instance.nowPlayer.curEXP += exp;
                    PlayerManager.instance.LevelUp();
                    StopAllCoroutines();
                    curHP = 0;
                    StartCoroutine("Die");
                }
                else
                {
                    if (hitAction && !groggy)
                    {
                        anim.SetTrigger("GetHit");
                    }
                    target = other.gameObject.transform.root.gameObject;
                    anim.SetBool("InBattle", true);
                    StartCoroutine("HitOn");
                }
            }
        }
        else if (other.tag == "StunAttack")
        {
            if (hitOn)
            {
                audioSource.clip = hit;
                audioSource.Play();
                int hitDamage = Damage();
                StartCoroutine("HitOn");
                if (!groggy)
                {
                    enemy.transform.LookAt(other.transform.root);
                }
                gameObject.transform.LookAt(other.gameObject.transform.parent);
                groggyHit = false;
                Instantiate(stunHit, transform.position + Vector3.up, transform.rotation);
                curHP -= hitDamage * 3;
                dsText.DamageOn(hitDamage * 3, gameObject.transform.position + new Vector3(0, DamageY, 0), isCritical);
                isCritical = false;
                if (curHP <= 0)
                {
                    gameObject.layer = 7;
                    isDie = true;
                    PlayerManager.instance.nowPlayer.curEXP += exp;
                    PlayerManager.instance.LevelUp();
                    StopAllCoroutines();
                    curHP = 0;
                    StartCoroutine("Die");
                }
                target = other.gameObject.transform.root.gameObject;
                anim.SetBool("InBattle", true);
                StartCoroutine("HitOn");
            }
        }
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
    public void RetrunToCurPos()
    {
        target = null;
        nav.speed = 10.0f;
        nav.enabled = true;
        nav.isStopped = false;
        detect.detect.enabled = false;
        detect.target = null;
        curHP = maxHP;
        nav.SetDestination(thisPos);
        anim.SetBool("MoveRun", false);
        anim.SetBool("InBattle", false);
        anim.SetBool("MoveWalk", true);
        isAttack = false;
    }

    //기본 공격
    IEnumerator OnAttack1()
    {
        yield return new WaitForSeconds(beforeAtkDelay);
        hitAction = false;
        strongAtk = false;
        if(target!=null)
        {
            enemy.transform.LookAt(target.transform.root);
        }
        nav.isStopped = true;
        anim.SetTrigger("Attack1");
        audioSource.clip = attackSound;
        audioSource.Play();
        yield return new WaitForSeconds(startAtkTime);
        attackArea.enabled = true;
        yield return new WaitForSeconds(attackingTime);
        attackArea.enabled = false;
        yield return new WaitForSeconds(attackingDelayTime);
        if (nav.enabled)
        {
            nav.isStopped = false;
        }
        hitAction = true;
        yield return new WaitForSeconds(afterAtkDelay);
        if (!isDie)
            isAttack = false;
    }
    // 강한 공격
    IEnumerator OnAttack2()
    {
        yield return new WaitForSeconds(beforeAtkDelayS);
        hitAction = false;
        strongAtk = true;
        if (target != null)
        {
            enemy.transform.LookAt(target.transform.root);
        }
        nav.isStopped = true;
        audioSource.clip = attackSound;
        audioSource.Play();
        anim.SetTrigger("Attack2");
        yield return new WaitForSeconds(startAtkTimeS);
        attackArea.enabled = true;
        yield return new WaitForSeconds(attackingTimeS);
        attackArea.enabled = false;
        yield return new WaitForSeconds(attackingDelayTimeS);
        if (nav.enabled)
        {
            nav.isStopped = false;
        }
        hitAction = true;
        yield return new WaitForSeconds(afterAtkDelayS);
        if (!isDie)
            isAttack = false;
    }
    IEnumerator HitOn()
    {
        hitOn = false;
        nav.speed = 0;
        yield return new WaitForSeconds(0.2f);
        hitOn = true;
        nav.speed = 3;                
    }
    IEnumerator Die()
    {
        kill = true;
        subKill = true;
        dieobj.SetActive(false);
        money = Instantiate(money);
        money.transform.position = transform.position + new Vector3(0, 1, 0);

        if (name == "슬라임")
            money.GetComponent<Coin>().coins = Random.Range(10, 20);
        else if (name == "아기 둥실거미")
            money.GetComponent<Coin>().coins = Random.Range(40, 60);

        stun.SetActive(false);
        nav.enabled = false;
        isAttack = true;
        target = null;
        detect.detect.enabled = false;
        detect.target = null;
        dieAudio.Play();
        anim.SetTrigger("Die");
        enemy.layer = 7;
        yield return new WaitForSeconds(3.0f);
        Destroy(enemy);
    }
}
