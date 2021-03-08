using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] neighbors;
    public Vector2[] validDirections;
    public float [] peso;
    public Vector2[] trayectory;
    public PacMan player;
    private Vector2 position;

    static private Node PacmanNotTheObject;


    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        validDirections = new Vector2[neighbors.Length];

        peso = new float[neighbors.Length];

        trayectory = new Vector2[neighbors.Length];


        if (player.transform.position == transform.position)
        {
            PacmanNotTheObject = this;
        }


        for (int i = 0; i < neighbors.Length; i++)
        {
            Node neighbor = neighbors[i];

            Vector2 temporalVector = neighbor.transform.localPosition - transform.localPosition;


            validDirections[i] = temporalVector.normalized;


            if (neighbor.transform.localPosition.x - transform.localPosition.x == 0)
            {
                peso[i] = Mathf.Abs(neighbor.transform.localPosition.y - transform.localPosition.y);
            }

            else if (neighbor.transform.localPosition.y - transform.localPosition.y == 0)
            {
                peso[i] = Mathf.Abs(neighbor.transform.localPosition.x - transform.localPosition.x);
            }

            trayectory[i] = temporalVector.normalized;
        }

    }

    private void Update()
    {
        if (player.transform.position == transform.position)
        {
            PacmanNotTheObject = this;
        }
    }


    public Vector2 GetPosition()
    {
        return position;
    }

}
//Medidas del gameboard (40,22)