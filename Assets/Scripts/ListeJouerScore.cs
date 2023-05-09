using JetBrains.Annotations;
using MathNet.Numerics.Optimization;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;


public class ListeJouerScore : MonoBehaviour
{
    //ScoreManager scoreManager;
    public GameObject prefabEntree;
    int scorePrecedent;
    private void Start()
    {
        string a = "app";
        scorePrecedent = ScoreManager.GetchangementScore();
        Debug.Log(a);
    }
 
    void Update()
    {
     
        //if (ScoreManager.GetchangementScore() == scorePrecedent)
            //return;

        scorePrecedent = ScoreManager.GetchangementScore();

        while (this.transform.childCount > 0)    
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);

        }

        List<ScoreManager.EntreeEnter> entres = ScoreManager.GetNoms(); // on recoit les noms tries selon leur score
        
        for (int i=0; i<entres.Count;i++)
        {
            GameObject copiePrefabEntre = (GameObject)Instantiate(prefabEntree);
            copiePrefabEntre.transform.SetParent(this.transform); // on veut que listeJoueurScore soit notre parent

            // On cherche ses enfants et on rajoute leur modification a chacun
            copiePrefabEntre.transform.Find("Nom").GetComponent<TMP_Text>().text = entres[i].Nom;
            copiePrefabEntre.transform.Find("Rang").GetComponent<TMP_Text>().text = (i+1).ToString();
            copiePrefabEntre.transform.Find("Temps").GetComponent<TMP_Text>().text = ScoreManager.GetTypeScore(entres[i],"Temps").ToString();
            copiePrefabEntre.transform.Find("Argent Collecte").GetComponent<TMP_Text>().text = ScoreManager.GetTypeScore(entres[i], "Argent Collecte").ToString();
        }

    }

    public void DEBUG_ADD_KILL_TO_QUILL()
    {
        var a = ScoreManager.scoreJoueurs.Find(x => x.Nom == "quill");
        ScoreManager.ChangerTypeScore(a, 1,"Temps");
    }
    /*public void DEBUG()
    {
        
        ScoreManager.SetJoueur("bob", 1, 15);
        ScoreManager.SetJoueur("amiri", 2, 20);
        ScoreManager.SetJoueur("gucci", 3, 30);
        ScoreManager.SetJoueur("vetement", 4, 99);
        ScoreManager.SetJoueur("prada", 5, 101);
        ScoreManager.SetJoueur("quill", 0, 0);
    }*/
}
