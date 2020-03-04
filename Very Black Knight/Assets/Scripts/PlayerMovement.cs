﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Vic
public class PlayerMovement : MonoBehaviour
{
    private GameObject gameContainerObject;
    private Game game;

    //Width of cells
    private float cellSize;
    //Height over the grid
    public float height = 1;

    //Time to finish movement
    [Range(0, 10)]
    public float timeToReach = 0.5f;
    private float timeCounter;

    [HideInInspector]
    public bool doingMovement = false;

    //Starting and ending position are used of interpolation
    private Vector3 newPosition;
    private Vector3 startingPosition;

    Vector3 direction;

    // Start is called before the first frame update

    void Start()
    {
        gameContainerObject = GameObject.Find("GameController");
        game = gameContainerObject.GetComponent<Game>();
        cellSize = game.getCellSize();


    }

    // Update is called once per frame
    void Update()
    {

        //Movement Input is only possible if the object is not moving
        if (!doingMovement)
        {

            //Forward
            if (Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Up or W key was pressed");

                if (!canMakeMovement(1, 0))
                {
                    Debug.Log("Can't move forward");
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
                }

            }

            //BackWard
            if (Input.GetKeyDown(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.S))
            {

                Debug.Log("Down or S key was pressed");

                if (!canMakeMovement(-1, 0))
                {
                    Debug.Log("Can't move BackWard");
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
                }
            }

            //Right
            if (Input.GetKeyDown(KeyCode.RightArrow) | Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("Right or D key was pressed");

                if (!canMakeMovement(0, -1))
                {
                    Debug.Log("Can't move Right");
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                }

            }

            //Left
            if (Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Left or A key was pressed");

                if (!canMakeMovement(0, 1))
                {
                    Debug.Log("Can't move Left");
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }

        }

    }

    //Physics must be done on FixedUpdate as it is always executed
    void FixedUpdate()
    {
        //Movement on the player are done if dointMovement is true
        if (doingMovement)
        {
            //Divide by timeToReach implies it will take such time until arrival
            timeCounter += Time.deltaTime / timeToReach;
            transform.position = Vector3.Lerp(startingPosition, newPosition, timeCounter);

            //Aproximating to the point
            if (Vector3.Distance(transform.position, newPosition) < 0.05)
            {
                transform.position = newPosition;
                doingMovement = false;
            }


        }
    }

    public bool canMakeMovement(float xMove, float zMove)
    {
        Vector3 movementVector = new Vector3();

        //Transform desired destination into grid coordinates
        movementVector.x = Mathf.Round((transform.position.x + xMove * cellSize) / cellSize) * cellSize;
        movementVector.z = Mathf.Round((transform.position.z + zMove * cellSize) / cellSize) * cellSize;

        //Start movement
        if (game.canMakeMovement(movementVector.x, movementVector.z))
        {
            //It is necesarry to store point B for Interpolation
            startingPosition = transform.position;


            //It is necesarry to store point B for Interpolation
            newPosition = movementVector;

            //We make sure the player's height is not the tile's height
            newPosition.y = height;

            doingMovement = true;
            timeCounter = 0;

            direction = newPosition - startingPosition;

            direction = Vector3.Normalize(direction);

            return true;
        }
        return false;
    }
}