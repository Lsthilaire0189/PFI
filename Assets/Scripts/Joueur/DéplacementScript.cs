using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DéplacementScript : MonoBehaviour
{
    [SerializeField] WheelCollider RoueAvantDroite;
    [SerializeField] WheelCollider RoueAvantGauche;
    [SerializeField] WheelCollider RoueArrièreDroite;
    [SerializeField] WheelCollider RoueArrièreGauche;
    public float accélération = 500f;
    public float ForceFreinage = 300f;
    public float angleMaximum = 15f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;


    private void FixedUpdate()
    {
        currentAcceleration = accélération * Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Space))
            currentBreakForce = ForceFreinage;
        else
            currentBreakForce = 0f;
        RoueAvantDroite.motorTorque = currentAcceleration;
        RoueAvantGauche.motorTorque = currentAcceleration;
        RoueAvantDroite.brakeTorque = currentBreakForce;
        RoueAvantGauche.brakeTorque= currentBreakForce;
        RoueArrièreDroite.brakeTorque = currentBreakForce;
        RoueArrièreGauche.brakeTorque= currentBreakForce;
    }


 //   Start is called before the first frame update
    void Start()
    {

    }

//    Update is called once per frame
    void Update()
    {

    }
}
