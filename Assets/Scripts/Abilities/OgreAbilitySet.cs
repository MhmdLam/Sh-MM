using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class OgreAbilitySet : IAbilitySet
{
    public float attackDamage = 1f;
    public Vector3 attackHalfSize = new Vector3(1.1f,1f,0.6f);
    public float attackDisFromPlayer = 0.5f;
    public float knockBackForce = 30f;

    [SerializeField]
    private float passiveChance = 0.2f;
    public float PassiveChance {get{return passiveChance;} set{passiveChance=value;}}
    [SerializeField]
    private bool passiveSuccessful = false;
    public bool PassiveSuccessful {get{return passiveSuccessful;} set{passiveSuccessful=value;}}

    private float attackInterval = 1f;
    public float AttackInterval {get{return attackInterval;} set{attackInterval=value;}}
    private float ability1Cooldown = 1f;
    public float Ability1Cooldown {get{return ability1Cooldown;} set{ability1Cooldown=value;}}
    private float ability2Cooldown = 1f;
    public float Ability2Cooldown {get{return ability2Cooldown;} set{ability2Cooldown=value;}}

    public float speedMultAmount = 2f;
    public float speedMultTime = 5f;

    public float stunRange = 3.5f;
    public float stunDuration = 5f;

    public float splashSpeed = 4f;


    public void Attack()
    {
        Debug.Log("Ogre attack!");

        CodeMonkey.Utils.FunctionTimer.Create(
        () => 
        {
            Collider[] hits = Physics.OverlapBox(
                                                PlayerController.player.transform.position+PlayerController.player.transform.forward*attackDisFromPlayer,
                                                attackHalfSize,
                                                PlayerController.player.transform.rotation
                                                );

            foreach(var hit in hits)
            {
                if(hit.tag == "Enemy")
                {
                    Character hitCharacter = hit.GetComponent<Character>();
                    hitCharacter.ApplyDamage(attackDamage);
                    hitCharacter.ApplyKnockBack(knockBackForce, PlayerController.player.transform.position);
                }
            }

            if (UnityEngine.Random.Range(0f, 1f)<=PlayerController.Instance.passiveChance)
            {
                PlayerController.Instance.Passive();
                SoundsManager.Instance.PlaySound("Big Swing");
            }
            else
            {
                SoundsManager.Instance.PlaySound("Swing");
            }
        },
        0.8f);
        
    }

    public void Ability1()
    {
        Debug.Log("Ogre Speed-up!");
        SoundsManager.Instance.PlaySound("Speed Up");
        SpeedUp();
    }

    public void Ability2()
    {
        Debug.Log("Ogre Stun!");
        StunBomb();
    }

    public void AbilityPassive()
    {
        Debug.Log("Splash!");
        SplashDamage();
    }

    private void SplashDamage()
    {
        GameObject Area = PoolsManager.Instance.Get(6, out bool temp);
        Area.SetActive(true);
        Area.transform.position = PlayerController.player.transform.position;
        Area.transform.rotation = PlayerController.player.transform.rotation;
        //GameObject Area = GameObject.Instantiate(PlayerController.Instance.splashPrefab, PlayerController.player.transform.position, PlayerController.player.transform.rotation);
        //Area.transform.position += transform.forward * SplashSpeed * Time.deltaTime;
        Rigidbody rigidbody = Area.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(PlayerController.player.transform.forward*splashSpeed,ForceMode.Impulse);
    }

    private void SpeedUp() // ********
    {
        PlayerController.player.currentMoveSpeed *= speedMultAmount;
        PlayerController.Instance.AttackInterval /= speedMultAmount;
        FunctionTimer.Create(
                            ()=>{
                                PlayerController.player.currentMoveSpeed /= speedMultAmount;
                                PlayerController.Instance.AttackInterval *= speedMultAmount;
                                },
                            speedMultTime,
                            "Speed Mult"
                            );
    }

    private void StunBomb()
    {
        GameObject bomb = PoolsManager.Instance.Get(7);
        // GameObject.Instantiate(PlayerController.Instance.stunBombPrefab);
        bomb.SetActive(true);
        bomb.GetComponent<StunBomb>().Throw(stunRange, stunDuration);
    }
}
