using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
public class GunData: ScriptableObject {
  public float fireRate;
  public float bulletLife;
} 
