using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XControl : MonoBehaviour
{
    float border = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = new Vector3(-collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
