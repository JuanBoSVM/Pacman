﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 direction = Vector2.zero;
    private Vector3 lastPosition = Vector3.zero;
    public Animator anim;
    public LayerMask ghosts;
    public AudioSource eatSound1;
    public AudioSource eatSound2;
    public AudioSource deathSound;
    private const float delay = .13f;

    // Node References
    public Node startingPosition;
    private Node
        currentNode,
        targetNode;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.right;
        currentNode = startingPosition;

        transform.position = startingPosition.GetPosition();
        targetNode = currentNode;

    }

    // Update is called once per frame
    void Update()
    {
        deathCheck();

        // Reference the target node to compare distance
        if (currentNode.GetPosition() == targetNode.GetPosition())
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

        // Try to move Horizontally

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = 0f;

            foreach (Node i in currentNode.neighbors)
            {
                if (direction.x == -1)
                {
                    if (currentNode.transform.position.x > i.transform.position.x)
                    {
                        targetNode = i;
                    }
                }
                else
                {
                    if (currentNode.transform.position.x < i.transform.position.x)
                    {
                        targetNode = i;
                    }
                }
            }
        }

        // Try to Move Vertically

        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            direction.x = 0f;
            direction.y = Input.GetAxisRaw("Vertical");

            foreach (Node i in currentNode.neighbors)
            {
                if (direction.y == -1)
                {
                    if (currentNode.transform.position.y > i.transform.position.y)
                    {
                        targetNode = i;
                    }
                }
                else
                {
                    if (currentNode.transform.position.y < i.transform.position.y)
                    {
                        targetNode = i;
                    }
                }
            }

        }


        else
        {
            foreach (Node i in currentNode.neighbors)
            {
                if (direction.x == -1)
                {
                    if (currentNode.transform.position.x > i.transform.position.x)
                    {
                        targetNode = i;
                    }
                }
                else if (direction.x == 1)
                {
                    if (currentNode.transform.position.x < i.transform.position.x)
                    {
                        targetNode = i;
                    }
                }
                else if (direction.y == -1)
                {
                    if (currentNode.transform.position.y > i.transform.position.y)
                    {
                        targetNode = i;
                    }
                }
                else
                {
                    if (currentNode.transform.position.y < i.transform.position.y)
                    {
                        targetNode = i;
                    }
                }
            }
        }
    }

    //Move PacMan towards move point
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetNode.GetPosition(), speed * Time.deltaTime);

        if (transform.position == (Vector3)targetNode.GetPosition())
        {
            currentNode = targetNode;
        }

        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);
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

