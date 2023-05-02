using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class AffichageVitesse : MonoBehaviour
{

    private TextMeshProUGUI texte;

    private Rigidbody rb;

    private void Awake()
    {
        texte = GameObject.Find("Vitesse").GetComponent<TextMeshProUGUI>();
        rb =GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        texte.text =  ((int)(rb.velocity.magnitude*100)).ToString();
    }
}
