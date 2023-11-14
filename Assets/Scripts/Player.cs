using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Manager manager;

    // Objects to manibulate
    public GameObject playerCamera;
    public GameObject light;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;
    public GameObject camera4;
    public GameObject cameraUI;
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject camEffect;
    public GameObject pupetMask;

    //Variables to activate
    private bool facingAwayFlash;
    public bool atLeftDoor;
    public bool atRightDoor;
    public bool rightDoorClosed;
    private bool leftDoorClosed;
    public bool PlayAlive;

    public bool isFlashing;
    public bool pupetMaskOn;
    public bool camOn;
    public bool isTurnedAround;
    public bool atPowerBox;

    // Animators
    private Animator cameraAnim;
    private Animator leftDoorAnim;
    private Animator rightDoorAnim;
    private Animator camOnAnim;

    // Start is called before the first frame update
    void Start()
    {
        // setting all bools to false
        facingAwayFlash = false;
        atLeftDoor = false;
        atRightDoor = false;
        rightDoorClosed = false;
        leftDoorClosed = false;
        isFlashing =false;
        PlayAlive = true;

        cameraAnim = playerCamera.GetComponent<Animator>();
        leftDoorAnim = leftDoor.GetComponent<Animator>();
        rightDoorAnim = rightDoor.GetComponent<Animator>();
        camOnAnim = camEffect.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement and animation old version
        //if(Input.GetKeyDown(KeyCode.D) && atLeftDoor == false && PlayAlive == true && isTurnedAround == false)
        //{
            //cameraAnim.SetBool("IsAtRightDoor", true);
            //atRightDoor = true;
        //}
        //if(Input.GetKeyDown(KeyCode.A) && atRightDoor == false && PlayAlive == true && isTurnedAround == false)
        //{
            //cameraAnim.SetBool("IsAtLeftDoor", true);
            //atLeftDoor = true;
            //facingAwayFlash = true;
        //}
        //if(Input.GetKeyDown(KeyCode.A) && atRightDoor == true && PlayAlive == true && isTurnedAround == false)
        //{
            //cameraAnim.SetBool("IsAtRightDoor", false);
            //atRightDoor = false;
        //}
        //if(Input.GetKeyDown(KeyCode.D) && atLeftDoor == true && PlayAlive == true && isTurnedAround == false)
        //{
            //cameraAnim.SetBool("IsAtLeftDoor", false);
            //atLeftDoor = false;
            //facingAwayFlash = false;
        //}
        //if(Input.GetKeyDown(KeyCode.W) && atLeftDoor == false && atRightDoor == false && PlayAlive == true && manager.powerAvailable == true && isTurnedAround == false)
        //{
            //camOn = true;
            //cameraAnim.SetBool("EnterCam", true);
            //StartCoroutine(waitStartAnim(1));
        //}
        // to turn around
        //if(Input.GetKeyDown(KeyCode.S) && atLeftDoor == false && atRightDoor == false && PlayAlive == true && camOn == false)
        //{
            //if(isTurnedAround == false)
            //{
                //cameraAnim.SetBool("TurnAroundAnimBool", true);
                //isTurnedAround = true; 
            //}
            //else if(isTurnedAround == true)
            //{
                //isTurnedAround = false;
                //cameraAnim.SetBool("TurnAroundAnimBool", false);
            //}
        //}
        // to leave camera view
        //if(Input.GetKeyDown(KeyCode.S) && atLeftDoor == false && atRightDoor == false && PlayAlive == true && camOn == true)
        //{
            //camOn = false;
            //cameraUI.SetActive(false);
            //cameraAnim.SetBool("EnterCam", false);
            //camera1.SetActive(false);
            //camera2.SetActive(false);
            //camera3.SetActive(false);
            //camera4.SetActive(false);
        //}
        if(manager.powerAvailable == false)
        {
            camOn = false;
            cameraUI.SetActive(false);
            cameraAnim.SetBool("EnterCam", false);
            camera1.SetActive(false);
            camera2.SetActive(false);
            camera3.SetActive(false);
            camera4.SetActive(false);
        }
        FlashLight();
        CloseLeftDoor();
        CloseRightDoor();
        if(camera4.activeSelf == true)
        {
            manager.chicaResetButton.SetActive(true);
        }
        else
        {
            manager.chicaResetButton.SetActive(false);
        }
        if(PlayAlive == false)
        {
            cameraUI.SetActive(false);
            camera1.SetActive(false);
            camera2.SetActive(false);
            camera3.SetActive(false);
            camera4.SetActive(false);
        }
        MaskMechanic();
    }

    void FlashLight()
    {
        //Flashlight logic
        if(facingAwayFlash == true && leftDoorClosed == false && PlayAlive == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                light.SetActive(true);
                isFlashing = true;
            }
            if(Input.GetKeyUp(KeyCode.E))
            {
                light.SetActive(false);
                isFlashing = false;
            }
        }  
        if(facingAwayFlash == false)
        {
            light.SetActive(false);
        }
    }

    void CloseLeftDoor()
    {
        if(Input.GetKeyDown(KeyCode.Space) && atLeftDoor == true)
        {
            // Play closing anim and assign a variable
            cameraAnim.SetBool("CloseLeftDoor", true);
            leftDoorAnim.SetBool("Close_L_Door", true);
            leftDoorClosed = true;
        }
        if(Input.GetKeyUp(KeyCode.Space) && atLeftDoor == true)
        {
            // Play opposite anim and make variable false
            cameraAnim.SetBool("CloseLeftDoor", false);
            leftDoorAnim.SetBool("Close_L_Door", false);
            leftDoorClosed = false;
        }
    }

    void MaskMechanic()
    {
        if(atRightDoor == false && atLeftDoor == false)
        {
            if(Input.GetKeyDown(KeyCode.F) && pupetMaskOn == false)
            {
                pupetMask.SetActive(true);
                pupetMaskOn = true;   
            }
            else if(Input.GetKeyUp(KeyCode.F))
            {
                pupetMask.SetActive(false);
                pupetMaskOn = false;
            }
        }
    }

    void CloseRightDoor()
    {
        if(Input.GetKeyDown(KeyCode.Space) && atRightDoor == true)
        {
            // Play closing anim and assign a variable
            cameraAnim.SetBool("CloseRightDoor", true);
            rightDoorAnim.SetBool("Close_R_Door", true);
            rightDoorClosed = true;
        }
        if(Input.GetKeyUp(KeyCode.Space) && atRightDoor == true)
        {
            // Play opposite anim and make variable false
            cameraAnim.SetBool("CloseRightDoor", false);
            rightDoorAnim.SetBool("Close_R_Door", false);
            rightDoorClosed = false;
        }
    }

    IEnumerator waitStartAnim(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        camOnAnim.SetBool("Camera_On", true);
        yield return new WaitForSeconds(1);
        camOnAnim.SetBool("Camera_On", false);
        cameraUI.SetActive(true);
        camera1.SetActive(true);
    }

    public void goToDDoor()
    {
        cameraAnim.SetBool("IsAtRightDoor", true);
        atRightDoor = true;
    }
    public void goToADoor()
    {
        cameraAnim.SetBool("IsAtLeftDoor", true);
        atLeftDoor = true;
        facingAwayFlash = true;
    }
    public void goBackFromRightDoor()
    {
        cameraAnim.SetBool("IsAtRightDoor", false);
        atRightDoor = false;
    }
    public void goBackFromLeftDoor()
    {
        cameraAnim.SetBool("IsAtLeftDoor", false);
        atLeftDoor = false;
        facingAwayFlash = false;
    }
    public void LookBehind()
    {
        if(isTurnedAround == false)
        {
            cameraAnim.SetBool("TurnAroundAnimBool", true);
            isTurnedAround = true; 
        }
        else if(isTurnedAround == true)
        {
            isTurnedAround = false;
            cameraAnim.SetBool("TurnAroundAnimBool", false);
        }
    }
    public void LookPowerBox()
    {
        atPowerBox = true;
        cameraAnim.SetBool("PowerBoxAnim", true);
    }
    public void LookBackFromPowerBox()
    {
        cameraAnim.SetBool("PowerBoxAnim", false);
    }
    public void CameraOff()
    {
        camOn = false;
        cameraUI.SetActive(false);
        cameraAnim.SetBool("EnterCam", false);
        camera1.SetActive(false);
        camera2.SetActive(false);
        camera3.SetActive(false);
        camera4.SetActive(false);
    }
    public void CameraOn()
    {
        camOn = true;
        cameraAnim.SetBool("EnterCam", true);
        StartCoroutine(waitStartAnim(1));
    }
}
