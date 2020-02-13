using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject gameContainerObject;
    private Game game;

    float gridX;
    float gridZ;

    //Limits on the grid
    private int rows;
    private int columns;

    //Width of cells
    private float cellSize;
    //Height over the grid
    public float height = 1;


    //Time to finish movement
    [Range(0, 10)]
    public float timeToReach = 0.5f;
    private float timeCounter;

    private bool doingMovement = false;

    //Starting and ending position are used of interpolation
    private Vector3 newPosition;
    private Vector3 startingPosition;

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
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("Up key was pressed");

                if (!canMakeMovement(1, 0))
                {
                    Debug.Log("Can't move forward");
                }
            }

            //BackWard
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                Debug.Log("Down key was pressed");

                if (!canMakeMovement(-1, 0))
                {
                    Debug.Log("Can't move BackWard");
                }
            }

            //Right
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Right key was pressed");

                if (!canMakeMovement(0, -1))
                {
                    Debug.Log("Can't move Right");
                }

            }

            //Left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Left key was pressed");

                if (!canMakeMovement(0, 1))
                {
                    Debug.Log("Can't move Left");
                }
            }

        }

    }

    void FixedUpdate()
    {
        if (doingMovement)
        {
            timeCounter += Time.deltaTime / timeToReach;
            transform.position = Vector3.Lerp(startingPosition, newPosition, timeCounter);


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

        movementVector.x = Mathf.Round((transform.position.x + xMove * cellSize) / cellSize) * cellSize;
        movementVector.z = Mathf.Round((transform.position.z + zMove * cellSize) / cellSize) * cellSize;

        if (game.canMakeMovement(movementVector.x, movementVector.z))
        {
            newPosition = movementVector;

            newPosition.y = height;

            startingPosition = transform.position;

            doingMovement = true;
            timeCounter = 0;

            return true;
        }
        return false;
    }
}