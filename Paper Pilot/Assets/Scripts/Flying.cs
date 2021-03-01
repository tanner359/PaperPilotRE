using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    public Rigidbody rb;

    public float airDensity = 1;
    public float wingArea = 1;
    public float fowardSpeed = 0;
    public float downSpeed = 0;
    public float tilt = 0;



    private void FixedUpdate()
    {


        rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y)/3, 0f) * Time.deltaTime, transform.localPosition, ForceMode.Acceleration);
        

    }





    public float GetLift(float airDensity, float wingArea, float airSpeed)
    {
        return Mathf.Clamp((0.5f * airDensity * wingArea * Mathf.Pow(airSpeed, 2)), 0, 10);
    }

    public float GetTilt(Transform Front, Transform L_Wing, Transform R_Wing)
    {
        float frontDisplacement = Front.position.y - ((L_Wing.position.y + R_Wing.position.y) / 2);
        return frontDisplacement;
    }




}
