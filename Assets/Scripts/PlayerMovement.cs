using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;
    public static PlayerMovement instance;
    
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private bool rapidFireActive;
    private float rapidFireStartTime;
    private float rapidFireDuration = 3f;
    private bool SloMoActive;
    private float SloMoStartTime;
    private float SloMoDuration = 3f;
    private bool isShooting = false;
    private bool isPaused = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        rapidFireActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {

            Horizontal = Input.GetAxisRaw("Horizontal");

            if(Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            Animator.SetBool("Running", Horizontal != 0.0F);

            Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
            if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
            {
                Grounded = true;
            }
            else Grounded = false;
            


            if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && Grounded)
            {

                Jump();
            }


            

            if(rapidFireActive) {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isShooting = true;
                } else if (Input.GetKeyUp(KeyCode.Space))
                {
                    isShooting = false;
                }

                if (isShooting)
                {
                    // Dispara continuamente mientras se mantenga presionada la tecla
                    if (Time.time > LastShoot + 0.1f) // Ajusta el intervalo de disparo según sea necesario
                    {
                        Shoot();
                        LastShoot = Time.time;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + 0.25f)
            {
                Shoot();
                LastShoot = Time.time;           
            }
        
        
            if(SloMoActive)
            {
                Time.timeScale = 0.3f;
            }
            else
            {
                Time.timeScale = 1;
            }
            

            if (rapidFireActive && Time.time > rapidFireStartTime + rapidFireDuration)
            {
                rapidFireActive = false;
            }

            if (SloMoActive && Time.time > SloMoStartTime + SloMoDuration)
            {
                SloMoActive = false;
            }
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if(transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
        
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RapidFire"))
        {
            rapidFireActive = true;
            rapidFireStartTime = Time.time;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("SloMo"))
        {
            SloMoActive = true;
            SloMoStartTime = Time.time;
            Destroy(other.gameObject);
        }

    }

    public void SetPauseState(bool paused)
    {
        isPaused = paused;
    }

}
