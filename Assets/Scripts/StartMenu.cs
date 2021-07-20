using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    
    public void StartNewGame()
    {
        //Перезапуск игры
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        Scene newGame=SceneManager.GetSceneByName("SampleScene");
        SceneManager.SetActiveScene(newGame);
        

    }
    public void CloseGame()
    {
        //Выключаем игру
        Application.Quit();
    }
    
}
