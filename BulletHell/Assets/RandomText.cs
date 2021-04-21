using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomText : MonoBehaviour
{
    private TextMeshProUGUI text;

    [SerializeField]
    private string[] DeathPoems = null;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = DeathPoems[Random.Range(0, DeathPoems.Length)];
    }
}