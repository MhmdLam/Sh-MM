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
    [HideInInspector] public Character Character { get; set;}

    public void AttackAsEnemy()
    {
        Character.animator.SetFloat("AttackSpeed", attackSpeed);
        Character.animator.SetTrigger("Attack");
        Character.isAttacking = true;
        FunctionTimer.Create(AttackAsEnemyEnd, attackTime/attackSpeed);
    }
    private void AttackAsEnemyEnd()
    {
        if (this.enabled && !Character.attackCanceled && Vector3.Distance(transform.position, PlayerController.player.transform.position)<=Character.attackRangeSecondary)
        {
            PlayerController.ApplyDamage(damageAmount);
        }
        Character.isAttacking = false;
    }

    public void AttackAsPlayer()
    {
        throw new System.NotImplementedException();
    }
}
