using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{
    public float upgradeAccélération;
    public int upgradeVitesseMaximale;
    public int upgradeForceFreinage;
    public int upgradeCapacitéEssence;
    public int upgradeVieMaximale;
    public int upgradeWrench;
    public int upgradePompe;

    public int ArgentDisponible;
    public int NbPoints;
    [SerializeField] TextMeshProUGUI MessageErreur;
    [SerializeField] TextMeshProUGUI NBargent, AccélérationText, VitesseText, FreinageText, EssenceText, VieText, WrenchText;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);// pour permettre de peser sur escape pour sortir du jeu.
    }

    public void AméliorerAccélération()
    {
        if (ArgentDisponible >= 3&& upgradeAccélération<3)
        {
            upgradeAccélération++;
            AccélérationText.text = $"Niveau d'amélioration: {upgradeAccélération}";
            ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {ArgentDisponible} ";
        }
        else
        {
            MessageErreur.text = "Il n'y a pas assez d'argent disponible";
        }
    }
    public void AméliorerForceFreinage()
    {
        if (ArgentDisponible >= 3&& upgradeForceFreinage<3)
        {
            upgradeForceFreinage++;
            FreinageText.text = $"Niveau d'amélioration: {upgradeForceFreinage}";
            ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {ArgentDisponible} ";
        }
        else
        {
            MessageErreur.text = "Il n'y a pas assez d'argent disponible";
        }
    }
    public void AméliorerVitesseMaximale()
    {
        if (ArgentDisponible >= 3&& upgradeVitesseMaximale<3)
        {
            upgradeVitesseMaximale++;
            VitesseText.text = $"Niveau d'amélioration: {upgradeVitesseMaximale}";
            ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {ArgentDisponible} ";
        }
        else
        {
            MessageErreur.text = "Il n'y a pas assez d'argent disponible";
        }
    }
    public void AméliorerCapacitéEssenceMaximum()
    {
        if (ArgentDisponible >= 3&& upgradeCapacitéEssence<3)
        {
            upgradeCapacitéEssence++;
            EssenceText.text = $"Niveau d'amélioration: {upgradeCapacitéEssence}";
            ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {ArgentDisponible} ";
        }
        else
        {
            MessageErreur.text = "Il n'y a pas assez d'argent disponible";
        }

    }
    public void AméliorerVieMaximaleJoueur()
    {
        if (ArgentDisponible >= 3&& upgradeVieMaximale<3)
        {
            upgradeVieMaximale++;
            VieText.text = $"Niveau d'amélioration: {upgradeVieMaximale}";
            ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {ArgentDisponible} ";
        }
        else
        {
            MessageErreur.text = "Il n'y a pas assez d'argent disponible";
        }
    }
    public void BonifierWrench()
    {
        if (ArgentDisponible >= 3&& upgradeWrench<3)
        {
            upgradeWrench ++;
            WrenchText.text = $"Niveau d'amélioration: {upgradeWrench}";
            ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {ArgentDisponible} ";
        }
        else
        {
            MessageErreur.text = "Il n'y a pas assez d'argent disponible";
        }

    }

    public void BonifierPompe()
    {
        upgradePompe += 5;
    }
    public void ChangerDeScène(int NoScéne)
    {
        SceneManager.LoadScene(NoScéne);

    }

}
