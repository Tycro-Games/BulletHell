﻿using UnityEngine;

[CreateAssetMenu(fileName = "new projectile", menuName = "Create staff/new projectile", order = 2)]
public class ProjectileObjects : ScriptableObject
{
    public GameObject projectilePrefab;
    public float speed = 10.0f;
    public int damage = 1;

    public float FirePerSecond = 5;

    public float lifetime = 5.0f;

    public float life = 2;
}