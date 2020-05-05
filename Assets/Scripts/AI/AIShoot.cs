using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIShoot : MonoBehaviour
{
    // GameObject que vas a disparar
    public GameObject bullet;

    // Cadencia en segundo en disparos
    public float shootRate = 3;

    //Rango al que dispara el enemigo
    public float range = 100.0f;

    //Velocidad de las balas que dispara
    public float bulletSpeed = 5;

    //Damage de las balas que dispara
    public float damage = 5;

    //Damage de las balas que dispara
    public float baseDamage = 5;

    //Number in the position on the table
    public int N = 10;

    //Transform desde el que disparar
    public Transform shootPosition;

    float timeLastShoot = 0;

    Movement player;

    public Animator body;

    private FIFO velocities;

    private double t = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        velocities = new FIFO(N);
    }

    // Update is called once per frame
    void Update()
    {
        velocities.Add(player.GetVelocity());
        Vector2 vw = velocities.Average();
        Vector2 xow = player.transform.position;

        Vector2 myPos = transform.position;

        float a = Vector2.Dot(vw, vw) - bulletSpeed * bulletSpeed;
        float b = 2 * Vector2.Dot(xow - myPos, vw);
        Vector2 aux = xow - myPos;
        float c = Vector2.Dot(aux, aux);

        double x2;
        SolveQuadratic(a, b, c, out t, out x2);

        if ((t > x2 && x2 > 0) || t < 0)
        {
            t = x2;
        }

        Vector2 finalPos = xow + vw * (float)t;

        Vector2 orient = (finalPos - myPos);


        orient.Normalize();

        //Debug.Log(orient);



        float angle = Movement.OrientDirection(orient);
        transform.rotation = Quaternion.Euler(0, 0, angle+90);

        timeLastShoot += Time.deltaTime;

        if (CanShoot())
        {
            //Debug.Log("DISPARA");
            timeLastShoot = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject project = Instantiate(bullet, shootPosition.position,
                            shootPosition.rotation);

        Collider2D projectileCollider = project.GetComponent<Collider2D>();
        Collider2D mycollider = transform.root.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(projectileCollider, mycollider);



        project.GetComponent<Bullet>().velocity = bulletSpeed;
        project.GetComponent<Bullet>().damage = damage;
        body.SetTrigger("throw");
    }

    private bool CanShoot()
    {
        return timeLastShoot >= shootRate && 
            (player.gameObject.transform.position - transform.position).magnitude < range;
    }


    class FIFO
    {
        Vector2[] data;
        int entry;

        public FIFO(int tam)
        {
            data = new Vector2[tam];
            entry = 0;
        }

        public void Add(Vector2 item)
        {
            data[entry] = item;
            entry = (entry + 1) % data.Length;
        }

        public Vector2 Average()
        {
            Vector2 sum = Vector2.zero;

            foreach (Vector2 v in data)
            {
                sum += v;
            }
            return sum / data.Length;
        }
    }

    public static void SolveQuadratic(double a, double b, double c, out double x1, out double x2)
    {
        //Quadratic Formula: x = (-b +- sqrt(b^2 - 4ac)) / 2a

        //Calculate the inside of the square root
        double insideSquareRoot = (b * b) - 4 * a * c;

        if (insideSquareRoot < 0)
        {
            //There is no solution
            x1 = double.NaN;
            x2 = double.NaN;
        }
        else
        {
            //Compute the value of each x
            //if there is only one solution, both x's will be the same
            double sqrt = System.Math.Sqrt(insideSquareRoot);
            x1 = (-b + sqrt) / (2 * a);
            x2 = (-b - sqrt) / (2 * a);
        }
    }
}
