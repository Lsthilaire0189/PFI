using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class GestionBarreEssence : MonoBehaviour
{
    private Slider slider;
    
    [SerializeField]
    private GestionJoueur gestionJoueur;
    private void Awake()
    {
        slider = GameObject.Find("BarreEssence").GetComponent<Slider>();
    }

    private void Start()
    {
        slider.maxValue = gestionJoueur.CapacitéEssenceMaximale;
    }
    void Update()
    {
        slider.value =gestionJoueur.JoueurEssence;
    }
}
