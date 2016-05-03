using UnityEngine;
using System.Collections;

public class CitizenAnimationManager : MonoBehaviour {

    private int speedID;
    private int idleID;
    private int horizontalID;
    private int shittingID;

    private Animator anim;

    private CitizenAIManager AIScript;
    private CitizenAIManager.AIStates AIState;
    private int currentDir;

    void Start()
    {
        anim = GetComponent<Animator>();

        speedID = Animator.StringToHash("Speed");
        idleID = Animator.StringToHash("Idle");
        horizontalID = Animator.StringToHash("Horizontal");
        shittingID = Animator.StringToHash("Shitting");

        AIScript = GetComponent<CitizenAIManager>();
    }

    void Update()
    {
 //       print("Current AI state: " + AIState);
        AIState = AIScript.state;
        currentDir = AIScript.currentDir;
        // Select the action of animation based on Player State (Walking, Idle(Means player intercepted him), Shitting, Switching, or Knocked ) 
        switch (AIState)
        {
            case CitizenAIManager.AIStates.Walking:
            case CitizenAIManager.AIStates.Switch:
                // play Walking animation based on current direction
                switch (currentDir)
                {
                    case CitizenAIManager.Up:
//                        print("Moving Up");
                        anim.SetBool(horizontalID, false);
                        anim.SetBool(idleID, false);
                        anim.SetFloat(speedID, 1f);
                        break;

                    case CitizenAIManager.Down:
  //                      print("Moving Down");
                        anim.SetBool(horizontalID, false);
                        anim.SetBool(idleID, false);
                        anim.SetFloat(speedID, -1f);
                        break;

                    case CitizenAIManager.Right:
 //                       print("Moving Right");
                        anim.SetBool(horizontalID, true);
                        anim.SetBool(idleID, false);
                        anim.SetFloat(speedID, 1f);
                        break;

                    case CitizenAIManager.Left:
  //                      print("Moving Left");
                        anim.SetBool(horizontalID, true);
                        anim.SetBool(idleID, false);
                        anim.SetFloat(speedID, -1f);
                        break;
                }

                break;

            case CitizenAIManager.AIStates.RunAway:
                if (AIScript.startInLeft)
                {
   //                 print("Moving Left");
                    anim.SetBool(horizontalID, true);
                    anim.SetBool(idleID, false);
                    anim.SetFloat(speedID, -1f);
                }
                else
                {
  //                  print("Moving Right");
                    anim.SetBool(horizontalID, true);
                    anim.SetBool(idleID, false);
                    anim.SetFloat(speedID, 1f);
                }
                break;

            case CitizenAIManager.AIStates.Idle:
                anim.SetBool(idleID, true);
                anim.SetFloat(speedID, 0f);
                break;

            case CitizenAIManager.AIStates.Shitting:
                anim.SetTrigger(shittingID);
                anim.SetFloat(speedID, 0f);
                break;
        }
    }
}
