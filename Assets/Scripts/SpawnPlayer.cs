using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public static event Action OnPlayerSpawned;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSpawn())
        {
            var pos = transform.position;
            pos.z += 1f;

            if (pos.y > 10)
            {
                pos.y += -9.5f;
            }

            Instantiate(player, pos, Quaternion.identity);

            if (OnPlayerSpawned != null)
                OnPlayerSpawned.Invoke();
        }
    }

    private bool CanSpawn()
    {
        return ControlManager.IsTaped() &&
               ControlManager.IsTouchingGameObject(gameObject) &&
               GameObject.FindGameObjectsWithTag(player.tag).Length == 0 &&
               LifeManager.Instance.GetLife() > 0;
    }
}
