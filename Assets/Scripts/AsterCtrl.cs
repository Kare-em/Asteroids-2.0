using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsterCtrl : MonoBehaviour
{
    public float minV = 20;
    public float maxV = 50;
    public GameObject Aster;
    int CountAster = 2;
    bool create = false;
    
    public IEnumerator SpawnAster()
    {
        create = true;
        //Debug.Log(" Запуск сопрограммы в метку времени:" + Time.time);
        yield return new WaitForSeconds(2);
        //Debug.Log("Завершенная сопрограмма на отметке времени:" + Time.time);
        for (int i = 0; i < CountAster; i++)
        {
            CreateAster(3);
        }
        CountAster++;
        //Debug.Log(CountAster);
        create = false;



    }

    public GameObject CreateAster(int state)
    {
        GameObject ast = PoolAster.SharedInstance.GetPooledObject();
        if (ast != null)
        {
            
            ast.transform.position = transform.position;
            ast.SetActive(true);
            ast.GetComponent<Asteroid>().State = state;
            ast.transform.localScale = new Vector3(state * 30, state * 30, 1);
            if (state == 3)
            {
                ast.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(Random.Range(-1f, 1f)) * Random.Range(minV, maxV), Mathf.Sign(Random.Range(-1f, 1f)) * Random.Range(minV, maxV)) ;

            }
        }
        return ast;
    }
    // Update is called once per frame
    void Update()
    {
        GameObject arr = GameObject.FindGameObjectWithTag("Asteroid");
        if (arr==null && !create)
        {
            Debug.Log("new ast");
            StartCoroutine(SpawnAster());

        }
    }
}
