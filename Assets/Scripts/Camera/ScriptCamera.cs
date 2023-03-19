using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    [SerializeField] Transform Cam�raEmplacement;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;
    private void Awake()
    {
        float rotationAngleY = playerHead.transform.rotation.eulerAngles.y - Cam�raEmplacement.rotation.eulerAngles.y;
        player.transform.Rotate(0, rotationAngleY, 0);
        var distanceDiff =Cam�raEmplacement.position - playerHead.transform.position;
        player.transform.position += distanceDiff; 
    }
   
}
