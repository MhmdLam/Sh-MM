// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using CodeMonkey.Utils;

// public class ZombieAbilitySet : IAbilitySet
// {
//     public void Ability1()
//     {
//         ZombieAbility1();
//     }

//     public void Ability2()
//     {
//         ZombieAbility2();
//     }

//     public void AbilityPassive() {}

//     [SerializeField] private float a1InvincibilityTime = 5f;
//     [SerializeField] private float a2SpeedMult = 2f;
//     [SerializeField] private float a2SpeedMultTime = 5f;

//     public float PassiveChance { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
//     public bool PassiveSuccessful { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

//     // makes the player invincible for some time
//     public void ZombieAbility1()
//     {
//         Debug.Log("zombie invincible!");
//         PlayerController.isInvincible = true;
//         FunctionTimer.Create(()=>{PlayerController.isInvincible=false;}, a1InvincibilityTime, "Invincible");
//     }

//     // makes the player faster for some time
//     public void ZombieAbility2()
//     {
//         Debug.Log("zombie speed-up!");
//         PlayerController.player.currentMoveSpeed *= a2SpeedMult;
//         FunctionTimer.Create(()=>{PlayerController.player.currentMoveSpeed/=a2SpeedMult;}, a2SpeedMultTime, "Invincible");
//     }
// }
