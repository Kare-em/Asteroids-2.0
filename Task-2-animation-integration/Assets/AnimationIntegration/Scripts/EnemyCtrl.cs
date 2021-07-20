using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    public float DeadTime = 5f;
    public float IdleMaxDist = 30f;

    public BoxCollider MainCollider;
    public Collider[] AllColliders;
    public GameObject Player;
    private Animator animEnemy;

    public void DoRagdoll(bool isRagdoll)
    {
        
        foreach (var col in AllColliders)
        {
            col.enabled = isRagdoll;
        }
        MainCollider.enabled = !isRagdoll;
        GetComponentInChildren<Animator>().enabled = !isRagdoll;
        GetComponent<Rigidbody>().useGravity = !isRagdoll;
    }

    public void Disappear()
    {
        StartCoroutine(Dead());
    }

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MainCollider = GetComponent<BoxCollider>();
        AllColliders = GetComponentsInChildren<Collider>();
        DoRagdoll(false);


    }



    IEnumerator Dead()
    {
        //transform.position += new Vector3(0f, 2f, 0f);
        DoRagdoll(true);
        

        yield return new WaitForSeconds(DeadTime);

        DoRagdoll(false);
        transform.position = new Vector3(Random.Range(-IdleMaxDist, IdleMaxDist), 0.9f, Random.Range(IdleMaxDist, IdleMaxDist));
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        animEnemy.SetBool("Idle", true);
        Player.GetComponent<Attack>().StopAttack();

    }
    private void Start()
    {
        animEnemy = GetComponentInChildren<Animator>();
    }


}
