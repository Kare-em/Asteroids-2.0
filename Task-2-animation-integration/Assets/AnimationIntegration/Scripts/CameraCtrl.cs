using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    GameObject player;
    public Vector3 CameraPos;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position+ CameraPos;
    }
}
