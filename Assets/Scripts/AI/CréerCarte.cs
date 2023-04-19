using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CréerCarte : MonoBehaviour
{
    public GameObject[,] carte;
    public List<GameObject> list;
    public GameObject[,] CréerLaCarte(GameObject roadHelper)
    {
        carte = new GameObject[100, 100];
        int Nbrues = roadHelper.transform.childCount;
        list = new List<GameObject>();
        for (int i = 0; i < Nbrues; i++)
        {
            list.Add(roadHelper.transform.GetChild(i).gameObject);
        }
        foreach (GameObject item in list)
        {
            int x = (int)item.transform.position.x + 50;
            int y = (int)item.transform.position.z + 50;
            carte[x, y] = item;
        }
        return carte;
    }
    
 

}
