﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastText : MonoBehaviour
{
    private Image text;

    [SerializeField]
    private UnityEvent OnEnd = null;

    [SerializeField]
    private UnityEvent nextLevel = null;

    [SerializeField]
    private Image EndGame;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount)
        {
            text = GetComponent<Image>();
            text = EndGame;
            OnEnd?.Invoke();
        }
        else
        {
            nextLevel?.Invoke();
        }
    }
}