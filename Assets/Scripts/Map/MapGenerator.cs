using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public float difficulty;

    public int x = 21;
    public int y = 21;

    public int numHouses = 10;

    public int numWheat = 10;


    public int numBanks = 2;

    public Texture2D tex;

    public GameObject wheat;
    public GameObject bank;
    public GameObject house;
    public GameObject wall;
    public GameObject floor;

    public List<AudioClip> music;

    private AudioSource audioSource;

    Vector2 tileSize = new Vector2(1, 1);
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music[UnityEngine.Random.Range(0, music.Count)];
        audioSource.Play();
        Country country = USSRManager.Instance.country;



        /*numHouses = 10;

        numWheat = 10;

        numBanks = 2;
        */
        // generar dependiendo del countrya

        InitializeMap();
    }

    public void InitializeMap()
    {
        tex = new Texture2D(x, y, TextureFormat.ARGB32, false);

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                tex.SetPixel(i, j, Color.black);
            }
        }

        UnityEngine.Random.InitState(System.DateTime.Now.Hour + System.DateTime.Now.Second);

        GenerateMap();

        SaveTextureAsPNG(tex, Application.dataPath + "texture.png");
    }

    void GenerateMap()
    {
        int generatedHouses = 0;
        int generatedBanks = 0;
        int generatedWheat = 0;
        //generate N banks
        while (generatedBanks<numBanks)
        {

            int generatedX = 0;
            int generatedY = 0;
            float ale = 0;
            GenerateAleatoriedPositions(ref generatedX, ref generatedY, ref ale);

            //Debug.Log("BANK: " + generatedX + " - " + generatedY);

            float prob = tex.GetPixel(this.x / 2 + generatedX, this.y / 2 + generatedY).r;

            //Debug.Log("PROOOOOOB: " + prob);

            if(prob == 0)
            {
                Generate(Tile.Bank, 1, tileSize, new Vector2(generatedX, generatedY), 1, 0);
                generatedBanks++;
            }
        }

        //generate wheat
        while (generatedWheat < numWheat)
        {

            int generatedX = 0;
            int generatedY = 0;
            float ale = 0;
            GenerateAleatoriedPositions(ref generatedX, ref generatedY, ref ale);

            //Debug.Log("WHEAT: " + generatedX + " - " + generatedY);

            float prob = tex.GetPixel(this.x/2 + generatedX, this.y / 2 + generatedY).r;

            //Debug.Log("PROOOOOOB: " + prob);

            if (prob<0.25f)
            {
                generatedWheat += Generate(Tile.Wheat, 2, tileSize, new Vector2(generatedX, generatedY), numWheat, 0);
            }
        }

        //generate houses
        while (generatedHouses < numHouses)
        {

            int generatedX = 0;
            int generatedY = 0;
            float ale = 0;
            GenerateAleatoriedPositions(ref generatedX, ref generatedY, ref ale);

            //Debug.Log("HOUSES: " + generatedX + " - " + generatedY);

            float probB = tex.GetPixel(this.x / 2 + generatedX, this.y / 2 + generatedY).r;
            float probH = tex.GetPixel(this.x / 2 + generatedX, this.y / 2 + generatedY).b;
            float probW = tex.GetPixel(this.x / 2 + generatedX, this.y / 2 + generatedY).g;


            //Debug.Log("PROOOOOOB: " + probB + ", " + probH + ", " + probW);

            if (probB < 0.25f && probH<.8f && probW<.5f)
            {
                generatedHouses += Generate(Tile.House, 2, tileSize, new Vector2(generatedX, generatedY), 3, 0);
            }
        }


        int aux = 0;
        for (aux = 1; aux < y; aux++)
        {
            Instantiate(wall, new Vector3((float)-x / 2, aux - (y / 2), -1+aux*0.1f), Quaternion.identity);
            Instantiate(wall, new Vector3((float)x / 2, aux - (y / 2), -1+aux * 0.1f), Quaternion.identity);
        }
        for (aux = 0; aux < x; aux++)
        {
            Instantiate(wall, new Vector3((float)aux - (x / 2), y / 2, -1), Quaternion.identity);
            Instantiate(wall, new Vector3((float)aux - (x / 2), -y / 2, -1), Quaternion.identity);
        }

        for(int i = 0; i<= x; i++)
        {
            for (int j = 0; j <= x; j++)
            {
                Vector3 position = new Vector3((float)i - (x / 2), (float)j - (y / 2), 5);

                Instantiate(floor, position, Quaternion.identity);
            }
        }

    }

    public void GenerateAleatoriedPositions(ref int x, ref int y, ref float ale)
    {
        float signX = 1;
        float signY = 1;

        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0:
                signX = 1;
                signY = 1;
                break;
            case 1:
                signX = -1;
                signY = 1;
                break;
            case 2:
                signX = 1;
                signY = -1;
                break;
            case 3:
                signX = -1;
                signY = -1;
                break;
        }

        x = (int)signX * UnityEngine.Random.Range(3, this.x / 2-3);
        y = (int)signY * UnityEngine.Random.Range(3, this.y / 2-3);

        ale = UnityEngine.Random.Range(0.0f, 1.0f);
    }

    public enum Tile { Bank, House, Wheat };


    //return the number of objects instantiated
    public int Generate(Tile tile, float radius, Vector2 tileSize, Vector2 center, int maxObjects, float z)
    {
        GameObject obj = bank;
        if(tile == Tile.House)
        {
            obj = house;
        }
        else if(tile == Tile.Wheat)
        {
            obj = wheat;
        }
        List<Vector3> positions = new List<Vector3>();

        for (int x = (int)-radius; x <= radius; x += (int)tileSize.x)
        {
            for (int y = (int)-radius; y <= radius; y += (int)tileSize.y)
            {
                float dist = Vector2.Distance(Vector2.zero, new Vector2(x, y));

                if (dist < radius)
                {
                    positions.Add(new Vector3(x, y, dist));
                }
            }
        }
        
        Texture2D tex = new Texture2D(this.tex.width, this.tex.height, TextureFormat.ARGB32, false);
        Graphics.CopyTexture(this.tex, tex);

        //positions.Shuffle();
        positions.Sort((a, b) => a.z.CompareTo(b.z));


        int instantiated = 0;
        int i = 0;
        while (instantiated < maxObjects && i < positions.Count)
        {
            Vector3 pos = positions[i];
            float aleatorio = UnityEngine.Random.Range(0.0f, 1.0f);
            float valorPos = (1 - System.Math.Abs(pos.z / radius));

            if (valorPos >= aleatorio)
            {
                /*Debug.Log(pos.z + ", " + radius + " -> " +
                     valorPos + " -- " + aleatorio);*/
                //instantiate object

                Vector3 finalPos = new Vector3(center.x + pos.x, center.y + pos.y, z);
                float probW = tex.GetPixel(this.x/2 + (int)finalPos.x, this.y / 2 + (int)finalPos.y).g;

                float probH = this.tex.GetPixel(this.x / 2 + (int)finalPos.x, this.y / 2 + (int)finalPos.y).b;

                float probB = tex.GetPixel(this.x / 2 + (int)finalPos.x, this.y / 2 + (int)finalPos.y).r;

                switch (tile)
                {
                    case Tile.Bank:
                        if (probB == 0)
                        {
                            GenerateInfluence(tile, 7.5f, finalPos);

                            GameObject c = Instantiate(obj, finalPos, Quaternion.identity);
                            instantiated++;
                        }
                        break;

                    case Tile.Wheat:
                        if (probW == 0 && probB<.80f)
                        {
                            GenerateInfluence(tile, 2, finalPos);

                            GameObject c = Instantiate(obj, finalPos, Quaternion.identity);
                            instantiated++;
                        }

                        break;
                    case Tile.House:
                        if (probW <.25f && probB < .80 && probH <.25f)
                        {
                            GenerateInfluence(tile, 4, finalPos);

                            GameObject c = Instantiate(obj, finalPos, Quaternion.identity);
                            instantiated++;
                        }
                        break;
                }
            }

            i++;
        }

        return instantiated;
    }

    void GenerateInfluence(Tile t, float radius, Vector2 pos)
    {
        pos.x = pos.x + x / 2;
        pos.y = pos.y + y / 2;

        Color c = Color.red;
        if(t == Tile.House)
        {
            c = Color.blue;
        }
        else if(t == Tile.Wheat)
        {
            c = Color.green;
        }
        tex.SetPixel((int)pos.x, (int)pos.y, c);

        for(int i = (int)-radius; i<=radius; i++)
        {
            for (int j = (int)-radius; j <= radius; j++)
            {
                float dist = Vector2.Distance(new Vector2(0, 0), new Vector2(i, j));

                int auxx = (int)pos.x + i;
                int auxy = (int)pos.y + j;

                if (dist < radius && auxx < x && auxy < y && auxx >= 0&& auxy >= 0)
                {
                    if (dist < 2 && t != Tile.Wheat)
                    {
                        tex.SetPixel(auxx, auxy, tex.GetPixel(auxx, auxy) + Color.Lerp(c, Color.black, 0.0f));
                    }
                    else
                    {
                        tex.SetPixel(auxx, auxy, tex.GetPixel(auxx, auxy) + Color.Lerp(c, Color.black, dist / radius));
                    }
                }
            }
        }
    }

    public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
    {
        byte[] _bytes = _texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(_fullPath, _bytes);
        Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + _fullPath);
    }
}
