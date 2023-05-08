using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenetreManager : MonoBehaviour
{

    public GameObject scoreBoard;

    // Use this for initialization
    void Start()
    {
        scoreBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            scoreBoard.SetActive(!scoreBoard.activeSelf);
        }

    }

}