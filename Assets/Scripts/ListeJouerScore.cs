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
        DEBUG();
        Debug.Log(a);
    }
 
    void Update()
    {
     
        if (ScoreManager.GetchangementScore() == scorePrecedent)
            return;

        scorePrecedent = ScoreManager.GetchangementScore();

        while (this.transform.childCount > 0)    
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);

        }

        List<ScoreManager.EntréeEnter> entrés = ScoreManager.GetNoms(); // on recoit les noms tries selon leur score
        
        for (int i=0; i<entrés.Count;i++)
        {
            GameObject copiePrefabEntré = (GameObject)Instantiate(prefabEntree);
            copiePrefabEntré.transform.SetParent(this.transform); // on veut que listeJoueurScore soit notre parent

            // On cherche ses enfants et on rajoute leur modification a chacun
            copiePrefabEntré.transform.Find("Nom").GetComponent<TMP_Text>().text = entrés[i].Nom;
            copiePrefabEntré.transform.Find("Rang").GetComponent<TMP_Text>().text = (i+1).ToString();
            copiePrefabEntré.transform.Find("Score").GetComponent<TMP_Text>().text = ScoreManager.GetTypeScore(entrés[i],"Score").ToString();
            copiePrefabEntré.transform.Find("Argent Collecté").GetComponent<TMP_Text>().text = ScoreManager.GetTypeScore(entrés[i], "Argent Collecté").ToString();

        }
       
    }
    public void DEBUG_ADD_KILL_TO_QUILL()
    {
        var a = ScoreManager.scoreJoueurs.Find(x => x.Nom == "quill");
        ScoreManager.ChangerTypeScore(a, 1,"Score");
    }
    public void DEBUG()
    {
        
        ScoreManager.SetJoueur("bob", 1, 15);
        ScoreManager.SetJoueur("amiri", 2, 20);
        ScoreManager.SetJoueur("gucci", 3, 30);
        ScoreManager.SetJoueur("vetement", 4, 99);
        ScoreManager.SetJoueur("prada", 5, 101);
        ScoreManager.SetJoueur("quill", 0, 0);
    }
}
