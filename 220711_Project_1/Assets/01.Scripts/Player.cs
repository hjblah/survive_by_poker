using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool move = false;
    [SerializeField] private GameObject player;
    public Vector2 moveDestination;
    public float moveSpeed;
    private bool check = false;
    private void Start()
    {
        RandomDestination();
    }

    private void Update()
    {
        if (Vector2.Distance(moveDestination, transform.position) > 0.1f)
        {
            RunToDestinatoin(moveDestination);
            LookAtDestination(moveDestination.x);
        }
        else{
            transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.0f);
            if (!check) {
                StartCoroutine(ResetDestination());
            }
        }
    }

    private void RunToDestinatoin(Vector3 destinatoin)
    {
        transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
        transform.position = Vector3.MoveTowards(transform.position, destinatoin, moveSpeed * Time.timeScale);
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

    private void RandomDestination() {
        moveDestination = new Vector2(Random.Range(-12f, 12f), Random.Range(-5f, 0f));
    }
    IEnumerator ResetDestination() {
        check = true;
        yield return new WaitForSeconds(5f);
        RandomDestination();
        check = false;
    }
}
