using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] GameObject voiture1;
    [SerializeField] GameObject voiture2;
    [SerializeField] GameObject XrOrigin;
    [SerializeField] GameObject RoadHelper;
    [SerializeField] List<GameObject> NPCVoitures;
    Cr�erCarte cr�erCarte;
    public GameObject[,] Carte;
    public List<GameObject> ListePoints;
    public int NbAutos = 5;
    GameObject Joueur;
    float TempsScore;
    int Score;
    PointageScript pointageScript;

    // Start is called before the first frame update
    void Start()
    {
        pointageScript = gameObject.GetComponent<PointageScript>();
        cr�erCarte = gameObject.GetComponent<Cr�erCarte>();
        StartCoroutine(Cr�erUneCarte());
        InstantierJoueur();

    }
    
    private void Awake()
    {
        
    }
    IEnumerator Cr�erUneCarte()
    {
        yield return new WaitForSeconds(.5f);
        Carte = cr�erCarte.Cr�erLaCarte(RoadHelper);
        ListePoints = cr�erCarte.list;
        InstantierNPC();
    }
    void InstantierJoueur()
    {
        Joueur = Instantiate(voiture1, Vector3.zero, Quaternion.identity, gameObject.transform);
        Joueur.GetComponent<GestionJoueur>().AssocierCamera(XrOrigin);
        Joueur.GetComponent<GestionJoueur>().InitierSp�cifications(2, 2, 2, 2);
    }
    public void InstantierNPC()
    {
        for (int i = 0; i < NbAutos; i++)
        {
            int NoVoiture = Random.Range(0, NPCVoitures.Count);
            GameObject voiture = Instantiate(NPCVoitures[NoVoiture], Vector3.zero, Quaternion.identity, gameObject.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {

        //TempsScore = Time.time;
        //Score = pointageScript.RetournerPointage(TempsScore);
    }
}
