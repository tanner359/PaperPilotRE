using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public float airDensity = 1;
    public float wingArea = 1; 
    public float drag = 0;    
    public float maxSpeed = 30;
    public float liftModifier = 400f;

    float roll = 0;
    float yaw = 0;
    float pitch = 0;

    Vector3 dragDirection;
    Vector3 liftDirection;


    bool jetStream = false;

    private void Awake()
    {
        PlayerStats.SetScore(0);
        Time.timeScale = 1;
    }

    private void Start()
    {       
        if (SystemInfo.supportsGyroscope)
        {
            print("This system supports gyro");
            Input.gyro.enabled = false;
        }        
        
        Screen.orientation = ScreenOrientation.Portrait;
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (other.CompareTag("Pickup"))
        {
            PlayerStats.SetScore(PlayerStats.score + 100);
            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>(Path.Combine("Audio", "GemPickup")), transform.position, Settings.volume / 10.0f);
        }
    }

    private void Update()
    {
        if(rb.velocity.magnitude > 18 && !jetStream)
        {
            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>(Path.Combine("Audio", "AirBlow")), transform.position, Settings.volume);
            jetStream = true;
        }
        else if (rb.velocity.magnitude < 16)
        {
            jetStream = false;
        }
    }

    private void FixedUpdate()
    {   
        roll = Input.gyro.rotationRateUnbiased.y;
        pitch = Input.gyro.rotationRateUnbiased.x;
        yaw = Input.gyro.rotationRateUnbiased.z;
             
        transform.Rotate(new Vector3(-pitch, -yaw, -roll) * Time.deltaTime * Settings.sensitivity);

        dragDirection = rb.velocity.normalized;
        liftDirection = Vector3.Cross(dragDirection, transform.right);

        if (rb.velocity != Vector3.zero)
        {
            rb.AddForce(liftDirection * GetLift() + dragDirection * drag);
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    public float GetLift()
    {       
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        float angleOfAttack = Mathf.Atan2(-localVelocity.y, localVelocity.z);
        float coefficient = Mathf.Pow(1225.04f * rb.velocity.magnitude, 2) - 1;
        float coefficientLift = (4 * angleOfAttack) / Mathf.Sqrt(coefficient);
        float lift =  0.5f * coefficientLift * airDensity * wingArea * Mathf.Pow(rb.velocity.magnitude, 2);
        return lift * liftModifier;
    }

}
