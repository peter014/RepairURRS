using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float sickleDamage;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Sickle").GetComponent<SickleDamage>().damage = sickleDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
