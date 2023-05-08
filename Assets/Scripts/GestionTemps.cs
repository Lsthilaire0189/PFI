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


    private void Awake()
    {
        gestionEssence =gameObject.GetComponent<GestionEssence>();
        gestionVieJoueur = gameObject.GetComponent<GestionVieJoueur>();
        texte = GameObject.Find("Temps").GetComponent<TextMeshProUGUI>();
        timer = new Stopwatch();
        timer.Start();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {

        texte.text = timer.Elapsed.Seconds.ToString();
        
        if (!gestionEssence.VérifierEssence() || !gestionVieJoueur.VérifierVieJoueur())
        {
            timer.Stop();
        }
    }
}
