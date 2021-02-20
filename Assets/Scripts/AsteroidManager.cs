using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager Instance;
    public Player player;
    public GameObject asteroidPrefab;
    public Transform spawnPoint;
    public Global.RangeFloat asteroidSpawnRate = new Global.RangeFloat(1.5f, 2);
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StartCoroutine(SpawnCycle());
    }
    private IEnumerator SpawnCycle()
    {
        while(true)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(Random.Range(asteroidSpawnRate.min, asteroidSpawnRate.max));
        }
    }
    private void SpawnAsteroid()
    {
        Instantiate(asteroidPrefab, spawnPoint.position + (Vector3.right * Random.Range(-2f, 2f)), Quaternion.identity);
    }
}
