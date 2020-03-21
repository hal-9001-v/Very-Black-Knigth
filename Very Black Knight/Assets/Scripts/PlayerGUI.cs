using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
    //################################
    //HEALTH
    public GameObject colorBarObject;
    Image colorBar;
    public GameObject backgroundBarObject;
    Image backgroundBar;

    float maxHealth;
    Vector3 maxLocalScale;
    //###############################


    //###############################
    //LEVEL
    public GameObject levelTextContainer;
    Text levelText;
    //###############################


    // Start is called before the first frame update
    void Start()
    {

        colorBar = colorBarObject.GetComponent<Image>();
        backgroundBar = backgroundBarObject.GetComponent<Image>();

        colorBar.rectTransform.pivot = new Vector2(0, 0.5f);
        colorBar.fillMethod = Image.FillMethod.Horizontal;

        maxLocalScale = colorBar.rectTransform.localScale;

        levelText = levelTextContainer.GetComponent<Text>();
    }

    public void setCurrentLevel(int level)
    {
        levelText.text = "Current LVL: " + level;
    }

    public void setMaxHealth(float maxHealth)
    {

        this.maxHealth = maxHealth;

    }

    public void setHealth(float hp)
    {
        if (hp < 0 | hp > maxHealth)
        {
            setHealth(0);

        }

        Vector3 actualScale = maxLocalScale;
        actualScale.x *= hp / maxHealth;
        colorBar.rectTransform.localScale = actualScale;


    }
}
