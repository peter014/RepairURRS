using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMeleeAttack : MonoBehaviour
{

    // Min distance to attack to player 
    public float distance = 1.0f;

    // Damage inflicted by this enemy
    public float damage = 20.0f;

    //Damage de las balas que dispara
    public float baseDamage = 5;

    // Position of the player
    GameObject player;

    AIMovement mov;

    Health health;
    // Start is called before the first frame update

    public float cadence = 2.5f;

    // Time since last hit
    float lastHitTime = 0;

    public Animator body;

    private void OnDisable()
    {
        Debug.Log("COSIS");
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            health = player.GetComponent<Health>();
        }

        mov = GetComponent<AIMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        lastHitTime += Time.deltaTime;
        Vector2 dir = player.transform.position - transform.position;
        if (distance >= dir.magnitude)
        {
            if(lastHitTime > cadence)
            {
                Debug.Log("Attack");
                //health.Damage(damage);
                lastHitTime = 0;
                mov.StopMoving(.25f);
                body.SetTrigger("attack");
            }
            else
            {
                mov.StopMoving(.25f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().Damage(damage);
        }
    }

}
