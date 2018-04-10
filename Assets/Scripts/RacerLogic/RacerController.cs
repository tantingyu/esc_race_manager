using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using RacerLogic.RacerAssets;
using RacerLogic;

public class RacerController : NetworkBehaviour
{
    [HideInInspector]
    public bool commandExecuted;
    private Animator anim;

    //Swipe Controller
    private Vector3 fp; //First touch position
    private Vector3 lp; //Last touch position

    private float dragDistance = Screen.height * 15 / 100; //minimum distance for a swipe to be registered
    private bool moveVertical = false;
    private bool moveHorizontal = false;
    public bool playerCollision = false;
    //private bool invincible

    private readonly float[] zPositions = { 1, 0, -1 };
    private readonly float[] positionVertical = { 1.97f, 0.61f, -0.7f };
    private readonly float[] positionHorizontal = { -5.5f, -4.5f, -3.5f, -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f, 3.5f, 4.5f, 5.5f };

    public int currentLane;
    public int targetLane;
    public int currentBlock;
    public int targetBlock;

    // private Rigidbody2D myRigidbody;

    // player Command

    // [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public float defaultSpeed = 5;
    [HideInInspector]
    public bool offGround = false;
    public bool changePosition = false;

    private SetupLocalPlayer gamePlayer;
    private Racer playerRacer;

    void Start()
    {
        anim = GetComponent<Animator>();
        moveSpeed = defaultSpeed;

        //swipe controller
        //dragDistance is 15% height of the screen
        //dragDistance = Screen.height * 15 / 100;
        //SetCurrentPos(gamePlayer.playerNumber, 0);

        // set initial player states

    }

    //Update is called once per frame
    void Update()
    {
        if (!commandExecuted)
            SwipeControl();        

        if (changePosition)
        {
            moveHorizontally();
            moveVertically();
        }
    }

    /*[Command]
    public void CmdRunCmd(int commandIndex)
    {
        Debug.Log("Running a command...");
        commandExecuted = true;
        //anim.SetTrigger(animHash[commandIndex]);

        //movetile
        if (playerRacer.commands[commandIndex].objectCreate != "")
        {
            
            if (playerRacer.commands[commandIndex].objectCreate == "Heal" || playerRacer.commands[commandIndex].objectCreate == "Shield")
            {

                GameObject equipment = (GameObject)Instantiate(Resources.Load(playerRacer.commands[commandIndex].objectCreate), 
                    Vector2.zero, Quaternion.identity);
                equipment.transform.parent = gameObject.transform;

            }
            
            NetworkServer.Spawn((GameObject)Instantiate(Resources.Load(playerRacer.commands[commandIndex].objectCreate),
            new Vector2(transform.position.x, transform.position.y), Quaternion.identity));
            
        }
        
        //change speed
            if (playerRacer.commands[commandIndex].changeSpeed != 0) {
            moveSpeed = playerRacer.commands[commandIndex].changeSpeed;
            SwipeControl();
        }


        //off ground
        if (playerRacer.commands[commandIndex].offGround) {
            offGround = true;
            Debug.Log("Currently Off Ground!");
        }

        //change position
        if (playerRacer.commands[commandIndex].changePosition[0] != 0 || playerRacer.commands[commandIndex].changePosition[1] != 0) {

            targetLane = currentLane + playerRacer.commands[commandIndex].changePosition[0];
            targetBlock = currentBlock + playerRacer.commands[commandIndex].changePosition[1];

            if ((targetLane >= 0) && (targetLane <= 2)
                && (targetBlock >= 0) && (targetBlock <= 11)
                 && !playerCollision)
            {

                changePosition = true;
            }
            else
            {
                targetLane = currentLane;
                targetBlock = currentBlock;
            }
        }

        // anim.SetTrigger(playerRacer.commands[commandIndex].name);
    }*/

    /*private void OnTriggerEnter2D(Collider2D collision)  //The first time the game obj touches a trigger
    {
        //check trap type here
        Debug.Log("Trigger Detected");

        if (collision.tag == "trap")
        {
            if (offGround && collision.gameObject.GetComponent<BaseTrap>().ground) { }

            else
            {
                // float damage = collision.gameObject.GetComponent<BaseTrap>().damage;
                // gamePlayer.hp -= damage;
                // gamePlayer.OnHpChange(-damage);
                // Debug.Log("HP -" + damage);
            }
        }

        if (collision.tag == "Player" && !offGround)
        {
            Debug.Log("Player Collision");
            playerCollision = true;

        }
    }*/

