using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    public GameObject arena;
    public Material[] mats;
    public Material[] accents;

    private void Awake()
    {
        for(int i = 0; i < arena.transform.childCount; i++)
        {
            GameObject myRoom = arena.transform.GetChild(i).gameObject;
            int randomKey = Random.Range(0, mats.Length);
            myRoom.GetComponent<Renderer>().material = mats[randomKey];
            if(myRoom.transform.Find("Lights") != null)
            {
                GameObject lights = myRoom.transform.Find("Lights").gameObject;
                for(int j = 0; j < lights.transform.childCount; j++)
                {
                    lights.transform.GetChild(j).GetComponent<Light>().color = mats[randomKey].color;
                }

            }
            if (myRoom.transform.Find("Geometry") != null)
            {
                GameObject geom = myRoom.transform.Find("Geometry").gameObject;
                for (int k = 0; k < geom.transform.childCount; k++)
                {
                    geom.transform.GetChild(k).GetComponent<Renderer>().material = mats[randomKey];
                }

            }
            if (myRoom.transform.Find("Accent") != null)
            {
                GameObject accent = myRoom.transform.Find("Accent").gameObject;
                for (int l = 0; l < accent.transform.childCount; l++)
                {
                    int randomAccent = Random.Range(0, accents.Length);
                    accent.transform.GetChild(l).GetComponent<Renderer>().material = mats[randomAccent];
                }

            }
            if (myRoom.transform.Find("Emissive") != null)
            {
                GameObject emissive = myRoom.transform.Find("Emissive").gameObject;
                for (int m = 0; m < emissive.transform.childCount; m++)
                {
                    int randomAccent = Random.Range(0, accents.Length);
                    emissive.transform.GetChild(m).GetComponent<Renderer>().material = accents[randomAccent];
                }

            }
        }
    }
}
