using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static ScoreManager;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;

public class Entr�esManager : MonoBehaviour
{
    public GameObject prefabEntree;

    [SerializeField] string filename;

    List<Entr�eEnter> entr�es = new List<Entr�eEnter>();//C'est utilis� entre les sc�nes (LeaderBoard)

    const int NbRecordsMax = 6;

    private void Start()
    {
        entr�es = FileHandler.ReadListFromJSON<Entr�eEnter>(filename);
    }

    public void AjouterEntr�es�Liste(Entr�eEnter entr�e)
    {
        entr�es.Add(new Entr�eEnter(entr�e.Nom, entr�e.Score, entr�e.ArgentCollect�));

        FileHandler.SaveToJSON<Entr�eEnter>(entr�es, filename);
    }

    private void SauvegarderScoreFinDeJeu()
    {
        var entr�esTemporaire = ScoreManager.GetNoms();
        int entreCount = entr�es.Count;

        for(int i =0; i< NbRecordsMax ;i++)
        {
            if ( (entr�es[entr�es.Count].Score < entr�esTemporaire[i].Score) || entreCount==0)
            {
                entr�es.Add(entr�esTemporaire[i]);
            }
            
        }
        entr�es.OrderByDescending(x => x.Score).ToList();
        FileHandler.SaveToJSON<Entr�eEnter>(entr�es, filename);

    }

    public void AfficherScoreFinDeJeu() 
    {
        entr�es = FileHandler.ReadListFromJSON<Entr�eEnter>(filename);
        for (int i = 0; i < entr�es.Count; i++)
        {
            GameObject copiePrefabEntr� = (GameObject)Instantiate(prefabEntree);
            copiePrefabEntr�.transform.SetParent(this.transform); // on veut que listeJoueurScore soit notre parent

            // On cherche ses enfants et on rajoute leur modification a chacun
            copiePrefabEntr�.transform.Find("Nom").GetComponent<TMP_Text>().text = entr�es[i].Nom;
            copiePrefabEntr�.transform.Find("Rang").GetComponent<TMP_Text>().text = (i + 1).ToString();
            copiePrefabEntr�.transform.Find("Score").GetComponent<TMP_Text>().text = ScoreManager.GetTypeScore(entr�es[i], "Score").ToString();
            copiePrefabEntr�.transform.Find("Argent Collect�").GetComponent<TMP_Text>().text = ScoreManager.GetTypeScore(entr�es[i], "Argent Collect�").ToString();

        }
    }
}
