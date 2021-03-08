﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform movePoint;
    private Vector2 direction = Vector2.zero;
    private Vector3 lastPosition = Vector3.zero;
    public Animator anim;
    public LayerMask colission;
    public LayerMask ghosts;
    public AudioSource eatSound1;
    public AudioSource eatSound2;
    public AudioSource deathSound;
    private bool moving = true;
    private const float delay = .13f;

    // Start is called before the first frame update
    void Start()
    {
        // Detach the move point from the player
        movePoint.parent = null;
        direction = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        deathCheck();

        if (Vector3.Distance(transform.position, movePoint.position) == 0f)
        {
            CheckInput();

            if (anim.GetBool("Alive"))
            {

                gameObject.GetComponent<Animator>().enabled = false;
            }
        }

        else
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }

        Move();
        UpdateOrientation();

        if (transform.position != lastPosition && anim.GetBool("Alive"))
        {
            if (!eatSound1.isPlaying && !eatSound2.isPlaying)
            {
                eatSound1.PlayDelayed(delay);
                eatSound2.PlayDelayed(delay * 2);
            }
        }

        lastPosition = transform.position;
    }

    void CheckInput()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = 0f;

        }

        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, colission))
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                direction.x = 0f;
                direction.y = Input.GetAxisRaw("Vertical");
            }

        }


        else
        {
            if (!Physics2D.OverlapCircle(movePoint.position + (Vector3)direction, .2f, colission))
            {
                movePoint.position += (Vector3)direction;
                moving = true;
            }

            else
            {
                moving = false;
            }
        }
    }

    //Move PacMan toward move point
    void Move()
    {

    }

    void UpdateOrientation()
    {
        if (direction == Vector2.left)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        else if (direction == Vector2.right)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        else if (direction == Vector2.up)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }

        else if (direction == Vector2.down)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
    }

    void deathCheck()
    {
        if (Physics2D.OverlapCircle(transform.position, .2f, ghosts) && anim.GetBool("Alive"))
        {
            gameObject.GetComponent<Animator>().enabled = true;
            speed = 0.0f;
            anim.SetBool("Alive", false);
            eatSound1.Stop();
            eatSound2.Stop();
            deathSound.Play();
        }
    }
}

