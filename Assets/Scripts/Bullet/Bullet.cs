using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity = 0;
    public float damage = 0;

    //Tiempo de vida de la bala
    public float lifeTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "House")
        {
            Destroy(this.gameObject);
        }

        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().Damage(damage);
            Destroy(this.gameObject);
        }
    }
}
