using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MeleeAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    private float attackSpeed = 1f;
    public float AttackSpeed {get; set;}
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private float attackOverTime = 0.1f;
    [HideInInspector] public Character Character { get; set;}


    private void OnDisable()
    {
        Character.attackCanceled = true;
    }
    public void AttackAsEnemy()
    {
        Character.animator.SetFloat("AttackSpeed", attackSpeed);
        Character.animator.SetTrigger("Attack");
        Character.isAttacking = true;
        FunctionTimer.Create(AttackAsEnemyEnd, attackTime/attackSpeed);
    }
    private void AttackAsEnemyEnd()
    {
        if (gameObject.activeInHierarchy && !Character.attackCanceled && Vector3.Distance(transform.position, PlayerController.player.transform.position)<=Character.attackRangeSecondary+PlayerController.player.bodyRadius)
        {
            PlayerController.ApplyDamage(damageAmount);
        }

        FunctionTimer.Create(() => { Character.isAttacking = false;}, (attackTime+attackOverTime)/attackSpeed);
    }

    public void AttackAsPlayer()
    {
        throw new System.NotImplementedException();
    }
}
