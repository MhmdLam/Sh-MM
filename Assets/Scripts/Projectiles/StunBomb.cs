using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBomb : MonoBehaviour, IPoolable
{
    [SerializeField] private int poolingID;
    public int PoolingID { get{return poolingID;} set{poolingID=value;}}
    public GameObject ThisGameObject {get {return gameObject;}}

    private Rigidbody rb;
    private AudioSource audioSource;
    [SerializeField] private Vector3 throwDirection = Vector3.up;
    [SerializeField] private float throwForce = 1f;
    [SerializeField] private float explosionDelay = 1f;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    // void Start()
    // {
    //     gameObject.SetActive(false);
    // }

    // throws the bomb to explode
    public void Throw(float stunRange, float stunDuration)
    {
        Throw(throwDirection, throwForce, explosionDelay, stunRange, stunDuration);
    }
    public void Throw(Vector3 direction, float force, float explosionDelay, float range, float stunDuration)
    {
        gameObject.SetActive(true);
        // change this part for specifying where the bomb is spawnd at first
        transform.position = PlayerController.player.transform.position
                            -PlayerController.player.transform.forward*0.2f
                            +PlayerController.player.transform.up*0.5f;


        rb.AddForce(direction*force);
        CodeMonkey.Utils.FunctionTimer.Create( () => Explode(range, stunDuration), explosionDelay);
    }

    // called when the bomb explodes
    private void Explode(float range, float stunDuration)
    {
        SoundsManager.Instance.PlaySoundSpatial("Stun Bomb", transform.position);

        Collider[] hits = Physics.OverlapSphere(transform.position,range);
        foreach(var hit in hits)
        {
            if(hit.tag == "Enemy")
            {
                hit.GetComponent<Character>().ApplyStun(stunDuration);
            }
        }

        PoolsManager.Instance.ReturnToPool(gameObject, poolingID);
    }

    public void ResetComponents()
    {
        throw new System.NotImplementedException();
    }
}
