using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class GestionTemps : MonoBehaviour
{
    private TextMeshProUGUI texte;

    [NonSerialized]
    public Stopwatch timer;

    private GestionEssence gestionEssence;

    private GestionVieJoueur gestionVieJoueur;

    private EntreesManager entreesManager;
    private GameObject sceneManager;
    private SceneManagerScript sceneManagerScript;
    private int tempsPresent;
    private int tempsPrecedent;

    private void Awake()
    {
        gestionEssence =gameObject.GetComponent<GestionEssence>();
        gestionVieJoueur = gameObject.GetComponent<GestionVieJoueur>();
        texte = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        timer = new Stopwatch();
        timer.Start();
        sceneManager = GameObject.Find("SceneManager");
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        tempsPresent = timer.Elapsed.Seconds;
        texte.text = tempsPresent.ToString();
        
        if (!gestionEssence.VérifierEssence() || !gestionVieJoueur.VérifierVieJoueur())
        {
            timer.Stop();
        }
        ScoreManager.ChangerTypeScore(ScoreManager.scoreJoueurs[sceneManagerScript.getNumeroPartie()], tempsPresent-tempsPrecedent, "Temps");
        tempsPrecedent = tempsPresent;


    }
}
