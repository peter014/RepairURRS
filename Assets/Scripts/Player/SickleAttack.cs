using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SickleAttack : MonoBehaviour
{
    public Slider slider;
    public float maxDistance = 10.0f;
    private float distanceToTravel;
    private float currentDistance;
    private bool meleeAttack = false;
    public float speed = 3.0f;
    private float strength = 1.0f;
    private Collider2D sickleCollider;
    private bool keyPressed = false;
    private float downTime, pressedTime;
    public float maxPressedTime = 2.0f;
    private bool attackInitiated = false;
    public float timeToReach = 3.0f;
    private Animator playerAnim;
    private GameObject sickle;

    public float damage = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        sickle = GameObject.FindGameObjectWithTag("Sickle");
    }

    // Update is called once per frame
    void Update()
    {
        if (keyPressed && !attackInitiated)
        {
            float tiempoPasado = Time.time - downTime;
            SetSliderValue(tiempoPasado);
        }

        if (Input.GetButtonDown("Sickle") && !keyPressed && !attackInitiated)
        {
            InitializeSliderValue();
            //Debug.Log("down");
            keyPressed = true;
            downTime = Time.time;
        }
        else if (Input.GetButtonUp("Sickle") && keyPressed && !attackInitiated)
        {
            //Debug.Log("Up");
            pressedTime = Time.time - downTime;
            keyPressed = false;

            if (pressedTime < 0.5f)
            {
                Debug.Log("melee: " + pressedTime);
                meleeAttack = true;
                sickle.GetComponent<CircleCollider2D>().enabled = true;
                playerAnim.SetTrigger("attacksickle");
                sickle.transform.position = transform.position + Vector3.Cross(transform.up, transform.forward) * 0.75f;
                attackInitiated = true;
                StartCoroutine(MeleeAttack(1));
            }
            else
            {
                Debug.Log("ranged: " + pressedTime);
                meleeAttack = false;
                strength = pressedTime / maxPressedTime;
                distanceToTravel = strength * maxDistance;
                sickle.transform.position = transform.position + Vector3.Cross(transform.up, transform.forward) * 0.75f;
                sickle.GetComponent<CircleCollider2D>().enabled = true;
                sickle.GetComponent<SpriteRenderer>().enabled = true;
                playerAnim.SetTrigger("throwsickle");
                attackInitiated = true;
                StartCoroutine(RangeAttack());
            }
            HideSlider();
        }

        
    }
    
    public IEnumerator MeleeAttack(float duration)
    {
        yield return new WaitForSeconds(duration);
        sickle.GetComponent<CircleCollider2D>().enabled = false;
        attackInitiated = false;
    }

    public IEnumerator RangeAttack()
    {
        float currentDistance = 0;
        Vector3 dir = Vector3.Cross(transform.up, transform.forward);

        while (currentDistance < distanceToTravel)
        {
            sickle.transform.Rotate(0, 0, 300 * Time.deltaTime);
            currentDistance += Time.deltaTime * speed;
            sickle.transform.position += dir * speed * Time.deltaTime;
            yield return null;
        }
        

        dir = (transform.position - sickle.transform.position);
        float distance = dir.magnitude;
        while (distance > 1)
        {
            sickle.transform.Rotate(0, 0, 300 * Time.deltaTime);
            dir = (transform.position - sickle.transform.position);
            distance = dir.magnitude;
            dir.Normalize();
            sickle.transform.position += dir * speed * Time.deltaTime;
            yield return null;
        }
        attackInitiated = false;
        sickle.GetComponent<CircleCollider2D>().enabled = false;
        sickle.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void InitializeSliderValue()
    {
        slider.value = 0.0f;
    }

    private void SetSliderValue(float tiempoPasado)
    {
        if (tiempoPasado > 0.25f)
        {
            //Debug.Log(tiempoPasado);
            slider.gameObject.SetActive(true);
        }
        slider.value += 0.01f;
    }

    private void HideSlider()
    {
        slider.gameObject.SetActive(false);
    }
}
