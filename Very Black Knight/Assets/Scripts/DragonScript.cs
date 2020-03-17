using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;
public class DragonScript : Enemy
{ /* PATHFINDING, NOT SUPPORTED
    SimplePriorityQueue<Node> openNodes;
    SimplePriorityQueue<Node> closedNodes;
    Node currentNode;
    Vector2 target;

    */

    private int currentState = 0;
    GameObject player;
    bool readyForNextTurn = true;

    List<GameObject> attackTiles;
    List<Vector2> movementList;

    bool delay = false;
    float waitTime;
    float currentTime;


    enum State
    {
        idle = 0,
        walking = 1
    }

    public override void startTurn()
    {
        if (readyForNextTurn)
        {
            readyForNextTurn = false;

            if (hurt())
            {
                currentState = (int)State.idle;
                resetTurnIn(2f);
                return;
            }

            switch (currentState)
            {
                //Idle
                case 0:

                    if (Vector3.Distance(transform.position, player.transform.position) < 20)
                    {
                        
                        movementList.Clear();

                        //Decide movement
                        if (player.transform.position.x > transform.position.x)
                        {
                            movementList.Add(new Vector2(1, 0));
                        }
                        if (player.transform.position.z > transform.position.z)
                        {
                            movementList.Add(new Vector2(0, 1));

                        }
                        if (player.transform.position.x < transform.position.x)
                        {
                            movementList.Add(new Vector2(-1, 0));

                        }
                        if (player.transform.position.z < transform.position.z)
                        {
                            movementList.Add(new Vector2(0, -1));
                        }


                        

                        if (move())
                        {
                            currentState = (int)State.walking;
                            myAnimator.SetBool("walking", true);
                            
                        }
                        else {
                            readyForNextTurn = true;
                        }

                    }
                    break;

                //Walking
                case 1:
                    // Look at endOfMovement() due to synchronization 

                    break;
            }
        }
    }
    public override void endOfMovementActions()
    {
        readyForNextTurn = true;
        currentState = (int)State.idle;
        myAnimator.SetBool("walking", false);

        setAttackTiles();

    }

    private bool move()
    {
        foreach (Vector2 movement in movementList)
        {
            if (canMakeMovement(movement.x, movement.y))
            {
                if (movement.x > 0) transform.eulerAngles = new Vector3(0, 90, 0);
                else if (movement.x < 0) transform.eulerAngles = new Vector3(0, -90, 0);

                else if (movement.y > 0) transform.eulerAngles = new Vector3(0, 0, 0);
                else if (movement.y < 0) transform.eulerAngles = new Vector3(0, 180, 0);

                return true;
            }
        }

        return false;
    }

