using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public Player playerScript;

    public Animator playerAnim;
    public Animator powerBoxAnim;
    
    public AudioSource winAudio;

    public GameObject player;
    public GameObject gameoverUI;
    public GameObject gameWonUI;
    public GameObject camErrorUI;
    public GameObject startNightUI;
    public GameObject nextNightUI;
    public GameObject chicaResetButton;
    public GameObject paycheckUI;

    //Animatronics gameobject
    public GameObject freddy;
    public GameObject chica;
    public GameObject bonnie;
    public GameObject cupcake;

    public Text nightStartText;
    public Text energyAmountText;

    public GameObject clock1am;
    public GameObject clock2am;
    public GameObject clock3am;
    public GameObject clock4am;
    public GameObject clock5am;
    public GameObject clock6am;
    public GameObject clock12am;
    public GameObject winEffect;
    public GameObject arrowControls;

    public float currentTime;
    private float nextBatteryActionTime = 0f;
    [SerializeField] private float defaultUseBatteryPeriod;
    [SerializeField] private float camUseBatteryPeriod;

    private float timeDifference = 1f;
    private float SpeedOfPowerIncrease = 0.5f;

    public static int nightNum;

    [SerializeField] private float energyAmount;

    public bool hasRun;
    public bool aboutMove;
    public bool resetButtonPressed;
    public bool chicaOn;
    public bool loopRan;
    public bool powerAvailable;
    public bool generatorOff;

    // Start is called before the first frame update
    void Start()
    {
        //IntialEnergyAmount = 100;
        energyAmount = 100;
        powerAvailable = true;

        Time.timeScale = 1;
        playerAnim = player.GetComponent<Animator>();
        nextNightUI.SetActive(false);
        currentTime = 0;
        gameStart();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        changeClockFace();

        if(aboutMove == true)
        {
            camErrorOn();
        }
        if(aboutMove == false)
        {
            camErrorOff();
        }
        if(Time.time > nextBatteryActionTime)
        {
            if(playerScript.camOn == true)
            {
                nextBatteryActionTime += camUseBatteryPeriod;
                energyAmount -= 1;
                Debug.Log("Cam battery");
            }
            else if(playerScript.camOn == false)
            {
                nextBatteryActionTime += defaultUseBatteryPeriod;
                energyAmount -= 1; 
                Debug.Log("standard battery");
            }
            updateEnergyText();
        }
        if(energyAmount <= 0)
        {
            powerAvailable = false;
        }
        if(Input.GetKey(KeyCode.Space) && playerScript.atPowerBox == true && energyAmount <= 100)
        {
            generatorOff = true;
            powerBoxAnim.Play("PowerBoxOffAnim");
        }
        if(Input.GetKey(KeyCode.Space) == false)
        {
            generatorOff = false;
            powerBoxAnim.Play("PowerBoxOnAnim");
        }
        if(generatorOff == true && energyAmount < 100 && Time.time > timeDifference)
        {
            timeDifference += Time.time - timeDifference + SpeedOfPowerIncrease;
            energyAmount += 1;
        }

        //animatronic night enabling
        if(nightNum == 1 || nightNum == 2)
        {
            freddy.SetActive(true);
            bonnie.SetActive(true);
            chica.SetActive(false);
            cupcake.SetActive(false);
        }
        else
        {
            freddy.SetActive(true);
            bonnie.SetActive(true);
            chica.SetActive(true);
            cupcake.SetActive(true);
        }
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
        winEffect.SetActive(true);
        if(nightNum < 5)
        {
            StartCoroutine(waitForNextNightUI());
        }
        else if(nightNum == 5)
        {
            StartCoroutine(waitForPaycheckUI());
        }
        Time.timeScale = 0;
        if(nightNum < 5)
        {
            NextNight();
        }
        SaveMaxNight();
        //nextNightUI.SetActive(true);
        //StartCoroutine(waitForNextNightUI());
    }

    public void GameOver()
    {
        arrowControls.SetActive(false);
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
        arrowControls.SetActive(true);
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

    public IEnumerator waitForJumpscare(float waitCertainTime)
    {
        yield return new WaitForSecondsRealtime(waitCertainTime);
        GameOver();
    }
    IEnumerator waitForPaycheckUI()
    {
        yield return new WaitForSecondsRealtime(13);
        paycheckUI.SetActive(true);
    }
    public void resetChica()
    {
        resetButtonPressed = true;
        energyAmount -= 10;
        updateEnergyText();
    }

    public void updateEnergyText()
    {
        energyAmountText.text = energyAmount + " %";
    }
}
