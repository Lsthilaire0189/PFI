using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static ScoreManager;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;

public class EntréesManager : MonoBehaviour
{
    public GameObject prefabEntree;

    [SerializeField] string filename;

    List<EntréeEnter> entrées = new List<EntréeEnter>();//C'est utilisé entre les scènes (LeaderBoard)

    const int NbRecordsMax = 6;

    private void Start()
    {
        entrées = FileHandler.ReadListFromJSON<EntréeEnter>(filename);
    }

    public void AjouterEntréesÀListe(EntréeEnter entrée)
    {
        entrées.Add(new EntréeEnter(entrée.Nom, entrée.Score, entrée.ArgentCollecté));

        FileHandler.SaveToJSON<EntréeEnter>(entrées, filename);
    }

    private void SauvegarderScoreFinDeJeu()
    {
        var entréesTemporaire = ScoreManager.GetNoms();
        int entreCount = entrées.Count;

        for(int i =0; i< NbRecordsMax ;i++)
        {
            if ( (entrées[entrées.Count].Score < entréesTemporaire[i].Score) || entreCount==0)
            {
                entrées.Add(entréesTemporaire[i]);
            }
            
        }
        entrées.OrderByDescending(x => x.Score).ToList();
        FileHandler.SaveToJSON<EntréeEnter>(entrées, filename);

    }

    public void AfficherScoreFinDeJeu() 
    {
        entrées = FileHandler.ReadListFromJSON<EntréeEnter>(filename);
        for (int i = 0; i < entrées.Count; i++)
        {
            GameObject copiePrefabEntré = (GameObject)Instantiate(prefabEntree);
            copiePrefabEntré.transform.SetParent(this.transform); // on veut que listeJoueurScore soit notre parent

            // On cherche ses enfants et on rajoute leur modification a chacun
            copiePrefabEntré.transform.Find("Nom").GetComponent<TMP_Text>().text = entrées[i].Nom;
            copiePrefabEntré.transform.Find("Rang").GetComponent<TMP_Text>().text = (i + 1).ToString();
            copiePrefabEntré.transform.Find("Score").GetComponent<TMP_Text>().text = ScoreManager.GetTypeScore(entrées[i], "Score").ToString();
            copiePrefabEntré.transform.Find("Argent Collecté").GetComponent<TMP_Text>().text = ScoreManager.GetTypeScore(entrées[i], "Argent Collecté").ToString();

        }
    }
}
