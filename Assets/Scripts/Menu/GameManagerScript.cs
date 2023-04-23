using System.Collections;
using System.Collections.Generic;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{
    public int upgradeAccélération;
    public int upgradeVitesseMaximale;
    public float upgradeForceFreinage;
    public int upgradeCapacitéEssence;
    public int upgradeVieMaximale;
    public int upgradeWrench;


    public int ArgentDisponible;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);// pour permettre de peser sur escape pour sortir du jeu.
    }

    public void AméliorerAccélération()
    {
        upgradeAccélération -=1;
    }
    public void AméliorerForceFreinage()
    {
        upgradeForceFreinage +=0.05f;
    }
    public void AméliorerVitesseMaximale()
    {
        upgradeVitesseMaximale +=2;
    }
    public void AméliorerCapacitéEssenceMaximum()
    {
        upgradeCapacitéEssence+=5;
    }
    public void AméliorerVieMaximaleJoueur()
    {
        upgradeVieMaximale+=3;
    }
    public void BonifierWrench()
    {
        upgradeWrench+=3;
    }
    public void ChangerDeScène(int NoScène)
    {
        SceneManager.LoadScene(NoScène);
       
    }

}
