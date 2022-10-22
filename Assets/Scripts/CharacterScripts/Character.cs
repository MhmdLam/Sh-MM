using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using NaughtyAttributes;

public class Character : MonoBehaviour, IPoolable
{
    // ****ID****
    [Header("****ID****")]
    [SerializeField] private int poolingID;
    public int PoolingID{ get{return poolingID;} set{poolingID=value;}}
    public GameObject ThisGameObject{get{return gameObject;}}
    public bool isPlayer = false;
    public CharacterType characterType = CharacterType.Wizard;

    // ****Health****
    [Header("****Health****")]
    public HealthBar healthBar;
    [SerializeField] private float maxHealth = 10f; // remember the float right below shold always be equal to this
    
    [ProgressBar("Health", 10f, EColor.Green)]
    [SerializeField]
    private float health;

    // ****Attack****
    [Header("****Attack****")]
    public float attackRange = 1f;
    public float attackRangeSecondary = 1.3f;
    public float bodyRadius = 0.2f;
    private float stunTimeLeft = 0f;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isStunned = false;
    [HideInInspector] public bool isFrozen = false;
    private float frozenTimeLeft = 0f;
    [HideInInspector] public bool attackCanceled = false; // becomes true when attack is canceled(e.g when stunned)

    private IAttack attackScript; // a component with all the attack logic

    // ****Movement****
    [Header("****Movement****")]
    public float baseMoveSpeed = 1f;
    [HideInInspector] public Vector2 moveDirection = Vector2.up;
    [HideInInspector] public float currentMoveSpeed;

    

    // ****Other****
    [Header("****Other****")]
    public Animator animator;
    private Rigidbody _rigidbody;

    

    private void Awake()
    {
        // assign references
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(true);
        attackScript = GetComponent<IAttack>();
        attackScript.Character = (Character) this;

        healthBar.ResetComponents();
    }

    private void OnEnable()
    {
        currentMoveSpeed = baseMoveSpeed;
        health = maxHealth;
        UpdateHealthBar();
    }

    // reassigns all references
    public void ResetComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(true);
        attackScript = GetComponent<IAttack>();
        attackScript.Character = (Character) this;

        healthBar.ResetComponents();
        currentMoveSpeed = baseMoveSpeed;
        health = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        if (!(isStunned||isFrozen)) // move and rotate if possible
        {
            if (!isAttacking)
            {
                // move the character
                _rigidbody.MovePosition(transform.position+new Vector3(moveDirection.x, 0f, moveDirection.y)*Time.deltaTime*currentMoveSpeed);
            }
            // rotate the character
            _rigidbody.MoveRotation(Quaternion.Euler(0f, Mathf.Atan2(moveDirection.x, moveDirection.y)*Mathf.Rad2Deg, 0f));
        }
        else // check if stun/freeze time ended
        {
            if (isStunned)
            {
                stunTimeLeft -= Time.deltaTime;
                if (stunTimeLeft<=0f)
                    EndStun();
            }
            if (isFrozen)
            {
                frozenTimeLeft -= Time.deltaTime;
                if (frozenTimeLeft<=0f)
                    EndFreeze();
            }
        }
    }

    // attack function called by the EnemyController
    public void Attack()
    {
        if (!isPlayer)
        {
            attackCanceled = false;
            attackScript.AttackAsEnemy();
        }
    }

    // applies some damage to this character
    public void ApplyDamage(float damageAmount)
    {
        health -= damageAmount;
        UpdateHealthBar();
        if (health<=0f)
            Die();
    }

    // stuns the character for the given duration
    public void ApplyStun(float duration)
    {
        attackCanceled = true;
        isStunned = true;
        stunTimeLeft = duration;
    }

    // called when the stun ends
    private void EndStun()
    {
        isStunned = false;
    }

    // freezes the character for the given duration
    public void ApplyFreeze(float duration)
    {
        attackCanceled = true;
        isFrozen = true;
        frozenTimeLeft = duration;
    }

    // called when the freeze ends
    private void EndFreeze()
    {
        isFrozen = false;
    }

    // applies a knock back to this character relative to the given position
    public void ApplyKnockBack(float force, Vector3 pos)
    {
        Vector3 calculatedForce = new Vector3(
                            transform.position.x-pos.x,
                            0f,
                            transform.position.z-pos.z
                            );
        calculatedForce.Normalize();

        _rigidbody.AddForce(calculatedForce*force, ForceMode.Impulse);
    }

    // updates this characters health bar
    public void UpdateHealthBar()
    {
        healthBar.UpdateBar(health/maxHealth);
    }

    // called when the character dies
    public void Die()
    {
        healthBar.UpdateBar(0f);
        //gameObject.SetActive(false);
        PoolsManager.Instance.ReturnToPool(gameObject, poolingID);
    }
}


// represents a character type (wizard, ogre, ...)
public enum CharacterType
{
    Wizard,
    Golem,
    Ogre,
    None
};