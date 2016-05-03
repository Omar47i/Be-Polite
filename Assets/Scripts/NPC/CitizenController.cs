// This script acts as the Controller of any thing the citizen will do
// Like when he pervert, time of shitting, way points he selects, etc..
using UnityEngine;
using System.Collections;

public class CitizenController : MonoBehaviour {

    CitizenAIManager AIScript;
    CitizenMovement MovementScript;
    WaypointManager WaypointScript;
    ScoreManager ScoreScript;
    HealthManager HealthScript;

    private GameObject player;

	[HideInInspector]
    public bool pass = true;
    private bool canMove = true;
    private bool bOneTime = false;

    void Start()
    {
        AIScript = GetComponent<CitizenAIManager>();
        MovementScript = GetComponent<CitizenMovement>();
        WaypointScript = GetComponent<WaypointManager>();
        ScoreScript = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ScoreManager>();
        HealthScript = GameObject.FindGameObjectWithTag(Tags.healthIcon).GetComponent<HealthManager>();

        player = GameObject.FindGameObjectWithTag(Tags.player);
    }

	void Update()
    {
        if (AIScript.type == CitizenAIManager.CitizenType.Good)
        {
            if (canMove)
                MovementScript.Walk();
            else
                AIScript.state = CitizenAIManager.AIStates.Idle;
        }
        else
        {
            if (!pass)
            {
                MovementScript.RunAway();
            }
            else if (canMove)
                MovementScript.WalkThenSwitch();
            else
                AIScript.state = CitizenAIManager.AIStates.Idle;

            if (AIScript.targetWPIndex > 0 && pass && !bOneTime)      // if the perverted NPC succeed in passing the guard
            {
                // derease score one time
                ScoreScript.DecHealth();

                // animate health icon
                HealthScript.AnimateColor(ScoreScript.HP);

                bOneTime = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.player)
        {
            if (AIScript.type == CitizenAIManager.CitizenType.Perverted)
            {
                // display the GUI bubble that says "I GOT YOU"
                player.GetComponent<PlayerUI>().DisplayBubble("I GOT YOU");

                canMove = false;

                pass = false;
            }
            else
            {
                pass = false;      // NPC couldn't pass the guard

                canMove = false;

                // increase score
                ScoreScript.IncHealth();
            }
            
        }   
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == Tags.player)
        {
            canMove = true;

            AIScript.state = CitizenAIManager.AIStates.Walking;
        }
    }
}
