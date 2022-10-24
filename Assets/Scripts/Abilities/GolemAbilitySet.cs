using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAbilitySet : IAbilitySet
{
    public float attackDamage = 1f;
    public float attackRadius = 2f;
    public float mdamage = 3f;
    public float radius = 2.5f;

    [SerializeField]
    private float passiveChance = 0.2f;
    public float PassiveChance {get{return passiveChance;} set{passiveChance=value;}}
    [SerializeField]
    private bool passiveSuccessful = false;
    public bool PassiveSuccessful {get{return passiveSuccessful;} set{passiveSuccessful=value;}}

    private float attackInterval = 2f;
    public float AttackInterval {get{return attackInterval;} set{attackInterval=value;}}
    private float ability1Cooldown = 1f;
    public float Ability1Cooldown {get{return ability1Cooldown;} set{ability1Cooldown=value;}}
    private float ability2Cooldown = 1f;
    public float Ability2Cooldown {get{return ability2Cooldown;} set{ability2Cooldown=value;}}

    private void AreaAttack(float maindamage, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(PlayerController.player.transform.position,radius);
        foreach(var hit in hits){
            if(hit.tag == "Enemy"){
                Character hitCharacter = hit.GetComponent<Character>();
                hitCharacter.ApplyDamage(mdamage);
                hitCharacter.ApplyKnockBack(30f, PlayerController.player.transform.position);
            }
        }
    }
    private void CiriticalDamage(float damage)
    {
        // not implemented
    }

    public void Attack()
    {
        Debug.Log("Golem Attack!");
        AreaAttack(mdamage, attackRadius);
    }
    public void Ability1(){
        Debug.Log("Golem Ability 1");
        AreaAttack(mdamage, radius);

    }
    public void Ability2(){
        Debug.Log("Golem Ability 2");
    }

    public void AbilityPassive() {}

    
}
