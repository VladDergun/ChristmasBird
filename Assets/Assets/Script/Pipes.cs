using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 4f;
    private float leftEdge;

    private void Start()
    {
        
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        MoveObject();
    }
    /*
    void MoveObject()
    {
        float smoothDeltaTime = Mathf.Lerp(Time.deltaTime, Time.smoothDeltaTime, 0.5f); // Use lerping for smoother deltaTime

        transform.position += Vector3.left * speed * smoothDeltaTime;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
    */
    void MoveObject()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }


}