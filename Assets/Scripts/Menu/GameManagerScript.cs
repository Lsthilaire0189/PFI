using System.Collections;
using System.Collections.Generic;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{
    public int upgradeAcc�l�ration;
    public int upgradeVitesseMaximale;
    public float upgradeForceFreinage;
    public int upgradeCapacit�Essence;
    public int upgradeVieMaximale;
    public int upgradeWrench;


    public int ArgentDisponible;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);// pour permettre de peser sur escape pour sortir du jeu.
    }

    public void Am�liorerAcc�l�ration()
    {
        upgradeAcc�l�ration -=1;
    }
    public void Am�liorerForceFreinage()
    {
        upgradeForceFreinage +=0.05f;
    }
    public void Am�liorerVitesseMaximale()
    {
        upgradeVitesseMaximale +=2;
    }
    public void Am�liorerCapacit�EssenceMaximum()
    {
        upgradeCapacit�Essence+=5;
    }
    public void Am�liorerVieMaximaleJoueur()
    {
        upgradeVieMaximale+=3;
    }
    public void BonifierWrench()
    {
        upgradeWrench+=3;
    }
    public void ChangerDeSc�ne(int NoSc�ne)
    {
        SceneManager.LoadScene(NoSc�ne);
       
    }

}
