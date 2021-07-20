using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public float AttackDistance;
    public float AttackAngle;
    public GameObject Message;
    public GameObject Gun;
    public GameObject Sword;



    private Animator animPlayer;
    private Mesh tmpMesh;

    public void StopAttack()
    {
        
        Gun.GetComponent<MeshFilter>().sharedMesh = Instantiate(tmpMesh);

        animPlayer.SetBool("Attack", false);
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;

        GetComponent<PlayerCtrl>().enabled = true;
    }

    private void Start()
    {
        animPlayer = GetComponentInChildren<Animator>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Message.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Enemy")
        {
            if (!Message.activeInHierarchy)
                Message.SetActive(true);
            print("gg");
            if (Input.GetKey(KeyCode.Space))
            {
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<PlayerCtrl>().enabled = false;

                tmpMesh = Gun.GetComponent<MeshFilter>().sharedMesh;
                Gun.GetComponent<MeshFilter>().sharedMesh = Instantiate(Sword.GetComponent<MeshFilter>().sharedMesh);

                transform.position = other.transform.position + new Vector3(AttackDistance, 0f, 0f);
                transform.rotation = Quaternion.Euler(0f, AttackAngle, 0f);

                animPlayer.SetBool("Attack", true);

                other.gameObject.GetComponent<EnemyCtrl>().Disappear();

                Message.SetActive(false);
            }
        }


    }
}
