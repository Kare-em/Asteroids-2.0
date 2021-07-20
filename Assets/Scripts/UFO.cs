using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : SceneSettings
{
    public float timeperspawnbegin = 2;
    public float timeperspawnend = 4;
    public float width = 1200;
    public float height = 1000;
    public float minV = 20;
    public float maxV = 40;
    public float minTperShot = 2;
    public float maxTperShot = 5;
    public AudioSource Explosion;
    public float shellvel = 100;//скорость снаряда
    public GameObject Ship;
    public AudioClip ShotAudio;

    private bool isActive = false;

    void Start()
    {
        Explosion = GameObject.FindGameObjectWithTag("Explosion").GetComponent<AudioSource>();

        Ship = GameObject.FindGameObjectWithTag("Player");
        Respawn();
    }
    IEnumerator WaitSpawnUFO()
    {
        isActive = false;
        //
        float waittime = Random.Range(timeperspawnbegin, timeperspawnend);

        yield return new WaitForSeconds(waittime);
        isActive = true;
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine(ShotUFO());
        

        float vector= Mathf.Sign(Random.Range(-1f, 1f));
        transform.position = new Vector3(-vector * (width / 2 - transform.localScale.x), Random.Range(-height / 2 * 0.6f, height / 2 * 0.6f), 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(minV, maxV) * vector, 0);

    }
    public void Respawn()
    {
        StartCoroutine(WaitSpawnUFO());

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Asteroid" || collision.tag == "Bullet")
        {
            Explosion.Play();
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Respawn();
            GetComponent<AudioSource>().PlayOneShot(ShotAudio);
        }
    }

    public IEnumerator ShotUFO()
    {
        while (isActive)
        {
            GetComponent<AudioSource>().PlayOneShot(ShotAudio);
            //Debug.Log("Завершенная сопрограмма на отметке времени:" + Time.time);
            GameObject bullet = PoolUFO.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody2D>().velocity = (Ship.transform.position - transform.position).normalized * shellvel;
            }
            yield return new WaitForSeconds(Random.Range(minTperShot, maxTperShot));
        }


    }
    // Update is called once per frame
    void Update()
    {

    }
}
