using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {
  // speed is the rate at which the object will rotate
  public float speed;
  public Transform target;

  void FixedUpdate() {
    // get screen positions of cursor and player character
    var playerPos = Camera.main.WorldToScreenPoint(transform.position);
    var mousePos = Input.mousePosition;
    // find difference
    var direction = mousePos - playerPos;
    // calculate angle
    var thing = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // apply rotation
    Quaternion targetRotation = Quaternion.Euler(0, 0, thing);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

    // move aim target gameObject
    target.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
  }
}
