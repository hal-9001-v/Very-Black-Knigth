﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int currentState;

    PlayerMovement movementScript;
    public float idleSpeed = 1;

    int health = 5;

    Animator myAnimator;

    bool finishedTurn = false;

    enum State
    {
        idle = 0,
        walking = 1,
        dead = 2
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

        if (myAnimator == null)
        {
            Debug.LogError("Animator Component is missing!");

        }


        currentState = (int)State.idle;
    }

    void Awake() {

        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {


        switch (currentState)
        {
            //Idle
            case 0:

                finishedTurn = false;

                if (movementScript.checkInput())
                {
                    currentState = (int)State.walking;

                }
                break;

            //Walking
            case 1:
                if (movementScript.doingMovement)
                {
                    myAnimator.SetBool("Walking", true);
                }
                else
                {
                    myAnimator.SetBool("Walking", false);
                    currentState = (int)State.idle;

                    finishedTurn = true;
                }

                break;
            //dead
            case 2:

                break;

            //Hurt
            case 3:

                break;

        }

    }


    public bool hasFinishedTurn()
    {

        return finishedTurn;
    }

    public void hurt(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            currentState = (int)State.dead;
            myAnimator.SetTrigger("dead");

        }
        else
        {

            myAnimator.SetTrigger("hurt");
        }

    }

}
