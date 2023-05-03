using System.Collections;
using System.Collections.Generic;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{
    public float upgradeAccélération;
    public int upgradeVitesseMaximale;
    public float upgradeForceFreinage;
    public int upgradeCapacitéEssence;
    public int upgradeVieMaximale;
    public int upgradeWrench;
    public int upgradePompe;

    public int ArgentDisponible;
    public int NbPoints;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);// pour permettre de peser sur escape pour sortir du jeu.
    }

    public void AméliorerAccélération()
    {
        upgradeAccélération +=0.007f;
    }
    public void AméliorerForceFreinage()
    {
        upgradeForceFreinage +=0.05f;
    }
    public void AméliorerVitesseMaximale()
    {
        upgradeVitesseMaximale +=10;
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
        upgradeWrench += 2;
    }

    public void BonifierPompe()
    {
        upgradePompe += 5;
    }
    public void ChangerDeScéne(int NoScéne)
    {
        SceneManager.LoadScene(NoScéne);
       
    }

}
