using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject EnemyBulletPrefab;
    public GameObject Player;
    
    private float LastShoot;
    private int Health = 3;
    private float startPosition;
    public float moveSpeed; // Velocidad de movimiento del enemigo
    private bool movingRight = true; // Dirección inicial de movimiento
    private bool isShooting = false;
    


    void Start()
    {
        startPosition = transform.position.x;
        movingRight = true;
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!Player.activeSelf) return;

        float distance = Mathf.Abs(Player.transform.position.x - transform.position.x);

        if (distance < 1.0f)
        {
            if(Time.time > LastShoot + 0.7f)
            {

                Vector3 direction = Player.transform.position - transform.position;
                if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                isShooting = true;
                EnemyShoot();
                LastShoot = Time.time;

            }
            
        }
        else
        {
            isShooting = false;
        }

        if (!isShooting)
        {
            //movimiento e invertir sprite
            if (movingRight)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                transform.position += moveSpeed * Time.deltaTime * Vector3.right;
            }
            else
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                transform.position += moveSpeed * Time.deltaTime * Vector3.left;
            }

            //limites de movimiento
            if (transform.position.x > startPosition + 0.5f)
            {
                movingRight = false;

            }
            else if (transform.position.x < startPosition - 0.5f)
            {
                movingRight = true;

            }
        }
    }


    private void EnemyShoot()
    {
        Vector3 direction;
        if(transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(EnemyBulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<EnemyBulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health = Health - 1;
        if(Health == 0) Destroy(gameObject);
    }
}
