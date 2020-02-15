﻿using System.Collections;
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

    public bool ignoreSnapX = false;
    public bool ignoreSnapZ = false;

    [RangeAttribute(0.1f, 1)]
    public float scaleFactor = 1;

    public Vector2 dimensions;

    MeshRenderer mRenderer;

    void Start()
    {
        mRenderer = gameObject.GetComponent<MeshRenderer>();

        //If there is no MeshRenderer on the object, it may exists in its child
        if (mRenderer == null)
        {
            mRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();

            if (mRenderer == null)
            {
                Debug.LogError("Mesh Renderer Missing!");
            }
        }



    }
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

                float gridX;
                float gridZ;

                if (!ignoreSnapX)
                    gridX = Mathf.Round(transform.position.x / cellSize) * cellSize;
                else
                {
                    gridX = transform.position.x;
                }

                if (!ignoreSnapZ)
                    gridZ = Mathf.Round(transform.position.z / cellSize) * cellSize;
                else
                {
                    gridZ = transform.position.z;
                }


                if (!touchingFloor)
                {


                    transform.position = new Vector3(gridX, transform.position.y, gridZ);
                }
                else
                {
                    float gridY = mRenderer.bounds.size.y / 2;
                    transform.position = new Vector3(gridX, gridY, gridZ);

                }


            }
        }
    }

    void scaleXtoFit()
    {
        float xSize = mRenderer.bounds.size.x;
        Vector3 actualScale = gameObject.transform.localScale;

        Vector3 scaleVector = new Vector3(actualScale.x * scaleFactor * cellSize * dimensions.x / xSize, actualScale.y, actualScale.z);

        gameObject.transform.localScale = scaleVector;
    }

    void scaleZtoFit()
    {
        float zSize = mRenderer.bounds.size.z;
        Vector3 actualScale = gameObject.transform.localScale;

        Vector3 scaleVector = new Vector3(actualScale.x, actualScale.y, actualScale.z * scaleFactor * cellSize * dimensions.y / zSize);
        gameObject.transform.
        gameObject.transform.localScale = scaleVector;
    }

}
