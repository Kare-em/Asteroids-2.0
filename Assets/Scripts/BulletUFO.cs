using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUFO : Bullet
{
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroid" || collision.tag == "Player")
            gameObject.SetActive(false);
    }
    // Update is called once per frame
    
}
