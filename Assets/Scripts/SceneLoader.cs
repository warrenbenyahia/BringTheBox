using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Animator wallTransition;
    public Animator canvasTransition;
    public GameObject coinScore;
    public GameObject emptyCoinScore;

    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        if (wallTransition)
            wallTransition.gameObject.SetActive(true);     
    }

    // Start is called before the first frame update
    void Start()
    {
    }
        
    public void LoadNextLevel()
    {
        ResetProperties();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 

    public void ReloadScene()
    {
        ResetProperties();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevelSelectorScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSelectedLevel()
    {
        ResetProperties();

        var levelNumber = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text;
        int n;

        if (Int32.TryParse(levelNumber, out n))
        {
            SceneManager.LoadScene(n + 1);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetProperties()
    {
        ScoreManager.score = 0;
        Physics.gravity = new Vector3(0, -(Math.Abs(Physics.gravity.y)), 0);
    }
    
    public void StartTransition()
    {
        StartCoroutine(WallTransition());
    }

    private IEnumerator WallTransition()
    {
        wallTransition.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(ScoreTransition());
    }

    private IEnumerator ScoreTransition()
    {
        canvasTransition.SetTrigger("Show");
        yield return new WaitForSeconds(0.2f);
        ShowScore();
    }

    private void ShowScore()
    {
        emptyCoinScore.SetActive(true);
        coinScore.SetActive(true);

        for (int i = 0; i < ScoreManager.score; i++)
        {
            coinScore.SetActive(true);
            var coin = coinScore.transform.GetChild(i).gameObject;
            coin.SetActive(true);
            coin.GetComponent<Animator>().SetTrigger("Show");
        }
    }
}