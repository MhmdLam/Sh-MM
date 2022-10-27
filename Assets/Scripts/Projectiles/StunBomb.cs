using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBomb : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Vector3 throwDirection = Vector3.up;
    [SerializeField] private float throwForce = 1f;
    [SerializeField] private float explosionDelay = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        gameObject.SetActive(false);
    }

    // throws the bomb to explode
    public void Throw(float stunRange, float stunDuration)
    {
        Throw(throwDirection, throwForce, explosionDelay, stunRange, stunDuration);
    }
    public void Throw(Vector3 direction, float force, float explosionDelay, float range, float stunDuration)
    {
        gameObject.SetActive(true);
        transform.position = PlayerController.player.transform.position
                            -PlayerController.player.transform.forward*0.5f
                            +PlayerController.player.transform.up*0.5f;


        rb.AddForce(direction*force);
        CodeMonkey.Utils.FunctionTimer.Create( () => Explode(range, stunDuration), explosionDelay);
    }

    // called when the bomb explodes
    private void Explode(float range, float stunDuration)
    {
        gameObject.SetActive(false);

        Collider[] hits = Physics.OverlapSphere(transform.position,range);
        foreach(var hit in hits)
        {
            if(hit.tag == "Enemy")
            {
                hit.GetComponent<Character>().ApplyStun(stunDuration);
            }
        }
    }
}
