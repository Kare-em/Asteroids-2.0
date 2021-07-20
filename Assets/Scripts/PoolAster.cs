using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAster : Pool
{
    // Start is called before the first frame update

    public static PoolAster SharedInstance;
    protected void Awake()
    {
        SharedInstance = this;
    }
    protected override void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);

            tmp.SetActive(false);

            pooledObjects.Add(tmp);
        }
    }
}
