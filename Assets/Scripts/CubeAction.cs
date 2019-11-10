using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAction : MonoBehaviour
{
    private List<GameObject> cubeList;

    private float cubeDiam = 31;
    public bool live = false;
    private bool rotating = false;

    public GameObject pivot;
    public GameObject frontPivot;
    public GameObject backPivot;
    public GameObject bottomPivot;
    public GameObject topPivot;
    public GameObject leftPivot;
    public GameObject rightPivot;

    public string axis;
    public string axisKey;
    public int lastAxis;
    public float multiplier;

    private Quaternion qKey;

    public float rotateTime;

    // Start is called before the first frame update
    void Start()
    {
        cubeList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Arena"));
    }

    // Update is called once per frame
    void Update()
    {
        if(!rotating && live)
        {
            executeRotation();
        }
    }

    public void executeRotation()
    {
        pickSide();
        setupRotation();
        qKey = generateQuaternion(axisKey);
        StartCoroutine(rotateCubes(pivot, qKey, rotateTime));
    }

    public void shuffle(int shuffles)
    {
        for(int i = 0; i < shuffles; i++)
        {
            pickSide();
            setupRotation();
            qKey = generateQuaternion(axisKey);
            instantRotateCubes(pivot, qKey);
        }
        //live = true;
    }

    public void setupRotation()
    {
        for (int i = 0; i < cubeList.Count; i++)
        {
            if (axis.Equals("front"))
            {
                pivot = frontPivot;
                if (cubeList[i].gameObject.transform.position.x < (cubeDiam * -1))
                {
                    cubeList[i].gameObject.transform.SetParent(pivot.transform);
                }
            }
            else if (axis.Equals("back"))
            {
                pivot = backPivot;
                if (cubeList[i].gameObject.transform.position.x > (cubeDiam))
                {
                    cubeList[i].gameObject.transform.SetParent(pivot.transform);
                }
            }
            else if (axis.Equals("bottom"))
            {
                pivot = bottomPivot;
                if (cubeList[i].gameObject.transform.position.y < (cubeDiam * -1))
                {
                    cubeList[i].gameObject.transform.SetParent(pivot.transform);
                }
            }
            else if (axis.Equals("top"))
            {
                pivot = topPivot;
                if (cubeList[i].gameObject.transform.position.y > (cubeDiam))
                {
                    cubeList[i].gameObject.transform.SetParent(pivot.transform);
                }
            }
            else if (axis.Equals("left"))
            {
                pivot = leftPivot;
                if (cubeList[i].gameObject.transform.position.z > (cubeDiam))
                {
                    cubeList[i].gameObject.transform.SetParent(pivot.transform);
                }
            }
            else if (axis.Equals("right"))
            {
                pivot = rightPivot;
                if (cubeList[i].gameObject.transform.position.z < (cubeDiam * -1))
                {
                    cubeList[i].gameObject.transform.SetParent(pivot.transform);
                }
            }
        }
    }

    IEnumerator rotateCubes(GameObject gameObjectToMove, Quaternion newRot, float duration)
    {
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        Quaternion currentRot = gameObjectToMove.transform.rotation;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.rotation = Quaternion.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        rotating = false;
    }

    public void instantRotateCubes(GameObject gameObjectToMove, Quaternion newRot)
    {
        gameObjectToMove.transform.rotation = newRot;
    }

    public void pickSide()
    {
        int index = Random.Range(1, 7);
        while(index == lastAxis)
        {
            index = Random.Range(1, 7);
        }
        if(index == 1)
        {
            axis = ("front");
            axisKey = ("x");
            lastAxis = 1;
        }
        else if (index == 2)
        {
            axis = ("back");
            axisKey = ("x");
            lastAxis = 2;
        }
        else if (index == 3)
        {
            axis = ("bottom");
            axisKey = ("y");
            lastAxis = 3;
        }
        else if (index == 4)
        {
            axis = ("top");
            axisKey = ("y");
            lastAxis = 4;
        }
        else if (index == 5)
        {
            axis = ("left");
            axisKey = ("z");
            lastAxis = 5;
        }
        else
        {
            axis = ("right");
            axisKey = ("z");
            lastAxis = 6;
        }
    }

    public Quaternion generateQuaternion(string axisKey)
    {
        float amount = 90;
        float curAngle = 0;
        Quaternion generated = Quaternion.Euler(0f, 0f, 0f);
        if (axisKey.Equals("x"))
        {
            if (pivot.transform.eulerAngles.x == 90)
            {
                amount = 0;
            }
            curAngle = pivot.transform.eulerAngles.x;
            generated = Quaternion.Euler(amount, 0, 0);
        }
        else if (axisKey.Equals("y"))
        {
            if (pivot.transform.eulerAngles.y == 90)
            {
                amount = 0;
            }
            curAngle = pivot.transform.eulerAngles.y;
            generated = Quaternion.Euler(0, amount, 0);
        }
        else if (axisKey.Equals("z"))
        {
            if (pivot.transform.eulerAngles.z == 90)
            {
                amount = 0;
            }
            curAngle = pivot.transform.eulerAngles.z;
            generated = Quaternion.Euler(0, 0, amount);
        }
        return generated;
    }

}
