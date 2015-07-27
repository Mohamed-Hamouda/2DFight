using UnityEngine;
using System.Collections;

public class PCInputMan : InputMan {
    public int PlayerNum;
    
    void Start()
    {
        Attacks = new bool[AttacksCount];
    }
	void Update () {
	    Horizontal = Input.GetAxis("Horizontal" + PlayerNum);
        Vertical = Input.GetAxis("Vertical" + PlayerNum);

        for (int i = 0; i < AttacksCount; i++)
        {
            Attacks[i] = Input.GetButtonDown("Attack" + (i+1) + PlayerNum);
            //print("Attack" + (i + 1) + PlayerNum);
        }
        Special = Attacks[0] && Attacks[1];
	}

    //public PCInputMan(int playerNum, int attacksCount)
    //    : base(attacksCount)
    //{
    //    PlayerNum = playerNum;
    //}
}
