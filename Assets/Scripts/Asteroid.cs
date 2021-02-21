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
        SelectScale();
        moveSpeed = 2 - (UtilScript.Remap(hp, 3, 10, 1, 2) - 1);
        deadDelay = 12f / moveSpeed;
        reward = hp * 10;
        rigidbody.velocity = Vector2.down * moveSpeed;
        Destroy(gameObject, deadDelay);
    }
    private void SelectScale()
    {
        hp = Random.Range(3, 11);
        float scale = (float)hp / 10;
        transform.localScale = new Vector3(scale , scale, 1);
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
