using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{

    public float moveSpeed = 3;
    public GameObject BulletPrefab;

    private float startPosition;
    private bool moveUp;
    private bool moveDown;
    private bool moveLeft;
    private bool moveRight;
    private float LastShoot;
    private int Health = 3;
    private bool isShooting = false; 
    private bool doubleShot;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = true;
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            isShooting = false;
        }

        if(isShooting && Time.time > LastShoot + 0.5f)
        {
            Shoot();
            LastShoot = Time.time; 
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;

        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        Vector2 move = Vector2.zero;

        if(moveUp)
        {
            move.y += moveAmount;
        }

        if(moveDown)
        {
            move.y -= moveAmount;
        }

        if(moveLeft)
        {
            move.x -= moveAmount;
        }

        if(moveRight)
        {
            move.x += moveAmount;
        }

        position += move;

        if(position.x <= 6.35f)
        {
            position.x = 6.35f;
        }
        if(position.x >= 11.440f)
        {
            position.x = 11.440f;
        }
        if(position.y <= 3.55f)
        {
            position.y = 3.55f;
        }
        if(position.y >= 6.45f)
        {
            position.y = 6.45f;
        }

        transform.position = position;
        
    }

    private void Shoot()
    {
        Vector3 direction = Vector3.right;
        if(doubleShot)
        {
            Vector3 position1 = transform.position;
            position1.y = transform.position.y - 0.05f;
            Vector3 position2 = transform.position;
            position2.y = transform.position.y + 0.05f;

            GameObject bullet = Instantiate(BulletPrefab, position1 + direction * 0.2f, Quaternion.identity);
            bullet.GetComponent<BulletScript>().SetDirection(direction);

            GameObject bullet2 = Instantiate(BulletPrefab, position2 + direction * 0.2f, Quaternion.identity);
            bullet2.GetComponent<BulletScript>().SetDirection(direction);
        }
        else
        {
            GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.2f, Quaternion.identity);
            bullet.GetComponent<BulletScript>().SetDirection(direction);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DoubleShot"))
        {
            doubleShot = true;
        }

        
    }

}
