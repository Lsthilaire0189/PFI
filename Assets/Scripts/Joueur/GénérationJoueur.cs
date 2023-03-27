using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GénérationJoueur : MonoBehaviour
{   
    Camera playerHead;
    [SerializeField]Transform CaméraEmplacement;
    GameObject player;
    int vieJoueur = 100;
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
    public void VérifierVieJoueur()
    {
        if (vieJoueur == 0)
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        playerHead.transform.position = CaméraEmplacement.transform.position;
        playerHead.transform.rotation = CaméraEmplacement.transform.rotation;
    }
   
}
