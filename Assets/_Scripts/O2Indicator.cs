using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class O2Indicator: MonoBehaviour
{
    public Text indicator;
    public PlayerController player;
    public GameManager GM;

    void Start() {
        GM = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        indicator.text = (player.remainingOxygen).ToString();    
    }
}
