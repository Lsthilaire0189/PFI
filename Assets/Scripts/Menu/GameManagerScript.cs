using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{
    public int upgradeAcc�l�ration;
    public int upgradeVitesseMaximale;
    public int upgradeForceFreinage;
    public int upgradeCapacit�Essence;
    public int upgradeVieMaximale;
    public int upgradeWrench;

    public int ArgentDisponible;
    public int NbPoints;
    [SerializeField] TextMeshProUGUI MessageErreur;
    [SerializeField] TextMeshProUGUI NBargent, Acc�l�rationText, VitesseText, FreinageText, EssenceText, VieText, WrenchText;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);// pour permettre de peser sur escape pour sortir du jeu.
    }

    public void Am�liorerAcc�l�ration()
    {
        if (ArgentDisponible >= 3&& upgradeAcc�l�ration<3)
        {
            upgradeAcc�l�ration++;
            Acc�l�rationText.text = $"Niveau d'am�lioration: {upgradeAcc�l�ration}";
            ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {ArgentDisponible} ";
        }
        else
        {
            MessageErreur.text = "Il n'y a pas assez d'argent disponible";
        }
    }
    public void Am�liorerForceFreinage()
    {
        if (ArgentDisponible >= 3&& upgradeForceFreinage<3)
        {
            upgradeForceFreinage++;
            FreinageText.text = $"Niveau d'am�lioration: {upgradeForceFreinage}";
            ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {ArgentDisponible} ";
        }
        else
        {
            MessageErreur.text = "Il n'y a pas assez d'argent disponible";
        }
    }
    public void Am�liorerVitesseMaximale()
    {
        if (ArgentDisponible >= 3&& upgradeVitesseMaximale<3)
        {
            upgradeVitesseMaximale++;
            VitesseText.text = $"Niveau d'am�lioration: {upgradeVitesseMaximale}";
            ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {ArgentDisponible} ";
        }
        else
        {
            MessageErreur.text = "Il n'y a pas assez d'argent disponible";
        }
    }
    public void Am�liorerCapacit�EssenceMaximum()
    {
        if (ArgentDisponible >= 3&& upgradeCapacit�Essence<3)
        {
            upgradeCapacit�Essence++;
            EssenceText.text = $"Niveau d'am�lioration: {upgradeCapacit�Essence}";
            ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {ArgentDisponible} ";
        }
        else
        {
            MessageErreur.text = "Il n'y a pas assez d'argent disponible";
        }

    }
    public void Am�liorerVieMaximaleJoueur()
    {
        if (ArgentDisponible >= 3&& upgradeVieMaximale<3)
        {
            upgradeVieMaximale++;
            VieText.text = $"Niveau d'am�lioration: {upgradeVieMaximale}";
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
            WrenchText.text = $"Niveau d'am�lioration: {upgradeWrench}";
            ArgentDisponible -= 3;
            NBargent.text = $"Argent disponible {ArgentDisponible} ";
        }
        else
        {
            MessageErreur.text = "Il n'y a pas assez d'argent disponible";
        }

    }
    public void ChangerDeSc�ne(int NoSc�ne)
    {
        SceneManager.LoadScene(NoSc�ne);

    }

}
