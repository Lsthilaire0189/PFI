using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenetreManager : MonoBehaviour
{

    public GameObject scoreBoard;
    //private LogitechGSDK.DIJOYSTATE2ENGINES rec;
    private void Awake()
    {
        //rec = new LogitechGSDK.DIJOYSTATE2ENGINES();
    }

    // Use this for initialization
    void Start()
    {
        scoreBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //rec = LogitechGSDK.LogiGetStateUnity(0);
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            scoreBoard.SetActive(!scoreBoard.activeSelf);
        }
       
    }
}