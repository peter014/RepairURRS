using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HousesHUD : MonoBehaviour
{
    private Text houseText;

    private int currentHouses;
    private int totalHouses;

    // Start is called before the first frame update
    void Start()
    {
        totalHouses = USSRManager.Instance.houses2generate;
    }

    private void Awake()
    {
        houseText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHouses = USSRManager.Instance.numHouses;
        houseText.text = "HOUSES: " + currentHouses + "/" + totalHouses;
    }
}
