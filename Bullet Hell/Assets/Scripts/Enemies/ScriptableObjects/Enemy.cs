﻿using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="New Enemy",menuName ="Create new enemy",order =1)]
public class Enemy : ScriptableObject
{
    public GameObject prefabEnemy;
}