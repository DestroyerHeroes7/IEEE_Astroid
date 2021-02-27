using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public GameObject buffPrefab;
    public Global.RangeFloat buffSpawnRate = new Global.RangeFloat(15f, 30f);
    public Transform spawnPoint;
    private void Start()
    {
        StartCoroutine(SpawnCycle());
    }
    public void SpawnBuff()
    {
        Instantiate(buffPrefab, spawnPoint.position + (Vector3.right * Random.Range(-2f, 2f)), Quaternion.identity);
    }
    private IEnumerator SpawnCycle()
    {
        while (true)
        {
            SpawnBuff();
            Debug.Log("Spawn");
            yield return new WaitForSeconds(Random.Range(buffSpawnRate.min, buffSpawnRate.max));
        }
    }
}
