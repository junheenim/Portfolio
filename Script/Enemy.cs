using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public DamageSign dsText;
    //����Ʈ
    public ParticleSystem onHit;
    public ParticleSystem stunHit;
    public ParticleSystem slash;
    //���� �Ӹ� ��
    public GameObject stun;

    public GameObject dieobj;
    public Rigidbody rigid;
    public GameObject enemy;
    public new string name = "������";
    public int maxHP = 100;
    public int curHP;
    public int exp = 11;
    // ������ġ�� ������ġ�� �Ÿ� ����
    public float targetDis = 20f;
    // ������ġ �̵���ǥ ���� ����
    public float maxDis = 10f;
    public float minDis = -10f;
    // �������� ����
    public int attack;
    public bool strongAtk = false;

    #region ���� �ð� ����
    // �ڿ� S ������ ������ ����
    // ���� ���� �� ���ð�
    public float beforeAtkDelay = 1f;
    public float beforeAtkDelayS = 1f;
    // ���� ����
    public float startAtkTime = 0.2f;
    public float startAtkTimeS = 0.4f;
    // �ݶ��̴� �����ð�
    public float attackingTime = 0.2f;
    public float attackingTimeS = 0.3f;
    // ���� �� ������
    public float attackingDelayTime = 0.2f;
    public float attackingDelayTimeS = 0.2f;
    // ���� �� ���ð�
    public float afterAtkDelay = 1f;
    public float afterAtkDelayS = 1f;
    #endregion
    // ������ǥ�� ��ǥ ����
    public float DamageY = 1f;
    // ũ��Ƽ�� ǥ��
    bool isCritical = false;

    Vector3 thisPos;
    bool moveAround = true;
    public Animator anim;
    public NavMeshAgent nav;
    public GameObject target;

    // Ÿ�� ���������
    bool hitOn = true;
    public bool isDie = false;
    public Collider attackArea;
    bool isAttack = false;
    bool hitAction = true;
    public bool groggy = false;
    public bool groggyHit = false;
    public bool groggyOut = false;
    public DetectPlayer detect;

    // ��� ������
    public GameObject money;

    //����Ʈ
    public bool kill;
    public bool subKill;

    //����
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
            // ��ȭ���� �̵�
            if (detect.target == null && target == null && nav.remainingDistance < nav.stoppingDistance)
            {
                nav.speed = 2.0f;
                detect.detect.enabled = true;
                anim.SetBool("MoveWalk", false);
            }
            // ��ȭ���� ���� ���ƴٴ�
            if (detect.target == null && target == null && moveAround)
            {
                moveAround = false;

                StartCoroutine("MoveAround");
                nav.isStopped = false;
                
            }
            // ��ǥ ���� ����
            if (target != null)
            {
                nav.speed = 3.0f;
                nav.SetDestination(target.transform.position);
            }
            // ��ǥ�� �Ÿ� �־��� or ������ġ���� �ʹ� �־���
            if (target != null && Vector3.Distance(target.transform.position, transform.position) >= 20f || Vector3.Distance(thisPos, transform.position) >= 20)
            {
                RetrunToCurPos();
            }
            // ���뼼�� ��ó�� ������ ���� �ƴ� 
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
    // ��ȭ���� ���� ���ƴٴϱ�
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

    // �����ɽ�Ʈ ����
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
            dsText.DamageOn(hitDamage, gameObject.transform.position + new Vector3(0, DamageY, 0), isCritical); //������ ǥ��
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
                dsText.DamageOn(hitDamage, gameObject.transform.position + new Vector3(0, DamageY, 0), isCritical); //������ ǥ��
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
                Instantiate(onHit, transform.position + Vector3.forward * 0.1f, transform.rotation); //����Ʈ ����
                curHP -= hitDamage;
                dsText.DamageOn(hitDamage, gameObject.transform.position + new Vector3(0, DamageY, 0), isCritical); //������ ǥ��
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

    //�⺻ ����
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
    // ���� ����
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

        if (name == "������")
            money.GetComponent<Coin>().coins = Random.Range(10, 20);
        else if (name == "�Ʊ� �սǰŹ�")
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
