using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSelect : MonoBehaviour
{

    public List<CountryController> countries;

    public Transform countryList;

    public CountryController selected;

    public GameObject listObject;

    public List<Button> botones;

    public EventSystem sim;

    // Start is called before the first frame update
    void Start()
    {
        selected = countries[0];

        botones = new List<Button>();

        //GenerateCountryList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateCountryList()
    {
        const int interlineado = 100;
        int count = 1;
        for(int i = 0; i < countries.Count; i++)
        {
            CountryController cc = countries[i];
            GameObject go = Instantiate(listObject, countryList);

            go.GetComponent<RectTransform>().localPosition -= count * new Vector3(0, interlineado, 0);

            Text text = go.GetComponentInChildren<Text>();

            go.GetComponent<Button>().onClick.AddListener(delegate { SelectedFromMenu(cc); });

            botones.Add(go.GetComponent<Button>());

            text.text = cc.country.name;

            count++;
        }

        sim.firstSelectedGameObject = botones[0].gameObject;
        

        for(int i = 0; i < botones.Count; i++)
        {
            Navigation customNav = new Navigation();
            customNav.mode = Navigation.Mode.Vertical;
            if (i > 0)
            {
                customNav.selectOnUp = botones[i - 1];
            }

            if (i < botones.Count-1)
            {
                customNav.selectOnDown = botones[i + 1];
            }
            botones[i].navigation = customNav;
        }
    }

    public void Select(CountryController c)
    {
        selected = c;
    }

    public void SelectedFromMenu(CountryController c)
    {
        //Debug.Log(c.country.countryName);
        USSRManager.Instance.LoadNewLevel();
    }
}
