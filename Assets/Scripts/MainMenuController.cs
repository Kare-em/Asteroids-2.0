using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject ButtonResume;
    public Text ButCtrlText;
    public GameObject SceneSet;
    public GameObject Ship;


    // Start is called before the first frame update
    void Start()
    {
        SceneSet = GameObject.FindGameObjectWithTag("SceneSet");
        Ship = GameObject.FindGameObjectWithTag("Player");

        Ship.GetComponent<ShipCtrl>().Gameover = false;
        SetMenuOff();

    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!MainMenu.gameObject.activeInHierarchy)
                SetMenuOn();
            else
                SetMenuOff();

        }
    }
    public void SetMenuOn()
    {
        if (!Ship.GetComponent<ShipCtrl>().Gameover)
            ButtonResume.SetActive(true);
        else
            ButtonResume.SetActive(false);
        MainMenu.SetActive(true);
        Time.timeScale = 0;
        Ship.GetComponent<ShipCtrl>().enabled = false;
    }

    //Метод для нажатия на кнопку Продолжить
    public void SetMenuOff()
    {
        Ship.GetComponent<ShipCtrl>().enabled = true;
        //Выключаем меню
        MainMenu.SetActive(false);
        //Запускаем игру дальше
        Time.timeScale = 1f;
    }

    //Метод для нажатия на кнопку Новая игра
    public void StartNewGame()
    {
        //Перезапуск игры
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    //Метод для нажатия на кнопку Управление
    public void ChangeControl()
    {
        if (SceneSet.GetComponent<SceneSettings>().KeyControl)
        {
            SceneSet.GetComponent<SceneSettings>().KeyControl = false;
            ButCtrlText.text = "Управление: клавиатура + мышь";
        }
        else
        {
            SceneSet.GetComponent<SceneSettings>().KeyControl = true;
            ButCtrlText.text = "Управление: клавиатура";
        }

    }
    //Метод для нажатия на кнопку exit
    public void CloseGame()
    {
        //Выключаем игру
        Application.Quit();
    }

}
