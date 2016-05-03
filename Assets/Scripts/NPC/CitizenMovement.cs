using UnityEngine;
using System.Collections;

public class CitizenMovement : MonoBehaviour {

    public float initialSpeed = 4f;         // initial citizen speed
    public float incSpeed = 1f;             // amount of addition in speed

    public float speed;
    private int currentDir;                 // direction of the NPC, 0->UP, 1->Right, 2->Down, 3->Left

    private Transform myTrans;              // cash these variables for efficient access
    private float step;

    private CitizenAIManager AIScript;      // to know if the player will start left or right (and then select the desired waypoints)
    private WaypointManager WaypointScript; // reference to the waypoints script, use: to move to a specific waypoint and register entering the bath
    private CitizenEffects EffectsScript;   

    private Vector3 targetPos = new Vector3(0f, 0f, 0f);      // cash for efficient access
    private Vector3 newPosition = new Vector3(0f, 0f, 0f);
    private Vector3 switchTarget = new Vector3(0f, 0f, 0f);

    private bool bOneTime = false;
    private bool bSwitchOneTime = false;
    private bool bSwitching = false;         // the NPC is currently switch sides
    private float startShittingTime;

    private float ySwitchPoint;       // x point captured when switching
    public float rightSideX = 5f;     // x point of the right side
    public float leftSideX = -5f;     // x point of the left side

    public float rightBorder = 12f;
    public float leftBorder = -12f;

    void Start()
    {
        speed = initialSpeed;

        currentDir = GetComponent<CitizenAIManager>().currentDir;

        myTrans = transform;

        WaypointScript = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<WaypointManager>();

        AIScript = GetComponent<CitizenAIManager>();

        EffectsScript = GetComponent<CitizenEffects>();
    }

    public void Walk()
    {
        step = speed * Time.deltaTime;

        if (AIScript.startInLeft)        // if the NPC is starting at the left side
        {
            MoveOnLeft(step);
        }
        else  // starting in the right direction
        {
            MoveOnRight(step);
        } // end else we are in the right direction

    }

    public void WalkThenSwitch()
    {
        step = speed * Time.deltaTime;

        // Check for switch point
        if (myTrans.position.y >= AIScript.ySwitchPoint && !bSwitchOneTime)   // reached switch point
        {
            AIScript.state = CitizenAIManager.AIStates.Switch;

            bSwitchOneTime = true;
            bSwitching = true;

            ySwitchPoint = myTrans.position.y;
        }

        if (AIScript.startInLeft && !bSwitching)        // if the NPC is starting at the left side
        {
            MoveOnLeft(step);
        }
        else if (!AIScript.startInLeft && !bSwitching)  // starting in the right direction
        {
            MoveOnRight(step);
        } // end else we are in the right direction
        else
        {
/*            if (EffectsScript != null)
                if (EffectsScript.start)
                    StartCoroutine(EffectsScript.FlickerOnSwitch());   // flicker on switching  */

            SwitchSide();
        }
    }

    public void RunAway()
    {
        AIScript.state = CitizenAIManager.AIStates.RunAway;

        step = speed * Time.deltaTime;

        if (AIScript.startInLeft)
        {
            targetPos = new Vector3(leftBorder, myTrans.position.y, 0f);
        }
        else
        {
            targetPos = new Vector3(rightBorder, myTrans.position.y, 0f);
        }

        newPosition = Vector2.MoveTowards(myTrans.position, targetPos, step);

        myTrans.position = newPosition;

        if (newPosition == targetPos)
        {
            // increase player score
            GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ScoreManager>().IncHealth();

            Destroy(gameObject);
        }
        
    }

    void SwitchSide()
    {
//        print("Switching");
        
        if (AIScript.startInLeft)         // if the NPC started on left, then set the target to the right side and vice-versa
        {
            switchTarget = new Vector3(rightSideX, ySwitchPoint, 0f);

            newPosition = Vector2.MoveTowards(myTrans.position, switchTarget, step);

            AIScript.currentDir = CitizenAIManager.Right;

            myTrans.position = newPosition;

            if (newPosition == switchTarget)        // reached switch target, walk normal
            {
                bSwitching = false;

                AIScript.currentDir = CitizenAIManager.Up;

                AIScript.state = CitizenAIManager.AIStates.Walking;

                AIScript.startInLeft = false;
            }
        }
        else
        {
            switchTarget = new Vector3(leftSideX, ySwitchPoint, 0f);

            newPosition = Vector2.MoveTowards(myTrans.position, switchTarget, step);

            AIScript.currentDir = CitizenAIManager.Left;

            myTrans.position = newPosition;

            if (newPosition == switchTarget)        // reached switch target, walk normal
            {
                bSwitching = false;

                AIScript.currentDir = CitizenAIManager.Up;

                AIScript.state = CitizenAIManager.AIStates.Walking;

                AIScript.startInLeft = true;
            }
        }
    }

