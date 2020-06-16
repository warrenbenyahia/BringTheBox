using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    public GameObject explosion;
    public GameObject player;

    private AudioSource coinSound;

    // Start is called before the first frame update
    void Start()
    {
        coinSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == player.tag)
        {
            ScoreManager.score++;
            DestroyItself();
        }
    }

    public void DestroyItself()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySound(SoundManager.SoundNames.coinSound, 0.1f);

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
