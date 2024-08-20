using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header ("Health UI")]
    public UnityEngine.UI.Image heart1, heart2, heart3, heart4, heart5;
    public Sprite heartFull, heartEmpty;

    public Text gemText;
    public Image fadeScreen;
    public float fadeSpeed;
    public GameObject levelCompleteText;

    private bool shouldFadeToBlack, shouldFadeFromBlack;

    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake(){

    instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        /* UpdateGemCount(); */
        /* FadeFromBlack(); */
    }

    // Update is called once per frame
    void Update()
    {
        
        if(shouldFadeToBlack){

            fadeScreen.color = new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f){
                shouldFadeToBlack = false;

                }
        }
        if(shouldFadeFromBlack){

            fadeScreen.color = new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f){
                
                shouldFadeToBlack = false;

            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }

#region HealthDisplay
    public void UpdateHealthDisplay(){

      switch(PlayerHealth.instance.currentHealth){

        case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartFull;
                heart5.sprite = heartFull;
            break;

        case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartFull;
                heart5.sprite = heartEmpty;
            break;

        case 3: heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
            break;

        case 2: heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
            break;

        case 1: heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
            break;

        case 0: heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
            break;

        default: 
                 heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                break;

      }


    }
#endregion


    /* public void UpdateGemCount(){

        gemText.text = LevelManager.instance.gemsCollected.ToString();

    } */

    public void FadeToBlack(){

        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;

    }

    public void FadeFromBlack(){

        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;

    }


#region GAME OVER
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        /* SoundManager.instance.PlaySound(gameOverSound); */

    }


    //Game Over functions
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
#endregion


#region Pause
    private void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        
        if(status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);

    }
#endregion

}
