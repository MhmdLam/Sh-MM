using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

[RequireComponent(typeof(InputManager))]
public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance {get; private set;}
    public string playerTag = "Player";
    public string enemyTag = "Enemy";
    [HideInInspector] public InputManager inputManager;
    // [SerializeField] private float turnSpeed = 1f;

    public static bool isInvincible = false;
    public static float invincibleTime = 2f;
    private CharacterType currentPlayerType;
    [SerializeField] private float moveSpeedMult = 1.5f;
    [SerializeField] private float healthBarSizeMult = 1.2f;

    [SerializeField] private Character playerCharcterScript;
    [SerializeField] public static Character player; // *****
    [SerializeField] private Canvas playerIndicator;
    [SerializeField] private GameObject ghost;
    [SerializeField] private float bodyChangeSpeed = 5f;
    [SerializeField] private float bodyChangeTime = 0.3f;
    [SerializeField] private float bodyChangeTimeScale = 0.3f;
    private float bodyChangeDistance;
    private bool currentlyChangingBodies = false;
    private Vector2 playerLastDir = Vector2.up;
    private Vector2 playerDir;

    private Action AttackAction;
    private Action Ability1Action;
    private Action Ability2Action;
    public static Action AbilityPassiveAction;
    public static float ability1Cooldown;
    public static float ability2Cooldown;
    public static float passiveChance;

    private BulletShooter bulletShooter;

    [SerializeField] private GameObject splashPrefab;
    public static GameObject SplashPrefab;


    private void Awake()
    {
        Instance = this;
        bulletShooter = GetComponent<BulletShooter>();
        SplashPrefab = splashPrefab;
    }

    private void Start()
    {
        // abilitySets.Add(new ZombieAbilitySet());
        // abilitySets.Add(new GolemAbilitySet());
        abilitySets.Add(new WizardAbilitySet());
        abilitySets.Add(new GolemAbilitySet());
        abilitySets.Add(new OgreAbilitySet());

        PlayerController.player = playerCharcterScript;
        SetAsPlayer(ref player);

        Ability1Action = abilitySets[0].Ability1;
        Ability2Action = abilitySets[0].Ability2;
        AbilityPassiveAction = abilitySets[0].AbilityPassive;
        passiveChance = abilitySets[0].PassiveChance;
    }

    private void Update()
    {
        if (currentlyChangingBodies)
        {
            // Debug.Log(ghost.transform.position);
            ghost.transform.position = Vector3.Lerp(ghost.transform.position, playerIndicator.transform.position, Time.deltaTime*bodyChangeSpeed*bodyChangeDistance);
        }
    }

    // called by the MainController to calculate the player direction
    public void Calculate()
    {
        playerDir = inputManager.inputVector;
        //playerDir.Normalize();

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
        if (!isInvincible)
            player.ApplyDamage(damageAmount);
    }

    // called when a new player is assigned
    private void SetAsPlayer(ref Character character)
    {
        isInvincible = true;
        FunctionTimer.Create(()=>{isInvincible=false;}, invincibleTime, "Invincible");

        player = character;
        player.gameObject.tag = playerTag;
        player.isPlayer = true;
        playerIndicator.transform.SetParent(player.transform);
        playerIndicator.transform.localPosition = Vector3.zero;
        player.animator.SetBool("Running", true);
        playerLastDir = player.moveDirection;
        player.currentMoveSpeed = player.baseMoveSpeed*moveSpeedMult;
        player.healthBar.transform.localScale *= healthBarSizeMult;
        player.healthBar.isPlayer = true;
        player.UpdateHealthBar();
    }

    // called when a character is no longer the player
    private void UnsetAsPlayer()
    {
        player.gameObject.tag = enemyTag;
        player.isPlayer = false;
        player.animator.SetBool("Running", false);
        player.currentMoveSpeed = player.baseMoveSpeed;
        player.healthBar.transform.localScale /= healthBarSizeMult;
        player.healthBar.isPlayer = false;
        player.UpdateHealthBar();
        //player.Die();
    }

    // changes players' body
    public void ChangeBody(Character character)
    {
        currentlyChangingBodies = true;

        ghost.SetActive(true);
        // Debug.Log(ghost.transform.position);
        ghost.transform.position = playerIndicator.transform.position;
        ghost.transform.LookAt(character.transform.position);
        SimpleCameraFollow.Instance.smoothMovement = true;
        Time.timeScale = bodyChangeTimeScale;
        bodyChangeDistance = Vector3.Distance(ghost.transform.position, character.transform.position);
        bodyChangeDistance = (float) Math.Sqrt(bodyChangeDistance);
        FunctionTimer.Create(
                            () => {
                                // Debug.Log("body change successful");
                                ghost.SetActive(false);
                                SimpleCameraFollow.Instance.smoothMovement = false;
                                Time.timeScale=1f;
                                currentlyChangingBodies=false;
                                },
                            bodyChangeTime, "Body Change", false, true
                            );

        UnsetAsPlayer();
        SetAsPlayer(ref character);
        if (currentPlayerType!=character.characterType)
        {
            currentPlayerType = character.characterType;
            Debug.Log("changed types:"+currentPlayerType.ToString());
            AttackAction = abilitySets[(int)currentPlayerType].Attack;
            Ability1Action = abilitySets[(int)currentPlayerType].Ability1;
            Ability2Action = abilitySets[(int)currentPlayerType].Ability2;
            AbilityPassiveAction = abilitySets[(int)currentPlayerType].AbilityPassive;

            ability1Cooldown = abilitySets[(int)currentPlayerType].Ability1Cooldown;
            ability2Cooldown = abilitySets[(int)currentPlayerType].Ability2Cooldown;
            passiveChance = abilitySets[(int)currentPlayerType].PassiveChance;
        }
        
        player.transform.tag = "Player";
    }

    public void DamageTest()
    {
        player.ApplyDamage(2f);
    }


    //******Abilities******
    //[Header("Abilities")]
    private List<IAbilitySet> abilitySets = new List<IAbilitySet>();
    
    // calls the first ability
    public void Attack()
    {
        if (AttackAction!=null)
            AttackAction();
    }

    // calls the first ability
    public void Ability1()
    {
        if (Ability1Action!=null)
            Ability1Action();
    }

    // calls the second ability
    public void Ability2()
    {
        if (Ability2Action!=null)
            Ability2Action();
    }
}
