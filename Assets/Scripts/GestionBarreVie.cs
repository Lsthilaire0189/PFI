using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class GestionBarreVie : MonoBehaviour
{
    private Slider slider;
    
    [SerializeField]
    private GestionJoueur gestionJoueur;
    private void Awake()
    {
        slider = GameObject.Find("BarreVie").GetComponent<Slider>();
    }

    private void Start()
    {
        slider.maxValue = gestionJoueur.VieMaximaleJoueur;
    }
    void Update()
    {
        slider.value =gestionJoueur.JoueurHP;
    }
    
}
