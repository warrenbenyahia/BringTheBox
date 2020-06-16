using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGameObject : MonoBehaviour
{
    public bool isMoving;
    public bool isHorizontal;
    public float distance;
    public float speed;

    private float leftEndPos;
    private float rightEndPos;
    private float upEndPos;
    private float downEndPos;
    private int dir = 1;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        leftEndPos = transform.position.x - distance;    
        rightEndPos = transform.position.x + distance;
        upEndPos = transform.position.y + distance;
        downEndPos = transform.position.y - distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (isHorizontal)
            {
                if (transform.position.x <= leftEndPos && dir == -1)
                    dir = 1;
                else if (transform.position.x >= rightEndPos && dir == 1)
                    dir = -1;

                transform.Translate(dir * Vector2.right * speed * Time.deltaTime, Space.World);
            }
            else
            {
                if (transform.position.y >= upEndPos && dir == 1)
                    dir = -1;
                else if (transform.position.y <= downEndPos && dir == -1)
                    dir = 1;

                transform.Translate(dir * Vector2.up * speed * Time.deltaTime, Space.World);                
            }            
        }
    }
}
