using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankScript : MonoBehaviour
{
    public enum bossStates { shooting, hurt, moving, ended };

    public bossStates currentStates;



    public Transform tank;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;


    public Transform leftPoint, rightPoint;
    private bool moveRight;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;


    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;

    public Collider2D hitBox;

    [Header("Health")]
    public float health = 5;
    public float maxHealth;
    public GameObject explosion;
    public Image healthBar;
    private bool isDefeated;


    public GameObject flag;



    // Start is called before the first frame update
    void Start()
    {

        currentStates = bossStates.shooting;
        maxHealth = health;

    }

    // Update is called once per frame
    void Update()
    {

        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        switch (currentStates)
        {

            case bossStates.shooting:

                shotCounter -= Time.deltaTime;

                if (shotCounter <= 0)
                {

                    shotCounter = timeBetweenShots;

                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = tank.localScale;

                }

                break;

            case bossStates.hurt:

                if (hurtCounter > 0)
                {
                    hitBox.enabled = false;

                    hurtCounter -= Time.deltaTime;

                    if (hurtCounter <= 0)
                    {
                        currentStates = bossStates.moving;

                        if (isDefeated)
                        {


                            tank.gameObject.SetActive(false);
                            Instantiate(explosion, tank.position, tank.rotation);
                            flag.SetActive(true);

                            //SoundManager.instance.StopBossMusic();
                            currentStates = bossStates.ended;

                        }

                    }


                }

                break;

            case bossStates.moving:

                if (moveRight)
                {

                    tank.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (tank.position.x > rightPoint.position.x)
                    {

                        tank.localScale = new Vector3(1f, 1f, 1f);


                        moveRight = false;

                        EndMovement();

                    }


                }
                else
                {

                    tank.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (tank.position.x < leftPoint.position.x)
                    {


                        tank.localScale = new Vector3(-1f, 1f, 1f);

                        moveRight = true;

                        EndMovement();


                    }



                }

                break;


        }

    }




    public void TakeHit()
    {


        currentStates = bossStates.hurt;
        hurtCounter = hurtTime;

        anim.SetTrigger("Hit");
        health--;

        if (health <= 0)
        {
            isDefeated = true;

        }

    }

    private void EndMovement()
    {

        currentStates = bossStates.shooting;
        shotCounter = timeBetweenShots;

        anim.SetTrigger("StopMoving");

        hitBox.enabled = true;

    }


}
