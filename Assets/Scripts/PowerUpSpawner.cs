using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject PowerUp;
    [SerializeField] float CountTime, PowerUpTime;
    void Start()
    {
        CountTime = 0f;
        PowerUpTime = 10f;
    }

    void Update()
    {

        if (CountTime >= PowerUpTime)
        {
            CountTime = 0f;
            SpawnPowerUp();
        }
        else
        {
            CountTime += 1 * Time.deltaTime;
        }
    }
    public void SpawnPowerUp()
    {
        Vector3 RandomPositionSpawn = PlayerController.player.transform.position;
        float RandomX = Random.Range(2f, 5f);
        float RandomZ = Random.Range(2f, 5f);
        RandomPositionSpawn.x += RandomX;
        RandomPositionSpawn.y = 4f;
        RandomPositionSpawn.z += RandomZ;
        GameObject PowerUp1 = Instantiate(PowerUp, RandomPositionSpawn, Quaternion.identity);
        Rigidbody rigidbody = PowerUp1.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.AddForce(new Vector3(0, -10, 0));
    }
}
