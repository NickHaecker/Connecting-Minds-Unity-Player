using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public void LoadLevel(string name)
    {
        //StartCoroutine(LoadAsynchronously(SceneIndex));
        SceneManagerController.LoadSceneSync(name,false);
        
    }

    IEnumerator LoadAsynchronously (int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/9f);
            Debug.Log(operation.progress);
            loadingSlider.value = progress;
            yield return null;
        }
    }
    //https://www.youtube.com/watch?v=YMj2qPq9CP8

}
