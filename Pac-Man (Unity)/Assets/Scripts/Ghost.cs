using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Ghost : MonoBehaviour
{

    // References
    public GameObject pacMan;
    public Node startingPosition;
    private Node
        currentNode,
        lastCheckedNode;
        public Node targetNode;
        
    // Attributes
    public float moveSpeed = 5f;
    private Vector2
        direction,
        nextDirection;
    private float
        lowestweight = float.MaxValue,
        pathDistance;
    


    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPosition.GetPosition();
        currentNode = startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector2 FindPath()
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
            foreach (Node i in currentNode.neighbors)
            {
                if (!nodesToVisit.Contains(i))
                {
                    nodesToVisit.Enqueue(i);
                }

                // Compare the weight to
                else
                {

                }
            }
                     
            // Calculate the path distance
            foreach (Node i in nodesToVisit)
            {
                float distance = CalculateDistance(currentNode, i);
                float newDistance = pathDistance + distance;
                lowestweight = Math.Min(pathDistance, newDistance);
            }

        }     

        return nextDirection;
    }

    private float CalculateDistance(Node _from, Node _to)
    {
        return (_from.transform.position = _to.transform.position).magnitude;
    }
}
