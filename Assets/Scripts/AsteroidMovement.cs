using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private float lifetime = 5f;
    private float astSpeed = 30f;

    private float rotationSpeedX;
    private float rotationSpeedY;
    private float rotationSpeedZ;

    private void Start()
    {
        StartCoroutine(DestroyAfter(lifetime));

        rotationSpeedX = Random.Range(-90, 90);
        rotationSpeedY = Random.Range(-90, 90);
        rotationSpeedZ = Random.Range(-90, 90);
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(0, 0, 1) * astSpeed * Time.fixedDeltaTime;

        transform.Rotate(rotationSpeedX * Time.fixedDeltaTime, rotationSpeedY * Time.fixedDeltaTime, rotationSpeedZ * Time.fixedDeltaTime);
    }

    private IEnumerator DestroyAfter(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
