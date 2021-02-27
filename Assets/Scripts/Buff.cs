using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float moveSpeed;
    public float deadDelay;
    private void Start()
    {
        rigidbody.velocity = Vector2.down * moveSpeed;
        Destroy(gameObject, deadDelay);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Laser"))
        {
            Player.Instance.OnGetBuff(GetRandomBuff());
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
    private Global.Buff GetRandomBuff()
    {
        return (Global.Buff)Random.Range(0, 4);
    }
}
