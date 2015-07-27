using UnityEngine;
using System.Collections;

public class GameBoxInputMan : InputMan {

    bool right;
    bool left;
    bool up;
    bool down;
	
    void Start()
    {
        Attacks = new bool[AttacksCount];
    }

    void Update()
    {
        if (left && Horizontal < 1)
                Horizontal += 0.1f;
        if (right && Horizontal > -1)
            Horizontal -= 0.1f;
        if (up && Vertical < 1)
            Vertical += 0.1f;
        if (down && Vertical >-1)
            Vertical -= 0.1f;
            
    }

    public void PunchDown()
    {
        Attacks[0] = true;
    }
    public void PunchUp()
    {
        Attacks[0] = false;
    }
    public void kickDown()
    {
        Attacks[1] = true;
    }

    public void kickUp()
    {
        Attacks[1] = false;
    }
    public void RightDown()
    {
        right = true;
    }
    public void RightUp()
    {
        Horizontal = 0;
        right = false;
    }

    public void LeftDown()
    {
        left = true;
    }
    public void LeftUp()
    {
        Horizontal = 0;
        left = false;
    }
    public void UpDown()
    {
        up = true;
    }
    public void UpUp()
    {
        Vertical = 0;
        up = false;
    }

    public void DownDown()
    {
        down = true;
    }
    public void DownUp()
    {
        Vertical = 0;

        down = false;
    }
    
    public void SpecilDown()
    {
        Special = true;
    }
    public void SpecialUp()
    {
        Special = false;
    }
}
