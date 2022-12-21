using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkColliderManager : MonoBehaviour
{
    public Vector2 atk_conDir;

    private void Start()
    {
        StartCoroutine(DelayDestory());
    }

    // Update is called once per frame
    void Update()
    {
        atk_conDir = new Vector2(atk_conDir.x * 100, atk_conDir.y * 100);
        if (atk_conDir.x > 1) {
            atk_conDir.x = 1;
        }
        if (atk_conDir.y > 1) {
            atk_conDir.y = 1;
        }

        transform.Translate(atk_conDir);
    }

    IEnumerator DelayDestory() {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }   
}
