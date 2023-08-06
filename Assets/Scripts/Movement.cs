using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem maineEngineParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();

        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateRight();

        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }

        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); 
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);

        }
        if (!maineEngineParticle.isPlaying)
        {
            maineEngineParticle.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        maineEngineParticle.Stop();
    }

    private void ApplyRotation(float rotationThisframe)
    {
        rb.freezeRotation = true;        // Freezing Rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisframe * Time.deltaTime);
        rb.freezeRotation = false;      // unfreezing system so that physics system can take over.
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticle.isPlaying)
        {
            leftThrusterParticle.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticle.isPlaying)
        {
            rightThrusterParticle.Play();
        }
    }

    private void StopRotating()
    {
        rightThrusterParticle.Stop();
        leftThrusterParticle.Stop();
    }

    
}
