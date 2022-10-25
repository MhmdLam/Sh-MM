using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCaller : MonoBehaviour
{
    public float speed;
    public bool canShoot = true;
    public GameObject BulletPrefab;
    [SerializeField] private Transform parentTransform;
    //public PlayerController pl;
    float time1;
    public float attackInterval;

    public static Bullet lastBullet;
    void Start()
    {
        time1 = 0f;
        //attackInterval = 1f;
    }

    void Update()
    {
        time1 += Time.deltaTime;
        if (time1 > attackInterval)
        {
            time1 = 0f;
            PlayerController.Instance.Attack();
            // if (UnityEngine.Random.Range(0f, 1f)<=PlayerController.Instance.passiveChance)
            // {
            //     PlayerController.Instance.Passive();
            // }
        }
    }
}
