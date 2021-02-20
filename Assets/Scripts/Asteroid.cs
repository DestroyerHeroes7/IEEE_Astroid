using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float moveSpeed;
    public float deadDelay;
    public int hp = 10;
    public int reward;
    void Start()
    {
        reward = hp * 10;
        rigidbody.velocity = Vector2.down * moveSpeed;
        Destroy(gameObject, deadDelay);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Laser"))
        {
            Destroy(collision.gameObject);
            hp -= AsteroidManager.Instance.player.damage;
            if (hp <= 0)
            {
                LevelManager.Instance.OnDestroyAsteroid(reward);
                UIManager.Instance.OnDestroyAsteroid();
                Destroy(gameObject);
            }
            else
                rigidbody.velocity = Vector2.down * moveSpeed;
        }
    }
}
