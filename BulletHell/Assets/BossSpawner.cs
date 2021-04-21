using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Enemies;

    [SerializeField]
    private float life = 10;

    public void Spawn()
    {
        GameObject enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, Quaternion.identity, transform);
        Destroy(enemy, life);
    }
}