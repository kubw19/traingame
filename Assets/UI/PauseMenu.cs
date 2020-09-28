using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

  

    public void PauseClick()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        TrainGame.Generator().PreventFromZoom = true;
    }
    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        TrainGame.Generator().PreventFromZoom = false;
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        TrainGame.Generator().SaveToDisc();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
