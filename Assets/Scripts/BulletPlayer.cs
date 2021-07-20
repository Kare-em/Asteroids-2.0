using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPlayer : Bullet
{
    private GameObject GameScore;
    void AddScore(int score)
    {
        GameScore.GetComponent<ScoreCtrl>().Scorecount += score;
    }
    protected override void AddObjScore()
    {
        GameScore =GameObject.FindGameObjectWithTag("GameScore");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroid")
        {

            switch (collision.gameObject.GetComponent<Asteroid>().State)
            {
                case 3:
                    AddScore(20);
                    break;
                case 2:
                    AddScore(50);
                    break;
                case 1:
                    AddScore(100);
                    break;
                default:
                    break;
            }
            gameObject.SetActive(false);

        }
        if (collision.tag == "UFO")
        {
            AddScore(200);
            gameObject.SetActive(false);

        }
    }

}
