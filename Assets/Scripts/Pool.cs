using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    // Start is called before the first frame update
    
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    
    protected virtual Color GetColor()
    {
        return new Color(0, 1.0f, 0);
    }
    protected virtual void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        objectToPool.GetComponent<SpriteRenderer>().color = GetColor();
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);

            tmp.SetActive(false);

            pooledObjects.Add(tmp);
        }
    }
    public virtual GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    
}
