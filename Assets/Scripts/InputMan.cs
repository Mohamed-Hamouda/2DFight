using UnityEngine;
using System.Collections;

public abstract class InputMan : MonoBehaviour {

    public float Horizontal;
    public float Vertical;
    public int AttacksCount;
    public bool[] Attacks;
    public bool Special;

    //public InputMan(int attacksCount)
    //{
    //    AttacksCount = attacksCount;
    //}
}
