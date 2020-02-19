using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Image progressBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(WhichScene.SceneToLoad);

        while(gameLevel.progress < 1)
        {
            Debug.Log(gameLevel.progress);
            progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
