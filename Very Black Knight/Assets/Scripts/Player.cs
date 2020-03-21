using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int playerLevel = 1;

    public GameObject playerGuiObject;
    private PlayerGUI myPlayerGUI;

    private int currentState;

    PlayerMovement movementScript;
    public float idleSpeed = 1;

    float health = 5;

    Animator myAnimator;

    bool finishedTurn = false;

    enum State
    {
        idle = 0,
        walking = 1,
        dead = 2
    }


    // Start is called before the first frame update
    void Start()
    {
        //Find Scripts
        movementScript = gameObject.GetComponent<PlayerMovement>();

        myAnimator = gameObject.GetComponent<Animator>();

        myPlayerGUI = playerGuiObject.GetComponent<PlayerGUI>();



        if (movementScript == null)
        {
            Debug.LogError("Movement Script is missing!");

        }

        if (myAnimator == null)
        {
            Debug.LogError("Animator Component is missing!");

        }

        currentState = (int)State.idle;

        myPlayerGUI.setMaxHealth(health);
        myPlayerGUI.setCurrentLevel(playerLevel);
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
            myPlayerGUI.setHealth(0);
        }
        else
        {
            myAnimator.SetTrigger("hurt");
            myPlayerGUI.setHealth(health);

        }

    }

    
}
