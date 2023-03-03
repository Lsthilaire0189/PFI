using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;
    [ContextMenu("Reset Position")]
    public void ResetPosition()
    {
        var rotationAngley = playerHead.transform.rotation.eulerAngles.y - resetTransform.rotation.eulerAngles.y;
        player.transform.Rotate(0, rotationAngley, 0);
        var distanceDiff =resetTransform.position - playerHead.transform.position;
        playerHead.transform.position += distanceDiff; 
    }

}
