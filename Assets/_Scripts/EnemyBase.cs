using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBase: ShootableBase {
  private Vector3 playerDelta;
  private bool canShoot = true;
  [SerializeField]
  private float fireRate = 1.0f;
  [SerializeField]
  private GameObject bulletPrefab;
  [SerializeField]
  private float gunSpeed = 20.0f;

  void Update() {
    var player = GameObject.FindGameObjectWithTag("Player");

    if (player != null) {
      var playerPos = player.transform.position;
      playerDelta = playerPos - transform.position;
      RaycastHit hit;

      if (Physics.Raycast(transform.position, playerDelta, out hit, 30))
      {
        if (hit.collider.gameObject.tag == "Player")
        {
          ShootTheSheriff();
        }
      }
    }

  }

  private void ShootTheSheriff() {
    if (canShoot) {
      var player = GameObject.FindGameObjectWithTag("Player");
      var playerPos = player.transform.position;
      playerDelta = playerPos - transform.position;
      playerDelta = playerDelta / playerDelta.magnitude;
      var shot = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
      var rigidBody = shot.GetComponent<Rigidbody>();

      rigidBody.velocity = playerDelta * gunSpeed;
      Destroy(shot, 5);
      StartCoroutine(EnactDelay());
    }
  }

  public IEnumerator EnactDelay() {
    canShoot = false;
    yield return new WaitForSeconds(1 / fireRate);
    canShoot = true;
  }
}
