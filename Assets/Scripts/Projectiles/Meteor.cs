using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour, IPoolable
{
    [SerializeField] private int poolingID;
    public int PoolingID{get; set;}
    public GameObject ThisGameObject{get{return gameObject;}}
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float damage = 3f;
    [SerializeField] private float explosionRange = 2.5f;
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
        transform.localPosition -= transform.up*moveSpeed*Time.deltaTime;
        //transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime, )
        time += Time.deltaTime;
        if (time>=activeTime)
            DestroyBullet();
    }

    private void OnTriggerEnter(Collider coll)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRange);
        foreach(var hit in hits){
            if(hit.tag == "Enemy")
            {
                hit.GetComponent<Character>().ApplyDamage(damage);
            }
        }
        DestroyBullet();
    }

    // called whenever bullet should be disabled
    private void DestroyBullet()
    {
        PoolsManager.Instance.ReturnToPool(gameObject, poolingID);
    }
}
