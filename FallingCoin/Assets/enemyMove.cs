using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    private Vector2 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.position = new Vector2(initialPosition.x,Mathf.Sin(Time.time)*5.0f+initialPosition.y);
    }
}
