using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_EnemyManager : MonoBehaviour
{
    private GameObject player;

    public bool isFarAttack = false;
    [SerializeField] private GameObject atk_collider;
    [SerializeField] private GameObject effectPose;
    public float ENEMY_ATK_FAR;
    public float ENEMY_ATK_SPEED_FAR;
    public float ENEMY_ATK_COLLIDER_SPEED_FAR;
    private bool Enemy_FirstShootingDelay = false;
    private bool atk_collider_creating = false;
    private float timer = 0f;

    public float ENEMY_HP;
    public float ENEMY_ATK;
    public float ENEMY_RNG;
    public float ENEMY_SPEED;
    private Rigidbody rb;

    private bool isCollider = false;
    private bool isFlag = false;

    public enum EnemyState
    {
        IDLE,
        RUN,
        ATTACK,
        DEATH
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            isCollider = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player") {
            isCollider = false;
        }
    }

    EnemyState state;
    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.RUN;
        player = GameObject.FindWithTag("Player").transform.gameObject;
        rb = transform.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isCollider) {
            if (isFlag)
            {
                InvokeRepeating("DMG", 0f, 1f);
                isFlag = false;
            }
        }
        else{
            CancelInvoke("DMG");
            isFlag = true;
        }

        switch (state)
        {
            case EnemyState.IDLE:
                break;
            case EnemyState.RUN:
                transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, ENEMY_SPEED * 0.001f);
                LookAtDestination(player.transform.position.x);
                break;
            case EnemyState.ATTACK:
                transform.GetComponentInChildren<Animator>().SetTrigger("Attack");
                break;
            case EnemyState.DEATH:
                isFarAttack = false;
                isCollider = false;
                transform.GetComponentInChildren<Animator>().SetTrigger("Die");
                StartCoroutine(RemoveObj());
                break;
        }

        if (ENEMY_HP <= 0) {
            state = EnemyState.DEATH;
        }

        if (state != EnemyState.DEATH)
        {
            if (isCollider)
            {
                state = EnemyState.ATTACK;
            }
            else
            {
                state = EnemyState.RUN;
            }
        }

        rb.velocity = Vector3.zero;

        if (isFarAttack)
        {
            Enemy_Far_Attak();

            if (Enemy_FirstShootingDelay)
            {
                timer += Time.deltaTime;
            }

            if (timer > ENEMY_ATK_SPEED_FAR)
            {
                Enemy_FirstShootingDelay = false;
                timer = 0;
            }
        }
    }

    private void DMG() {
        GameObject.FindWithTag("PlayerManager").GetComponent<Unit_Player_Manager>().hp -= ENEMY_ATK;
        GameObject.FindWithTag("PlayerManager").GetComponent<Unit_Player_Manager>().hp_Gage.localScale =
            new Vector3(GameObject.FindWithTag("PlayerManager").GetComponent<Unit_Player_Manager>().hp / GameObject.FindWithTag("PlayerManager").GetComponent<Unit_Player_Manager>().full_hp, 1, 1);
    }

    private void LookAtDestination(float destinationX)
    {
        if (transform.position.x < destinationX)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }

    private void Enemy_Far_Attak()
    {
        if (!Enemy_FirstShootingDelay)
        {
            StartCoroutine("CreateCollider");
            Enemy_FirstShootingDelay = true;
        }
    }

    IEnumerator CreateCollider()
    {
        var heading = GameObject.FindWithTag("PlayerManager").GetComponent<Unit_Player_Manager>().player.transform.position - transform.position;
        var dist = heading.magnitude;
        var direction = heading / dist;

        GameObject ins = Instantiate(atk_collider, effectPose.transform.position, new Quaternion(0, 0, 0, 0));
        ins.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        ins.GetComponent<ProjectileMover>().DMG = ENEMY_ATK_FAR;
        ins.GetComponent<ProjectileMover>().speed = ENEMY_ATK_COLLIDER_SPEED_FAR;
        yield return new WaitForSeconds(ENEMY_ATK_SPEED_FAR);
    }


    IEnumerator RemoveObj() {
        transform.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
