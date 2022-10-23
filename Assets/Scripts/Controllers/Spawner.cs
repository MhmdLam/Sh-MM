using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private float minPlayerDistance=12f, maxPlayerDistance=15f;
    [SerializeField] private Transform[] parentTransforms;
    [SerializeField] private float spawnInterval = 5f;
    private float timePassed = 0f;
    [SerializeField] private float spawnIntervalMultiplier = 0.95f;
    [SerializeField] private List<SpawnObject> spawnList;
    private float chanceSum;

    private void Start()
    {
        chanceSum = 0;
        foreach (SpawnObject spawnObject in spawnList)
        {
            chanceSum += spawnObject.spawnChance;
        }
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed>=spawnInterval)
        {
            timePassed = 0f;
            SpawnRandom();
            spawnInterval *= spawnIntervalMultiplier;
        }
    }

    private float z=0f, g=0f;

    // spawns a random spawn object based on its chance
    public void SpawnRandom()
    {
        float randomNumber = UnityEngine.Random.Range(0f, chanceSum);
        float tempSum = 0f;

        //foreach (SpawnObject spawnObject in spawnList)
        for (int i=0; i<spawnList.Count; i++)
        {
            tempSum += spawnList[i].spawnChance;

            if (randomNumber<=tempSum)
            {
                if (i==0)
                    z += 1f;
                else
                    g += 1f;

                Vector3 spawnLocation = new Vector3();
                spawnLocation.y = 0f;
                spawnLocation.x = (UnityEngine.Random.Range(0,2)==0 ? 1:-1) * UnityEngine.Random.Range(minPlayerDistance, maxPlayerDistance);
                spawnLocation.z = (UnityEngine.Random.Range(0,2)==0 ? 1:-1) * UnityEngine.Random.Range(minPlayerDistance, maxPlayerDistance);

                bool shouldAdd;
                GameObject obj = PoolsManager.Instance.Get(spawnList[i].poolingID, out shouldAdd);
                obj.SetActive(true);
                obj.transform.position = spawnLocation;
                obj.transform.SetParent(parentTransforms[i]);
                if (shouldAdd)
                    enemyController.characters.Add(obj.GetComponent<Character>());
                //enemyController.characters.Add(Instantiate(spawnList[i].gameObject, spawnLocation, Quaternion.identity, parentTransform).GetComponent<Character>());

                break;
            }
        }
    }


    [System.Serializable]
    public struct SpawnObject
    {
        public GameObject gameObject;
        public int poolingID;
        public float spawnChance;
    }
}
