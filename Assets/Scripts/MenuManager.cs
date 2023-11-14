using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject night5WonStar;

    void Update()
    {
        if(PlayerPrefs.GetInt("maxNightWon") == 5)
        {
            night5WonStar.SetActive(true);
        }   
    }
    public void newGame()
    {
        Manager.nightNum = 1;
        PlayerPrefs.SetInt("maxNightWon", 1);
        SceneManager.LoadScene("SampleScene");
    }

    public void continueGame()
    {
        Manager.nightNum = PlayerPrefs.GetInt("maxNightWon");
        SceneManager.LoadScene("SampleScene");
    }
}
