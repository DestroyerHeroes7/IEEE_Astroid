using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager Instance;
    public Player player;
    public GameObject asteroidPrefab;
    public Transform spawnPoint;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SpawnAsteroid();
    }
    void Update()
    {
        
    }
    private void SpawnAsteroid()
    {
        Instantiate(asteroidPrefab, spawnPoint.position + (Vector3.right * Random.Range(-2f, 2f)), Quaternion.identity);
    }
}
