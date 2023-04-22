using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class GestionJoueur : MonoBehaviour
{
    GameObject Joueur;

    [SerializeField] Transform CaméraEmplacement;
    [SerializeField] public GameObject PointFaible;

    DéplacementScript déplacementScript;
    GestionEssence gestionEssence;
    GestionVieJoueur gestionVieJoueur;


    public int JoueurEssence;
    public int JoueurHP = 5;
    public int JoueurArgent;


    public int VieMaximaleJoueur;
    public int CapacitéEssenceMaximale;

    public bool FinPartie = true;

    public void Awake()
    {
        déplacementScript = gameObject.GetComponent<DéplacementScript>();
        gestionVieJoueur = gameObject.GetComponent<GestionVieJoueur>();
        gestionEssence = gameObject.GetComponent<GestionEssence>();
    }
    public void InitierSpécifications(int UpgradeAccélération, int UpgradeVie, int UpgradeEssence, int UpgradeVitesseMaximale)
    {
        //déplacementScript.ValeurAccélération += UpgradeAccélération;
        //déplacementScript.VitesseMaximum += UpgradeVitesseMaximale;
    }

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
        gestionVieJoueur.VérifierVieJoueur();
    }
}