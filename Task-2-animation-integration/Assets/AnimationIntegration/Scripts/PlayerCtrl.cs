using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Transform RotBone;
    // speed is the rate at which the object will rotate
    public float PlayerVel = 30;



    private Animator animPlayer;
    private Vector3 moveVector;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        animPlayer = GetComponentInChildren<Animator>();
    }
    
    
    private void RotateTowardMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);
            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            // Smoothly rotate towards the target point.
            //Quaternion ang = Quaternion.Slerp(transform.rotation, targetRotation , speed * Time.deltaTime);
            //Debug.Log(ang);
            RotBone.rotation = targetRotation * parent.transform.localRotation;
        }
    }

    void LateUpdate()
    {
        RotateTowardMouse();
    }
    // Update is called once per frame
    void Update()
    {
        moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveVector.z = PlayerVel;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector.z = -PlayerVel;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector.x = -PlayerVel;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector.x = PlayerVel;
        }
        
        
        if (moveVector == Vector3.zero)
        {
            animPlayer.SetBool("Move", false);
            moveVector = transform.forward;
        }
        else
        {
            animPlayer.SetBool("Move", true);
            transform.position += moveVector * Time.deltaTime;
        }
        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, PlayerVel, 0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

    }
}