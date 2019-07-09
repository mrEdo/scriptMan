using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody2D rb;

    private float t = 0.0f;
    private bool moving = false;

    void Awake()
    {
        SpriteRenderer sprRend = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;

        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("image128x128");
        gameObject.transform.Translate(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        //Press the Up arrow key to move the RigidBody upwards
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(0.0f, 2.0f);
            moving = true;
            t = 0.0f;
        }

        //Press the Down arrow key to move the RigidBody downwards
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(0.0f, -1.0f);
            moving = true;
            t = 0.0f;
        }

        if (moving)
        {
            // Record the time spent moving up or down.
            // When this is 1sec then display info
            t = t + Time.deltaTime;
            if (t > 1.0f)
            {
                Debug.Log(gameObject.transform.position.y + " : " + t);
                t = 0.0f;
            }
        }
    }
}