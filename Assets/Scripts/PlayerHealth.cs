using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


//VARIABLES
public static PlayerHealth instance;

public int currentHealth, maxHealth;

public float invicibleLenght; 
private float invicibleCounter;

private SpriteRenderer theSR;

public GameObject deathEffect;
private PlayerRespawn playerRespawn;


public void Awake(){

instance = this;


}




    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
       theSR = GetComponent<SpriteRenderer>();
       playerRespawn = GetComponent<PlayerRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invicibleCounter > 0){

           invicibleCounter -= Time.deltaTime;

           if(invicibleCounter <= 0){

                theSR.color = new Color(theSR.color.r,theSR.color.g, theSR.color.b, 1f);

           }

        }
    }

    public void DealDamage(){

        if(invicibleCounter <= 0){

        currentHealth--;
        UIController.instance.UpdateHealthDisplay();

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                gameObject.SetActive(false);
        

                if (playerRespawn != null)
                {
                    playerRespawn.Respawn();
                    currentHealth = maxHealth; // Restauramos la salud al máximo
                    UIController.instance.UpdateHealthDisplay(); // Actualizamos el UI
                }
            }
            else
            { 
                invicibleCounter = invicibleLenght;
                theSR.color = new Color(theSR.color.r,theSR.color.g, theSR.color.b, .5f);
            }


        }

    }

    public void HealPlayer(){

        currentHealth++;
        if(currentHealth > maxHealth){

            currentHealth = maxHealth;
        }

     /* UIController.instance.UpdateHealthDisplay(); */

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
