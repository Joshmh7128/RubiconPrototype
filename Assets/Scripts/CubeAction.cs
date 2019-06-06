using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAction : MonoBehaviour
{
    private List<GameObject> cubeList;

    private float cubeDiam = 31;
    public bool live = true;
    public bool rotating = false;

    public GameObject pivot;
    public GameObject frontPivot;
    public GameObject backPivot;
    public GameObject bottomPivot;
    public GameObject topPivot;
    public GameObject leftPivot;
    public GameObject rightPivot;

    public string axis;
    public string axisKey;
    public float rotKey;
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
        pickAmount();
        pickSide();
        setupRotation();
        qKey = generateQuaternion(rotKey, axisKey);
        StartCoroutine(rotateCubes(pivot, qKey, rotateTime * multiplier));
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
        gameObjectToMove.transform.rotation = newRot;
        rotating = false;
    }

    public void pickSide()
    {
        int index = Random.Range(1, 6);
        if(index == 1)
        {
            axis = ("front");
            axisKey = ("x");
        }
        else if (index == 2)
        {
            axis = ("back");
            axisKey = ("x");
        }
        else if (index == 3)
        {
            axis = ("bottom");
            axisKey = ("y");
        }
        else if (index == 4)
        {
            axis = ("top");
            axisKey = ("y");
        }
        else if (index == 5)
        {
            axis = ("left");
            axisKey = ("z");
        }
        else
        {
            axis = ("right");
            axisKey = ("z");
        }
    }

    public void pickAmount()
    {
        int index = Random.Range(1, 4);
        rotKey = (float)index;
    }

    public Quaternion generateQuaternion(float amountKey, string axisKey)
    {
        float amount = 90 * amountKey;
        float curAngle = 0;
        Quaternion generated = Quaternion.Euler(0f, 0f, 0f);
        if (axisKey.Equals("x"))
        {
            if (Mathf.Abs(pivot.transform.eulerAngles.x - amount) < 45f || Mathf.Abs(pivot.transform.eulerAngles.x + amount) < 45f)
            {
                return generateQuaternion(amountKey + 1, axisKey);
            }
            curAngle = pivot.transform.eulerAngles.x;
            generated = Quaternion.Euler(amount, 0, 0);
        }
        else if (axisKey.Equals("y"))
        {
            if (Mathf.Abs(pivot.transform.eulerAngles.y - amount) < 45f || Mathf.Abs(pivot.transform.eulerAngles.y + amount) < 45f)
            {
                return generateQuaternion(amountKey + 1, axisKey);
            }
            curAngle = pivot.transform.eulerAngles.y;
            generated = Quaternion.Euler(0, amount, 0);
        }
        else if (axisKey.Equals("z"))
        {
            if (Mathf.Abs(pivot.transform.eulerAngles.z - amount) < 45f || Mathf.Abs(pivot.transform.eulerAngles.z + amount) < 45f)
            {
                return generateQuaternion(amountKey + 1, axisKey);
            }
            curAngle = pivot.transform.eulerAngles.z;
            generated = Quaternion.Euler(0, 0, amount);
        }
        multiplier = Mathf.Abs(rotKey - (curAngle / 90));
        if(multiplier > 1)
        {
            multiplier = 2;
        }
        else
        {
            multiplier = 1;
        }
        return generated;
    }

}
