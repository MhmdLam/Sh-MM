using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAbilitySet : IAbilitySet
{
    public float attackDamage = 1f;
    public float attackRadius = 2f;
    public float ability1Damage = 3f;
    public float ability1Radius = 2.5f;
    public float ability2Damage = 1f;
    public float ability2Radius = 2f;
    public int ability2Counter = 0;
    public float knockBackForce = 30f;

    public float criticalMult = 1f;
    private float criticalMin=1f, criticalMax=2f;

    [SerializeField]
    private float passiveChance = 1f;
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

    private int AreaAttack(float maindamage, float radius, bool knockBack=true)
    {
        int counter = 0;
        Collider[] hits = Physics.OverlapSphere(PlayerController.player.transform.position,radius);
        foreach(var hit in hits){
            if(hit.tag == "Enemy")
            {
                Character hitCharacter = hit.GetComponent<Character>();
                if (ability2Counter==0)
                {
                    hitCharacter.ApplyDamage(ability1Damage*criticalMult);
                }
                else
                {
                    hitCharacter.ApplyDamage((ability1Damage*(1+0.1f*ability2Counter))*criticalMult);
                    ability2Counter--;
                }
                criticalMult = 1f;
                if (knockBack) hitCharacter.ApplyKnockBack(knockBackForce, PlayerController.player.transform.position);
                counter++;
            }
        }

        return counter;
    }

    public void Attack()
    {
        Debug.Log("Golem Attack!");
        AreaAttack(attackDamage, attackRadius);
    }
    public void Ability1()
    {
        AreaAttack(ability1Damage, ability1Radius);
    }
    public void Ability2()
    {
        Debug.Log("Golem strength stealer!");
        ability2Counter = AreaAttack(ability2Damage, ability2Radius, false);
    }

    public void AbilityPassive() 
    {
        //criticalMult = UnityEngine.Random.Range(criticalMin, criticalMax);
        Debug.Log("next attack is a critical!");
        criticalMult = criticalMax;
    }

    
}
