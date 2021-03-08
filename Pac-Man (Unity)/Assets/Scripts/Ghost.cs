using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Ghost : MonoBehaviour
{

    // References
    public Node startingPosition;
    private Node
        currentNode,
        lastCheckedNode,
        targetNode;
        
    // Attributes
    public float speed = 4.5f;
    private Vector2 direction;
    private float
        lowestweight = float.MaxValue,
        pathDistance;    


    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPosition.GetPosition();
        currentNode = startingPosition;
        targetNode = startingPosition;

    }

    // Update is called once per frame
    void Update()
    {
        // Get end Goal
        if (currentNode.GetPosition() == targetNode.GetPosition())
        {
            targetNode = currentNode.GetPac();

            // Get next step
            //FindPath();
            moveOne();

        }


        // Move
        move();
    }

    void moveOne()
    {
        Node potential = targetNode;

        foreach (Node i in currentNode.neighbors)
        {
            pathDistance = Math.Abs(i.transform.position.x - targetNode.transform.position.x) + Math.Abs(i.transform.position.y - targetNode.transform.position.y);

            if (pathDistance < lowestweight)
            {
                if (i != lastCheckedNode)
                {
                    lowestweight = pathDistance;
                    potential = i;
                }                
            }
        }

        lowestweight = float.MaxValue;
        targetNode = potential;
    }

    void FindPath()
    {
        var visitedNodes = new List<Node>();
        var nodesToVisit = new Queue<Node>();

        // Start from the end
        nodesToVisit.Enqueue(targetNode);

        // If we haven't checked all the nodes, keep going
        while (nodesToVisit.Count > 0)
        {
            // Set the start of the algorithm at the end
            currentNode = nodesToVisit.Dequeue();

            // Add the new nodes to visit to the queue
            for (int i = 0; i < currentNode.neighbors.Length; i++)
            {
                // Check if the node has already been visited
                if (!visitedNodes.Contains(currentNode.neighbors[i]))
                {
                    // Check if the node is on the waiting list
                    if (!nodesToVisit.Contains(currentNode.neighbors[i]))
                    {
                        currentNode.neighbors[i].distanceTo = currentNode.peso[i];
                        nodesToVisit.Enqueue(currentNode.neighbors[i]);
                    }

                    // Compare the weight to
                    else
                    {
                        foreach (Node j in nodesToVisit)
                        {
                            if (currentNode.neighbors[i] == j)
                            {
                                j.distanceTo = Math.Min(j.distanceTo, currentNode.neighbors[i].distanceTo);
                            }
                        }
                    }
                }
            }



            // Calculate the path distance
            foreach (Node i in nodesToVisit)
            {
                if (i.distanceTo < lowestweight)
                {
                    lowestweight = i.distanceTo;
                }
            }

            foreach (Node i in nodesToVisit)
            {
                if (currentNode.distanceTo == lowestweight)
                {
                    lastCheckedNode = currentNode;
                    currentNode = nodesToVisit.Dequeue();
                }
            }

            lowestweight = float.MaxValue;

            visitedNodes.Add(currentNode);
        }

        targetNode = lastCheckedNode;
    }

    void move()
    {
        lastCheckedNode = currentNode;

        transform.position = Vector3.MoveTowards(transform.position, targetNode.GetPosition(), speed * Time.deltaTime);

        if (transform.position == (Vector3)targetNode.GetPosition())
        {
            currentNode = targetNode;
        }
    }
}
