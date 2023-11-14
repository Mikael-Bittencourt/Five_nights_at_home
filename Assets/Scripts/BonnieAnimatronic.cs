using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonnieAnimatronic : MonoBehaviour
{
    [SerializeField] private Animator bAnimator;

    public Player player;
    public Manager manager;

    public GameObject BStart;
    public GameObject BPool;
    public GameObject BCamLook;
    public GameObject BHall;
    public GameObject BKill;

    [SerializeField]private float teleportTime;
    [SerializeField]private float time;
    [SerializeField]private float timePassed;

    [SerializeField]private bool BStartBool;
    [SerializeField]private bool BPoolBool;
    [SerializeField]private bool BCamBool;
    [SerializeField]private bool BHallBool;
    [SerializeField]private bool BKillBool;
    [SerializeField]private bool HasRan;
    private bool doorClosed;

    private Vector3 BStartPos;
    private Vector3 BPoolPos;
    private Vector3 BCamLookPos;
    private Vector3 BHallPos;
    private Vector3 BKillPos;
    //private Vector3 BcurrentPos;

    public int teleportChoice;

    // Start is called before the first frame update
    void Start()
    {

        BStartPos = BStart.transform.position;
        BPoolPos = BPool.transform.position;
        BCamLookPos = BCamLook.transform.position;
        BHallPos = BHall.transform.position;
        BKillPos = BKill.transform.position;

        StartCoroutine(BStartTimer());

        bAnimator.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //BcurrentPos = this.gameObject.transform.position;

        if(BStartBool == true)
        {
            BStartBool = false;
            StartCoroutine(BStartTeleport());
        }
        if(BPoolBool == true)
        {
            BPoolBool = false;
            StartCoroutine(BPoolTeleport());
        }
        if(BCamBool == true)
        {
            BCamBool = false;
            StartCoroutine(BCamTeleport());
        }
        if(BHallBool == true)
        {
            BHallBool = false;
            StartCoroutine(BHallTeleport());
        }
        if(BKillBool == true)
        {
            if(HasRan == false)
            {
                Debug.Log("is running");
                time = 0;
                time += Time.deltaTime;
                HasRan = true;
            }
            if(player.rightDoorClosed == true && time < 10)
            {
                doorClosed = true;
                StartCoroutine(BGoingBackTeleport());
            }
            if(doorClosed == false && time > 10 && player.PlayAlive == true)
            {
                if(player.atRightDoor == true)
                {
                    player.PlayAlive = false;
                    bAnimator.Play("JumpscareRightHall");
                    manager.playerAnim.Play("BHallJumpPlayer");
                    StartCoroutine(manager.waitForJumpscare(5));
                }
                else
                {
                    player.PlayAlive = false;
                    bAnimator.Play("JumpscareRoom");
                    manager.playerAnim.Play("BRoomJumpPlayer");
                    StartCoroutine(manager.waitForJumpscare(2));
                }
            }
        }
    }

    private void teleportTimeSetter()
    {
        switch(Manager.nightNum)
        {
        //case 0:
            //teleportTime = Random.Range(5f, 10f);
            //break;
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

    //IEnumerators for the teleport choices
    // first tp
    IEnumerator BStartTeleport()
    {
        yield return new WaitForSeconds(teleportTime);
        teleportChoice = Random.Range(1,4);
        switch(teleportChoice)
            {
            case 1:
                StartCoroutine(BPoolTimer());
                break;
            case 2:
                StartCoroutine(BCamTimer());
                break;
            case 3:
                StartCoroutine(BStartTeleport());
                break;
            }
    }
    // second tp
    IEnumerator BPoolTeleport()
    {
        yield return new WaitForSeconds(teleportTime);
        teleportChoice = Random.Range(1,4);
        switch(teleportChoice)
            {
            case 1:
                StartCoroutine(BHallTimer());
                break;
            case 2:
                StartCoroutine(BCamTimer());
                break;
            case 3:
                StartCoroutine(BPoolTeleport());
                break;
            }
    }
    // third tp
    IEnumerator BCamTeleport()
    {
        yield return new WaitForSeconds(teleportTime);
        teleportChoice = Random.Range(1,3);
        switch(teleportChoice)
            {
            case 1:
                StartCoroutine(BHallTimer());
                break;
            case 2:
                StartCoroutine(BCamTeleport());
                break;
            }
    }
    // fourth tp
    IEnumerator BHallTeleport()
    {
        yield return new WaitForSeconds(teleportTime);
        teleportChoice = Random.Range(1,3);
        switch(teleportChoice)
            {
            case 1:
                StartCoroutine(BKillTimer());
                break;
            case 2:
                StartCoroutine(BHallTeleport());
                break;
            }
    }
    // fifth tp, when player closes door on him
    IEnumerator BGoingBackTeleport()
    {
        BKillBool = false;
        HasRan = false;
        doorClosed = false;
        yield return new WaitForSeconds(1);
        teleportChoice = Random.Range(1,4);
        switch(teleportChoice)
            {
            case 1:
                StartCoroutine(BStartTimer());
                break;
            case 2:
                StartCoroutine(BPoolTimer());
                break;
            case 3:
                StartCoroutine(BCamTimer());
                break;
            }
    }

    // IEnumerators for the movement itself
    IEnumerator BStartTimer()
    {
        teleportTimeSetter();
        yield return new WaitForSeconds(teleportTime);
        manager.aboutMove = true;
        bAnimator.Play("Start");
        transform.position = BStartPos;
        yield return new WaitForSeconds(1);
        manager.aboutMove = false;
        Debug.Log("Pos 1");
        BStartBool = true;
    }
    IEnumerator BPoolTimer()
    {
        teleportTimeSetter();
        manager.aboutMove = true;
        bAnimator.Play("NearPool");
        transform.position = BPoolPos;
        yield return new WaitForSeconds(1);
        manager.aboutMove = false;
        yield return new WaitForSeconds(teleportTime);
        Debug.Log("Pos 2");
        BPoolBool = true;
    }
    IEnumerator BCamTimer()
    {
        teleportTimeSetter();
        manager.aboutMove = true;
        bAnimator.Play("LookingAtCam");
        yield return new WaitForSeconds(1);
        manager.aboutMove = false;
        transform.position = BCamLookPos;
        yield return new WaitForSeconds(teleportTime);
        Debug.Log("Pos 3");
        BCamBool = true;
    }
    IEnumerator BHallTimer()
    {
        teleportTimeSetter();
        yield return new WaitForSeconds(teleportTime);
        Debug.Log("Pos 4");
        manager.aboutMove = true;
        bAnimator.Play("Hall");
        transform.position = BHallPos;
        yield return new WaitForSeconds(1);
        manager.aboutMove = false;
        BHallBool = true;
    }
    IEnumerator BKillTimer()
    {
        teleportTimeSetter();
        yield return new WaitForSeconds(teleportTime);
        Debug.Log("Pos 5");
        manager.aboutMove = true;
        bAnimator.Play("AboutToKill");
        transform.position = BKillPos;
        yield return new WaitForSeconds(1);
        manager.aboutMove = false;
        BKillBool = true;
    }

    //IEnumerator waitForJumpscare()
    //{
        //yield return new WaitForSeconds(5);
        //manager.GameOver();
    //}
}
