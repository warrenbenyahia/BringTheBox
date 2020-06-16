using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    public static event Action OnPlayerDeath;

    private float height;
    private float width;
    private float xOffset = 1f;
    private float yTopOffset = 5f;
    private float yBottomOffset = 2f;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize;
        width = Camera.main.aspect * height;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > width - xOffset
            || transform.position.x < -width + xOffset
            || transform.position.y < -height + yBottomOffset
            || transform.position.y > height + yTopOffset)
        {
            PlayerDeath();
        }         
    }

    private void OnParticleCollision(GameObject other)
    {
        AnimatePlayerDeath();
    }

    public void AnimatePlayerDeath()
    {
        StartCoroutine(DestroyPlayer());
    }

    IEnumerator DestroyPlayer()
    {
        yield return new WaitForSeconds(0.4f);
        PlayerDeath();
    }

    private void PlayerDeath()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySound(SoundManager.SoundNames.dieSound, 0.2f);

        Instantiate(explosion, transform.position, Quaternion.identity);
        
        if (OnPlayerDeath != null)
            OnPlayerDeath.Invoke();

        Destroy(gameObject);
    }
}
