using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, ISelectHandler // required interface when using the OnSelect method.
{

    public CountryController cc;

    public MenuSelect menu;

    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");

        foreach(CountryController cc in menu.countries)
        {
            cc.UnSelect();
        }

        cc.Select();

        USSRManager.Instance.country = cc.country;

    }
}
