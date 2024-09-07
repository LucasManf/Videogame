using System;
using System.Collections;
using System.Collections.Generic;
/* using Unity.Mathematics; */
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject GranadePrefab;
    public float Speed;
    public float JumpForce;
    public static PlayerMovement instance;

    public GameObject RapidFireText; // Prefabricado del sprite que deseas mostrar como texto
    public GameObject SloMoText;
    public GameObject ShotgunText;
    public float textDuration = 1f;
    public float TextMovementSpeed = 0.2f; // Velocidad de movimiento hacia arriba

    
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private bool rapidFireActive;
    private float rapidFireStartTime;
    private float rapidFireDuration = 5f;
    private bool SloMoActive;
    private float SloMoStartTime;
    private float SloMoDuration = 3f;
    private bool ShotgunActive;
    private float ShotgunStartTime;
    private float ShotgunDuration = 7;
    private bool isShooting = false;
    private bool isPaused = false;

    private int granades = 3;

    

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
            Animator.SetBool("Jumping", Grounded != true);


            if(Physics2D.Raycast(transform.position, Vector3.down, 0.15f))
            {
                Grounded = true;
            }
            else Grounded = false;
            


            if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && Grounded)
            {

                Jump();
            }


            

            if(rapidFireActive)
            {
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
                        if(ShotgunActive)
                        {
                            Shotgun();
                        }
                        else
                        {
                            Shoot();
                        }
                        LastShoot = Time.time;   
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + 0.25f)
            {
                if(ShotgunActive)
                {
                    if(Time.time > LastShoot + 0.8f)
                    {
                        Shotgun();
                        LastShoot = Time.time;
                    }
                    
                }
                else
                {
                    Shoot();
                    LastShoot = Time.time;
                }
                
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
                Speed = 1;
            }

            if (ShotgunActive && Time.time > ShotgunStartTime + ShotgunDuration)
            {
                ShotgunActive = false;
                Speed = 1;
            }
        }
    }



    private IEnumerator ShowTemporaryText(GameObject text, float duration)
    {
        // Instanciar el sprite
        GameObject spriteObject = Instantiate(text, transform.position, Quaternion.identity);

        // Obtener el SpriteRenderer
        SpriteRenderer spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // Mover el sprite hacia arriba
            spriteObject.transform.Translate(Vector3.up * TextMovementSpeed * Time.deltaTime);

            // Reducir la transparencia
            Color color = spriteRenderer.color;
            color.a = 1f - elapsedTime / duration;
            spriteRenderer.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destruir el sprite
        Destroy(spriteObject);
    }

    private void Shoot()
    {
        Vector3 direction;
        if(transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.15f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
        
    }

    private void Shotgun()
    {
        // Shoot straight bullet
        Vector3 direction = transform.localScale.x == 1.0f ? Vector2.right : Vector2.left;
        GameObject bullet1 = Instantiate(BulletPrefab, transform.position + direction * 0.15f, Quaternion.identity);
        bullet1.GetComponent<BulletScript>().SetDirection(direction);

        // Shoot bullet at 30 degrees up
        float angleUp = 30.0f * Mathf.Deg2Rad;  // Convert degrees to radians
        direction = Quaternion.Euler(0, 0, angleUp) * (transform.localScale.x == 1.0f ? Vector2.right : Vector2.left);
        GameObject bullet2 = Instantiate(BulletPrefab, transform.position + direction * 0.15f, Quaternion.identity);
        bullet2.GetComponent<BulletScript>().SetDirection(direction);

        // Shoot bullet at 30 degrees down
        float angleDown = -30.0f * Mathf.Deg2Rad;  // Convert degrees to radians
        direction = Quaternion.Euler(0, 0, angleDown) * (transform.localScale.x == 1.0f ? Vector2.right : Vector2.left);
        GameObject bullet3 = Instantiate(BulletPrefab, transform.position + direction * 0.15f, Quaternion.identity);
        bullet3.GetComponent<BulletScript>().SetDirection(direction);

        // Adjust the angle for the second and third bullets
        float angleAdjustment = 300.0f * Mathf.Deg2Rad;  // Adjust the angle as needed
        angleUp += angleAdjustment;
        angleDown -= angleAdjustment;

        // Calculate directions for the second and third bullets with the adjusted angles
        direction = Quaternion.Euler(0, 0, angleUp) * (transform.localScale.x == 1.0f ? Vector2.right : Vector2.left);
        bullet2.GetComponent<BulletScript>().SetDirection(direction);

        direction = Quaternion.Euler(0, 0, angleDown) * (transform.localScale.x == 1.0f ? Vector2.right : Vector2.left);
        bullet3.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
        Animator.SetBool("Jumping", true);
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
            StartCoroutine(ShowTemporaryText(RapidFireText, textDuration));
        }

        if (other.gameObject.CompareTag("SloMo"))
        {
            SloMoActive = true;
            SloMoStartTime = Time.time;
            Destroy(other.gameObject);
            Speed = 1.5f;
            StartCoroutine(ShowTemporaryText(SloMoText, textDuration));
        }

        if (other.gameObject.CompareTag("Shotgun"))
        {
            ShotgunActive = true;
            ShotgunStartTime = Time.time;
            Destroy(other.gameObject);
            StartCoroutine(ShowTemporaryText(ShotgunText, textDuration));
        }

        if (other.gameObject.CompareTag("Medikit"))
        {
            PlayerHealth.instance.HealPlayer();
            Destroy(other.gameObject);
        }

    }

    
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Dark"))
        {
  
            other.gameObject.SetActive(false);
        }

        if (!other.gameObject.CompareTag("Dark"))
        {
  
            other.gameObject.SetActive(true);
        }
    }

/*     private void ThrowGranade()
    {
        Vector3 direction;
        if(transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(GranadePrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<GranadeScript>().SetDirection(direction);
    } */

    public void SetPauseState(bool paused)
    {
        isPaused = paused;
    }

}
