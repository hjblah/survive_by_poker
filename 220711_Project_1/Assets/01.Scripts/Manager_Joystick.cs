using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Joystick : MonoBehaviour
{
    public FixedJoystick fixedJoystick;

    // Start is called before the first frame update
    void Start()
    { 
    
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (fixedJoystick.transform.GetChild(i).gameObject.activeSelf)
            {
                fixedJoystick.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (fixedJoystick.Direction == new Vector2(0, 0))
        {
            return;
        }
        else
        {
            if (fixedJoystick.Direction.x > 0f && fixedJoystick.Direction.y > 0f)
            {
                //오른쪽 위
                fixedJoystick.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (fixedJoystick.Direction.x < 0f && fixedJoystick.Direction.y > 0f)
            {
                //왼쪽 위
                fixedJoystick.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (fixedJoystick.Direction.x > 0f && fixedJoystick.Direction.y < 0f)
            {
                //오른쪽 아래
                fixedJoystick.transform.GetChild(2).gameObject.SetActive(true);
            }
            else if (fixedJoystick.Direction.x < 0f && fixedJoystick.Direction.y < 0f)
            {
                //왼쪽 아래
                fixedJoystick.transform.GetChild(3).gameObject.SetActive(true);
            }
        }

    }
}
