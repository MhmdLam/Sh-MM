using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInput : InputManager
{
    private void Update()
    {
        if (!bodyChangeActive)
        {
            // inputVector.x = -joystick.Vertical;
            // inputVector.y = joystick.Horizontal;
            inputVector.x = -Input.GetAxis("Vertical");
            inputVector.y = Input.GetAxis("Horizontal");

            inputVector.Normalize();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.gameObject.tag=="Enemy")
            {
                Character character = hitInfo.collider.GetComponent<Character>();
                if (OnCharacterRayCast!=null)
                    OnCharacterRayCast.Invoke(character);
                if (OnBodyChangeEnded!=null)
                    OnBodyChangeEnded.Invoke();
                bodyChangeActive = false;
            }
        }
    }
}
