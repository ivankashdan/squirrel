using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class reset : MonoBehaviour
{


    float rTimer = 0;
    public float rTimeLimit = 600;

    public bool resetOn;

    void FixedUpdate()
    {
        if (resetOn)
        {
            Gamepad current = Gamepad.current;


            rTimer += Time.deltaTime;

            //Debug.Log(rTimer);

            if (rTimer > rTimeLimit)
            {
                SceneManager.LoadScene("1 Whirligig", LoadSceneMode.Single);
            }

            if (current != null)
            {
                if ((current.dpad.left.isPressed) ||
                    (current.dpad.right.isPressed) ||
                    (current.aButton.isPressed) ||
                    (current.yButton.isPressed) ||
                    (current.leftStick.x.ReadValue() == 1f) ||
                    (current.leftStick.x.ReadValue() == -1f)
              )
                {
                    rTimer = 0;
                }
            }

          
        }

      
    }
}
