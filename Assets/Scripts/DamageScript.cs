using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.root != transform.root && col.tag != "Ground" &&!col.isTrigger )
        {
            PlayerControl ctrl = col.GetComponent<PlayerControl>();
            if (ctrl && !ctrl.damage)
            {
                ctrl.damage = true;
                col.transform.root.GetComponentInChildren<Animator>().SetTrigger("Damage");
            }
        }
    }
}
