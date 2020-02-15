using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollower : MonoBehaviour
{
    Camera camera;
    public GameObject targetContainerObject;
    Transform target;

    public bool adjustFarPlanetoHeight = false;
    [RangeAttribute(0,10)]
    public float height = 2;
    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.GetComponent<Camera>();        
        target = targetContainerObject.GetComponent<Transform>();

        Vector3 auxiliarVector = target.position;
        auxiliarVector.y += height;

        transform.position = auxiliarVector;


        if (adjustFarPlanetoHeight) {
            camera.farClipPlane = transform.position.y*2;
        }

        if (camera == null) {
            Debug.LogError("Camera is missing on object!");
        }

        if (target == null) {
            Debug.LogError("Target Reference is missing!");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 auxiliarVector = target.position;
        auxiliarVector.y += height;

        transform.position = auxiliarVector;

        
    }

}
