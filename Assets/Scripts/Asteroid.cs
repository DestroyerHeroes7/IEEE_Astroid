using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float moveSpeed;
    public float deadDelay;
    public int hp = 10;
    void Start()
    {
        rigidbody.velocity = Vector2.down * moveSpeed;
        Destroy(gameObject, deadDelay);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if(collision.gameObject.CompareTag("Laser"))
        {
            Destroy(collision.gameObject);
            hp -= AsteroidManager.Instance.player.damage;
            if (hp <= 0)
                Destroy(gameObject);
            else
                rigidbody.velocity = Vector2.down * moveSpeed;
        }
    }
}
