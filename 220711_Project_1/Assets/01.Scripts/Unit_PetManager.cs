using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_PetManager : MonoBehaviour
{
    [SerializeField] private Unit_Player_Manager unit_playerManager;
    private bool atk_collider_creating = false;
    [SerializeField] private GameObject effectPose;

    [SerializeField] private GameObject[] atk_collider;
    private GameObject atk_collider_ins;
    [SerializeField] private float atk_speed;
    [SerializeField] private float atk_collider_speed;
    [SerializeField] private float atk_dmg;

    [HideInInspector] public bool pet_FirstShootingDelay = false;
    private float timer = 0f;

    [HideInInspector]public int currentCardMade = 0;

    // Update is called once per frame
    void Update()
    {
        Pet_Attak();

        if (pet_FirstShootingDelay)
        {
            timer += Time.deltaTime;
        }

        if (timer > atk_speed)
        {
            pet_FirstShootingDelay = false;
            timer = 0;
        }
    }

    private void Pet_Attak() {
        if (!pet_FirstShootingDelay)
        {
            if (unit_playerManager.atk_conDir == Vector2.zero)
            {
                StopCoroutine("CreateCollider");
                atk_collider_creating = false;
            }
            else
            {
                if (!atk_collider_creating)
                {
                    StartCoroutine("CreateCollider");
                }
            }
        }
    }

    IEnumerator CreateCollider()
    {
        atk_collider_creating = true;

        switch (transform.name.Split('_')[0]) {
            case "0":
                atk_collider_ins = Instantiate(atk_collider[0], effectPose.transform.position, new Quaternion(0, 0, 0, 0));
                break;
            case "1":
                atk_collider_ins = Instantiate(atk_collider[1], effectPose.transform.position, new Quaternion(0, 0, 0, 0));
                break;
            case "2":
                atk_collider_ins = Instantiate(atk_collider[2], effectPose.transform.position, new Quaternion(0, 0, 0, 0));
                break;
            case "3":
                atk_collider_ins = Instantiate(atk_collider[3], effectPose.transform.position, new Quaternion(0, 0, 0, 0));
                break;

        }

        atk_collider_ins.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(unit_playerManager.atk_conDir.y, unit_playerManager.atk_conDir.x) * Mathf.Rad2Deg);
        atk_collider_ins.GetComponent<ProjectileMover>().DMG = atk_dmg;
        atk_collider_ins.GetComponent<ProjectileMover>().speed = atk_collider_speed;
        yield return new WaitForSeconds(atk_speed);
        atk_collider_creating = false;
    }

    public void Load_CardObj(string objName) {
        string objShape = objName.Split('_')[0];
        string objNum = objName.Split('_')[1];
        GameObject obj = Resources.Load<GameObject>("Card/" + objShape + "/" + objNum);
        GameObject ins = Instantiate(obj);

        ins.transform.SetParent(transform);
        ins.transform.localPosition = Vector3.zero;
        ins.transform.localScale = new Vector3(2f, 2f, 1f);

        switch (objNum)
        {
            case "0":
                atk_speed = 2.0f;
                atk_collider_speed = 10f;
                atk_dmg = 1f;
                break;
            case "1":
                atk_speed = 1.9f;
                atk_collider_speed = 10f;
                atk_dmg = 2f;
                break;
            case "2":
                atk_speed = 1.8f;
                atk_collider_speed = 10f;
                atk_dmg = 3f;
                break;
            case "3":
                atk_speed = 1.7f;
                atk_collider_speed = 10f;
                atk_dmg = 4f;
                break;
            case "4":
                atk_speed = 1.6f;
                atk_collider_speed = 10f;
                atk_dmg = 5f;
                break;
            case "5":
                atk_speed = 1.5f;
                atk_collider_speed = 10f;
                atk_dmg = 6f;
                break;
            case "6":
                atk_speed = 1.4f;
                atk_collider_speed = 10f;
                atk_dmg = 7f;
                break;
            case "7":
                atk_speed = 1.3f;
                atk_collider_speed = 10f;
                atk_dmg = 8f;
                break;
            case "8":
                atk_speed = 1.2f;
                atk_collider_speed = 10f;
                atk_dmg = 9f;
                break;
            case "9":
                atk_speed = 1.1f;
                atk_collider_speed = 15f;
                atk_dmg = 10f;
                break;
            case "10":
                atk_speed = 1.0f;
                atk_collider_speed = 15f;
                atk_dmg = 11f;
                break;
            case "11":
                atk_speed = 0.9f;
                atk_collider_speed = 15f;
                atk_dmg = 12f;
                break;
            case "12":
                atk_speed = 0.8f;
                atk_collider_speed = 15f;
                atk_dmg = 13f;
                break;

        }

        switch (currentCardMade) {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;

        }
    }
}
