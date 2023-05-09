using System.Collections;
using System.Collections.Generic;

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
        gestionPointage = gameObject.GetComponent<GestionPointage>();
        sceneManagerScript= gameObject.GetComponentInParent<SceneManagerScript>();
        gestionCollision = gameObject.GetComponent<GestionCollision>();
        gestionSurfaceCollision = gameObject.GetComponent<GestionSurfaceCollision>();
        gestionEssence = gameObject.GetComponent<GestionEssence>();

    }
    public void InitierSpécifications()
    {
        déplacementScript.ValeurAccélération += InformationJeu.inf.upgradeAccélération *0.007f;
        déplacementScript.VitesseMaximale += InformationJeu.inf.upgradeVitesseMaximale *10;
        déplacementScript.ValeurForceFreinage += InformationJeu.inf.upgradeForceFreinage *0.05f;
        JoueurHP = VieMaximaleJoueur + InformationJeu.inf.upgradeVieMaximale *3;
        JoueurEssence = CapacitéEssenceMaximale + InformationJeu.inf.upgradeCapacitéEssence *5;
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
    // Update is called once per frame
    void Update()
    {
        if (gestionVieJoueur.VérifierVieJoueur() && gestionEssence.VérifierEssence())
        {
            Joueur.transform.position = CaméraEmplacement.transform.position;
            Joueur.transform.rotation = CaméraEmplacement.transform.rotation;
            //gestionPointage.ModifierPointage(1);
            //Debug.Log(JoueurPointage);
        }
        else
        {
            sceneManagerScript.PartieEstTerminée(JoueurPointage,JoueurArgent);
        }

    }
}