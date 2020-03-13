using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : Enemy
{
    List<Node> openNodes;
    List<Node> closedNodes;
    Node currentNode;

    protected float cellSize;

    public override void move()
    {
        if (canMakeMovement(1, 0))
        {
            Debug.LogWarning("Dragon moved X: " + 1);
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


    private void pathFinder(GameObject target) {


    }

    private bool generateChildren() {


        return true;
    }

    public float aPathValue(int x, int z) {


        return 0;
    }

    public class Node {
        
        public float g;
        public float f;

        Vector2 position;

        static float cellSize = 1;

        Node parent;

        public Node(Node parent, Vector2 target, int x, int y) {
            this.parent = parent;

            position = parent.getPosition();

            position.x += x * cellSize;
            position.y += y * cellSize;

            //G is the distance between parent and this node
            g = parent.getG() + Vector2.Distance(position,parent.getPosition());
            //F is g + distance to target in Manhattan Distance
            f = g + Mathf.Abs(position.x - target.x) + Mathf.Abs(position.y - target.y);

        }

        public float getG() {
            return g;
        }

        public void setG(float g) {
            this.g = g;
        }

        public float getF() {
            return f;
        }

        public void setF(float f) {
            this.f = f;
        }

        public Vector2 getPosition() {
            return position;
        }
    
    }
}
