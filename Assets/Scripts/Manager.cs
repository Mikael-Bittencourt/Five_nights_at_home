using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public AudioSource winAudio;

    public GameObject gameoverUI;
    public GameObject gameWonUI;
    public GameObject camErrorUI;
    public GameObject startNightUI;
    public GameObject nextNightUI;
    public Text nightStartText;

    public GameObject clock1am;
    public GameObject clock2am;
    public GameObject clock3am;
    public GameObject clock4am;
    public GameObject clock5am;
    public GameObject clock6am;
    public GameObject clock12am;

    public float currentTime;

    public static int nightNum;

    public bool hasRun;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        nextNightUI.SetActive(false);
        currentTime = 0;
        gameStart();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        changeClockFace();
    }

    private void changeClockFace()
    {
        if(currentTime > 90 && currentTime < 179)
        {
            Debug.Log("1am");
            clock12am.SetActive(false);
            clock1am.SetActive(true);
        }
        if(currentTime > 179 && currentTime < 268)
        {
            clock1am.SetActive(false);
            clock2am.SetActive(true);
        }
        if(currentTime > 268 && currentTime < 357)
        {
            clock2am.SetActive(false);
            clock3am.SetActive(true);
        }
        if(currentTime > 357 && currentTime < 446)
        {
            clock3am.SetActive(false);
            clock4am.SetActive(true);
        }
        if(currentTime > 446 && currentTime < 535)
        {
            clock4am.SetActive(false);
            clock5am.SetActive(true);
        }
        if(currentTime > 535 && hasRun == false)
        {
            clock5am.SetActive(false);
            clock6am.SetActive(true);
            GameWon();
            hasRun = true;
        }
    }

    public void GameWon()
    {
        winAudio.Play();
        gameWonUI.SetActive(true);
        StartCoroutine(waitForNextNightUI());
        Time.timeScale = 0;
        NextNight();
        SaveMaxNight();
        //nextNightUI.SetActive(true);
        //StartCoroutine(waitForNextNightUI());
    }

    public void GameOver()
    {
        gameoverUI.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(waitForBacktoMenu());
    }

    public void NextNight()
    {
        nightNum += 1;
    }
    public void SaveMaxNight()
    {
        if(nightNum > PlayerPrefs.GetInt("maxNightWon"))
        {
            PlayerPrefs.SetInt("maxNightWon", nightNum);
        }
    }
    public void ResetMaxNight()
    {
        PlayerPrefs.SetInt("maxNightWon", 0);
    }

    private void gameStart()
    {
        nightStartText.text = "Night " + nightNum + ":";
        startNightUI.SetActive(true);
        StartCoroutine(waitForStartUIDisaper());
        hasRun = false;
    }

    public void camErrorOn()
    {
        camErrorUI.SetActive(true);
    }
    public void camErrorOff()
    {
        camErrorUI.SetActive(false);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void NextNightLoad()
    {
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator waitForStartUIDisaper()
    {
        yield return new WaitForSeconds(3);
        startNightUI.SetActive(false);
    }

    IEnumerator waitForNextNightUI()
    {
        yield return new WaitForSecondsRealtime(13);
        nextNightUI.SetActive(true);
    }
    IEnumerator waitForBacktoMenu()
    {
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene("Menu");
    }
}
