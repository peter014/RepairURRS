using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class USSRManager : MonoBehaviour
{
    public Country country;

    private static USSRManager _instance;

    public float level = 1;

    public int numHouses = 0;

    public int numWheats = 0;

    public int banks2generate = 2;

    public int wheats2generate = 5;

    public int houses2generate = 5;

    public string nextScene;

    private bool newLevelLoaded = false;

    public AudioSource victorySound;

    public AudioSource gameOverSound;

    public int enemyCount = 0;

    public int enemyMax = 10;

    public static USSRManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else{
            _instance = this;
        }

        numHouses = 0;
        numWheats = 0;

        banks2generate *= (int) level;

        houses2generate *= (int) level;

        wheats2generate *= (int) level;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            newLevelLoaded = false;
            numHouses = 0;
            numWheats = 0;
            Debug.Log("polls");
            SceneManager.LoadScene("Start", LoadSceneMode.Single);
        }
    }

    public void IncrementNumWheats()
    {
        numWheats++;
        if (numHouses >= houses2generate && numWheats >= wheats2generate)
        {
            newLevelLoaded = true;
            WonLevel();
        }
    }

    public void IncrementNumHouses()
    {
        numHouses++;
        if (numHouses >= houses2generate && numWheats >= wheats2generate)
        {
            newLevelLoaded = true;
            WonLevel();
        }
    }

    public void OnPlayerDeath()
    {
        gameOverSound.Play();
        newLevelLoaded = false;
        numHouses = 0;
        numWheats = 0;
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    public void WonLevel()
    {
        victorySound.Play();
        Debug.Log("won");
        nextScene = "Start";
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        level++;
        newLevelLoaded = false;
        numHouses = 0;
        numWheats = 0;
    }

    public void LoadNewLevel()
    {
        enemyCount = 0;
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
        newLevelLoaded = false;
        numHouses = 0;
        numWheats = 0;
    }
}
