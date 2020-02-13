using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Move status is used to know the status of the player and their desired move. With this data class such information can be passed. 
public class MoveStatus
{
    private int currentX;
    private int currentY;
    
    private int newX;
    private int newY;

    public MoveStatus(int currentX, int currentY, int newX, int newY) {

        this.currentX = currentX;
        this.currentY = currentY;

        this.newX = newX;
        this.newY = newY;
    
    }

    public int getCurrentX() {
        return currentX;
    }

    public int getCurrentY()
    {
        return currentY;
    }

    public int getNewX()
    {
        return newX;
    }

    public int getNewY()
    {
        return newY;
    }
}
