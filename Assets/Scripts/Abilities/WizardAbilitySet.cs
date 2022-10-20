using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAbilitySet : IAbilitySet
{
    public float freezeRange = 3.5f;
    public float freezeDuration = 5f;

    public float meteorSpawnY = 5f;
    public int meteorsNum = 3;
    public float meteorSpawnInterval = 0.5f;

    [SerializeField]
    private float passiveChance = 0.2f;
    public float PassiveChance {get{return passiveChance;} set{passiveChance=value;}}
    [SerializeField]
    private bool passiveSuccessful = false;
    public bool PassiveSuccessful {get{return passiveSuccessful;} set{passiveSuccessful=value;}}
    private float ability1Cooldown = 1f;
    public float Ability1Cooldown {get{return ability1Cooldown;} set{ability1Cooldown=value;}}
    private float ability2Cooldown = 1f;
    public float Ability2Cooldown {get{return ability2Cooldown;} set{ability2Cooldown=value;}}

    public static Bullet lastBullet;


        public void Attack()
    {
        Transform firePoint = PlayerController.player.transform.GetChild(0);
        Bullet bullet = PoolsManager.Instance.Get(0, out bool newObjectInstantiated).GetComponent<Bullet>();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        //bullet.transform.SetParent(bulletsParent);

        lastBullet = bullet;
    }
    public void Ability1() // Rain Of Fire
    {
        Vector3 spawnLocation = new Vector3(
            PlayerController.player.transform.position.x,
            meteorSpawnY,
            PlayerController.player.transform.position.z
            );

        SpawnMeteor(spawnLocation, meteorsNum);
    }

    public void Ability2() // Area Freeze
    {
        Collider[] hits = Physics.OverlapSphere(PlayerController.player.transform.position,freezeRange);
        foreach(var hit in hits){
            if(hit.tag == "Enemy"){
                hit.GetComponent<Character>().ApplyFreeze(freezeDuration);
            }
        }
    }

    public void AbilityPassive() // Shoot big FireBall
    {
        Debug.Log("Big is Good!");
        PassiveSuccessful = true;
        if (lastBullet)
            lastBullet.DestroyBullet();
        
        Transform firePoint = PlayerController.player.transform.GetChild(0);
        Bullet bullet = PoolsManager.Instance.Get(4, out bool newObjectInstantiated).GetComponent<Bullet>();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        //bullet.transform.SetParent(parentTransform);
    }

    // called by ability1; spawns a meteorite
    private void SpawnMeteor(Vector3 pos, int meteorsNum=1)
    {
        GameObject meteor = PoolsManager.Instance.Get(3, out bool boo);
        meteor.SetActive(true);
        meteor.transform.position = pos;
        if (meteorsNum>1)
        {
            CodeMonkey.Utils.FunctionTimer.Create(()=>{SpawnMeteor(pos, meteorsNum-1);}, meteorSpawnInterval);
        }
    }
}
