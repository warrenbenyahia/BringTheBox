using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var levelName = GetComponent<Text>();
        levelName.text = SceneManager.GetActiveScene().name;
    }
}
