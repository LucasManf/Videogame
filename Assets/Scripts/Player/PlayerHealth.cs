using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


    //VARIABLES
    public static PlayerHealth instance;

    public int currentHealth, maxHealth;

    public float invincibleLenght; 
    private float invincibleCounter;

    private SpriteRenderer theSR;

    public GameObject deathEffect;
    private PlayerRespawn playerRespawn;
    private UIController uiController;


    public void Awake(){

    instance = this;


    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        theSR = GetComponent<SpriteRenderer>();
        playerRespawn = GetComponent<PlayerRespawn>();
        uiController = FindObjectOfType<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0){

           invincibleCounter -= Time.deltaTime;

           if(invincibleCounter <= 0){

                theSR.color = new Color(theSR.color.r,theSR.color.g, theSR.color.b, 1f);

           }

        }
    }

    public void DealDamage(){

        if(invincibleCounter <= 0){

        currentHealth--;
        UIController.instance.UpdateHealthDisplay();

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                gameObject.SetActive(false);
                uiController.GameOver();
        

                /* if (playerRespawn != null)
                {
                    playerRespawn.Respawn();
                    currentHealth = maxHealth; // Restauramos la salud al máximo
                    UIController.instance.UpdateHealthDisplay(); // Actualizamos el UI
                } */
            }
            else
            { 
                invincibleCounter = invincibleLenght;
                theSR.color = new Color(theSR.color.r,theSR.color.g, theSR.color.b, .5f);
            }


        }

    }

    public void HealPlayer(){

        if (currentHealth < maxHealth)
        {
            currentHealth = currentHealth + 1;
        }
        if(currentHealth > maxHealth){

            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay();

    }

    private void OnCollisionEnter2D(Collision2D other){

            if(other.gameObject.tag == "Platform"){

                transform.parent = other.transform;

            }

    }

    private void OnCollisionExit2D(Collision2D other){

            if(other.gameObject.tag == "Platform"){

                transform.parent = null;

            }

    }

    
}
