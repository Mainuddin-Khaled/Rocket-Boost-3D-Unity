using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
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
            rigidBody.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotation()
    {
        float input = rotation.ReadValue<float>();
        
        if (input < 0)
        {
            ApplyRotation(rotationStrength);
        }
        else if (input > 0)
        {
            ApplyRotation(-rotationStrength);
        }
    }

    private void ApplyRotation(float negativeInput)
    {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * negativeInput * Time.fixedDeltaTime);
        rigidBody.freezeRotation = false;
    }
}
