using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatLeft : MonoBehaviour
{
    // Health amount the player will be heal
    public float healthAmount = 20;

    public AudioSource pickUpWheatSound;

    public Wheat papa;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pickUpWheatSound.Play();
            if(collision.gameObject.GetComponent<Health>().health <
                collision.gameObject.GetComponent<Health>().maxHealth)
            {
                collision.gameObject.GetComponent<Health>().Heal(healthAmount);
            }
            else{
                USSRManager.Instance.IncrementNumWheats();
            }
            Destroy(this.gameObject);
            papa.StartCoroutine(papa.Revive());
        }
        /*
        if(collision.gameObject.tag == "Wheat")
        {
            Destroy(this.gameObject);
        }
        */
    }
}
