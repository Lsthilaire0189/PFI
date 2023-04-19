using System.Collections;
using System.Collections.Generic;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{
    int upgradeAccélération =1;
    int upgradeVitesseMaximale=1;
    public int VitesseMaximale;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);// pour permettre de peser sur escape pour sortir du jeu.
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangerAccélération()
    {
        upgradeAccélération++;

    }
    public void ChangerVitesseMaximale()
    {

    }
    public void ChangerDeScène(int NoScène)
    {
        SceneManager.LoadScene(NoScène);
    }
}
