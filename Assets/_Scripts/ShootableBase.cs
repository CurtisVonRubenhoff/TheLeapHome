using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableBase : MonoBehaviour {
    [SerializeField]
    protected int myHealth = 1;

    protected virtual void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "PlayerShot") {
            CheckDamage();
            Destroy(col.gameObject);
        }
    }

    protected virtual void CheckDamage() {
        myHealth--;

        if (myHealth < 1) {
            Destroy(gameObject);
        }
    }
}
