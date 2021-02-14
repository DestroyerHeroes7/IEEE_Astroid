using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float moveSpeed;
    public float deadDelay;
    void Start()
    {
        rigidbody.velocity = Vector2.up * moveSpeed;
        Destroy(gameObject, deadDelay);
    }
    void Update()
    {
        
    }
}
