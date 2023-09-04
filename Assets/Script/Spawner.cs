using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private GameObject[] levels;
    [SerializeField] private float stayTime = 15.0f;

    private bool isSpawned;
    private float elapsedTime;

    public void SpawnLevel()
    {
        if (levels == null) return;
        int randomIndex = Random.Range(0, levels.Length);
        GameObject level = Instantiate(levels[randomIndex], transform.position, transform.rotation) as GameObject;
        level.name = level.name.Replace("(Clone)", "").Trim();
        isSpawned = true;
    }

    private void Update()
    {
        if (!isSpawned) return;
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= stayTime)
        {
            elapsedTime = 0;
            isSpawned = false;
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
