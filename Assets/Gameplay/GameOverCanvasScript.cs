using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCanvasScript : MonoBehaviour {

    public GameObject Reason, Score, Message;

    public void ButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TrainGame.Generator().PreventFromZoom = false;
    }
}
