using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmoBox : MonoBehaviour
{
    [SerializeField] private bool wireframe = true;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 size;

    private void OnDrawGizmos()
    {
        Debug.Log("aaa");
        Gizmos.color = Color.red;
        if (wireframe)
        {
            Gizmos.DrawWireCube(position, size);
        }
        else
        {
            Gizmos.DrawCube(position, size);
        }
    }
}
