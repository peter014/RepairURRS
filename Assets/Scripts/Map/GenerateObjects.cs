using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class GenerateObjects : MonoBehaviour
{
    Vector2 tileSize = new Vector2(1, 1);

    //GameObject to generate
    public GameObject obj;

    // Center of the generation
    public Transform center;

    // radio de la generacion
    public float radius = 3;

    // max objects
    public int maxObjects = 7;

    public float hope = 0.05f;

    int instantiated = 0;

    private Random rng = new Random();


    public void Generate()
    {
        List<Vector3> positions = new List<Vector3>();

        for (int x = (int)-radius; x <= radius; x+=(int)tileSize.x)
        {
            for (int y = (int)-radius; y <= radius; y+= (int)tileSize.y)
            {
                float dist = Vector2.Distance(Vector2.zero, new Vector2(x, y));

                if (dist < radius)
                {
                    positions.Add(new Vector3(x, y, dist));
                }
            }
        }


        //positions.Shuffle();
        positions.Sort((a, b) => a.z.CompareTo(b.z));

        int i = 0;
        while(instantiated < maxObjects && i<positions.Count)
        {
            Vector3 pos = positions[i];
            float aleatorio = Random.Range(0.0f, 1.0f);
            float valorPos = (1 - System.Math.Abs(pos.z / radius));

            if(valorPos >= aleatorio)
            {
                /*Debug.Log(pos.z + ", " + radius + " -> " +
                     valorPos + " -- " + aleatorio);*/
                //instantiate object
                instantiated++;

                pos.z = -center.position.z;

                GameObject c = Instantiate(obj, center.position + pos, center.rotation);
            }

            i++;
        }

    }


   
}

public static class ShuffleList
{
    public static void Shuffle<T>(this IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (System.Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
