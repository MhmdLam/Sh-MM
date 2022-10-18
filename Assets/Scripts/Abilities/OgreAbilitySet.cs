using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class OgreAbilitySet : IAbilitySet
{
    [SerializeField]
    private float passiveChance = 0.2f;
    public float PassiveChance {get{return passiveChance;} set{passiveChance=value;}}
    [SerializeField]
    private bool passiveSuccessful = false;
    public bool PassiveSuccessful {get{return passiveSuccessful;} set{passiveSuccessful=value;}}

    public float speedMultAmount = 2f;
    public float speedMultTime = 5f;

    public float stunRange = 3.5f;
    public float stunDuration = 5f;

    public float splashSpeed = 4f;


    public void Ability1()
    {
        Debug.Log("Ogre Speed-up!");
        SpeedUp();
    }

    public void Ability2()
    {
        Debug.Log("Ogre Stun!");
        Stun();
    }

    public void AbilityPassive()
    {
        Debug.Log("Splash!");
        SplashDamage();
    }

    private void SplashDamage()
    {
        GameObject Area = GameObject.Instantiate(PlayerController.SplashPrefab, PlayerController.player.transform.position, PlayerController.player.transform.rotation);
        //Area.transform.position += transform.forward * SplashSpeed * Time.deltaTime;
        Rigidbody rigidbody = Area.GetComponent<Rigidbody>();
        rigidbody.AddForce(PlayerController.player.transform.forward*splashSpeed,ForceMode.Impulse);
        
        GameObject.Destroy(Area, 10);
    }

    private void SpeedUp()
    {
        PlayerController.player.currentMoveSpeed *= speedMultAmount;
        FunctionTimer.Create(()=>{PlayerController.player.currentMoveSpeed/=speedMultAmount;}, speedMultTime, "Speed Mult");
    }

    private void Stun()
    {
        Collider[] hits = Physics.OverlapSphere(PlayerController.player.transform.position,stunRange);
        foreach(var hit in hits)
        {
            if(hit.tag == "Enemy")
            {
                hit.GetComponent<Character>().ApplyStun(stunDuration);
            }
        }
    }
}
