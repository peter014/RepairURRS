using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    // Distance to player 
    public float distance = 5.0f;

    // Speed of the enemy
    public float speed = 7.0f;

    // Position of the player
    public Transform player;

    public Animator body;
    public Animator legs;

    Rigidbody2D rigid;

    bool mov = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        rigid = GetComponent<Rigidbody2D>();
    }

    public void StopMoving(float duration)
    {
        mov = false;
        body.SetBool("move", false);
        legs.SetBool("move", false);
        StartCoroutine(Stop(duration));
    }

    IEnumerator Stop(float duration)
    {
        yield return new WaitForSeconds(duration);
        mov = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mov)
        {
            Vector2 dir = player.position - transform.position;

            if (dir.magnitude > distance)
            {
                dir.Normalize();
                Vector2 pos = transform.position;

                // mover al enemigo
                // transform.position = pos + dir * speed * Time.deltaTime;

                rigid.MovePosition(pos + dir * speed * Time.deltaTime);
                body.SetBool("move", true);
                legs.SetBool("move", true);
            }
            else
            {
                StopMoving(0.25f);
                body.SetBool("move", false);
                legs.SetBool("move", false);
            }
            dir.Normalize();
            float angle = Movement.OrientDirection(dir);
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }

}
