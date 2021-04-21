using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LastText : MonoBehaviour
{
    private TextMeshProUGUI text;

    [SerializeField]
    private UnityEvent OnEnd = null;

    [SerializeField]
    private UnityEvent nextLevel = null;

    [SerializeField]
    private string EndGame;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount)
        {
            text = GetComponent<TextMeshProUGUI>();
            text.text = EndGame;
            OnEnd?.Invoke();
        }
        else
        {
            nextLevel?.Invoke();
        }
    }
}