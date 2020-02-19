using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Image progressBar;
    public Text tipText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayTip());
        StartCoroutine(LoadAsyncOperation());
    }

    private string RandomTip()
    {
        string tip = "Random Tip";
        int key = Random.Range(1, 28);
        switch(key)
        {
            case 1:
                tip = "text";
                break;
            case 2:
                tip = "text";
                break;
            case 3:
                tip = "text";
                break;
            case 4:
                tip = "text";
                break;
            case 5:
                tip = "text";
                break;
            case 6:
                tip = "text";
                break;
            case 7:
                tip = "text";
                break;
            case 8:
                tip = "text";
                break;
            case 9:
                tip = "text";
                break;
            case 10:
                tip = "text";
                break;
            case 11:
                tip = "text";
                break;
            case 12:
                tip = "text";
                break;
            case 13:
                tip = "text";
                break;
            case 14:
                tip = "text";
                break;
            case 15:
                tip = "text";
                break;
            case 16:
                tip = "text";
                break;
            case 17:
                tip = "text";
                break;
            case 18:
                tip = "text";
                break;
            case 19:
                tip = "text";
                break;
            case 20:
                tip = "text";
                break;
            case 21:
                tip = "text";
                break;
            case 22:
                tip = "text";
                break;
            case 23:
                tip = "text";
                break;
            case 24:
                tip = "text";
                break;
            case 25:
                tip = "text";
                break;
            case 26:
                tip = "text";
                break;
            case 27:
                tip = "text";
                break;
        }
        return tip;
    }

    IEnumerator LoadAsyncOperation()
    {
        tipText.text = RandomTip();
        yield return new WaitForSeconds(0.5f);
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(WhichScene.SceneToLoad);

        while(gameLevel.progress < 1)
        {
            Debug.Log(gameLevel.progress);
            progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator DisplayTip()
    {
        tipText.text = RandomTip();
        while(true)
        {
            yield return new WaitForSeconds(4);
            tipText.text = RandomTip();
        }
    }
}
