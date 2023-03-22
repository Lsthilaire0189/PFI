using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CréerCarte : MonoBehaviour
{
    [SerializeField] GameObject roadHelper;
    public int[,] carte;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        carte = new int[100,100];
        int Nbrues = roadHelper.transform.childCount;
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Nbrues; i++)
        {
            list.Add(roadHelper.transform.GetChild(i).gameObject);
        }
    }
}
