using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    [SerializeField] Transform CaméraEmplacement;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;
    private void Awake()
    {
        float rotationAngleY = playerHead.transform.rotation.eulerAngles.y - CaméraEmplacement.rotation.eulerAngles.y;
        player.transform.Rotate(0, rotationAngleY, 0);
        var distanceDiff =CaméraEmplacement.position - playerHead.transform.position;
        player.transform.position += distanceDiff; 
    }
   
}
