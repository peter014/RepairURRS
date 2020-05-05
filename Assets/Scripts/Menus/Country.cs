using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "USSR/Country")]
public class Country : ScriptableObject
{
    public string countryName;

    // in Millions
    public float population;
    // in km^2
    public float size;

    public float difficulty;
    
    public float GetDifficulty()
    {
        return USSRManager.Instance.level;
    }

    public float GetEnemyDamageMelee()
    {
        float damage = 5;

        damage = damage * USSRManager.Instance.level;

        return damage;
    }

    public float GetEnemyDamageRanged()
    {
        float damage = 5;

        damage = damage * USSRManager.Instance.level;

        return damage;
    }

    public int GetHousesObjetive()
    {
        int houses = 10;

        houses = houses * (int) USSRManager.Instance.level;

        return houses;
    }

    public int GetWheatObjetive()
    {
        int wheat = 10;

        wheat = wheat * (int)USSRManager.Instance.level;

        return wheat;
    }

    public List<Country> countries;
}
