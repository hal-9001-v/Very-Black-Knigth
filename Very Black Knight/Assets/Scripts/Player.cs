using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject playerGuiObject;
    private PlayerGUI myPlayerGUI;

    public bool loadFromDisk;

    System.Diagnostics.Stopwatch timer;

    PlayerMovement movementScript;
    Animator myAnimator;

    float MAXHEALTH;
    float health;

    private int playerLevel;
    private int upgrades;

    public int movementLevel { get; private set; }
    public int healthLevel { get; private set; }

    private int currentState;
    bool finishedTurn = false;
    bool playerActive = true;

    enum State
    {
        idle = 0,
        walking = 1,
        dead = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        //Find Scripts
        movementScript = gameObject.GetComponent<PlayerMovement>();

        myAnimator = gameObject.GetComponent<Animator>();

        myPlayerGUI = playerGuiObject.GetComponent<PlayerGUI>();

        timer = new System.Diagnostics.Stopwatch();
        timer.Start();

        if (movementScript == null)
        {
            Debug.LogError("Movement Script is missing!");

        }

        if (myAnimator == null)
        {
            Debug.LogError("Animator Component is missing!");

        }

        currentState = (int)State.idle;

        if (loadFromDisk)
        {
            loadData();
        }
        else {
            setData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerActive)
        {
            switch (currentState)
            {
                //Idle
                case 0:

                    finishedTurn = false;

                    if (movementScript.checkInput())
                    {
                        currentState = (int)State.walking;

                    }
                    break;

                //Walking
                case 1:
                    if (movementScript.doingMovement)
                    {
                        myAnimator.SetBool("Walking", true);
                    }
                    else
                    {
                        myAnimator.SetBool("Walking", false);
                        currentState = (int)State.idle;

                        finishedTurn = true;
                    }

                    break;
                //dead
                case 2:

                    break;

                //Hurt
                case 3:

                    break;

            }
        }

    }


    public bool hasFinishedTurn()
    {

        return finishedTurn;
    }

    public void hurt(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            currentState = (int)State.dead;
            myAnimator.SetTrigger("dead");
            myPlayerGUI.setHealth(0);
        }
        else
        {
            myAnimator.SetTrigger("hurt");
            myPlayerGUI.setHealth(health);

        }

    }

    public void levelUp(int ups)
    {
        playerActive = false;

        upgrades = ups;
        playerLevel += ups;
        myPlayerGUI.setCurrentLevel(playerLevel);

        displayLevelUpScreen();
    }

    public void activePlayer(bool b) {
        playerActive = b;
    }

    public void upgradeHealth() {
        if (upgrades > 0) {
            upgrades--;

            healthLevel++;

            if (healthLevel == 2) {
                MAXHEALTH += 2;
                health = MAXHEALTH;

                myPlayerGUI.setMaxHealth(MAXHEALTH);
                myPlayerGUI.setHealth(health);
            }
        }
    }
    public void upgradeMovement() {
        if (upgrades > 0)
        {
            upgrades--;

            movementLevel++;

            if (movementLevel == 2)
            {
                movementScript.setTimeToReach(0.3f);
            }
        }

        if (movementLevel == 2) {
            //movementScript.timeToReach;
        }
    }
    public void upgradeAttack() {
        if (upgrades > 0)
        {
            upgrades--;

            movementLevel++;
        }
    }

    private void displayLevelUpScreen() {
        playerActive = false;

        myPlayerGUI.setLevelUpIndicators(movementLevel, healthLevel, upgrades);

    }

    public void hideLevelUpScreen() {
        playerActive = true;

        myPlayerGUI.hideLevelUpIndicators();
    }

    private void loadData() {
        //FLOATS
        MAXHEALTH = PlayerPrefs.GetFloat("MAXHEALTH");
        health = MAXHEALTH;
        
        //INTEGERS
        playerLevel = PlayerPrefs.GetInt("playerLevel");
        upgrades = PlayerPrefs.GetInt("upgrades");
        movementLevel = PlayerPrefs.GetInt("movementLevel");
        healthLevel = PlayerPrefs.GetInt("healthLevel");

        myPlayerGUI.setMaxHealth(MAXHEALTH);
        myPlayerGUI.setCurrentLevel(playerLevel);

    }

    private void setData() {
        //FOATS
        MAXHEALTH = 5;
        health = MAXHEALTH;
        
        //INTEGERS
        playerLevel = 1;
        upgrades = 0;
        movementLevel = 1;
        healthLevel = 1;


        myPlayerGUI.setMaxHealth(MAXHEALTH);
        myPlayerGUI.setCurrentLevel(playerLevel);        
    }

    private void saveData()
    {
        //FLOATS
        PlayerPrefs.SetFloat("MAXHEALTH", MAXHEALTH);


        //INTEGERS
        PlayerPrefs.SetInt("playerLevel", playerLevel);
        PlayerPrefs.SetInt("upgrades", upgrades);
        PlayerPrefs.SetInt("movementLevel", movementLevel);
        PlayerPrefs.SetInt("healthLevel", healthLevel);

        PlayerPrefs.Save();
    }

    public void nextLevel() {
        int nextScene = UnityEngine.SceneManagement.SceneManager.sceneCount + 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.sceneCount + 1);
    }
}
