using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Vector3 startPos, targetPos;
    public float slider = 0f;
    //[SerializeField] private float speed = 1f;
    [SerializeField] private Animation sliderAnimation;
    
    void Update()
    {
        Debug.Log(slider);
        transform.position = Vector3.Lerp(startPos, targetPos, slider);
        //slider += Time.deltaTime*speed;
    }

    public void StartAnimation(Vector3 startPos, Vector3 targetPos)
    {
        this.startPos = startPos;
        this.targetPos = targetPos;
        transform.position = startPos;
        transform.LookAt(targetPos);
        sliderAnimation.Play();
    }
}
