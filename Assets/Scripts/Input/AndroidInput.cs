using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidInput : InputManager
{
    private void Update()
    {
        //directionState = DirectionState.Forward;

        if (Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);

            // check if touch should be ignored
            bool ignoreTouch = false;
            foreach (RectTransform rect in areasToIgnore)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(rect, touch.position))
                {
                    ignoreTouch = true;
                    break;
                }
            }

            if (!bodyChangeActive)
            {
                inputVector.x = -joystick.Vertical;
                inputVector.y = joystick.Horizontal;
                inputVector.Normalize();
            }
            else if (!ignoreTouch)
            {
                Ray ray = mainCamera.ScreenPointToRay(touch.position);
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
}
