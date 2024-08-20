using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        EnemyScript enemy = collision.GetComponent<EnemyScript>();

        if (collision.CompareTag("RapidFire"))
        {

            return;
        }
    
        if(player != null)
        {
            player.DealDamage();
            DestroyBullet();
        }
        if(enemy != null)
        {
            enemy.Hit();
            DestroyBullet();
        }
        
    }

}
