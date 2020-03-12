using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : Enemy
{
    public override void move()
    {
        if (canMakeMovement(1, 0))
        {
            Debug.LogWarning("Dragon moved X: "+1);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        initialize();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        makeMovement();
    }
}
