using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour {

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private bool moveVertical = false;
    
    private readonly float[] position = { 0.8f, -0.4F, -1.6f };
    private int currentLane;
    private int targetLane;
    private Rigidbody2D myRigidbody;

    public float moveSpeedHorizontal;
    public float moveSpeedVertical;

    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        //currentLane=gameObject.transform
        myRigidbody = GetComponent<Rigidbody2D>();
        SetCurrentLane(1);
        myRigidbody.velocity = new Vector2(moveSpeedHorizontal, myRigidbody.velocity.y); //vector means point like(x,y)
    }

    void Update()
    {
        //myRigidbody.velocity = new Vector2(moveSpeedHorizontal, myRigidbody.velocity.y); //vector means point like(x,y)
        if (!moveVertical)
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
                            }
                            else
                            {   //Left swipe
                                Debug.Log("Left Swipe");
                            }
                        }
                        else
                        {   //the vertical movement is greater than the horizontal movement
                            if (lp.y > fp.y)  //If the movement was up
                            {   //Up swipe
                                Debug.Log("Up Swipe");
                                if (targetLane > 0) targetLane -= 1;
                            }
                            else
                            {   //Down swipe
                                Debug.Log("Down Swipe");
                                if (targetLane < 2) targetLane += 1;
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
                if (targetLane > 0)
                {
                    targetLane -= 1;
                    moveVertical = true;
                }
            if (Input.GetKey("down"))
                if (targetLane < 2)
                {
                    targetLane += 1;
                    moveVertical = true;
                }
        }

        else
        {
            Debug.Log("currentLane:"+currentLane);
            Debug.Log("targetLane:"+targetLane);
            
            float step = moveSpeedVertical * Time.deltaTime;
            Vector2 targetPos = new Vector2(transform.position.x, GetTargetLanePos(targetLane));

            if (GetPlayerCurrentPos() < GetTargetLanePos(targetLane))
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
            }
            else if (GetPlayerCurrentPos() > GetTargetLanePos(targetLane))
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
            }
            else
            {
                moveVertical = false;
                currentLane = targetLane;
            }
        }
    }

    void MoveDown() { 
    }

    float GetPlayerCurrentPos() {
        return gameObject.transform.position.y;
    }

    float GetTargetLanePos(int lane) {
        return position[lane];
    }

    void SetCurrentLane(int lane) {
        transform.position = new Vector2(-5.5f,GetTargetLanePos(lane));
        targetLane = lane;
    }
}
