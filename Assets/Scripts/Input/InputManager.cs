using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class InputManager : MonoBehaviour
{
    //public DirectionState directionState = DirectionState.Forward;
    [HideInInspector] public Vector2 inputVector = new Vector2();
    [HideInInspector] public Camera mainCamera;
    [SerializeField] protected FixedJoystick joystick;
    protected bool bodyChangeActive = false;
    public RectTransform[] areasToIgnore;

    [SerializeField] protected UnityEvent<Character> OnCharacterRayCast;
    [SerializeField] protected UnityEvent OnBodyChangeStarted;
    [SerializeField] protected UnityEvent OnBodyChangeCanceled;
    [SerializeField] protected UnityEvent OnBodyChangeEnded;




    // private void Update()
    // {
    //     //directionState = DirectionState.Forward;

    //     if (Input.touchCount>0)
    //     {
    //         Touch touch = Input.GetTouch(0);

    //         // check if touch should be ignored
    //         bool ignoreTouch = false;
    //         foreach (RectTransform rect in areasToIgnore)
    //         {
    //             if (RectTransformUtility.RectangleContainsScreenPoint(rect, touch.position))
    //             {
    //                 ignoreTouch = true;
    //                 break;
    //             }
    //         }

    //         if (!bodyChangeActive)
    //         {
    //             inputVector.x = -joystick.Vertical;
    //             inputVector.y = joystick.Horizontal;
    //             inputVector.Normalize();
    //         }
    //         else if (!ignoreTouch)
    //         {
    //             Ray ray = mainCamera.ScreenPointToRay(touch.position);
    //             RaycastHit hitInfo;
    //             if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.gameObject.tag=="Enemy")
    //             {
    //                 Character character = hitInfo.collider.GetComponent<Character>();
    //                 if (OnCharacterRayCast!=null)
    //                     OnCharacterRayCast.Invoke(character);
    //                 if (OnBodyChangeEnded!=null)
    //                     OnBodyChangeEnded.Invoke();
    //                 bodyChangeActive = false;
    //             }
    //         }
    //     }
    // }

    // starts/cancels BodyChange
    public void ToggleBodyChange()
    {
        if (bodyChangeActive)
        {
            CancelBodyChange();
        }
        else
        {
            StartBodyChange();
        }
    }

    // called when looking for a new body
    public void StartBodyChange()
    {
        if (OnBodyChangeStarted!=null)
            OnBodyChangeStarted.Invoke();
        bodyChangeActive = true;
    }

    // called when trying to cancel BodyChange
    public void CancelBodyChange()
    {
        if (OnBodyChangeCanceled!=null)
            OnBodyChangeCanceled.Invoke();
        bodyChangeActive = false;
    }
}

public enum DirectionState
{
    Forward,
    Right,
    Left
};
