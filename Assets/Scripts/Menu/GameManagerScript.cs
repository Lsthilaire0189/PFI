using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);// pour permettre de peser sur escape pour sortir du jeu.
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangerDeSc�ne(int NoSc�ne)
    {
        SceneManager.LoadScene(NoSc�ne);
    }
}
