using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int currentState;

    

    PlayerMovement movementScript;
    public float idleSpeed = 1;
    private float walkingSpeed;

    Animator myAnimator;

    enum State
    {
        walking = 1,
        idle = 0,
    }

   //Animation Times
   /*
    Walking Animation is 1.250 long
         */

    // Start is called before the first frame update
    void Start()
    {
        //Find Scripts
        movementScript = gameObject.GetComponent<PlayerMovement>();


        myAnimator = gameObject.GetComponent<Animator>();


        if (movementScript == null)
        {
            Debug.LogError("Movement Script is missing!");

        }
        else {
            walkingSpeed = movementScript.timeToReach;
            walkingSpeed = walkingSpeed / 1.25f;
            

        }

        if (myAnimator == null)
        {
            Debug.LogError("Animator Component is missing!");

        }


        currentState = (int)State.idle;
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            //Idle
            case 0:
                movementScript.enabled = true;
                
                if (movementScript.doingMovement)
                {

                    currentState = (int)State.walking;
                }

                break;

            //Walking
            //Walking animation is 1.250 long
            case 1:
                movementScript.enabled = true;

                if (movementScript.doingMovement)
                {
                    myAnimator.SetBool("Walking", true);
                }
                else {
                    myAnimator.SetBool("Walking", false);
                    currentState = (int)State.idle;
                }



                break;


        }

    }



}
