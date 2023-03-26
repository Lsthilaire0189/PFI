using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GénérationJoueur : MonoBehaviour
{
    
    Camera playerHead;
    [SerializeField]Transform CaméraEmplacement;
    GameObject player;
    private void Awake()
    {
        player = gameObject;
        playerHead = Camera.main;
    }

    public void AssocierCamera()
    {
        float rotationAngleY = playerHead.transform.rotation.eulerAngles.y - CaméraEmplacement.rotation.eulerAngles.y;
        player.transform.Rotate(0, rotationAngleY, 0);
        var distanceDiff = CaméraEmplacement.position - playerHead.transform.position;
        player.transform.position += distanceDiff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