    private void setAttackTiles()
    {

        foreach (GameObject tile in attackTiles)
        {
            tile.GetComponent<MeshRenderer>().material.color -= attackColor;
        }

        attackTiles.Clear();
        Vector3 auxiliarVector;

            auxiliarVector = transform.position;
            attackTiles.Add(game.getTile(auxiliarVector));
        

        if (tileExists(1, 0))
        {
            auxiliarVector = transform.position;
            auxiliarVector.x += cellSize;
            attackTiles.Add(game.getTile(auxiliarVector));
        }

        if (tileExists(-1, 0))
        {
            auxiliarVector = transform.position;
            auxiliarVector.x -= cellSize;
            attackTiles.Add(game.getTile(auxiliarVector));
        }

        if (tileExists(0, 1))
        {
            auxiliarVector = transform.position;
            auxiliarVector.z += cellSize;
            attackTiles.Add(game.getTile(auxiliarVector));
        }

        if (tileExists(0, -1))
        {
            auxiliarVector = transform.position;
            auxiliarVector.z -= cellSize;
            attackTiles.Add(game.getTile(auxiliarVector));
        }

        if (tileExists(1, 1))
        {
            auxiliarVector = transform.position;
            auxiliarVector.x += cellSize;
            auxiliarVector.z += cellSize;
            attackTiles.Add(game.getTile(auxiliarVector));
        }

        if (tileExists(-1, 1))
        {
            auxiliarVector = transform.position;
            auxiliarVector.x -= cellSize;
            auxiliarVector.z += cellSize;
            attackTiles.Add(game.getTile(auxiliarVector));
        }

        if (tileExists(1, -1))
        {
            auxiliarVector = transform.position;
            auxiliarVector.x += cellSize;
            auxiliarVector.z -= cellSize;
            attackTiles.Add(game.getTile(auxiliarVector));
        }

        if (tileExists(-1, -1))
        {
            auxiliarVector = transform.position;
            auxiliarVector.x -= cellSize;
            auxiliarVector.z -= cellSize;
            attackTiles.Add(game.getTile(auxiliarVector));
        }

        foreach (GameObject tile in attackTiles)
        {
            tile.GetComponent<MeshRenderer>().material.color += attackColor;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        movementList = new List<Vector2>();

        initialize();

        attackTiles = new List<GameObject>();
    }

    void Update()
    {
        delayFunction();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        makeMovement();
    }

    void resetTurnIn(float t) {
        waitTime = t;
        currentTime = 0;

        delay = true;
    }

    void delayFunction() {
        if (delay)
        {
            if (currentTime > waitTime)
            {
                readyForNextTurn = true;
                delay = false;
            }
            currentTime += Time.deltaTime;
        }
    }

    bool hurt()
    {
        foreach (GameObject tile in attackTiles)
        {

            if (tile.GetComponent<GridTile>().movable(player.transform.position.x, player.transform.position.z))
            {
                player.GetComponent<Player>().hurt(2);
                
                return true;

            }

        }
        return false;

    }


    /*
private void pathFinder(GameObject target)
{

   openNodes.Clear();
   closedNodes.Clear();

   Vector2 targetVector;
   targetVector.x = target.transform.position.x / cellSize;
   targetVector.y = target.transform.position.z / cellSize;

   Node auxNode = new Node(null, targetVector, 0, 0);
   openNodes.Enqueue(auxNode, 0);

   while (openNodes.Count > 0)
   {

       currentNode = openNodes.Dequeue();

       //if(currentNode == )

       closedNodes.Enqueue(currentNode, currentNode.getF());

       generateChild(1, 0);
       generateChild(-1, 0);
       generateChild(0, 1);
       generateChild(0, -1);



   }
}

private void generateChild(int x, int y)
{
   Node node;
   node = new Node(currentNode, target, x, y);

   //if closedNodes doesnt contain node
   if (!closedNodes.Contains(node))
   {

       //If openNodes doesnt contain node
       if (!openNodes.Contains(node))
       {
           openNodes.Enqueue(node, node.getF());
       }
       //If openNodes contains node
       else
       {
           //If g in list is bigger, set new parent and recalculate f

       }
   }
}

public float aPathValue(int x, int z)
{


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

   public Node(Node parent, Vector2 target, int x, int y)
   {
       this.parent = parent;
       this.target = target;

       position = parent.getPosition();

       position.x += x;
       position.y += y;

       recalculateFG();

   }

   public int getG()
   {
       return g;
   }

   public void setG(int g)
   {
       this.g = g;
   }

   public int getF()
   {
       return f;
   }

   public void setF(int f)
   {
       this.f = f;
   }

   public Vector2 getPosition()
   {
       return position;
   }

   public Node getParent()
   {
       return parent;
   }

   public void setParent(Node parent)
   {
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

   public void recalculateFG()
   {
       //G is the distance between parent and this node
       g = parent.getG() + (int)Mathf.Abs(position.x - target.x) + (int)Mathf.Abs(position.y - target.y);

       //F is g + distance to target in Manhattan Distance
       f = g + (int)Mathf.Abs(position.x - target.x) + (int)Mathf.Abs(position.y - target.y);
   }
}


Node listContains(List<Node> list, Node nodeToCompare)
{
   foreach (Node node in list)
   {
       if (node.getPosition() == nodeToCompare.getPosition())
       {
           return node;
       }
   }
   return null;
}
*/
}
