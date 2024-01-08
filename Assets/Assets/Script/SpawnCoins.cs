using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public GameObject coinPrefab;
    public float chance = 0.3f;

    void Start()
    {



        TrySpawn();


    }

    void TrySpawn()
    {
        float randomValue = Random.value;

        
        if (randomValue < chance)
        {
            SpawnCoin();
        }

    }
    void SpawnCoin()
    {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);

    }
}
