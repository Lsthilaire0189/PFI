using System.Collections.Generic;
using UnityEngine;
public class GestionSurfaceCollision : MonoBehaviour
{
    public List<WheelCollider> listeRoueColliders;

    private int boueLayer = 11;
    private int eauLayer = 12;
    
    private WheelFrictionCurve avantFrictionEau;
    private WheelFrictionCurve latFrictionEau;
    
    private WheelFrictionCurve avantFrictionNormale;
    private WheelFrictionCurve latFrictionNormale;
    
    private void Awake()
    {
        avantFrictionEau = new WheelFrictionCurve();
        avantFrictionNormale = new WheelFrictionCurve();
        latFrictionEau = new WheelFrictionCurve();
        latFrictionNormale = new WheelFrictionCurve();
        
        avantFrictionEau = listeRoueColliders[0].forwardFriction;
        avantFrictionNormale = listeRoueColliders[0].forwardFriction;
        
        latFrictionEau = listeRoueColliders[0].sidewaysFriction;
        latFrictionNormale = listeRoueColliders[0].sidewaysFriction;
        
        //le stifness représente la quantité de friction qu'a un WheelCollider avec la surface sur laquelle il se retrouve. Ceci va de 0 à 1
        avantFrictionEau.stiffness = 0.3f; //on établie la friction avant qu'aurant les WheelColliders lorsqu'ils se trouvent sur une flaque d'eau
        avantFrictionNormale.stiffness = 1;//on établie la friction avant qu'aurant les WheelColliders lorsqu'ils se trouvent sur une surface normale
        latFrictionEau.stiffness = 0.1f;//on établie la friction latérale qu'auront les WheelColliders lorsqu'ils se trouvent sur une flaque d'eau
        latFrictionNormale.stiffness = 1;//on établie la friction latérale qu'auront les WheelColliders lorsqu'ils se trouvent sur une surface normale
    }

    private void FixedUpdate()
    {
        WheelHit hit;
        foreach (var roueCollider in listeRoueColliders)//on vérifie pour chaque WheelCollider des roues de la voiture
        {
            if(roueCollider.GetGroundHit(out hit))
            {
                if (hit.collider.gameObject.layer == boueLayer)
                {//lorsque le WheelCollider se trouve sur une flaque de boue, on divise de /1.0075 sa quantité de torque totale tant quelle se trouve dessus,
                    //ralentissant ainsi la voiture
                    roueCollider.motorTorque /=1.0075f;
                }
                else if (hit.collider.gameObject.layer == eauLayer)
                {//ceci permet de diminuer la friction du WheelCollider lorsqu'il se retrouve sur une flaque d'eau
                    roueCollider.forwardFriction = avantFrictionEau;
                    roueCollider.sidewaysFriction = latFrictionEau;
                }
                else
                {//s'il se trouve sur une autre surface qu'une flaque de boue ou d'eau, sa friction revient à une quantité normale
                    roueCollider.forwardFriction = avantFrictionNormale;
                    roueCollider.sidewaysFriction = latFrictionNormale;
                }
            }
        }
    }
}
