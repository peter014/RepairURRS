using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheatHUD : MonoBehaviour
{
    private Text wheatText;

    private int currentWheats;
    private int totalWheats;

    // Start is called before the first frame update
    void Start()
    {
        totalWheats = USSRManager.Instance.wheats2generate;
    }

    private void Awake()
    {
        wheatText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentWheats = USSRManager.Instance.numWheats;
        wheatText.text = "WHEAT: " + currentWheats + "/" + totalWheats;
    }

}
