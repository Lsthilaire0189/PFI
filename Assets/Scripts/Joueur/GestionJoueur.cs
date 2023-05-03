using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class GestionJoueur : MonoBehaviour
{
    GameObject Joueur;

    [SerializeField] Transform CaméraEmplacement;

    DéplacementScript déplacementScript;
    GestionVieJoueur gestionVieJoueur;
    GestionPointage gestionPointage;
    SceneManagerScript sceneManagerScript;
    GestionCollision gestionCollision; 
    GestionSurfaceCollision gestionSurfaceCollision;

    public int JoueurEssence;
    public int JoueurHP;
    public int JoueurArgent;
    public int JoueurPointage;

    public int VieMaximaleJoueur = 10;
    public int CapacitéEssenceMaximale = 100;

    public bool FinPartie = true;


    public void Awake()
    {
        déplacementScript = gameObject.GetComponent<DéplacementScript>();
        gestionVieJoueur = gameObject.GetComponent<GestionVieJoueur>();
        gestionPointage = gameObject.GetComponent<GestionPointage>();
        sceneManagerScript= gameObject.GetComponentInParent<SceneManagerScript>();
        gestionCollision = gameObject.GetComponent<GestionCollision>();
        gestionCollision = gameObject.GetComponent<GestionCollision>();
        gestionSurfaceCollision = gameObject.GetComponent<GestionSurfaceCollision>();

    }
    public void InitierSpécifications(float UpgradeAccélération, int UpgradeVitesseMaximale, float UpgradeForceFreinage, int UpgradeEssence, int UpgradeVie, int UpgradeGainEssence, int UpgradeGainVie)
    {
        déplacementScript.ValeurAccélération += UpgradeAccélération;
        déplacementScript.VitesseMaximale += UpgradeVitesseMaximale;
        déplacementScript.ValeurForceFreinage += UpgradeForceFreinage;
        JoueurHP = VieMaximaleJoueur + UpgradeVie;
        JoueurEssence = CapacitéEssenceMaximale + UpgradeEssence;
        gestionCollision.gainEssence += UpgradeGainEssence;
        gestionCollision.gainHP += UpgradeGainVie;
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
        if (gestionVieJoueur.VérifierVieJoueur())
        {
            Joueur.transform.position = CaméraEmplacement.transform.position;
            Joueur.transform.rotation = CaméraEmplacement.transform.rotation;
            //gestionPointage.ModifierPointage(1);
            Debug.Log(JoueurPointage);
        }
        else
        {
            sceneManagerScript.PartieEstTerminée(JoueurPointage,JoueurArgent);
        }

    }
}