    void MoveOnLeft(float step)
    {
        targetPos = WaypointScript.WaypointsLeft[AIScript.targetWPIndex];

        newPosition = Vector2.MoveTowards(myTrans.position, targetPos, step);

        myTrans.position = newPosition;

        if (newPosition == targetPos)        // switch to the next waypoint
        {
            if (AIScript.targetWPIndex == 0)  // switch to a bath if available 
            {
                // iterate first through the availability of each bathroom
                for (int i = 0; i < WaypointScript.bathCapacity; i++)
                {
                    if (WaypointScript.bathsAvailable_Left[i] == true)
                    {
                        // Bath is available, register to this bath and switch the next waypoint index
                        WaypointScript.SetLeftBathState(i, false);

                        AIScript.targetWPIndex = i + 1;

                        // set the selected path index for later reference
                        AIScript.selectedPathIndex = i;

                        AIScript.currentDir = CitizenAIManager.Left;

                        break;
                    }

                    if (i == (WaypointScript.bathCapacity - 1) && WaypointScript.bathsAvailable_Left[i] == false)
                    {
                        // if all baths are occupied, start idle state and wait
                        AIScript.state = CitizenAIManager.AIStates.Idle;
                    }

                } // end iterating through each bath available

            } // end if we are heading for the first WP and we reached it


            else if (AIScript.targetWPIndex > 0 && AIScript.targetWPIndex <= WaypointScript.bathCapacity)
            {
                // Set AI state
                AIScript.state = CitizenAIManager.AIStates.Shitting;

                if (!bOneTime)
                {
                    bOneTime = true;
                    startShittingTime = Time.time + AIScript.shittingTime;
                }

//                print("Curr Time: " + Time.time + ", final Time: " + startShittingTime + AIScript.shittingTime);
                while (Time.time < startShittingTime)
                {
                    return;
                }

                bOneTime = false;

                // After shitting, go to the next waypoint
                AIScript.state = CitizenAIManager.AIStates.Walking;

                // set target waypoint
                AIScript.targetWPIndex = WaypointScript.bathCapacity + 1;

                // release the bath after using it
                WaypointScript.SetLeftBathState(AIScript.selectedPathIndex, true);

                // set the NPC direction
                AIScript.currentDir = CitizenAIManager.Right;

            }   // end if we are heading for a bath and we reached it

            else
            {
                Destroy(gameObject);        // destroy this cirizen after reaching the end
            }  // end if we are heading for the final point and we reached it
        }
    }

    void MoveOnRight(float step)
    {
 //       print("Moving on right");
        targetPos = WaypointScript.WaypointsRight[AIScript.targetWPIndex];

        newPosition = Vector2.MoveTowards(myTrans.position, targetPos, step);

        myTrans.position = newPosition;

        if (newPosition == targetPos)         // switch to the next waypoint
        {
//            print("Target reached");
            if (AIScript.targetWPIndex == 0)  // switch to a bath if available 
            {
                // iterate first through the availability of each bathroom
                for (int i = 0; i < WaypointScript.bathCapacity; i++)
                {
                    if (WaypointScript.bathsAvailable_Right[i] == true)
                    {
 //                       print("Bath index " + i + " is available");
                        // Bath is available, register to this bath and switch the next waypoint index
                        WaypointScript.SetRightBathState(i, false);

                        // set the selected path index for later reference
                        AIScript.selectedPathIndex = i;

                        AIScript.targetWPIndex = i + 1;

 //                       print("Set dir to right");
                        AIScript.currentDir = CitizenAIManager.Right;

                        break;
                    }

                    if (i == (WaypointScript.bathCapacity - 1) && WaypointScript.bathsAvailable_Right[i] == false)
                    {
                        // if all baths are occupied, start idle state and wait
                        AIScript.state = CitizenAIManager.AIStates.Idle;
                    }
                } // end iterating through each bath available

            } // end if we are heading for the first WP and we reached it


            else if (AIScript.targetWPIndex > 0 && AIScript.targetWPIndex <= WaypointScript.bathCapacity)
            {
 //               print("Reaching one of the Baths");
                // Set AI state
                AIScript.state = CitizenAIManager.AIStates.Shitting;

                if (!bOneTime)
                {
                    bOneTime = true;
                    startShittingTime = Time.time + AIScript.shittingTime;
                }

 //               print("Curr Time: " + Time.time + ", final Time: " + startShittingTime + AIScript.shittingTime);
                while (Time.time < startShittingTime)
                {
                    return;
                }

                // After shitting, go to the next waypoint
                AIScript.state = CitizenAIManager.AIStates.Walking;

                // set target waypoint
                AIScript.targetWPIndex = WaypointScript.bathCapacity + 1;

 //               print("Setting Bath state of index " + (AIScript.selectedPathIndex) + " to true");
                // release the bath after using it
                WaypointScript.SetRightBathState(AIScript.selectedPathIndex, true);

                // set the NPC direction
                AIScript.currentDir = CitizenAIManager.Left;

            }   // end if we are heading for a bath and we reached it

            else
            {
                Destroy(gameObject);        // destroy this cirizen after reaching the end
            }  // end if we are heading for the final point and we reached it
        }
        else
        {
 //           print("Normal moving");
        }
    }
}
