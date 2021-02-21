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
    public float rateTimer;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        rateTimer += Time.deltaTime;
        if(rateTimer >= 10f)
        {
            UpdateAsteroidSpawnRate();
            rateTimer = 0;
        }
    }
    private void UpdateAsteroidSpawnRate()
    {
        if(asteroidSpawnRate.min > 0.25f)
        {
            asteroidSpawnRate.min -= 0.1f;
            asteroidSpawnRate.max -= 0.1f;
        }
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
