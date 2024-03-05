using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGeneratorScript : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidObjects;
    [SerializeField] private float asteroidSpawnInterval = 1f;
    [SerializeField] private int asteroidPosLimit = 12;

    private void Start()
    {
        StartCoroutine(AsteroidSpawner(asteroidSpawnInterval));
    }

    private IEnumerator AsteroidSpawner(float spawnInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int randPosX = Random.Range(-asteroidPosLimit, asteroidPosLimit);
            int randPosY = Random.Range(-asteroidPosLimit, asteroidPosLimit);
            Vector3 posVector = new Vector3(randPosX, randPosY, transform.position.z);

            int randInt = Random.Range(0, asteroidObjects.Length);
            Instantiate(asteroidObjects[randInt], posVector, Quaternion.identity);
        }
    }
}
