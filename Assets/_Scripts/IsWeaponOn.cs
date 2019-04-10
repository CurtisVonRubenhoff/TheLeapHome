using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsWeaponOn : MonoBehaviour
{
    public Image me;
    public PlayerController player;
    public PlayerGuns whoAmI;

    // Update is called once per frame
    void Update()
    {
        if (player.myGun == whoAmI) me.color = Color.red;
        else me.color = Color.white;
    }
}
