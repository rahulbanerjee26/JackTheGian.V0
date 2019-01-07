using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D body;
    Animator anim;
    public float speed = 8f, maxSpeed = 4f;

    //Initializing the components
    void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate is called twice per frame

    void FixedUpdate()
    {
        move();
    }

    //function to control movement
    void move()
    {
        float forceX = 0f; //Force in x direction
        float velocity = Mathf.Abs(body.velocity.x); //current velocity

        float h = Input.GetAxisRaw("Horizontal"); //input from user 

        if (h > 0) //right key
        {
            if (velocity < maxSpeed)
            {
                forceX = speed;
                anim.SetBool("Walk", true);
                Vector3 direction = transform.localScale;
                direction.x = 1.3f;
                transform.localScale = direction;
            }
             
        }
        else if(h < 0) //left key
        {
            if (velocity < maxSpeed)
            {
                anim.SetBool("Walk", true);
                forceX = -speed;
                Vector3 direction = transform.localScale;
                direction.x = -1.3f;
                transform.localScale = direction;
            }
        }
        else if( h == 0)
        {
            anim.SetBool("Walk", false);
        }

        body.AddForce(new Vector2(forceX, 0));

    }
}
