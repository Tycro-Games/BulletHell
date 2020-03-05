﻿using UnityEngine;
[CreateAssetMenu (fileName = "new projectile", menuName = "Create new projectile", order = 2)]
public class ProjectileObjects : ScriptableObject
{
    public GameObject projectilePrefab;
    public float speed = 10.0f;
    public int damage = 1;
    public float Thickness = 0.25f;
   
}
