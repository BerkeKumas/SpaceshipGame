using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGeneratorScript : MonoBehaviour
{
    [SerializeField] private GameObject starObject;
    [SerializeField] private int starPosLimit = 10;
    [SerializeField] private float spawnInterval = 20f;


    void Start()
    {
        StartCoroutine(StarGenerator(spawnInterval));
    }

    private IEnumerator StarGenerator(float spawnInterval)
    {
        while (true) 
        { 
            int randPosX = Random.Range(-starPosLimit, starPosLimit);
            int randPosY = Random.Range(-starPosLimit, starPosLimit);
            Vector3 posVector = new Vector3(randPosX, randPosY, transform.position.z);
            Instantiate(starObject, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
