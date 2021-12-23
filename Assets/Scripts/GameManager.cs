using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] zombies;
    private bool isRising = false;
    private bool isFalling = false;

    private int riseSpeed = 1;
    private int scoreThreshold = 5;

    private int zombiesSmashed;
    private int livesRemaining;
    private bool gameOver;

    public Vector2 startPosition;
    private int activeZombieIndex = 0;

    public Image life1;
    public Image life2;
    public Image life3;
    public TMP_Text score;

    public Button gameOverButton;

    // Start is called before the first frame update
    void Start()
    {
        zombiesSmashed = 0;
        livesRemaining = 3;
        gameOver = false;
        score.text = "0";
        PickNewZombie();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {

            if (isRising)
            {
                if (zombies[activeZombieIndex].transform.position.y - startPosition.y >= 2.5f)
                {
                    isRising = false;
                    isFalling = true;
                }
                else
                {
                    zombies[activeZombieIndex].transform.Translate(Vector2.up * Time.deltaTime * riseSpeed);
                }
            }
            else if (isFalling)
            {
                if (zombies[activeZombieIndex].transform.position.y - startPosition.y <= 0f)
                {
                    isFalling = false;
                    isRising = false;
                    livesRemaining--;

                    UpdateLifeUI();
                }
                else
                {
                    zombies[activeZombieIndex].transform.Translate(Vector2.down * Time.deltaTime * riseSpeed);
                }
            }
            else
            {
                zombies[activeZombieIndex].transform.position = startPosition;
                PickNewZombie();
            }
        }
    }
    private void PickNewZombie()
    {
        isRising = true;
        isFalling = false;

        // generate a random number between 0 and 6;
        int zombieIndex = UnityEngine.Random.Range(0, zombies.Length);
        activeZombieIndex = zombieIndex;

        GameObject activeZombie = zombies[activeZombieIndex];
        Transform activeTransform = activeZombie.transform;
    
        startPosition = activeTransform.position;
    }

    public void KillEnemy()
    {
        // increase score
        zombiesSmashed++;
        IncreaseSpawnSpeed();
        score.text = zombiesSmashed.ToString();

        // kill enemy
        zombies[activeZombieIndex].transform.position = startPosition;

        PickNewZombie();
    }

    public void UpdateLifeUI()
    {
        if (livesRemaining == 3)
        {
            life1.gameObject.SetActive(true);
            life2.gameObject.SetActive(true);
            life3.gameObject.SetActive(true);
        }
        else if (livesRemaining == 2)
        {
            life1.gameObject.SetActive(true);
            life2.gameObject.SetActive(true);
            life3.gameObject.SetActive(false);
        }
        else if (livesRemaining == 1)
        {
            life1.gameObject.SetActive(true);
            life2.gameObject.SetActive(false);
            life3.gameObject.SetActive(false);
        }
        else if (livesRemaining == 0)
        {
            // gameOver
            life1.gameObject.SetActive(false);
            life2.gameObject.SetActive(false);
            life3.gameObject.SetActive(false);

            gameOver = true;
            gameOverButton.gameObject.SetActive(true);
        }
    }
    private void IncreaseSpawnSpeed()
    {
        if (zombiesSmashed >= scoreThreshold)
        {
            riseSpeed++;
            scoreThreshold *= 2;
        }
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
