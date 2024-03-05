using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float damping = 0.90f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float interactDistance = 1f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private float turnLimit = 25f;
    [SerializeField] private bool dampBool = true;

    private Quaternion startRotation;
    private Vector3 velocity;
    private AudioSource astCrashSound;
    private bool playCrashSound = true;

    private float dampingTimer = 0f;
    private float dampingInterval = 0.1f;

    private void Awake()
    {
        startRotation = transform.rotation;
        astCrashSound = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        ShipMovement(inputVector);
        ApplyRotation(inputVector);
        DetectAsteroids();
    }

    private void ShipMovement(Vector2 inputVector)
    {
        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0);

        velocity += moveDir * moveSpeed * Time.fixedDeltaTime;
        transform.position += velocity;

        velocity *= damping;

        if (velocity.magnitude <= 0.01f)
        {
            velocity = Vector3.zero;
        }
    }

    private void ApplyRotation(Vector2 inputVector)
    {
        float turnAmountZ = -inputVector.x * turnSpeed;
        float turnAmountX = -inputVector.y * turnSpeed;

        Vector3 currentEulerAngles = transform.rotation.eulerAngles;

        float newZRotation = NormalizeRotation(currentEulerAngles.z, turnAmountZ);
        float newXRotation = NormalizeRotation(currentEulerAngles.x, turnAmountX);

        Quaternion targetRotation = Quaternion.Euler(new Vector3(newXRotation, 0, newZRotation));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * turnSpeed);

        if(inputVector.x == 0)
        {
            Quaternion startRotationZ = new Quaternion(transform.rotation.x, transform.rotation.y, startRotation.z, transform.rotation.w);
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotationZ, returnSpeed * Time.fixedDeltaTime);
        }
        if (inputVector.y == 0)
        {
            Quaternion startRotationX = new Quaternion(startRotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotationX, returnSpeed * Time.fixedDeltaTime);
        }
    }

    private float NormalizeRotation(float eulerAngle, float turnAmount)
    {
        float newRotation = eulerAngle + turnAmount;
        newRotation = (newRotation > 180) ? newRotation - 360 : newRotation;
        newRotation = Mathf.Clamp(newRotation, -turnLimit, turnLimit);
        return newRotation;
    }

    private void DetectAsteroids()
    {

        Vector3 halfExtents = new Vector3(5, 1.5f, 1);
        Quaternion orientation = Quaternion.identity;

        if (Physics.BoxCast(transform.position, halfExtents, transform.forward, out RaycastHit hitInfo, orientation, interactDistance))
        {
            if (hitInfo.transform.TryGetComponent(out Fracture fracture))
            {
                fracture.FractureObject();
                if (playCrashSound)
                {
                    StartCoroutine(AsteroidCrash());
                }
            }
        }
    }

    private IEnumerator AsteroidCrash()
    {
        astCrashSound.Play();
        yield return null;
    }
}
