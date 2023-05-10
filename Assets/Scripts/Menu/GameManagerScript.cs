using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private EntreesManager entreesManager;
    public int NbPoints;
    [SerializeField] TextMeshProUGUI MessageErreur;
    [SerializeField] TextMeshProUGUI NBargent, AccelerationText, VitesseText, FreinageText, EssenceText, VieText, WrenchText;
    

    //[NonSerialized] public int numeroPartie;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);// pour permettre de peser sur escape pour sortir du jeu.
        entreesManager = new EntreesManager();
    }

    public void AmeliorerAccélération()
    {
        if (InformationJeu.inf.ArgentDisponible >= 3 && InformationJeu.inf.upgradeAcceleration< 3)
        {
            InformationJeu.inf.upgradeAcceleration++;
            AccelerationText.text = $"Niveau d'amelioration: {InformationJeu.inf.upgradeAcceleration}";
            InformationJeu.inf.ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {InformationJeu.inf.ArgentDisponible} ";
        }
        else
        {
            if (InformationJeu.inf.upgradeAcceleration == 3)
            {
                MessageErreur.text = "Niveau maximum atteint";
            }
            else
            {
                MessageErreur.text = "Il n'y a pas assez d'argent disponible";
            }
        }
        
    }
    public void AmeliorerForceFreinage()
    {
        if (InformationJeu.inf.ArgentDisponible >= 3 && InformationJeu.inf.upgradeForceFreinage < 3)
        {
            InformationJeu.inf.upgradeForceFreinage++;
            FreinageText.text = $"Niveau d'amelioration: {InformationJeu.inf.upgradeForceFreinage}";
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
    public void AmeliorerVitesseMaximale()
    {
        if (InformationJeu.inf.ArgentDisponible >= 3 && InformationJeu.inf.upgradeVitesseMaximale < 3)
        {
            InformationJeu.inf.upgradeVitesseMaximale++;
            VitesseText.text = $"Niveau d'amelioration: {InformationJeu.inf.upgradeVitesseMaximale}";
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
    public void AmeliorerCapaciteEssenceMaximum()
    {
        if (InformationJeu.inf.ArgentDisponible >= 3 && InformationJeu.inf.upgradeCapaciteEssence < 3)
        {
            InformationJeu.inf.upgradeCapaciteEssence++;
            EssenceText.text = $"Niveau d'amelioration: {InformationJeu.inf.upgradeCapaciteEssence}";
            InformationJeu.inf.ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {InformationJeu.inf.ArgentDisponible} ";
        }
        else
        {
            if (InformationJeu.inf.upgradeCapaciteEssence == 3)
            {
                MessageErreur.text = "Niveau maximum atteint";
            }
            else
            {
                MessageErreur.text = "Il n'y a pas assez d'argent disponible";
            }
        }

    }
    public void AmeliorerVieMaximaleJoueur()
    {
        if (InformationJeu.inf.ArgentDisponible >= 3 && InformationJeu.inf.upgradeVieMaximale < 3)
        {
            InformationJeu.inf.upgradeVieMaximale++;
            VieText.text = $"Niveau d'amelioration: {InformationJeu.inf.upgradeVieMaximale}";
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
            WrenchText.text = $"Niveau d'amelioration: {InformationJeu.inf.upgradeWrench}";
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
    public void ChangerDeScène(int NoScene)
    {
        if (NoScene == 2)
        {
            entreesManager.SauvegarderScoreFinDeJeu();
            SceneManager.LoadScene(NoScene);
            entreesManager.AfficherScoreFinDeJeu();
            //numeroPartie++;
        }

        SceneManager.LoadScene(NoScene);

    }
    private void Update()
    {
        NBargent.text = $"Argent disponible { InformationJeu.inf.ArgentDisponible} ";
    }
}
