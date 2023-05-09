using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private EntréesManager entréesManager;
    public int NbPoints;
    [SerializeField] TextMeshProUGUI MessageErreur;
    [SerializeField] TextMeshProUGUI NBargent, AccélérationText, VitesseText, FreinageText, EssenceText, VieText, WrenchText;
    

    //[NonSerialized] public int numeroPartie;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);// pour permettre de peser sur escape pour sortir du jeu.
        entréesManager = new EntréesManager();
    }

    public void AméliorerAccélération()
    {
        if (InformationJeu.inf.ArgentDisponible >= 3 && InformationJeu.inf.upgradeAccélération< 3)
        {
            InformationJeu.inf.upgradeAccélération++;
            AccélérationText.text = $"Niveau d'amélioration: {InformationJeu.inf.upgradeAccélération}";
            InformationJeu.inf.ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {InformationJeu.inf.ArgentDisponible} ";
        }
        else
        {
            if (InformationJeu.inf.upgradeAccélération == 3)
            {
                MessageErreur.text = "Niveau maximum atteint";
            }
            else
            {
                MessageErreur.text = "Il n'y a pas assez d'argent disponible";
            }
        }
        
    }
    public void AméliorerForceFreinage()
    {
        if (InformationJeu.inf.ArgentDisponible >= 3 && InformationJeu.inf.upgradeForceFreinage < 3)
        {
            InformationJeu.inf.upgradeForceFreinage++;
            FreinageText.text = $"Niveau d'amélioration: {InformationJeu.inf.upgradeForceFreinage}";
            InformationJeu.inf.ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {InformationJeu.inf.ArgentDisponible} ";
        }
        else
        {
            if (InformationJeu.inf.upgradeForceFreinage== 3)
            {
                MessageErreur.text = "Niveau maximum atteint";
            }
            else
            {
                MessageErreur.text = "Il n'y a pas assez d'argent disponible";
            }
        }
    }
    public void AméliorerVitesseMaximale()
    {
        if (InformationJeu.inf.ArgentDisponible >= 3 && InformationJeu.inf.upgradeVitesseMaximale < 3)
        {
            InformationJeu.inf.upgradeVitesseMaximale++;
            VitesseText.text = $"Niveau d'amélioration: {InformationJeu.inf.upgradeVitesseMaximale}";
            InformationJeu.inf.ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {InformationJeu.inf.ArgentDisponible} ";
        }
        else
        {
            if (InformationJeu.inf.upgradeVitesseMaximale == 3)
            {
                MessageErreur.text = "Niveau maximum atteint";
            }
            else
            {
                MessageErreur.text = "Il n'y a pas assez d'argent disponible";
            }
        }
    }
    public void AméliorerCapacitéEssenceMaximum()
    {
        if (InformationJeu.inf.ArgentDisponible >= 3 && InformationJeu.inf.upgradeCapacitéEssence < 3)
        {
            InformationJeu.inf.upgradeCapacitéEssence++;
            EssenceText.text = $"Niveau d'amélioration: {InformationJeu.inf.upgradeCapacitéEssence}";
            InformationJeu.inf.ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {InformationJeu.inf.ArgentDisponible} ";
        }
        else
        {
            if (InformationJeu.inf.upgradeCapacitéEssence == 3)
            {
                MessageErreur.text = "Niveau maximum atteint";
            }
            else
            {
                MessageErreur.text = "Il n'y a pas assez d'argent disponible";
            }
        }

    }
    public void AméliorerVieMaximaleJoueur()
    {
        if (InformationJeu.inf.ArgentDisponible >= 3 && InformationJeu.inf.upgradeVieMaximale < 3)
        {
            InformationJeu.inf.upgradeVieMaximale++;
            VieText.text = $"Niveau d'amélioration: {InformationJeu.inf.upgradeVieMaximale}";
            InformationJeu.inf.ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {InformationJeu.inf.ArgentDisponible} ";
        }
        else
        {
            if (InformationJeu.inf.upgradeVieMaximale == 3)
            {
                MessageErreur.text = "Niveau maximum atteint";
            }
            else
            {
                MessageErreur.text = "Il n'y a pas assez d'argent disponible";
            }
        }
    }
    public void BonifierWrench()
    {
        if (InformationJeu.inf.ArgentDisponible >= 3 && InformationJeu.inf.upgradeWrench < 3)
        {
            InformationJeu.inf.upgradeWrench++;
            WrenchText.text = $"Niveau d'amélioration: {InformationJeu.inf.upgradeWrench}";
            InformationJeu.inf.ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {InformationJeu.inf.ArgentDisponible} ";
        }
        else
        {
            if (InformationJeu.inf.upgradeWrench== 3)
            {
                MessageErreur.text = "Niveau maximum atteint";
            }
            else
            {
                MessageErreur.text = "Il n'y a pas assez d'argent disponible";
            }
        }

    }

    public void BonifierPompe()
    {
       InformationJeu.inf.upgradePompe += 5;
    }
    public void ChangerDeScène(int NoScéne)
    {
        if (NoScéne == 2)
        {
            entréesManager.SauvegarderScoreFinDeJeu();
            SceneManager.LoadScene(NoScéne);
            entréesManager.AfficherScoreFinDeJeu();
            //numeroPartie++;
        }

        SceneManager.LoadScene(NoScéne);

    }
    private void Update()
    {
        NBargent.text = $"Argent disponible { InformationJeu.inf.ArgentDisponible} ";
    }
}
