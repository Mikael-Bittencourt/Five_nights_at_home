using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChicaAnimatronic : MonoBehaviour
{
    public Player player;
    public Animator chicaAnimator;
    public Manager manager;

    [SerializeField]private float stageTime;
    [SerializeField]private int CstageChoice;

    private bool cKill;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(offStage());
    }

    // Update is called once per frame
    void Update()
    {
        if(cKill == true && player.PlayAlive == true)
        {
            player.PlayAlive = false;
            chicaAnimator.Play("StandardKillAnim");
            manager.playerAnim.Play("CStandardJumpPlayer");
            StartCoroutine(manager.waitForJumpscare(2));
        }
        if(manager.resetButtonPressed == true)
        {
            Debug.Log("ResetPressed");
            chicaAnimator.Play("Stage0");
            manager.resetButtonPressed = false;
            StopAllCoroutines();
            StartCoroutine(offStage());
        }
    }

    private void stageTimeSetter()
    {
        switch(Manager.nightNum)
        {
        case 0:
            stageTime = Random.Range(5f, 10f);
            break;
        case 1:
            stageTime = Random.Range(40f, 50f);
            break;
        case 2:
            stageTime = Random.Range(30f, 40f);
            break;
        case 3:
            stageTime = Random.Range(20f, 30f);
            break;
        case 4:
            stageTime = Random.Range(10f, 20f);
            break;
        case 5:
            stageTime = Random.Range(5f, 10f);
            break;
        }
    }

    IEnumerator offStage()
    {
        stageTimeSetter();
        Debug.Log("OffStage");
        chicaAnimator.Play("Stage0");
        yield return new WaitForSeconds(stageTime);
        switch(CstageChoice)
        {
            case 1:
                StartCoroutine(offStage());
                break;
            case 2:
                StartCoroutine(stage1());
                break;
        }
    }
    IEnumerator stage1()
    {
        stageTimeSetter();
        Debug.Log("Stage1");
        chicaAnimator.Play("Stage1");
        yield return new WaitForSeconds(stageTime);
        switch(CstageChoice)
        {
            case 1:
                StartCoroutine(stage1());
                break;
            case 2:
                StartCoroutine(stage2());
                break;
        }
    }
    IEnumerator stage2()
    {
        stageTimeSetter();
        Debug.Log("Stage2");
        chicaAnimator.Play("Stage2");
        yield return new WaitForSeconds(stageTime);
        switch(CstageChoice)
        {
            case 1:
                StartCoroutine(stage2());
                break;
            case 2:
                StartCoroutine(stage3());
                break;
        }
    }
    IEnumerator stage3()
    {
        stageTimeSetter();
        Debug.Log("Stage3");
        chicaAnimator.Play("Stage3");
        yield return new WaitForSeconds(stageTime);
        cKill = true;
    }   
}
