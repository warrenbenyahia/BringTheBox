using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    [SerializeField]
    private int life;

    private GameObject numbers;

    public static LifeManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        numbers = GameObject.Find("Numbers");
        SpawnPlayer.OnPlayerSpawned += ReduceLife;
        PlayerGameOver.OnPlayerDeath += GameOver;
        ShowNumber();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int GetLife()
    {
        return life;
    }
    private void ReduceLife()
    {
        HideNumber();
        life--;
        ShowNumber();
    }

    private void GameOver()
    {
        if (life == 0)
            StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySound(SoundManager.SoundNames.gameOverSound, 1f);

        yield return new WaitForSeconds(1.5f);
        SceneLoader.Instance.ReloadScene();
    }

    private void HideNumber()
    {
        numbers.transform.GetChild(life)?.gameObject.SetActive(false);
    }

    private void ShowNumber()
    {
        numbers.transform.GetChild(life)?.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        SpawnPlayer.OnPlayerSpawned -= ReduceLife;
        PlayerGameOver.OnPlayerDeath -= GameOver;
    }
}
