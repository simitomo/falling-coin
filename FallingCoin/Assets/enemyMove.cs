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
        transform.position = new Vector2(initialPosition.x,Mathf.Sin(Time.time)*10.0f+initialPosition.y);
    }
}
