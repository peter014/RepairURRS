using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryController : MonoBehaviour
{

    public Country country;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        GetComponent<Image>().color = Color.red;
    }

    public void UnSelect()
    {
        GetComponent<Image>().color = Color.white;
    }
}
