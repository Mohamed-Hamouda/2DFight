using UnityEngine;
using System.Collections;

public class EventHandler : MonoBehaviour {
    PlayerControl pl;
	// Use this for initialization
	void Start () {
        pl = transform.root.GetComponent<PlayerControl>();
	}

    public void ThrowProjectile()
    {
        pl.specialAttack = true;
    }
}
