using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimit : MonoBehaviour {

    public GameObject ball;
    public float maxSpeed;
    Rigidbody2D bBody;
    // Update is called once per frame
    void Update () {
        if (ball.GetComponent<Rigidbody2D>().velocity.x > maxSpeed)
        {
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed, ball.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (ball.GetComponent<Rigidbody2D>().velocity.y > maxSpeed)
        {
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(ball.GetComponent<Rigidbody2D>().velocity.x, maxSpeed);
        }
        if (ball.GetComponent<Rigidbody2D>().velocity.x < -maxSpeed)
        {
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-maxSpeed, ball.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (ball.GetComponent<Rigidbody2D>().velocity.y < -maxSpeed)
        {
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(ball.GetComponent<Rigidbody2D>().velocity.x, -maxSpeed);
        }
        Debug.Log(ball.GetComponent<Rigidbody2D>().velocity.x + "x," + ball.GetComponent<Rigidbody2D>().velocity.x + "y");
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        bBody = ball.GetComponent<Rigidbody2D>();
        Debug.Log(bBody.angularVelocity);
        bBody.angularVelocity += Random.Range(-10, 10);
    }
}
