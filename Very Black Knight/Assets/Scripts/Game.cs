﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//Author: Vic
//Game class is made to link elements within the game. Thus, it can  be used to modify many elements.

public class Game : MonoBehaviour
{

    //Tiletag is the tag name which floor tiles have
    public string tileTag;

    //Size in the grid
    public float cellSize = 1;

    GameObject playerObject;
    Player myPlayerScript;

    //Enemies list
    List<GameObject> enemiesList;

    //List of floor tiles
    GameObject[] tiles;

    bool enemyMovementActive = false;

    //Player Level Manager
    public int playerLevel;
   public TextMeshProUGUI lvlTxt;


    //This function is called to check wether floor tiles are next to the given coordinates, thus they are accessble
    public bool canMakeMovement(float x, float y)
    {
        //We have to check every floor tile we have
        foreach (GameObject go in tiles)
        {

            if (go.GetComponent<GridTile>().movable(x, y))
            {

                if (go.GetComponent<GridTile>().endingTile)
                {
                    Debug.Log("End of Game");
                    levelUp();
                    SceneManager.LoadScene("Level_" + playerLevel);
                }
                return true;
            }
        }

        return false;
    }




    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");

        myPlayerScript = playerObject.GetComponent<Player>();

        enemiesList = new List<GameObject>();

        //Find enemies in scene
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
           
            enemiesList.Add(enemy);

        }

        //Tiles will get every gameObject whose tag is "tileTag"
        tiles = GameObject.FindGameObjectsWithTag(tileTag);
        Debug.Log("Current Tiles in the scene: " + tiles.Length);

        //There's only a starting point in our scene, we have to count how many of them we have
        int startingTileCounter = 0;

        //This loop can be use to configure the scene
        foreach (GameObject go in tiles)
        {

            //Moving player to the starting point
            if (go.GetComponent<GridTile>().startingTile)
            {
                startingTileCounter++;

                Vector3 aux = go.transform.position;
                aux.y = playerObject.transform.position.y;

                playerObject.transform.position = aux;


            }


        }

        //There is only one Ending tile per Scene
        if (startingTileCounter > 1)
        {
            Debug.LogError("There are " + startingTileCounter + " starting Tiles");
        }

        //Player Level init
        lvlTxt.text = playerLevel.ToString();

    }


    void Awake()
    {

        DontDestroyOnLoad(gameObject);


    }


    // Update is called once per frame
    void Update()
    {
        if(!enemyMovementActive)
        StartCoroutine(EnemyMoves());

      

    }


    public string getTileTag()
    {
        return tileTag;
    }

    IEnumerator EnemyMoves()
    {
        enemyMovementActive = true;
        if (myPlayerScript.hasFinishedTurn())
        {
    
            Enemy enemyScript;
            foreach (GameObject enemyObject in enemiesList)
            {
                enemyScript = enemyObject.GetComponent<Enemy>();
                enemyScript.move();

                yield return 0;
            }
        }

        enemyMovementActive = false;
    }

    public float getCellSize()
    {
        return cellSize;
    }



   public void levelUp()
    {

        playerLevel++;
        lvlTxt.text = playerLevel.ToString();

    }
}
