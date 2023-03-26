using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] GameObject voiture1;
    [SerializeField] GameObject voiture2;
    GameObject Joueur;
    float TempsScore;
    int Score;
    PointageScript pointageScript;
    G�n�rationJoueur g�n�rationJoueur;
    // Start is called before the first frame update
    void Start()
    {
        pointageScript = gameObject.GetComponent<PointageScript>();
    }
    private void Awake()
    {
        Joueur = Instantiate(voiture1, gameObject.transform);
        Joueur.GetComponent<G�n�rationJoueur>().AssocierCamera();
    }
    // Update is called once per frame
    void Update()
    {
        TempsScore = Time.time;
        Score = pointageScript.RetournerPointage(TempsScore);
    }
}
