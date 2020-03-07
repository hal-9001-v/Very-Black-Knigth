using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Author: Vic
//Game class is made to link elements within the game. Thus, it can  be used to modify many elements.
[ExecuteInEditMode]
public class Game : MonoBehaviour
{
    //Tiletag is the tag name which floor tiles have
    public string tileTag;

    //Size in the grid
    public float cellSize = 1;

    //Lighting settings
    public float ambientLightRed = 0;
    public float ambientLightGreen = 0;
    public float ambientLightBlue = 0;

    GameObject player;

    //List of floor tiles
    GameObject[] tiles;

    //Height of the player over the floor
    float playerHeight;



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
                }
                return true;
            }
        }

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerHeight = player.GetComponent<PlayerMovement>().height;

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
                aux.y = player.GetComponent<PlayerMovement>().height;

                player.transform.position = aux;


            }


        }

        if (startingTileCounter > 1)
        {
            Debug.LogError("There are " + startingTileCounter + " starting Tiles");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //This is executed only in play mode
        if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {
            
        }
    }

    public string getTileTag()
    {
        return tileTag;
    }

    public float getCellSize()
    {
        return cellSize;
    }
}
