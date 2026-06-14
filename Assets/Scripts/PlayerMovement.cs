using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] ParticleSystem rocketJetParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    Rigidbody rigidBody;
    AudioSource audioSource;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        Thrust();
        Rotation();
    }

    private void Thrust()
    {
        if (thrust.IsPressed())
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }
    private void StartThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSound);
        }
        if (!rocketJetParticles.isPlaying)
        {
            rocketJetParticles.Play();
        }
    }

    private void StopThrust()
    {
        audioSource.Stop();
        rocketJetParticles.Stop();
    }


    private void Rotation()
    {
        float input = rotation.ReadValue<float>();
        
        if (input < 0)
        {
            RightRotation();
        }
        else if (input > 0)
        {
            LeftRotation();
        }
        else
        {
            StopRotation();
        }
    }

    private void RightRotation()
    {
        ApplyRotation(rotationStrength);
        if (!rightThrustParticles.isPlaying)
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Play();
        }
    }

    private void LeftRotation()
    {
        ApplyRotation(-rotationStrength);
        if (!leftThrustParticles.isPlaying)
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Play();
        }
    }
    private void StopRotation()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    private void ApplyRotation(float negativeInput)
    {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * negativeInput * Time.fixedDeltaTime);
        rigidBody.freezeRotation = false;
    }
}
