using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class GestionBarreEssence : MonoBehaviour
{
    [SerializeField] GameObject barreEssence;
    private Slider slider;
    
    [SerializeField]
    private GestionJoueur gestionJoueur;
    
    // Start is called before the first frame update
    private void Awake()
    {
        slider =barreEssence.GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        slider.maxValue = gestionJoueur.CapacitéEssenceMaximale;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value =gestionJoueur.JoueurEssence;
    }
}
