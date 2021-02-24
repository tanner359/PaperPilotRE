using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float airDensity = 1;
    public float wingArea = 1;
    public float drag = 0;
    public float turnSpeed = 1;
    public float roll = 0;
    public float yaw = 0;
    public float pitch = 0;

    public Vector3 dragDirection;
    public Vector3 liftDirection;

    private void Start()
    {       
        if (SystemInfo.supportsGyroscope)
        {
            print("This system supports gyro");
            Input.gyro.enabled = true;
        }
        
        Screen.orientation = ScreenOrientation.Portrait;
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (other.CompareTag("Pickup"))
        {
            PlayerStats.SetScore(PlayerStats.score + 100);
        }
    }
    
    private void FixedUpdate()
    {
        roll = Input.gyro.rotationRateUnbiased.y;
        pitch = Input.gyro.rotationRateUnbiased.x;
        yaw = Input.gyro.rotationRateUnbiased.z;

        transform.Rotate(new Vector3(-pitch, -yaw, -roll) * Time.deltaTime * turnSpeed);

      
        dragDirection = rb.velocity.normalized;
        liftDirection = Vector3.Cross(dragDirection, transform.right);

        if(rb.velocity != Vector3.zero)
        {
            rb.AddForce(liftDirection * GetLift() + dragDirection * drag);
        }
    }

    public float GetLift()
    {       
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        float angleOfAttack = Mathf.Atan2(-localVelocity.y, localVelocity.z);
        float coefficient = Mathf.Pow(1225.04f * rb.velocity.magnitude, 2) - 1;
        float coefficientLift = (4 * angleOfAttack) / Mathf.Sqrt(coefficient);
        float lift =  0.5f * coefficientLift * airDensity * wingArea * Mathf.Pow(rb.velocity.magnitude, 2);
        return lift * 600;
    }

}
