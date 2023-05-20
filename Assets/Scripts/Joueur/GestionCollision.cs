using UnityEngine;

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
    int stopLayer = 17;

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
            gestionArgent.ModifierArgent(1);//on augmente la quantité d'argent du joueur de +1
            sonCollection.Play();
            Destroy(other.gameObject);
            ScoreManager.ChangerTypeScore(ScoreManager.scoreJoueurs[sceneManagerScript.getNumeroPartie()], 1, "Argent Collecte"); 
            //Fait par Theodor Trif - on ajoute +1 dans
            //l'argent collecté dans la liste static de ScoreManager. Cette liste
            //communiquera ensuite avec le gameobject 'ListeJoueurScore' dans la scene. 
        }
        if (other.gameObject.layer == wrenchLayer)
        {
            gestionVieJoueur.ModifierVie(gainHP); //on augmente la vie de joueur de +gainHP
            sonGainHP.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == gasLayer)
        {
            gestionEssence.ModifierEssence(gainEssence); //on augmente l'essence du joueur de +gainEssence
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
                collisionDommage += 3;//si le joueur frappe l'objet avec son point faible situé derrière la voiture, il prendra 3 dégats de plus
            }

            if (collision.gameObject.layer == NPCLayer)
            {
                Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity, transform);
                Destroy(collision.gameObject);
            }

            gestionVieJoueur.ModifierVie(-collisionDommage);//on ajoute -collisionDommage à la quantité de vie totale du joueur
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
        else if (collision.gameObject.layer == stopLayer)
        {
            sonCollisionObjet.Play();
            collisionDommage = 1;
            Destroy(collision.gameObject);
            gestionVieJoueur.ModifierVie(-collisionDommage);
        }
    }
}
