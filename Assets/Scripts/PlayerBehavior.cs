using Assets.Scripts;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public GameObject platform;
    public GameObject goal;
    public GameObject button;
    public GameObject coin;
        
    [SerializeField]private LayerMask groundLayer;
    private Rigidbody rb;
    private AudioSource pointSound;
    private float dir;
    private PlatformMovement platformMovement;
    private bool success;
    private bool isCollidingWithButton;
    private bool isCollidingWithGoal;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pointSound = GetComponent<AudioSource>();
        dir = 1f;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    void FixedUpdate()
    {
        var colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, groundLayer);
       
        //If player is in the air OR only in the trigger part of a platform
        if ((colliders.Length == 0 || colliders.All(c => c.isTrigger)) && !isCollidingWithButton && !isCollidingWithGoal)
        {
            transform.Rotate(Vector3.back * 5f * dir);
        }
        else
        {
            var collider = colliders.FirstOrDefault(p => p?.gameObject?.tag == platform.tag);
            platformMovement = collider?.GetComponent<PlatformMovement>();

            StopRotation(collider);

            if (platformMovement == null)
                return;


            rb.velocity = platformMovement.velocity;
            dir = platformMovement.direction;
        }
    }

    private void StopRotation(Collider c)
    {
        Quaternion rot = transform.rotation;
        rot.z = Mathf.Round(rot.z / 90) * 90;
        transform.rotation = rot;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == button.tag)
        {
            isCollidingWithButton = true;
        }
        else if (collision.gameObject.tag == goal.tag && !isCollidingWithGoal)
        {
            isCollidingWithGoal = true;

            if (SoundManager.Instance)
                SoundManager.Instance.PlaySound(SoundManager.SoundNames.levelCompletedSound, 0.7f);

            StartCoroutine(LevelComplete());
        }
    }

    private IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(1f);
        SceneLoader.Instance.StartTransition();
    }
}
