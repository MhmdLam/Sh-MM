using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public static SimpleCameraFollow Instance {get; private set;}
    [SerializeField] private Transform target;
    public bool smoothMovement = true;
    public float targetChangeSpeed = 5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (smoothMovement)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*targetChangeSpeed);
        }
        // else
        // {
        //     transform.position = target.position;
        // }
    }

    private void LateUpdate()
    {
        if (!smoothMovement)
        {
            transform.position = target.position;
        }
    }
}
