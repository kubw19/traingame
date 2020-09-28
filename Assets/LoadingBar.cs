using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour {


    public Slider slider;
    private void Start()
    {
        LoadLevel(GameObject.Find("LevelPasser").GetComponent<LevelPasser>().SceneName);
        Destroy(GameObject.Find("LevelPasser"));
    }

    public void LoadLevel(string SceneName)
    {
        StartCoroutine(LoadLevelCoroutine(SceneName));
    }

    IEnumerator LoadLevelCoroutine(string SceneName)
    {
        AsyncOperation Loading = SceneManager.LoadSceneAsync(SceneName);
        Loading.allowSceneActivation = false;
        while (!Loading.isDone)
        {
            float progress = Mathf.Clamp01(Loading.progress / .9f);
            slider.value = progress;
            if (progress == 1f) Loading.allowSceneActivation = true;
            //Debug.Log(progress);
            yield return null;
        }
    }

}
