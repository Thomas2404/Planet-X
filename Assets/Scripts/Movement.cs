using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 20f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }      
    } 

    void ProcessRotation() {
    
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {

        } else if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);

        }
        else if (Input.GetKey(KeyCode.D)) {
           ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation so we can rotate without conflict with the physics engine.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation.
    }
}
