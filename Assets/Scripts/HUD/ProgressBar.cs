using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{

    public float maximum;
    public float current;
    public Image mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setCurrentFill(float fill)
    {
        //float currentOffset = current - min
        mask.fillAmount = fill;
    }
}
