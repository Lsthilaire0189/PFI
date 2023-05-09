using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static ScoreManager;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;
using System;

[Serializable]
public class EntreesManager : MonoBehaviour
{
   

    public GameObject prefabEntree;

    [SerializeField] string filename;

    List<EntreeEnter> entrees = new List<EntreeEnter>();//C'est utilise entre les scenes (LeaderBoard)

    const int NbRecordsMax = 6;

  
    private void Start()
    {
        entrees = FileHandler.ReadListFromJSON<EntreeEnter>(filename);
    }

    public void AjouterEntreesAListe(EntreeEnter entree)
    {
        entrees.Add(new EntreeEnter(entree.Nom, entree.Temps, entree.ArgentCollecte));

        FileHandler.SaveToJSON<EntreeEnter>(entrees, filename);
    }

    public void SauvegarderScoreFinDeJeu()
    {
        var entreesTemporaire = ScoreManager.GetNoms();
        int entreCount = entrees.Count;

        for(int i =0; i< NbRecordsMax ;i++)
        {
            if ( (entrees[entrees.Count].Temps < entreesTemporaire[i].Temps) || entreCount==0)
            {
                entrees.Add(entreesTemporaire[i]);
            }
            
        }
        entrees.OrderByDescending(x => x.Temps).ToList();
        FileHandler.SaveToJSON<EntreeEnter>(entrees, filename);

    }

    public void AfficherScoreFinDeJeu() 
    {
        entrees = FileHandler.ReadListFromJSON<EntreeEnter>(filename);
        for (int i = 0; i < entrees.Count; i++)
        {
            GameObject copiePrefabEntre = (GameObject)Instantiate(prefabEntree);
            copiePrefabEntre.transform.SetParent(this.transform); // on veut que listeJoueurScore soit notre parent

            // On cherche ses enfants et on rajoute leur modification a chacun
            copiePrefabEntre.transform.Find("Nom").GetComponent<TMP_Text>().text = entrees[i].Nom;
            copiePrefabEntre.transform.Find("Rang").GetComponent<TMP_Text>().text = (i + 1).ToString();
            copiePrefabEntre.transform.Find("Temps").GetComponent<TMP_Text>().text = ScoreManager.GetTypeScore(entrees[i], "Temps").ToString();
            copiePrefabEntre.transform.Find("Argent Collecte").GetComponent<TMP_Text>().text = ScoreManager.GetTypeScore(entrees[i], "Argent Collecte").ToString();

        }
    }
}
