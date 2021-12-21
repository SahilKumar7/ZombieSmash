using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] zombies;
    public bool isRising = false;
    public bool isFalling = false;

    private int activeZombieIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    
        Vector2 startPosition = activeTransform.position;
    }

}
