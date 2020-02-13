using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Game : MonoBehaviour
{

    public string tileTag;
    public float cellSize = 1;
    GameObject player;
    GameObject[] tiles;
    float playerHeight;


    public bool canMakeMovement(float x, float y)
    {
        foreach (GameObject go in tiles)
        {
            if (go.GetComponent<GridTile>().movable(x, y)) return true;
        }

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerHeight = player.GetComponent<PlayerMovement>().height;
        tiles = GameObject.FindGameObjectsWithTag(tileTag);
        Debug.Log("Current Tiles in the scene: " + tiles.Length);

        int startingTileCounter = 0;

        foreach (GameObject go in tiles)
        {
            if (go.GetComponent<GridTile>().startingTile)
            {
                startingTileCounter++;

                if (startingTileCounter > 1) {
                    Debug.LogError("There are "+startingTileCounter+" starting Tiles");
                }

                Vector3 aux = go.transform.position;
                aux.y = player.GetComponent<PlayerMovement>().height;

                player.transform.position = aux;
                
                
            }

            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) { 
            
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
