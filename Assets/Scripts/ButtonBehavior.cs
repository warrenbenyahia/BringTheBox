using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem fireParticle;

    private Animator animator;
    private Collision col;
    private bool animationFinished;
    private bool soundPlayed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == player.tag)
        {
            if (!soundPlayed)
            {
                if (SoundManager.Instance)
                {
                    SoundManager.Instance.PlaySound(SoundManager.SoundNames.buttonActivatedSound);
                    soundPlayed = true;
                }
            }

            if (animationFinished)
            {
                DestroyPlayer();
                return;
            }

            col = collision;
            
            animator.SetBool("playButtonDown", true);

            var main = fireParticle.main;
            main.startLifetime = 0.15f;
            main.startSize = 0.2f;
            main.simulationSpeed = 0.3f;

            var emission = fireParticle.emission;
            emission.rateOverTime = 30;
        }

    }

    public void AnimationEnded()
    {
        DestroyPlayer();
        animationFinished = true;
    }

    private void DestroyPlayer()
    {
        col.gameObject.GetComponent<PlayerGameOver>()?.AnimatePlayerDeath();
    }
}
