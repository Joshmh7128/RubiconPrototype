using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Transform mainCenter;
    public Transform optionsCenter;
    public Transform creditsCenter;
    public Transform target;

    public Button playButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button exitButton;

    public Transform cameraPos;

    private void Start()
    {
        target = mainCenter;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos.position = target.position;
        Vector3.Lerp(cameraPos.position, target.position, 1f);
    }

    public void SetTarget(int targetPos)
    {
        switch (targetPos)
        {
            case 1:
                target = mainCenter;
                break;

            case 2:
                target = optionsCenter;
                break;

            case 3:
                target = creditsCenter;
                break;
        }
    }
}
