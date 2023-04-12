using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GénérationJoueur : MonoBehaviour
{
    [SerializeField] Transform CaméraEmplacement;
    GameObject Joueur;

    public void AssocierCamera(GameObject XRorigin)
    {
        Joueur = XRorigin;
        float rotationAngleY = Joueur.transform.rotation.eulerAngles.y - CaméraEmplacement.rotation.eulerAngles.y;
        Joueur.transform.Rotate(0, rotationAngleY, 0);
        var distanceDiff = CaméraEmplacement.position - Joueur.transform.position;
        Joueur.transform.position += distanceDiff;
    }
    // Update is called once per frame
    void Update()
    {
        Joueur.transform.position = CaméraEmplacement.transform.position;
        Joueur.transform.rotation = CaméraEmplacement.transform.rotation;
    }

}