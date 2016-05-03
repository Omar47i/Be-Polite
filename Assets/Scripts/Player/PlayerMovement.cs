using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

    public float initSpeed = 5f;        // initial player speed
    public float incSpeed = 1f;

    public float speed;                // current player speed
    private Animator anim;              // reference to the animator

    private Transform myTrans;          // reference to the player transform component
    private int leftPos = 0;
    private int leftDoorPos = 1;
    private int middlePos = 2;
    private int rightDoorPos = 3;
    private int rightPos = 4;
    private int currPos = 2;            // start at the middle

    private int speedID;                // cash animator variables

    public List<Vector3> Positions;     // all possible 2d positions

    private bool bLeftDir = true;       // assume start direction is left
    private bool bMoving = false;       // player is moving or idle

    void Start()
    {
        myTrans = transform;

        anim = GetComponent<Animator>();

        speedID = Animator.StringToHash("Speed");

        speed = initSpeed;
    }

    void Update()
    {
        if (bMoving)           // if move flag is set to true
        {
            if (bLeftDir)      // player is moving to the left direction
            {
                // play left animation
                anim.SetFloat(speedID, -1f);
            }
            else
            {
                // play right animation
                anim.SetFloat(speedID, 1f);
            }

            float step = speed * Time.deltaTime;     // amount of movement

            Vector3 newPos = Vector2.MoveTowards(myTrans.position, Positions[currPos], step);
            if (newPos == Positions[currPos])        // reached target
            {
                anim.SetFloat(speedID, 0f);
                bMoving = false;
            }

            myTrans.position = newPos;

            
        }
    }

    public void MoveToRight()
    {
        // Insure move ability
        if (currPos >= rightPos || bMoving == true)
            return;

        // set flag top indicate which animation will be played
        bLeftDir = false;

        // player is in move state
        bMoving = true;

        currPos++;
    }

    public void MoveToLeft()
    {
        // Insure move ability
        if (currPos <= leftPos || bMoving == true)
            return;

        // set flag top indicate which animation will be played
        bLeftDir = true;

        // player is in move state
        bMoving = true;

        currPos--;
    }
}
