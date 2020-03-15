using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;
public class DragonScript : Enemy
{
    SimplePriorityQueue<Node> openNodes;
    SimplePriorityQueue<Node> closedNodes;
    
    Node currentNode;
    Vector2 target;

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

        openNodes.Clear();
        closedNodes.Clear();
       
        Vector2 targetVector;
        targetVector.x = target.transform.position.x / cellSize;
        targetVector.y = target.transform.position.z / cellSize;

        Node auxNode = new Node(null, targetVector, 0,0);
        openNodes.Enqueue(auxNode,0);

        while (openNodes.Count > 0) {

            currentNode = openNodes.Dequeue();

            //if(currentNode == )

            closedNodes.Enqueue(currentNode, currentNode.getF());

            generateChild(1,0);
            generateChild(-1,0);
            generateChild(0,1);
            generateChild(0,-1);



        }
    }

    private void generateChild(int x, int y) {
        Node node;
        node = new Node(currentNode, target, x, y);
        
        //if closedNodes doesnt contain node
        if (!closedNodes.Contains(node)) {
            
            //If openNodes doesnt contain node
            if (!openNodes.Contains(node))
            {
                openNodes.Enqueue(node,node.getF());
            }
            //If openNodes contains node
            else {
                //If g in list is bigger, set new parent and recalculate f

            }
        }
    }

    public float aPathValue(int x, int z) {


        return 0;
    }

    public class Node : System.IComparable
    {
        
        private int f;
        private int g;

        Vector2 position;
        Vector2 target;

        static float cellSize = 1;

        Node parent;

        public Node(Node parent, Vector2 target, int x, int y) {
            this.parent = parent;
            this.target = target;

            position = parent.getPosition();

            position.x += x;
            position.y += y;

            recalculateFG();

        }

        public int getG() {
            return g;
        }

        public void setG(int g) {
            this.g = g;
        }

        public int getF() {
            return f;
        }

        public void setF(int f) {
            this.f = f;
        }

        public Vector2 getPosition() {
            return position;
        }

        public Node getParent() {
            return parent;
        }

        public void setParent(Node parent) {
            this.parent = parent;
        }

        public int CompareTo(object obj)
        {
            Node otherNode = obj as Node;

            //Lowest f is better
            if (otherNode.getPosition() == position)
            {
                //return ;
            }

            return 1;
            
        }

        public void recalculateFG() {
            //G is the distance between parent and this node
            g = parent.getG() + (int)Mathf.Abs(position.x - target.x) + (int)Mathf.Abs(position.y - target.y);

            //F is g + distance to target in Manhattan Distance
            f = g + (int)Mathf.Abs(position.x - target.x) + (int)Mathf.Abs(position.y - target.y);
        }
    }


    Node listContains(List<Node> list, Node nodeToCompare) {
        foreach (Node node in list) {
            if (node.getPosition() == nodeToCompare.getPosition()) {
                return node;
            }
        }
        return null;
    }


}
