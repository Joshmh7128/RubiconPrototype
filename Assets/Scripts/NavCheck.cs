using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavCheck : MonoBehaviour
{
    private Button myButton;
    public GameObject activeChecker;
    private Navigation nav;
    public Button selectIfActive;
    public Button selectIfInactive;

    private void Start()
    {
        myButton = this.GetComponent<Button>();
        nav = myButton.navigation;
    }

    private void Update()
    {
        if(activeChecker.activeInHierarchy)
        {
            nav.selectOnDown = selectIfActive;
        }
        else
        {
            nav.selectOnDown = selectIfInactive;
        }
    }

}
