using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
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
        transform.Rotate(Vector3.forward * negativeInput * Time.fixedDeltaTime);
    }
}
