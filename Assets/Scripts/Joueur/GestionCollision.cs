using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GestionCollision : MonoBehaviour
{
    [SerializeField] public GameObject PointFaible;

    GestionVieJoueur gestionVieJoueur;
    GestionEssence gestionEssence;
    GestionArgent gestionArgent;

    public AudioSource sonCollection;
    public AudioSource sonCollisionPolice;
    public AudioSource sonCollisionObjet;
    public AudioSource sonEssence;
    public AudioSource sonGainHP;

    int wrenchLayer = 7;
    int gasLayer = 8;
    int BatimentLayer = 9;
    int ArgentLayer = 13;

    private void Awake()
    {
        gestionVieJoueur = GetComponent<GestionVieJoueur>();
        gestionEssence = GetComponent<GestionEssence>();
        gestionArgent = GetComponent<GestionArgent>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ArgentLayer)
        {
            gestionArgent.ModifierArgent(1);
            sonCollection.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == wrenchLayer)
        {
            gestionVieJoueur.ModifierVie(2);
            sonGainHP.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == gasLayer)
        {
            gestionEssence.ModifierEssence(10);
            sonEssence.Play();
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        int collisionDommage = 0;
        ContactPoint c = collision.GetContact(0);
        if (collision.gameObject.layer == BatimentLayer)
        {
            collisionDommage = 5;
        }
        if (c.thisCollider.gameObject == PointFaible)
        {
            collisionDommage += 3;
        }
        gestionVieJoueur.ModifierVie(-collisionDommage);


    }
}
