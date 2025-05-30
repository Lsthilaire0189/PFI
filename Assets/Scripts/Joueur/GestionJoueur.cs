using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GestionJoueur : MonoBehaviour
{
    GameObject Joueur;

    [SerializeField] Transform CaméraEmplacement;

    DéplacementScript déplacementScript;
    GestionVieJoueur gestionVieJoueur;
    SceneManagerScript sceneManagerScript;
    GestionCollision gestionCollision; 
    GestionSurfaceCollision gestionSurfaceCollision; 
    GestionEssence gestionEssence;

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
        sceneManagerScript= gameObject.GetComponentInParent<SceneManagerScript>();
        gestionCollision = gameObject.GetComponent<GestionCollision>();
        gestionSurfaceCollision = gameObject.GetComponent<GestionSurfaceCollision>();
        gestionEssence = gameObject.GetComponent<GestionEssence>();
    }
    public void InitierSpécifications()
    {
        déplacementScript.ValeurAccélération += InformationJeu.inf.upgradeAcceleration *0.007f;
        déplacementScript.VitesseMaximale += InformationJeu.inf.upgradeVitesseMaximale *10;
        déplacementScript.ValeurForceFreinage += InformationJeu.inf.upgradeForceFreinage *0.05f;
        JoueurHP = VieMaximaleJoueur + InformationJeu.inf.upgradeVieMaximale *3;
        JoueurEssence = CapacitéEssenceMaximale + InformationJeu.inf.upgradeCapaciteEssence *5;
        gestionCollision.gainEssence += InformationJeu.inf.upgradePompe* 5;
        gestionCollision.gainHP += InformationJeu.inf.upgradeWrench* 2;
    }
    public void AssocierCamera(GameObject XRorigin)
    {
        Joueur = XRorigin;
        float rotationAngleY = Joueur.transform.rotation.eulerAngles.y - CaméraEmplacement.rotation.eulerAngles.y;
        Joueur.transform.Rotate(0, rotationAngleY, 0);
        var distanceDiff = CaméraEmplacement.position - Joueur.transform.position;
        Joueur.transform.position += distanceDiff;
    }
    void Update()
    {
        if (gestionVieJoueur.VérifierVieJoueur() && gestionEssence.VérifierEssence())
        {
            Joueur.transform.position = CaméraEmplacement.transform.position;
            Joueur.transform.rotation = CaméraEmplacement.transform.rotation;
        }
        else
        {
            sceneManagerScript.PartieEstTerminée(JoueurPointage,JoueurArgent);
        }

    }
}