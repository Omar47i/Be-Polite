using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour {

    public List<Vector3> WaypointsRight;       // List of waypoints that an NPC iterate through them until reaching the end of NPC way
    public List<Vector3> WaypointsLeft;        // left section waypoints

    public const int waypointCapacity = 5;     // number of waypoints

    [HideInInspector]
    public List<bool> bathsAvailable_Right;    // availability of each bath room, if an NPC went to a bath, the proceeding NPC cannot enter the bath until it's free
    [HideInInspector]
    public List<bool> bathsAvailable_Left;          
    public int bathCapacity = 3;               // number of bath rooms
    void Start()
    {
        bathsAvailable_Right = new List<bool>();
        bathsAvailable_Left = new List<bool>();

        for (int i = 0; i < bathCapacity; i++)
        {
            bathsAvailable_Right.Add(true);
            bathsAvailable_Left.Add(true);
        }
    }

    /// <summary>
    /// This function sets the state of each bath if it's available or not
    /// </summary>
    /// <param name="bathIndex">Hint: Bath index is from 1 to 3 respectively</param>
    /// <param name="state">Available or not</param>
    public void SetLeftBathState(int bathIndex, bool state)
    {
        bathsAvailable_Left[bathIndex] = state;
    }

    /// <summary>
    /// This function sets the state of each bath if it's available or not
    /// </summary>
    /// <param name="bathIndex">Hint: Bath index is from 1 to 3 respectively</param>
    /// <param name="state">Available or not</param>
    public void SetRightBathState(int bathIndex, bool state)
    {
        bathsAvailable_Right[bathIndex] = state;
    }
}
