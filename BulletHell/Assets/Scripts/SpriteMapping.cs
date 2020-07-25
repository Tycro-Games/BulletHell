using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMapping : MonoBehaviour
{
    public Sprite roomUpDownLeftRight, // 1
    roomUpDownLeft,      // 2
    roomUpDownRight,     // 3
    roomUpDown,          // 4
    roomUp,              // 5
    roomDown,            // 6
    roomUpLeft,          // 7
    roomUpRight,         // 8
    roomUpLeftRight,     // 9
    roomDownLeft,        // 10
    roomDownRight,       // 11
    roomDownLeftRight,   // 12
    roomRightLeft,       // 13
    roomRight,           // 14
    roomLeft;            // 15

    public bool doorTop = true, doorBot = true, doorRight = true, doorLeft = true;

    public int type = 0;

    public Color normalColor,startColor,actualColor,bossColor,treasureColor;
    Color mainColor;

    SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        SpritePicker();
        ColorPicker();
    }

    void SpritePicker()
    {
        // urmeaza cel mai frumos(scarbos) cod pe care l-ai vazut vreodata
        if(doorTop)
        {
            if(doorBot)
            {
                if(doorRight)
                {
                    if(doorLeft)
                    {
                        rend.sprite = roomUpDownLeftRight;
                    }
                    else
                    {
                        rend.sprite = roomUpDownRight;
                    }
                }
                else
                {
                    if(doorLeft)
                    {
                        rend.sprite = roomUpDownLeft;
                    }
                    else
                    {
                        rend.sprite = roomUpDown;
                    }
                }
            }
            else
            {
                if(doorRight)
                {
                    if(doorLeft)
                    {
                        rend.sprite = roomUpLeftRight;
                    }
                    else
                    {
                        rend.sprite = roomUpRight;
                    }
                }
                else
                {
                    if(doorLeft)
                    {
                        rend.sprite = roomUpLeft;
                    }
                    else
                    {
                        rend.sprite = roomUp;
                    }
                }
            }
        }
        else
        {
            if(doorBot)
            {
                if (doorRight)
                {
                    if (doorLeft)
                    {
                        rend.sprite = roomDownLeftRight;
                    }
                    else
                    {
                        rend.sprite = roomDownRight;
                    }
                }
                else
                {
                    if (doorLeft)
                    {
                        rend.sprite = roomDownLeft;
                    }
                    else
                    {
                        rend.sprite = roomDown;
                    }
                }
            }
            else
            {
                if (doorRight)
                {
                    if(doorLeft)
                    {
                        rend.sprite = roomRightLeft;
                    }
                    else
                    {
                        rend.sprite = roomRight;
                    }
                }
                else
                {
                    rend.sprite = roomLeft;
                }
            }
        }
    }

    void ColorPicker()
    {
        if(type == 0)                  // normal room
        {
            mainColor = normalColor;
        }
        else if(type == 1)             // start room
        {
            mainColor = startColor;
        }
        else if (type == 2)            // active room color
        {
            mainColor = actualColor;
        }
        else if (type == 3)            // boss room
        {
            mainColor = bossColor;
        }
        else if (type == 4)            // treasure room
        {
            mainColor = treasureColor;
        }
        rend.color = mainColor;
    }
}
