using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour, IPoolable
{
    [SerializeField] private int poolingID;
    public int PoolingID { get{return poolingID;} set{poolingID=value;}}
    public GameObject ThisGameObject {get {return gameObject;}}

    public float lifeTime = 10f;
    public float damageAmount = 1f;

    private void Start()
    {
        CodeMonkey.Utils.FunctionTimer.Create( () => { PoolsManager.Instance.ReturnToPool(gameObject, poolingID);}, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Enemy")
        {
            other.GetComponent<Character>().ApplyDamage(damageAmount);

        }
    }

    public void ResetComponents()
    {
        throw new System.NotImplementedException();
    }
}
