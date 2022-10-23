using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private int poolingID;
    public int PoolingID{get; set;}
    public GameObject ThisGameObject{get{return gameObject;}}
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float damage = 3f;
    [SerializeField] private int penitration = 1;
    [SerializeField] private float activeTime = 10f;
    private float time = 0f;

    private void OnEnable()
    {
        PoolingID = poolingID;
        time = 0f;
    }

    public void ResetComponents(){}

    private void Update()
    {
        transform.localPosition += transform.forward*moveSpeed*Time.deltaTime;
        //transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime, )
        time += Time.deltaTime;
        if (time>=activeTime)
            DestroyBullet();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag=="Enemy")
        {
            coll.GetComponent<Character>().ApplyDamage(damage);

            penitration--;
            if (penitration<=0)
                DestroyBullet();
        }
    }

    // called whenever bullet should be disabled
    public void DestroyBullet()
    {
        PoolsManager.Instance.ReturnToPool(gameObject, poolingID);
    }
}
