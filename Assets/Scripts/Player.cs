using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Objects to manibulate
    public GameObject playerCamera;
    public GameObject light;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;
    public GameObject cameraUI;
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject camEffect;

    //Variables to activate
    private bool facingAwayFlash;
    private bool atLeftDoor;
    private bool atRightDoor;
    private bool rightDoorClosed;
    private bool leftDoorClosed;

    public bool isFlashing;

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

        cameraAnim = playerCamera.GetComponent<Animator>();
        leftDoorAnim = leftDoor.GetComponent<Animator>();
        rightDoorAnim = rightDoor.GetComponent<Animator>();
        camOnAnim = camEffect.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement and animation
        if(Input.GetKeyDown(KeyCode.D) && atLeftDoor == false)
        {
            cameraAnim.SetBool("IsAtRightDoor", true);
            atRightDoor = true;
        }
        if(Input.GetKeyDown(KeyCode.A) && atRightDoor == false)
        {
            cameraAnim.SetBool("IsAtLeftDoor", true);
            atLeftDoor = true;
            facingAwayFlash = true;
        }
        if(Input.GetKeyDown(KeyCode.A) && atRightDoor == true)
        {
            cameraAnim.SetBool("IsAtRightDoor", false);
            atRightDoor = false;
        }
        if(Input.GetKeyDown(KeyCode.D) && atLeftDoor == true)
        {
            cameraAnim.SetBool("IsAtLeftDoor", false);
            atLeftDoor = false;
            facingAwayFlash = false;
        }
        if(Input.GetKeyDown(KeyCode.W) && atLeftDoor == false && atRightDoor == false)
        {
            cameraAnim.SetBool("EnterCam", true);
            StartCoroutine(waitStartAnim(1));
        }
        if(Input.GetKeyDown(KeyCode.S) && atLeftDoor == false && atRightDoor == false)
        {
            cameraUI.SetActive(false);
            cameraAnim.SetBool("EnterCam", false);
            camera1.SetActive(false);
            camera2.SetActive(false);
            camera3.SetActive(false);
        }
        FlashLight();
        CloseLeftDoor();
        CloseRightDoor();
    }

    void FlashLight()
    {
        //Flashlight logic
        if(facingAwayFlash == true && leftDoorClosed == false)
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
}
