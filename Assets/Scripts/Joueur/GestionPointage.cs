using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPointage: MonoBehaviour
{
    GestionJoueur gestionJoueur;
    private void Awake()
    {
        gestionJoueur= GetComponent<GestionJoueur>();
    }

    public void ModifierPointage(int NbPoints)
    {
        gestionJoueur.JoueurPointage += NbPoints;
    }
}
