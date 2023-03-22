using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AIDéplacement : MonoBehaviour
{

    List<Vector3> chemins;
    // Start is called before the first frame update
    void Awake()
    {

        var creerCarte = gameObject.GetComponentInParent<CréerCarte>();
        chemins = creerCarte.chemin;
    }

    // Update is called once per frame
    void Update()
    {
        float step = 0.05f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, chemins[1], step);
    }
}
