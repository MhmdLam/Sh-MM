using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilitySet
{
    float AttackInterval {get; set;}
    float Ability1Cooldown {get; set;}
    float Ability2Cooldown {get; set;}
    float PassiveChance {get; set;}
    bool PassiveSuccessful {get; set;}

    void Attack();
    void Ability1();
    void Ability2();
    void AbilityPassive();
}
