using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    public static PoolsManager Instance { get; private set;}
    [SerializeField] private List<GameObjectPool> pools;

    private void Awake()
    {
        Instance = this;
    }

    // returns an object from the pool (Instantiates it if neccessary)
    public GameObject Get(int poolingID)
    {
        return pools[poolingID].Get(out bool temp);
    }
    // returns an object from the pool (Instantiates it if neccessary)
    public GameObject Get(int poolingID, out bool newObjectInstantiated)
    {
        return pools[poolingID].Get(out newObjectInstantiated);
    }

    // returns an object to the pool
    public void ReturnToPool(GameObject objectToReturn, int poolingID)
    {
        pools[poolingID].ReturnToPool(objectToReturn);
    }
}
