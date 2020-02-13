using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public bool startingTile = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = GameObject.Find("GameController").GetComponent<Game>().getTileTag();
    }

    public bool movable(float x, float z) {

        //Debug.Log(x+" y "+z +" "+transform.position.x+" y "+transform.position.z);

        if (transform.position.x != x) return false;
        if (transform.position.z != z) return false;

        return true;
    
    }
}
