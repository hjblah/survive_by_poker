using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;

    private Unit_Player_Manager playerManager;
    public float DMG;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject, 5);

        //////////////// add line
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<Unit_Player_Manager>();
    }

    void FixedUpdate()
    {
        if (speed != 0)
        {
            //rb.velocity = transform.forward * speed;
            transform.position += transform.right * (speed * Time.deltaTime);
        }
    }

    //https ://docs.unity3d.com/ScriptReference/Rigidbody.OnCollisionEnter.html
    void OnCollisionEnter(Collision collision)
    {
        if (!this.gameObject.name.Contains("EnemyCollider"))
        {
            if (collision.transform.tag == "Enemy")
            {
                //Debug.Log("##############################");
                //Lock all axes movement and rotation
                rb.constraints = RigidbodyConstraints.FreezeAll;
                speed = 0;

                ContactPoint contact = collision.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point + contact.normal * hitOffset;

                if (hit != null)
                {
                    var hitInstance = Instantiate(hit, pos, rot);
                    if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
                    else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
                    else { hitInstance.transform.LookAt(contact.point + contact.normal); }

                    var hitPs = hitInstance.GetComponent<ParticleSystem>();
                    if (hitPs != null)
                    {
                        Destroy(hitInstance, hitPs.main.duration);
                    }
                    else
                    {
                        var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(hitInstance, hitPsParts.main.duration);
                    }
                }
                foreach (var detachedPrefab in Detached)
                {
                    if (detachedPrefab != null)
                    {
                        detachedPrefab.transform.parent = null;
                    }
                }
                Destroy(gameObject);

                collision.transform.GetComponent<Unit_EnemyManager>().ENEMY_HP -= DMG;
            }
        }
        else
        {
            if (collision.transform.tag == "Player")
            {
                //Lock all axes movement and rotation
                rb.constraints = RigidbodyConstraints.FreezeAll;
                speed = 0;

                ContactPoint contact = collision.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point + contact.normal * hitOffset;

                if (hit != null)
                {
                    var hitInstance = Instantiate(hit, pos, rot);
                    if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
                    else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
                    else { hitInstance.transform.LookAt(contact.point + contact.normal); }

                    var hitPs = hitInstance.GetComponent<ParticleSystem>();
                    if (hitPs != null)
                    {
                        Destroy(hitInstance, hitPs.main.duration);
                    }
                    else
                    {
                        var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(hitInstance, hitPsParts.main.duration);
                    }
                }
                foreach (var detachedPrefab in Detached)
                {
                    if (detachedPrefab != null)
                    {
                        detachedPrefab.transform.parent = null;
                    }
                }
                Destroy(gameObject);

                GameObject.FindWithTag("PlayerManager").GetComponent<Unit_Player_Manager>().hp -= DMG;
                GameObject.FindWithTag("PlayerManager").GetComponent<Unit_Player_Manager>().hp_Gage.localScale =
                    new Vector3(GameObject.FindWithTag("PlayerManager").GetComponent<Unit_Player_Manager>().hp / GameObject.FindWithTag("PlayerManager").GetComponent<Unit_Player_Manager>().full_hp, 1, 1);
            }
        }
    }
}
