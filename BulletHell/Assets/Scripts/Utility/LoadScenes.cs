﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float transTime = 1f;

    public void LoadScene(string name)
    {
        StartCoroutine(LoadLevel(0));
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetTrigger("Fade");

        yield return new WaitForSecondsRealtime(transTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}