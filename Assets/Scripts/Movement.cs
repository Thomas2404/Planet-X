using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 20f;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem rightBooster;
    [SerializeField] ParticleSystem leftBooster;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            StartThrusting();
        } else {
            StopThrusting();
        }     
    } 

    void ProcessRotation() {
    
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            StopThrustRotation();
        } else if (Input.GetKey(KeyCode.A)) {
            StartThrustingLeft();
        } else if (Input.GetKey(KeyCode.D)) {
           StartThrustingRight();
        } else {
            StopThrustRotation();
        }
    }

    void StopThrusting() {
        audioSource.Stop();
        mainBooster.Stop();
    }

    void StartThrustingRight() {
        ApplyRotation(-rotationThrust);
           if (!leftBooster.isPlaying) {
                leftBooster.Play();
            }
    }

    void StartThrustingLeft() {
        ApplyRotation(rotationThrust);
            if (!rightBooster.isPlaying) {
                rightBooster.Play();
            }
    }

    void StartThrusting() {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
            if (!mainBooster.isPlaying) {
               mainBooster.Play(); 
            }
    }

    void StopThrustRotation() {
        rightBooster.Stop();
        leftBooster.Stop();
    }

    void ApplyRotation(float rotationThisFrame) {
        rb.freezeRotation = true; // Freezing rotation so we can rotate without conflict with the physics engine.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation.
    }
}
