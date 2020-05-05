using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{


    public Image flag;

    public Image stalin;

    public Text title;

    public AudioSource audioSource;

    public GameObject menu;

    public GameObject credits;

    public GameObject tutorial;

    public GameObject creditsBack;

    public GameObject tutorialBack;

    public EventSystem es;

    public GameObject play;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartUp(5f, 1f));
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float Accelerated(float t)
    {
        return Mathf.Pow(t, 2);
    }

    IEnumerator StartUp(float delay, float duration)
    {
        float time = 0;
        Color fin = flag.color;
        Color ini = fin;
        fin.a = 1;
        ini.a = 0;

        //TODOS LOS ALPHA
        flag.color = ini;
        stalin.color = ini;

        Color c = title.color;
        c.a = ini.a;
        title.color = c;

        yield return new WaitForSeconds(delay);
        while (time < duration)
        {
            time += Time.deltaTime;

            ini.a = Accelerated(time / duration);

            //SETEAR LOS COLORES
            flag.color = ini;
            stalin.color = ini;
            c.a = ini.a;
            title.color = c;

            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        menu.SetActive(true);
        play = es.firstSelectedGameObject;
        play.GetComponent<Button>().Select();
    }

    public void loadPlayScene() 
    {
        SceneManager.LoadScene("Level");
    }

    public void Credits()
    {
        menu.SetActive(false);
        es.SetSelectedGameObject(creditsBack);
        creditsBack.GetComponent<Button>().Select();
        credits.SetActive(true);

    }

    public void Tutorial()
    {
        menu.SetActive(false);
        es.SetSelectedGameObject(tutorialBack);
        tutorialBack.GetComponent<Button>().Select();
        tutorial.SetActive(true);
    }

    public void ShowMenu()
    {
        menu.SetActive(true);
        credits.SetActive(false);
        tutorial.SetActive(false);
        es.SetSelectedGameObject(play);
    }

    public void quit()
    {
        Application.Quit();
    }
}
