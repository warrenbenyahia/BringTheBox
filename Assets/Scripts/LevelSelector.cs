using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public void LoadSelectedLevel()
    {
        var levelNumber  = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text;
        int n;

        if (Int32.TryParse(levelNumber, out n))
        {
            SceneManager.LoadScene(n + 1);
        }
    }
}
