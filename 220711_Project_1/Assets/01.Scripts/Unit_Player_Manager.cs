using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Player_Manager : MonoBehaviour
{
    [HideInInspector] public bool move = false;
    [HideInInspector] public GameObject player;
    public GameObject[] pet;
    [SerializeField] private GameObject effectPose;

    public float moveSpeed = 5f;
    [SerializeField] private Joystick move_joyStick;
    private Vector2 move_conDir;

    [SerializeField] private Joystick atk_joyStick;
    [HideInInspector] public Vector2 atk_conDir;
    [SerializeField] private GameObject atk_collider;
    private bool atk_collider_creating = false;

    public float atk_speed;
    public float atk_dmg;
    public float hp;
    public float full_hp;
    public Transform hp_Gage;

    public bool unit_FirstShootingDelay = false;
    private float timer = 0f;
    [SerializeField] private GameObject gameover_Canvas;
    [SerializeField] private GameObject enemyCreate_Pose;

    private void FixedUpdate()
    {
        if (hp <= 0)
        {
            hp = 0;
            player.transform.GetComponentInChildren<Animator>().SetTrigger("Die");
            StartCoroutine(GameOver(1.5f));
            return;
        }

        PlayerMove();
        PlayerMove_PC();
        PlayerAttack();

        if (unit_FirstShootingDelay) {
            timer += Time.deltaTime;
        }

        if (timer > atk_speed) {
            unit_FirstShootingDelay = false;
            timer = 0;
        }
    }

    private void PlayerMove_PC()
    {
        //player.transform.position = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            player.transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            player.transform.Translate(move_conDir * Time.fixedDeltaTime * moveSpeed);
            player.transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
            for (int i = 0; i < pet.Length; i++)
            {
                //pet[i].transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            player.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            hp_Gage.transform.parent.eulerAngles = new Vector3(0f, 0f, 0f);

            player.transform.Translate(move_conDir * Time.fixedDeltaTime * moveSpeed);
            player.transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
            for (int i = 0; i < pet.Length; i++)
            {
                //pet[i].transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
            }

            pet[1].transform.localPosition = new Vector3(-0.86f, -0.68f, 0f);
            pet[0].transform.localPosition = new Vector3(0.86f, -0.68f, 0f);

            pet[4].transform.localPosition = new Vector3(-0.958f, 0.34f, 0f);
            pet[3].transform.localPosition = new Vector3(0.958f, 0.34f, 0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.position -= Vector3.up * moveSpeed * Time.deltaTime;


            player.transform.Translate(move_conDir * Time.fixedDeltaTime * moveSpeed);
            player.transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
            for (int i = 0; i < pet.Length; i++)
            {
                //pet[i].transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.position -= Vector3.left * moveSpeed * Time.deltaTime;
            player.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            hp_Gage.transform.parent.eulerAngles = new Vector3(0f, 0f, 0f);

            player.transform.Translate(move_conDir * Time.fixedDeltaTime * moveSpeed);
            player.transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
            for (int i = 0; i < pet.Length; i++)
            {
                //pet[i].transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
            }

            pet[1].transform.localPosition = new Vector3(0.86f, -0.68f, 0f);
            pet[0].transform.localPosition = new Vector3(-0.86f, -0.68f, 0f);

            pet[4].transform.localPosition = new Vector3(0.958f, 0.34f, 0f);
            pet[3].transform.localPosition = new Vector3(-0.958f, 0.34f, 0f);
        }
    }

    private void PlayerMove()
    {
        move_conDir = move_joyStick.Direction;

        if (move_conDir == Vector2.zero)
        {
            player.transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.0f);
            for (int i = 0; i < pet.Length; i++)
            {
                //pet[i].transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.0f);
            }
        }
        else
        {
            if (move_conDir.x >= 0)
            {
                player.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                hp_Gage.transform.parent.eulerAngles = new Vector3(0f, 180f, 0f);
                move_conDir = new Vector2(-move_conDir.x, move_conDir.y);

                pet[1].transform.localPosition = new Vector3(0.86f, -0.68f, 0f);
                pet[0].transform.localPosition = new Vector3(-0.86f, -0.68f, 0f);
                pet[4].transform.localPosition = new Vector3(0.958f, 0.34f, 0f);
                pet[3].transform.localPosition = new Vector3(-0.958f, 0.34f, 0f);

            }
            else
            {
                player.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                hp_Gage.transform.parent.eulerAngles = new Vector3(0f, 0f, 0f);

                pet[1].transform.localPosition = new Vector3(-0.86f, -0.68f, 0f);
                pet[0].transform.localPosition = new Vector3(0.86f, -0.68f, 0f);
                pet[4].transform.localPosition = new Vector3(-0.958f, 0.34f, 0f);
                pet[3].transform.localPosition = new Vector3(0.958f, 0.34f, 0f);
            }

            player.transform.Translate(move_conDir * Time.fixedDeltaTime * moveSpeed);
            player.transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
            for (int i = 0; i < pet.Length; i++)
            {
                //pet[i].transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
            }
        }

        Camera.main.transform.position = new Vector3(player.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        enemyCreate_Pose.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0) - (Vector3.up * 3f);
    }

    private void PlayerAttack()
    {
        if (!unit_FirstShootingDelay)
        {
            atk_conDir = atk_joyStick.Direction;

            if (atk_conDir == Vector2.zero)
            {
                StopCoroutine(CreateCollider());
                atk_collider_creating = false;
            }
            else
            {
                if (!atk_collider_creating)
                {
                    StartCoroutine(CreateCollider());
                }
            }
        }
    }

    IEnumerator CreateCollider()
    {
        atk_collider_creating = true;
        GameObject ins = Instantiate(atk_collider, effectPose.transform.position, new Quaternion(0, 0, 0, 0));
        ins.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(atk_conDir.y, atk_conDir.x) * Mathf.Rad2Deg);
        ins.GetComponent<ProjectileMover>().DMG = atk_dmg;
        yield return new WaitForSeconds(atk_speed);
        atk_collider_creating = false;
    }

    IEnumerator GameOver(float time) {
        yield return new WaitForSeconds(time);
        gameover_Canvas.SetActive(true);
    }
}