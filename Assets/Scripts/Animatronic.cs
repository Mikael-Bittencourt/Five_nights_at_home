using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatronic : MonoBehaviour
{
    private Animator tpAnimation;

    public Player player;
    public Manager manager;
    
    public GameObject animatronic;
    public GameObject StartPos;
    public GameObject StairsMid;
    public GameObject StairsTop;
    public GameObject EnteringHall;
    public GameObject KillPosition;
    public GameObject facingAway;
    public GameObject hiddingSpot;

    //cude that is attached to flashlight to check when players had flash enemy
    public GameObject flashLightDetect;

    public int teleportChoice;

    [SerializeField]private float teleportTime;
    [SerializeField]private float time;
    [SerializeField]private float timePassed;

    [SerializeField]private bool startPositionBool;
    [SerializeField]private bool stairsMidBool;
    [SerializeField]private bool stairsTopBool;
    [SerializeField]private bool enteringHallBool;
    [SerializeField]private bool killBool;
    [SerializeField]private bool facingAwayBool;

    private bool playerHasFalshed;
    [SerializeField]private bool HasRan;
    private bool aboutMove;

    private Vector3 startPos;
    private Vector3 stairsTopPos;
    private Vector3 stairsMidPos;
    private Vector3 enteringHallPos;
    private Vector3 killPos;
    private Vector3 currentPos;
    private Vector3 facingAwayPos;
    private Vector3 hiddingPos;

    // Start is called before the first frame update
    void Start()
    {
        startPositionBool = false;
        stairsMidBool = false;
        playerHasFalshed = false;

        tpAnimation = animatronic.GetComponent<Animator>();
        startPos = StartPos.transform.position;
        stairsTopPos = StairsTop.transform.position;
        stairsMidPos = StairsMid.transform.position;
        enteringHallPos = EnteringHall.transform.position;
        killPos = KillPosition.transform.position;   
        facingAwayPos = facingAway.transform.position;
        hiddingPos = hiddingSpot.transform.position;
        StartCoroutine(StartPosTimer());

        tpAnimation.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        currentPos = this.gameObject.transform.position;

        if(startPositionBool == true)
        {
            startPositionBool = false;
            StartCoroutine(firstTeleport());
        }
        if(stairsMidBool == true)
        {
            stairsMidBool = false;
            StartCoroutine(StairsMidTeleport());
        }
        if(stairsTopBool == true)
        {
            stairsTopBool = false;
            StartCoroutine(StairsTopTeleport());
        }
        if(facingAwayBool == true)
        {
            facingAwayBool = false;
            StartCoroutine(LookingAwayTeleport());
        }
        if(enteringHallBool == true)
        {
            if(HasRan == false)
            {
                Debug.Log("is running");
                time = 0;
                time += Time.deltaTime;
                HasRan = true;
            }
            //enteringHallBool = false;
            if(player.isFlashing == true && time < 10)
            {
                playerHasFalshed = true;
                StartCoroutine(hiddingPosTeleport());
            }
            if(playerHasFalshed == false && time > 10)
            {
                Debug.Log("HUH");
                manager.GameOver();
            }
        }
        if(aboutMove == true)
        {
            manager.camErrorOn();
        }
        if(aboutMove == false)
        {
            manager.camErrorOff();
        }
    }

    private void teleportTimeSetter()
    {
        switch(Manager.nightNum)
        {
        case 1:
            teleportTime = Random.Range(40f, 50f);
            break;
        case 2:
            teleportTime = Random.Range(30f, 40f);
            break;
        case 3:
            teleportTime = Random.Range(20f, 30f);
            break;
        case 4:
            teleportTime = Random.Range(10f, 20f);
            break;
        case 5:
            teleportTime = Random.Range(5f, 10f);
            break;
        }
    }

    //Teleporters that randomly select a timer to run depending on the position
    IEnumerator hiddingPosTeleport()
    {
        enteringHallBool = false;
        HasRan = false;
        playerHasFalshed = false;
        aboutMove = true;
        tpAnimation.Play("Hidding_Pos");
        transform.position = hiddingPos;
        yield return new WaitForSeconds(1);
        aboutMove = false;
        yield return new WaitForSeconds(teleportTime);
        teleportChoice = 0;
        teleportChoice = Random.Range(1,4);
        Debug.Log("Working");
            switch(teleportChoice)
            {
            case 1:
                StartCoroutine(LookingAwayTimer());
                break;
            case 2:
                StartCoroutine(StairsMidTimer());
                break;
            case 3:
                StartCoroutine(StairsTopTimer());
                break;
            case 4:
                StartCoroutine(hiddingPosTeleport());
                break;
            }
    }

    IEnumerator LookingAwayTeleport()
    {
        yield return new WaitForSeconds(teleportTime);
        teleportChoice = 0;
        teleportChoice = Random.Range(1,3);
        Debug.Log("Working");
            switch(teleportChoice)
            {
            case 1:
                StartCoroutine(EnteringHallTimer());
                break;
            case 2:
                StartCoroutine(LookingAwayTeleport());
                break;
            } 
    }

    IEnumerator StairsMidTeleport()
    {
       yield return new WaitForSeconds(teleportTime);
       teleportChoice = 0;
       teleportChoice = Random.Range(1,5);
       Debug.Log("Working");
            switch(teleportChoice)
            {
            case 1:
                StartCoroutine(LookingAwayTimer());
                break;
            case 2:
                StartCoroutine(StairsTopTimer());
                break;
            case 3:
                StartCoroutine(EnteringHallTimer());
                break;
            case 4:
                StartCoroutine(StairsMidTeleport());
                break;
            } 
    }
    IEnumerator StairsTopTeleport()
    {
       yield return new WaitForSeconds(teleportTime);
       teleportChoice = 0;
       teleportChoice = Random.Range(1,4);
       Debug.Log("Working");
            switch(teleportChoice)
            {
            case 1:
                StartCoroutine(LookingAwayTimer());
                break;
            case 2:
                StartCoroutine(EnteringHallTimer());
                break;
            case 3:
                StartCoroutine(StairsTopTeleport());
                break;
            } 
    }

    IEnumerator firstTeleport()
    {
        yield return new WaitForSeconds(teleportTime);
        teleportChoice = Random.Range(1,4);
            switch(teleportChoice)
            {
            case 1:
                StartCoroutine(StairsMidTimer());
                break;
            case 2:
                StartCoroutine(StairsTopTimer());
                break;
            case 3:
                StartCoroutine(firstTeleport());
                break;
            }
    }

    // timers and position change IEnumerator
    IEnumerator StartPosTimer()
    {
        teleportTimeSetter();
        yield return new WaitForSeconds(teleportTime);
        Debug.Log("bru");
        aboutMove = true;
        tpAnimation.Play("Start_Pos");
        transform.position = startPos;
        yield return new WaitForSeconds(1);
        aboutMove = false;
        startPositionBool = true;
    }
    IEnumerator StairsTopTimer()
    {
        teleportTimeSetter();
        aboutMove = true;
        tpAnimation.Play("Stairs_Top");
        transform.position = stairsTopPos;
        yield return new WaitForSeconds(1);
        aboutMove = false;
        yield return new WaitForSeconds(teleportTime);
        stairsTopBool = true;
    }
    IEnumerator StairsMidTimer()
    {
        teleportTimeSetter();
        aboutMove = true;
        tpAnimation.Play("Stairs_Mid");
        transform.position = stairsMidPos;
        yield return new WaitForSeconds(1);
        aboutMove = false;
        yield return new WaitForSeconds(teleportTime);
        stairsMidBool = true;
    }
    IEnumerator EnteringHallTimer()
    {
        teleportTimeSetter();
        aboutMove = true;
        tpAnimation.Play("Entering_Hall");
        transform.position = enteringHallPos;
        enteringHallBool = true;
        yield return new WaitForSeconds(1);
        aboutMove = false;
        yield return new WaitForSeconds(teleportTime);
    }
    IEnumerator LookingAwayTimer()
    {
        teleportTimeSetter();
        aboutMove = true;
        tpAnimation.Play("Looking_Away");
        transform.position = facingAwayPos;
        yield return new WaitForSeconds(1);
        aboutMove = false;
        yield return new WaitForSeconds(teleportTime);
        facingAwayBool = true;
    }
}
