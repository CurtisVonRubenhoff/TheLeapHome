using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBase : MonoBehaviour
{
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;
    }

    void GetItem() {
        GM.UpgradeO2Tank(40f);
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            GetItem();
            Destroy(gameObject);
        }
    }
}
