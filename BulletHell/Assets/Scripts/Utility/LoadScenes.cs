using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float transTime = 1f;

    private int indexa = 0;

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
        indexa = levelIndex;
        yield return new WaitForSecondsRealtime(transTime);

        SceneManager.LoadScene(levelIndex);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(indexa);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}