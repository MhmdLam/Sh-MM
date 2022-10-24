using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance {get; private set;} // this class's singleton


    // ****Tags****
    [HideInInspector] public string playerTag = "Player";
    [HideInInspector] public string enemyTag = "Enemy";


    // ****References****
    [Header("****Refrences****")]
    [SerializeField] private Character playerCharcterScript;
    [HideInInspector] public InputManager inputManager;
    [SerializeField] public static Character player; // *****
    [SerializeField] private Canvas playerIndicator;
    [SerializeField] private GameObject ghost;
    public AttackCaller attackCaller;
    public GameObject splashPrefab;


    // ****BodyChange****
    [Header("****BodyChange****")]
    // [SerializeField] private float bodyChangeSpeed = 5f;
    [SerializeField] private float bodyChangeTime = 0.3f;
    [SerializeField] private float bodyChangeTimeScale = 0.3f;
    // private float bodyChangeDistance;
    private bool currentlyChangingBodies = false;
    [SerializeField] [ReadOnly] private CharacterType currentPlayerType;
    

    // ****Abilities****
    private Action AttackAction;
    public float AttackInterval { get{return attackCaller.attackInterval;} set{attackCaller.attackInterval=value;}}
    private Action Ability1Action;
    [Header("****Abilities****")]
    public float ability1Cooldown;
    private Action Ability2Action;
    public float ability2Cooldown;
    private Action AbilityPassiveAction;
    public float passiveChance;


    // ****OtherValues****
    [Header("****Other****")]
    public float invincibleTime = 2f;
    [HideInInspector] public bool isInvincible = false;
    [SerializeField] private float moveSpeedMult = 1.5f;
    [SerializeField] private float healthBarSizeMult = 1.2f;


    // ****Other****
    private Vector2 playerLastDir = Vector2.up;
    private Vector2 playerDir;


    private void Awake()
    {
        // assign references
        Instance = this;
        attackCaller = GetComponent<AttackCaller>();
    }

    private void Start()
    {
        // ***Add new ability sets here***
        abilitySets.Add(new WizardAbilitySet());
        abilitySets.Add(new GolemAbilitySet());
        abilitySets.Add(new OgreAbilitySet());

        currentPlayerType = CharacterType.None;
        PlayerController.player = playerCharcterScript;
        SetAsPlayer(ref player);
    }


    // called by the MainController to calculate the player direction
    public void Calculate()
    {
        playerDir = inputManager.inputVector;

        if (playerDir.magnitude!=0)
        {
            player.moveDirection = playerDir;
            playerLastDir = playerDir;
        }
        else
        {
            player.moveDirection = playerLastDir;
        }
    }

    // applies damage to the player
    public static void ApplyDamage(float damageAmount)
    {
        if (!Instance.isInvincible)
            player.ApplyDamage(damageAmount);
    }

    // called to prepare the new player's body(character script)
    private void SetAsPlayer(ref Character character)
    {
        isInvincible = true;
        FunctionTimer.Create(()=>{isInvincible=false;}, invincibleTime, "Invincible");

        player = character;
        player.gameObject.tag = playerTag;
        player.isPlayer = true;

        player.currentMoveSpeed = player.baseMoveSpeed*moveSpeedMult;
        playerLastDir = player.moveDirection;

        player.animator.SetBool("Running", true);

        player.healthBar.transform.localScale *= healthBarSizeMult;
        player.healthBar.isPlayer = true;
        player.UpdateHealthBar();

        playerIndicator.transform.SetParent(player.transform);
        playerIndicator.transform.localPosition = Vector3.zero;

        // change ability sets if needed
        if (currentPlayerType!=character.characterType)
        {
            currentPlayerType = character.characterType;
            // Debug.Log("changed types:"+currentPlayerType.ToString());
            AttackAction = abilitySets[(int)currentPlayerType].Attack;
            Ability1Action = abilitySets[(int)currentPlayerType].Ability1;
            Ability2Action = abilitySets[(int)currentPlayerType].Ability2;
            AbilityPassiveAction = abilitySets[(int)currentPlayerType].AbilityPassive;

            AttackInterval = abilitySets[(int)currentPlayerType].AttackInterval;
            ability1Cooldown = abilitySets[(int)currentPlayerType].Ability1Cooldown;
            ability2Cooldown = abilitySets[(int)currentPlayerType].Ability2Cooldown;
            passiveChance = abilitySets[(int)currentPlayerType].PassiveChance;
        }
    }

    // called to reset the former player body(character script)
    private void UnsetAsPlayer()
    {
        player.isPlayer = false;
        player.gameObject.tag = enemyTag;

        player.currentMoveSpeed = player.baseMoveSpeed;

        player.animator.SetBool("Running", false);

        player.healthBar.transform.localScale /= healthBarSizeMult;
        player.healthBar.isPlayer = false;
        player.UpdateHealthBar();
        //player.Die();
    }

    // changes player's body
    public void ChangeBody(Character character)
    {
        currentlyChangingBodies = true;

        // start ghost transition animation
        ghost.SetActive(true);
        ghost.GetComponent<Ghost>().StartAnimation(player.transform.position, character.transform.position);
        SimpleCameraFollow.Instance.smoothMovement = true;
        Time.timeScale = bodyChangeTimeScale;
        FunctionTimer.Create(
                            () => {
                                ghost.SetActive(false);
                                SimpleCameraFollow.Instance.smoothMovement = false;
                                Time.timeScale=1f;
                                currentlyChangingBodies=false;
                                },
                            bodyChangeTime, "Body Change", false, true
                            );

        // actually change bodies
        UnsetAsPlayer();
        SetAsPlayer(ref character);
    }


    // ******AbilitiesFunctions******
    private List<IAbilitySet> abilitySets = new List<IAbilitySet>();
    
    // calls the attack function
    public void Attack()
    {
        if (AttackAction!=null)
            AttackAction();
        else
            Debug.Log("Attack Action is Null!");
    }

    // calls the first ability
    public void Ability1()
    {
        if (Ability1Action!=null)
            Ability1Action();
        else
            Debug.Log("Ability1 Action is Null!");
    }

    // calls the second ability
    public void Ability2()
    {
        if (Ability2Action!=null)
            Ability2Action();
        else
            Debug.Log("Ability1 Action is Null!");
    }

    // calls the passive ability
    public void Passive()
    {
        if (Ability2Action!=null)
            AbilityPassiveAction();
        else
            Debug.Log("AbilityPassive Action is Null!");
    }
}
