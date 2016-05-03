using UnityEngine;
using System.Collections;

public class CitizenAIManager : MonoBehaviour {

    // All possible actions that a citizen can make
    public enum AIStates { Idle, Walking, Shitting, Knocked, Switch, RunAway };
    [HideInInspector]
    public AIStates state;    // current citizen AI state
    public enum CitizenType { Good, Perverted };
    public CitizenType type;

    public float shittingTime = 1.2f;   // time of NPC shitting

    public const int Up = 0;        // define the four possible directions
    public const int Right = 1;
    public const int Down = 2;
    public const int Left = 3;

    [HideInInspector]
    public int currentDir = 0;      // start with Up direction

    [HideInInspector]
    public int targetWPIndex = 0;   // target index of the next waypoint

    [HideInInspector]
    public int selectedPathIndex = 0;

    public bool startInLeft = true; // start in left or right direction
    public float ySwitchPoint = -3.2f;         // at this point, the perverted NPC will change their side

    void Start()
    {
        state = AIStates.Walking;
    }
}
