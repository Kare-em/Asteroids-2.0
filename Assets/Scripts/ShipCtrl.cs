using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCtrl : MonoBehaviour
{
    public GameObject SceneSet;
    public Camera Camera;
    public GameObject MainMenu;
    public GameObject GameScore;

    public float shipac = 2000.0f;//ускорение корабля
    public float angrot = 0.5f;//скорость вращения корабля
    public float maxv = 500.0f;//макс v корабля
    public float shellvel;//скорость снаряда
    public float timebetweenshots = 1 / 3f;
    public float SpawningTime = 3f;
    public float BlinkTime = 0.5f;
    public int Lives = 5;//кол-во жизней

    public AudioClip ShotAudio;
    public AudioClip MoveAudio;
    public AudioSource Explosion;

    public bool Gameover = false;


    private GameObject LivesTab;
    private Transform SpawnShip;
    private Rigidbody2D rbShip;

    private float realshottime = 0f;

    private bool ready = true;//готовность


    // Start is called before the first frame update
    void Start()
    {
        GameScore = GameObject.FindGameObjectWithTag("GameScore");
        SpawnShip = GameObject.FindGameObjectWithTag("Respawn").transform;
        LivesTab = GameObject.FindGameObjectWithTag("Lives");
        rbShip = GetComponent<Rigidbody2D>();
        Explosion = GameObject.FindGameObjectWithTag("Explosion").GetComponent<AudioSource>();
        Respawn();
    }

    private IEnumerator SpawnBlink()
    {
        for (int i = 0; i < SpawningTime / BlinkTime - 1f; i++)
        {
            GetComponent<MeshRenderer>().enabled = false;

            yield return new WaitForSeconds(BlinkTime);
            GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(BlinkTime);

        }
    }
    private IEnumerator SpawnDefence()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        print(Time.time);
        yield return new WaitForSeconds(SpawningTime);
        print(Time.time);
        GetComponent<BoxCollider2D>().enabled = true;
    }
    private void Respawn()
    {
        GameScore.GetComponent<ScoreCtrl>().Livescount--;
        transform.position = SpawnShip.position;
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        rbShip.velocity = new Vector2(0f, 0f);
        StartCoroutine(SpawnBlink());
        StartCoroutine(SpawnDefence());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Asteroid" || collision.tag == "BulletUFO" || collision.tag == "UFO")
        {
            Explosion.Play();
            Respawn();
        }
    }

    public void StopGame()
    {

        Gameover = true;
        //включаем меню
        Camera.GetComponent<MainMenuController>().SetMenuOn();

    }

    public void Shot()
    {
        GetComponent<AudioSource>().PlayOneShot(ShotAudio);
        GameObject bullet = PoolPlayer.SharedInstance.GetPooledObject();
        if (bullet != null && ready)
        {
            ready = false;

            bullet.transform.position = transform.position;
            bullet.transform.rotation = new Quaternion(0, 0, 0, 0);

            //Debug.Log(transform.forward + " " + shell.rbShip.velocity.magnitude);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().velocity = transform.up * shellvel;
        }

    }
    private void RotateTowardMouse()
    {
        Vector3 moveDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 moveDirection = (moveDir - transform.position);
        var angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.Euler(0, 0, angle - 90f);
        Quaternion ang = Quaternion.Slerp(transform.rotation, targetRotation, Mathf.Deg2Rad * angrot * Time.deltaTime);
        transform.rotation = ang;
    }

    private void AccelerateShip()
    {
        rbShip.AddForce(transform.up * shipac * Time.deltaTime);
        if (rbShip.velocity.magnitude > maxv)
        {
            rbShip.velocity = rbShip.velocity.normalized * maxv;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("v=" + rbShip.velocity.magnitude);
        if (!ready)
        {
            realshottime += Time.deltaTime;
            if (realshottime > timebetweenshots)
            {


                ready = true;
                realshottime = 0f;
            }
        }


        if (SceneSet.GetComponent<SceneSettings>().KeyControl)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                AccelerateShip();
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                GetComponent<AudioSource>().Play();
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                GetComponent<AudioSource>().Stop();
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, 0, angrot * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, 0, -angrot * Time.deltaTime);

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shot();
            }
        }
        else
        {
            //Поворот за мышью
            RotateTowardMouse();

            //Кнопка W или стрелка вверх или правая кнопка мыши - ускорение.
            //Пробел или левая кнопка мыши - выстрел.

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(1))
            {
                AccelerateShip();
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButton(1))
            {
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }


            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Shot();
            }

        }
    }
}
