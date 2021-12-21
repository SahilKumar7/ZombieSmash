using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] zombies;
    public bool isRising = false;
    public bool isFalling = false;
    public int riseSpeed = 1;

    public Vector2 startPosition;
    private int activeZombieIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        pickNewZombie();
    }

    // Update is called once per frame
    void Update()
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
            }
            else
            {
                zombies[activeZombieIndex].transform.Translate(Vector2.down * Time.deltaTime * riseSpeed);
            }
        }
        else
        {
            zombies[activeZombieIndex].transform.position = startPosition;
            pickNewZombie();
        }
    }

    private void pickNewZombie()
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

}