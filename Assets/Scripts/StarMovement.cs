using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovement : MonoBehaviour
{
    [SerializeField] private float starSpeed = 5f;
    private float lifetime = 20f;

    private void Start()
    {
        StartCoroutine(DestroyAfter(lifetime));
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(0, 0, 1) * starSpeed * Time.fixedDeltaTime;
    }

    private IEnumerator DestroyAfter(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
