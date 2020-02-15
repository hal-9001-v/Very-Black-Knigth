using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Vic
[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour
{
    private float offsetLimitX;
    private float offsetLimitZ;

    public float cellSize = 1;
    public bool placeOnGrid = false;
    public bool touchingFloor = true;
    

    
    public bool scaleXToFitCell = false;
    public bool scaleZToFitCell = false;

    public Vector2 dimensions;

    private bool centered = true;
    [RangeAttribute(-1,1)]
    public float offsetInX;
    [RangeAttribute(-1, 1)]
    public float offsetInZ;

    private bool changeDone;

    Vector3 originalPosition;
    // Update is called once per frame
    void Update()
    {
        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {

            if (dimensions.x == 0) dimensions.x = 1;
            if (dimensions.y == 0) dimensions.y = 1;
            if (cellSize == 0) cellSize = 1;


            if (placeOnGrid)
            {
                if (scaleXToFitCell) scaleXtoFit();
                if (scaleZToFitCell) scaleZtoFit();


                float gridX = Mathf.Round(transform.position.x / cellSize) * cellSize;
                float gridZ = Mathf.Round(transform.position.z / cellSize) * cellSize;

                if (!touchingFloor)
                    transform.position = new Vector3(gridX, transform.position.y, gridZ);
                else
                {
                    float gridY = GetComponent<MeshRenderer>().bounds.size.y / 2;
                    transform.position = new Vector3(gridX, gridY, gridZ);
                }

                if (!centered)
                {
                    if (!changeDone) {
                        Vector3 auxiliarVector = new Vector3();
                        auxiliarVector = transform.position;

                        auxiliarVector.x += gameObject.GetComponent<MeshRenderer>().bounds.size.x;
                        transform.position = auxiliarVector;
                        changeDone = true;
                    }
                
                    
                }
                else {
                    changeDone = false;
                }
            }
        }
    }

    void scaleXtoFit()
    {
        float xSize = gameObject.GetComponent<MeshRenderer>().bounds.size.x;
        Vector3 actualScale = gameObject.transform.localScale;
        
        Vector3 scaleVector = new Vector3(actualScale.x * cellSize * dimensions.x / xSize, actualScale.y, actualScale.z);

        gameObject.transform.localScale = scaleVector;
    }

    void scaleZtoFit()
    {
        float zSize = gameObject.GetComponent<MeshRenderer>().bounds.size.z;
        Vector3 actualScale = gameObject.transform.localScale;
       
        Vector3 scaleVector = new Vector3(actualScale.x, actualScale.y, actualScale.z * cellSize*dimensions.y / zSize);

        gameObject.transform.localScale = scaleVector;
    }


}
