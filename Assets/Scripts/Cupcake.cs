using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupcake : MonoBehaviour
{
    [SerializeField] private Animator cupcakeAnimator;

    public Player player;
    public Manager manager;

    private bool readyToattack;
    private bool HasRan;

    private int stageChoice;
    private int cupcakePositionOffice;

    [SerializeField]private float time;
    [SerializeField]private float stageTime;

    void Start()
    {
        StartCoroutine(Idle());
    }

    void Update()
    {
        time += Time.deltaTime;

        if(readyToattack == true)
        {
            if(HasRan == false)
            {
                time = 0;
                time += Time.deltaTime;
                HasRan = true;
            }
            if(player.pupetMaskOn == true && time < 10)
            {
                StopAllCoroutines();
                StartCoroutine(Idle());
            }
            if(player.pupetMaskOn == false && time > 10 && player.PlayAlive == true)
            {
                // to make still game over and jumpscare
                player.PlayAlive = false;
                cupcakeAnimator.Play("CupcakeJumpScare");
                StartCoroutine(manager.waitForJumpscare(2));
            }
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
            stageTime = Random.Range(40f, 50f);
            break;
        case 3:
            stageTime = Random.Range(30f, 40f);
            break;
        case 4:
            stageTime = Random.Range(30f, 40f);
            break;
        case 5:
            stageTime = Random.Range(20f, 30f);
            break;
        }
    }

    IEnumerator Idle()
    {
        readyToattack = false;
        HasRan = false;
        stageTimeSetter();
        cupcakeAnimator.Play("IdleAnim");
        yield return new WaitForSeconds(stageTime);
        stageChoice = Random.Range(1,3);
        switch(stageChoice)
        {
            case 1:
                StartCoroutine(Idle());
                break;
            case 2:
                StartCoroutine(AtOffice());
                break;
        }
    }

    IEnumerator AtOffice()
    {
        stageTimeSetter();
        yield return new WaitForSeconds(stageTime);
        cupcakePositionOffice = Random.Range(1,5);
        switch(cupcakePositionOffice)
        {
            case 1:
                cupcakeAnimator.Play("AtOfficeAnim");
                break;
            case 2:
                cupcakeAnimator.Play("AtOfficeAnim2");
                break;
            case 3:
                cupcakeAnimator.Play("AtOfficeAnim3");
                break;
            case 4:
                cupcakeAnimator.Play("AtOfficeAnim4");
                break;
        }
        readyToattack = true;
    }

}
