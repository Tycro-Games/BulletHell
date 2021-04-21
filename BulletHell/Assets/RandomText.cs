using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomText : MonoBehaviour
{
    private Image text;

    [SerializeField]
    private Sprite[] DeathPoems = null;

    private void OnEnable()
    {
        text = GetComponent<Image>();
        text.sprite = DeathPoems[Random.Range(0, DeathPoems.Length)];
    }
}