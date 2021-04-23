using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Enemies;

    [SerializeField]
    private float life = 10;

    [SerializeField]
    private Transform trans = null;

    public void Spawn()
    {
        GameObject enemy;
        if (trans == null)
            enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, Quaternion.identity);
        else
            enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, Quaternion.identity, trans);
        Destroy(enemy, life);
    }

    public void SpawnWithRotationInv()
    {
        GameObject enemy;
        if (trans == null)
            enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, Quaternion.Inverse(transform.rotation));
        else
            enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, Quaternion.identity, trans);
        Destroy(enemy, life);
    }
}