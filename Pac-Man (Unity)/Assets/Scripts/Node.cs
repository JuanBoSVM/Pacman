using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] neighbors;
    public Vector2[] validDirections;

    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        validDirections = new Vector2[neighbors.Length];

        for (int i = 0; i < neighbors.Length; i++)
        {
            Node neighbor = neighbors[i];

            Vector2 temporalVector = neighbor.transform.localPosition - transform.localPosition;

            validDirections[i] = temporalVector.normalized;
        }

    }


    public Vector2 GetPosition()
    {
        return position;
    }

}
//Medidas del gameboard (40,22)