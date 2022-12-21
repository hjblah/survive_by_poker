using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Map : MonoBehaviour
{
    public GameObject[] mapData;
    public bool isMapMove = false;
    public int maxStage = 0;
    int stageNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(mapData[stageNum], new Vector3(0, 0, 0), Quaternion.identity, transform);
        Instantiate(mapData[stageNum + 1], new Vector3(0, 18.45f, 0), Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (stageNum < maxStage - 1)
        {
            if (isMapMove)
            {
                if (transform.position.y > -18.45f)
                {
                    transform.position += new Vector3(0, -0.05f, 0);
                }
                else
                {
                    stageNum++;
                    transform.position = Vector3.zero;
                    transform.GetChild(1).transform.position = Vector3.zero;
                    if (stageNum < maxStage - 1)
                    {
                        Instantiate(mapData[stageNum + 1], new Vector3(0, 18.45f, 0), Quaternion.identity, transform);
                    }
                    Destroy(transform.GetChild(0).gameObject);
                    isMapMove = false;
                }
            }
        }
    }
}
