using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionBarreVie : MonoBehaviour
{
    Slider barreVie;

    public GameObject voiture;

    private GestionJoueur gestionJoueur;
    
    // Start is called before the first frame update
    private void Awake()
    {
        barreVie = GetComponent<Slider>();
        gestionJoueur = voiture.GetComponent<GestionJoueur>();
        barreVie.maxValue = gestionJoueur.VieMaximaleJoueur;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        print($"player has {gestionJoueur.JoueurHP}");
        barreVie.value =gestionJoueur.JoueurHP;
    }
}
