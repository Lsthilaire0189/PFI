using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneManagerScript : MonoBehaviour
{

    [SerializeField] GameObject voiture1;
    [SerializeField] GameObject XrOrigin;
    [SerializeField] GameObject RoadHelper;
    [SerializeField] List<GameObject> NPCVoitures;
    [SerializeField] GameObject Police;
    CréerCarte créerCarte;
    public GameObject[,] Carte;
    public List<GameObject> ListePoints;

    public int NbAutos = 5;
    public int NbAutosActuel;
    GameObject Joueur;
    [NonSerialized] public static int numeroPartie;

    private static int compteurInitial;


    void Start()
    {
        créerCarte = gameObject.GetComponent<CréerCarte>();
        StartCoroutine(CréerUneCarte());
    }

    public int getNumeroPartie() => numeroPartie;

    private void Awake()
    {
        InstantierJoueur();
        if (compteurInitial == 0)
        {
            numeroPartie = -1;
        }
        numeroPartie++;
        compteurInitial++;
        ScoreManager.SetJoueur($"partie: {numeroPartie}", 0, 0);
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
        Joueur.GetComponent<GestionJoueur>().InitierSpécifications();
    }

    public void InstantierNPC()
    {
        for (int i = 0; i < NbAutos; i++)
        {
            int NoVoiture = Random.Range(0, NPCVoitures.Count);
            GameObject voiture = Instantiate(NPCVoitures[NoVoiture], Vector3.zero, Quaternion.identity,
                gameObject.transform);
        }

        Instantiate(Police, new Vector3(0, 0.01f, 0), Quaternion.identity, gameObject.transform);
    }

    public void PartieEstTerminée(int NbPoints, int argent)
    {
        InformationJeu.inf.ArgentDisponible = argent ;
        Task.Delay(2);
        SceneManager.LoadScene(0);
    }
}