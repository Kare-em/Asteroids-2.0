using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Ship;
    public GameObject SceneSet;
    Vector3 prepos;
    
    float livetime;
    // Start is called before the first frame update
    void Start()
    {
        Ship = GameObject.FindGameObjectWithTag("Player");
        SceneSet = GameObject.FindGameObjectWithTag("SceneSet");
        
        livetime = 0;
        prepos = transform.position;
        AddObjScore();
    }
    protected virtual void AddObjScore()
    {

    }
    // Update is called once per frame
    private void Update()
    {
        livetime += Time.deltaTime;
        if (livetime > SceneSet.GetComponent<SceneSettings>().Width / Ship.GetComponent<ShipCtrl>().shellvel)
        {
            livetime = 0;
            gameObject.SetActive(false);
        }

    }
}
