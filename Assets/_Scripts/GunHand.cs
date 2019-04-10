using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHand : MonoBehaviour {
    private bool canShoot = true;
    [SerializeField]
    private float fireRate = 1.0f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float gunSpeed = 20.0f;

    [SerializeField]
    private PlayerController player;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) {
            switch (player.myGun) {
                case PlayerGuns.PISTOL:
                    TryShoot();
                    break;
                case PlayerGuns.SPREAD:
                    SpreadShot();
                    break;
                default:
                    break;
            }
        }
    }

    private void TryShoot() {
        fireRate = 2.5f;
        if (canShoot) {
            var shot = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            var rigidBody = shot.GetComponent<Rigidbody>();

            rigidBody.velocity = transform.right * gunSpeed;
            Destroy(shot, 5);
            StartCoroutine(EnactDelay());
        }
    }

    private  void SpreadShot() {
        fireRate = .7f;
        var eulers = transform.rotation.eulerAngles;
        if (canShoot) {
            var upAngle = Quaternion.Euler(new Vector3(eulers.x, eulers.y, eulers.z - 15));
            var downAngle = Quaternion.Euler(new Vector3(eulers.x, eulers.y, eulers.z + 15)); 
            

            var shot1 = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            var rigidBody1 = shot1.GetComponent<Rigidbody>();
            var shot2 = GameObject.Instantiate(bulletPrefab, transform.position, upAngle) as GameObject;
            var rigidBody2 = shot2.GetComponent<Rigidbody>();
            var shot3 = GameObject.Instantiate(bulletPrefab, transform.position, downAngle) as GameObject;
            var rigidBody3 = shot3.GetComponent<Rigidbody>();

            rigidBody1.velocity = shot1.transform.right * gunSpeed;
            Destroy(shot1, .5f);
            rigidBody2.velocity = shot2.transform.right * gunSpeed;
            Destroy(shot2, .5f);
            rigidBody3.velocity = shot3.transform.right * gunSpeed;
            Destroy(shot3, .5f);
            StartCoroutine(EnactDelay());
        }
    }

  public IEnumerator EnactDelay() {
    canShoot = false;
    yield return new WaitForSeconds(1 / fireRate);
    canShoot = true;
  }
}
