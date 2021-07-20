using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCtrl : MonoBehaviour
{
    public Text Score;
    public Text Lives;
    public GameObject Player;
    // Start is called before the first frame update
    private int scorecount;

    public int Scorecount
    {
        get => scorecount;
        set
        {
            scorecount = value;
            Score.text = "Очки: " + scorecount;
        }
    }

    private int livescount;

    public int Livescount
    {
        get => livescount;
        set
        {
            livescount = value;
            Lives.text = "Жизни: " + livescount;
            if (livescount<1)
            {
                Player.GetComponent<ShipCtrl>().StopGame();
            }
        }
    }
    private void Start()
    {
        Scorecount = 0;
        Livescount = Player.GetComponent<ShipCtrl>().Lives;
    }

    
}
