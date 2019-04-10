using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public static GameManager instance;

  public float PlayerMaxOxygen = 30f;
  public float PlayerMaxBoost = 2f;

  public int PlayerMinerals = 0;

  private void Start() {
    if (GameManager.instance == null) GameManager.instance = this;
    else Destroy(gameObject);
  }

  public void UpgradeO2Tank(float value) {
    PlayerMaxOxygen += value;
  }

  public void AwardMinerals(int value) {
    PlayerMinerals += value;
  }

  public void GameOver(){}
  public void GameStart(){}

  public void WinGame(){}
}
