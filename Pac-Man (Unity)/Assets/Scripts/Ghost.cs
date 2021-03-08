using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Modes
    private enum Mode
    {
        Chase,
        Scatter
    }

    // Timers
    private const int scatterMode_01 = 7;
    private const int scatterMode_02 = 5;
    private const int ChaseMode = 20;

    // Mode Flags
    private Mode currentMode = Mode.Scatter;
    private Mode lastMode;

    // References
    public GameObject pacMan;
    public Node startingPosition;
    private Node
        currentNode,
        lastNode,
        targetNode;
        
    // Attributes
    public float moveSpeed = 4.9f;
    private Vector2
        direction,
        nextDirection;

    


    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPosition.GetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Node GetNodeAtPosition (Vector2 _position)
    {
        return startingPosition;
    }
}
