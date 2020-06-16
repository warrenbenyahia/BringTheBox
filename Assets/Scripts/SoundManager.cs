using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip[] sounds;

    private AudioSource audioSource;
    private List<string> soundsList = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            AddSound();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void AddSound()
    {
        foreach (var s in sounds)
        {
            soundsList.Add(s.name);
        }
    }

    public void PlaySound(string soundName, float vol = 1f)
    {
        audioSource.clip = sounds.FirstOrDefault(s => s.name == soundName);
        audioSource.volume = vol;
        audioSource.Play();
    }

    public static class SoundNames
    {
        public static readonly string buttonActivatedSound = "ButtonActivated";
        public static readonly string dieSound = "Die";
        public static readonly string gameOverSound = "GameOver";
        public static readonly string levelCompletedSound = "LevelCompleted";
        public static readonly string coinSound = "Coin";
    }
}


