using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public AudioClip[] smashSounds;
    private AudioSource audioSource;

    public GameObject bloodEffect;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Enemy")
                {
                    audioSource.PlayOneShot(smashSounds[Random.Range(0, smashSounds.Length)], 0.3f);
                    gameObject.GetComponent<GameManager>().KillEnemy();
                    DisplayBloodEffect(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
        }
    }

    private void DisplayBloodEffect(Vector2 pos)
    {
        bloodEffect.transform.position = pos;
        bloodEffect.GetComponent<Animator>().SetTrigger("Smashed");
    }
}