    public void SwipeControl()
    {
        //swipe controller
        //myRigidbody.velocity = new Vector2(moveSpeedHorizontal, myRigidbody.velocity.y); //vector means point like(x,y)
        if (!moveVertical && !moveHorizontal)
        {
            if (Input.touchCount == 1) // user is touching the screen with a single touch
            {
                Touch touch = Input.GetTouch(0); // get the touch
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                {
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {
                    lp = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 20% of the screen height
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {//It's a drag
                     //check if the drag is vertical or horizontal
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {   //If the horizontal movement is greater than the vertical movement...
                            if ((lp.x > fp.x))  //If the movement was to the right)
                            {   //Right swipe
                                Debug.Log("Right Swipe");
                                if (targetBlock < positionHorizontal.Length - 1 && !playerCollision)
                                {
                                    targetBlock += 1;
                                }
                            }
                            else
                            {   //Left swipe
                                Debug.Log("Left Swipe");
                                if (targetBlock > 0 && !playerCollision)
                                {
                                    targetBlock -= 1;
                                }
                            }

                            //edited by GU ZHIYAO 2018-03-21
                            moveHorizontal = true;
                        }
                        else
                        {   //the vertical movement is greater than the horizontal movement


                            if (lp.y > fp.y)  //If the movement was up
                            {   //Up swipe
                                Debug.Log("Up Swipe");
                                if (targetLane > 0 && !playerCollision) targetLane -= 1;
                            }
                            else
                            {   //Down swipe
                                Debug.Log("Down Swipe");
                                if (targetLane < positionVertical.Length - 1 & !playerCollision) targetLane += 1;
                            }
                            moveVertical = true;
                        }
                    }
                    else
                    {   //It's a tap as the drag distance is less than 20% of the screen height
                        Debug.Log("Tap");
                    }
                }
            }

            //computer debug
            if (Input.GetKey("up"))
                if (targetLane > 0 && !playerCollision)
                {
                    targetLane -= 1;
                    moveVertical = true;
                }
            if (Input.GetKey("down"))
                if (targetLane < positionVertical.Length - 1 && !playerCollision)
                {
                    targetLane += 1;
                    moveVertical = true;
                }
            if (Input.GetKey("left"))
                if (targetBlock > 0 && !playerCollision)
                {
                    //targetLane -= 1;
                    targetBlock -= 1;
                    moveHorizontal = true;
                }
            if (Input.GetKey("right"))
                if (targetBlock < positionHorizontal.Length - 1 && !playerCollision)
                {
                    //Debug.Log(positionHorizontal.Length - 1);
                    targetBlock += 1;
                    moveHorizontal = true;
                }
        }

        else if (!moveHorizontal && moveVertical)
        {
            moveVertically();
        }

        else if (!moveVertical && moveHorizontal)
        {
            moveHorizontally();
        }
    }

    public void moveHorizontally()
    {
        Debug.Log("currentBlock:" + currentBlock);
        Debug.Log("targetBlock:" + targetBlock);

        float step = moveSpeed * Time.deltaTime;
        Vector3 targetPos = new Vector3(GetTargetBlockPos(targetBlock), transform.position.y, zPositions[targetLane]);

        if (GetPlayerCurrentPosX() < GetTargetBlockPos(targetBlock))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        }
        else if (GetPlayerCurrentPosX() > GetTargetBlockPos(targetBlock))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        }
        else
        {
            moveHorizontal = false;
            currentBlock = targetBlock;
        }
    }

    public void moveVertically()
    {
        Debug.Log("targetLane:" + targetLane);

        float step = moveSpeed * Time.deltaTime;
        Vector3 targetPos = new Vector3(transform.position.x, GetTargetLanePos(targetLane), zPositions[targetLane]);

        if (GetPlayerCurrentPosY() < GetTargetLanePos(targetLane))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        }
        else if (GetPlayerCurrentPosY() > GetTargetLanePos(targetLane))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        }
        else
        {
            moveVertical = false;
            currentLane = targetLane;
            Debug.Log("currentLane:" + currentLane);
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)  //When you are in the frame, the trap is triggerred
    {
        Debug.Log("You Enterred the trap! Trigger Detected");
    }

    private void OnTriggerExit2D(Collider2D collision) //When you leave the frame, the trap is triggerred
    {
        Debug.Log("You left the trap! Trigger Detected");
    }
    */

    // swipe controller
    public float GetPlayerCurrentPosY()
    {
        return gameObject.transform.position.y;
    }

    public float GetPlayerCurrentPosX()
    {
        return gameObject.transform.position.x;
    }

    public float GetTargetLanePos(int lane)
    {
        return positionVertical[lane];
    }

    public float GetTargetBlockPos(int block)
    {
        return positionHorizontal[block];
    }

    public void SetCurrentPos(int lane, int block)
    {
        transform.position = new Vector3(GetTargetBlockPos(block), GetTargetLanePos(lane), zPositions[lane]);
        targetLane = lane;
        targetBlock = block;
        currentLane = lane;
        currentBlock = block;
    }
}
