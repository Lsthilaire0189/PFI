using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    GameObject GameManager;
    GameManagerScript gameManagerScript;

    [SerializeField] GameObject voiture1;
    [SerializeField] GameObject XrOrigin;
    [SerializeField] GameObject RoadHelper;
    [SerializeField] List<GameObject> NPCVoitures;
    [SerializeField] GameObject Police;
    CréerCarte créerCarte;
    public GameObject[,] Carte;
    public List<GameObject> ListePoints;

    public int NbAutos = 5;
    GameObject Joueur;

    
    void Start()
    {
        créerCarte = gameObject.GetComponent<CréerCarte>();
        StartCoroutine(CréerUneCarte());
    }
    
    private void Awake()
    {
        GameManager = GameObject.Find("GameManager");
        gameManagerScript = GameManager.GetComponent<GameManagerScript>();
        InstantierJoueur();
    }
    IEnumerator CréerUneCarte()
    {
        yield return new WaitForSeconds(.5f);
        Carte = créerCarte.CréerLaCarte(RoadHelper);
        ListePoints = créerCarte.list;
        InstantierNPC();
    }
    void InstantierJoueur()
    {
        Joueur = Instantiate(voiture1, Vector3.zero, Quaternion.identity, gameObject.transform);
        Joueur.GetComponent<GestionJoueur>().AssocierCamera(XrOrigin);
        Joueur.GetComponent<GestionJoueur>().InitierSpécifications(gameManagerScript.upgradeAccélération, gameManagerScript.upgradeVitesseMaximale, gameManagerScript.upgradeForceFreinage, gameManagerScript.upgradeCapacitéEssence, gameManagerScript.upgradeVieMaximale, gameManagerScript.upgradePompe, gameManagerScript.upgradeWrench);
    }
    
    public void InstantierNPC()
    {
        for (int i = 0; i < NbAutos; i++)
        {
            int NoVoiture = Random.Range(0, NPCVoitures.Count);
            GameObject voiture = Instantiate(NPCVoitures[NoVoiture], Vector3.zero, Quaternion.identity, gameObject.transform);
        }
        Instantiate(Police,new Vector3(0,0.01f,0), Quaternion.identity, gameObject.transform);
    }
    public void PartieEstTerminée(int NbPoints, int argent)
    {

       
        gameManagerScript.ArgentDisponible = argent;
        gameManagerScript.NbPoints = NbPoints;
        
        gameManagerScript.ChangerDeScène(0);
    }
}
