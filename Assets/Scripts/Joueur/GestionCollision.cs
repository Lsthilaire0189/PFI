using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionCollision : MonoBehaviour
{
    [SerializeField] public GameObject PointFaible;

    GestionVieJoueur gestionVieJoueur;
    GestionEssence gestionEssence;
    GestionArgent gestionArgent;

    public AudioSource sonCollection;
    public AudioSource sonCollisionObjet;
    public AudioSource sonEssence;
    public AudioSource sonGainHP;

    public Transform explosion;

    private GameObject sceneManager;
    private SceneManagerScript sceneManagerScript;

    int wrenchLayer = 7;
    int gasLayer = 8;
    int BatimentLayer = 9;
    int policeLayer = 10;
    int NPCLayer = 16;
    int ArgentLayer = 13;

    public int gainHP;
    public int gainEssence;
    

    private void Awake()
    {
        gestionVieJoueur = GetComponent<GestionVieJoueur>();
        gestionEssence = GetComponent<GestionEssence>();
        gestionArgent = GetComponent<GestionArgent>();
        sceneManager = GameObject.Find("SceneManager");
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ArgentLayer)
        {
            gestionArgent.ModifierArgent(1);
            sonCollection.Play();
            Destroy(other.gameObject);
            ScoreManager.ChangerTypeScore(ScoreManager.scoreJoueurs[sceneManagerScript.getNumeroPartie()], 1, "Argent Collecte");
        }
        if (other.gameObject.layer == wrenchLayer)
        {
            gestionVieJoueur.ModifierVie(gainHP);
            sonGainHP.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == gasLayer)
        {
            gestionEssence.ModifierEssence(gainEssence);
            sonEssence.Play();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        int collisionDommage;
        ContactPoint c = collision.GetContact(0);
        if (collision.gameObject.layer == BatimentLayer || collision.gameObject.layer == NPCLayer)
        {
            sonCollisionObjet.Play();
            collisionDommage = 5;
            if (c.thisCollider.gameObject == PointFaible)
            {
                collisionDommage += 3;
            }

            if (collision.gameObject.layer == NPCLayer)
            {
                Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity, transform);
                Destroy(collision.gameObject);
            }

            gestionVieJoueur.ModifierVie(-collisionDommage);
        }
        else if (collision.gameObject.layer == policeLayer)
        {
            sonCollisionObjet.Play();
            collisionDommage = 10;
            if (c.thisCollider.gameObject == PointFaible)
            {
                collisionDommage += 3;
            }

            Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity, transform);
            Destroy(collision.gameObject);
            

            gestionVieJoueur.ModifierVie(-collisionDommage);


        }
    }
}
