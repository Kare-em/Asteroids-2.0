using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : AsterCtrl
{
    private int state; // backing store
    public int State
    {
        get { return state; }
        set
        {
            state = value;
            if (state < 1) gameObject.SetActive(false);

        }
    }
    public AudioSource Explosion;
    // Start is called before the first frame update
    Vector2 vel;
    private void Start()
    {
        Explosion = GameObject.FindGameObjectWithTag("Explosion").GetComponent<AudioSource>();
        vel = GetComponent<Rigidbody2D>().velocity;
    }
    private void Separate()
    {
        State--;
        Explosion.Play();
        transform.localScale = new Vector3(state * 30, state * 30, 1);
        float tmpvel = Random.Range(minV, maxV);
        float ang = 45f;

        GameObject ast = CreateAster(state);

        ast.GetComponent<Rigidbody2D>().velocity = Quaternion.AngleAxis(ang, Vector3.forward) * vel.normalized * tmpvel;
        vel = Quaternion.AngleAxis(ang, -Vector3.forward) * vel.normalized * tmpvel;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Separate();
        }
        else if (collision.tag == "UFO")
        {
            Explosion.Play();
            gameObject.SetActive(false);
        }
    }
    
}
