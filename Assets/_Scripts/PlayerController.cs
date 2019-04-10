using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerGuns {
    PISTOL,
    SPREAD
}

public class PlayerController : MonoBehaviour {
    public GameManager GM;
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private Transform modelTransform;
    [SerializeField]
    private Transform targetTransform;
    
    [SerializeField]
    private int playerSpeed = 5;
    [SerializeField]
    private int boostPower = 20;
    private float boostFuel = 2f;
    public float remainingOxygen = 1f;
    [SerializeField]
    private float oxygenDecayRate = 5f;
    private bool boostDepleted = false;
    [SerializeField]
    private float gravity = -10f;
    private CharacterController myController;
    [SerializeField]
    Image BoostIndicator;
    [SerializeField]
    Image OxygenIndicator;
    private bool chargingO2 = true;

    public int playerHealth = 10;

    public PlayerGuns myGun = PlayerGuns.PISTOL;


    // Start is called before the first frame update
    void Start() {
        if (myController == null) myController = gameObject.GetComponent<CharacterController>();
        GM = GameManager.instance;
        boostFuel = GM.PlayerMaxBoost;
    }

    // Update is called once per frame
    void Update() {
        var input = Input.GetAxis("Horizontal");
        var boost = Input.GetButton("Jump");

        HandlePlayerMovement(input, boost);
        CheckBooster(boost);
        UpdateO2();

        HandleGunSwitch();
    }

    private void HandleGunSwitch() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            myGun = PlayerGuns.PISTOL;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            myGun = PlayerGuns.SPREAD;
        }
    }

    private void HandlePlayerMovement(float input, bool booster) {
        if (input < 0.01f && input > -0.01f) {
            input = 0f;
        }

        playerAnim.SetFloat("Move", myController.isGrounded ? input : 0f);

        var mousePosition = Input.mousePosition;
        var screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        var boostValue = (booster && !boostDepleted) ? boostPower : 0f;
        var moveDirection = new Vector3(input * playerSpeed, boostValue, 0f);

        var facingLeft = modelTransform.rotation.eulerAngles.y == 270f;

        if (facingLeft) {
            var shouldTurn = mousePosition.x > screenPosition.x;
            if (shouldTurn) {
                modelTransform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
        } else {
            var shouldTurn = mousePosition.x < screenPosition.x;
            if (shouldTurn)
            {
                modelTransform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
        }

        moveDirection.y = moveDirection.y + (gravity * Time.deltaTime);
        myController.Move(moveDirection * Time.deltaTime);
    }

    private void CheckBooster(bool booster) {
        if (boostDepleted) {
            var boostValue = (!booster && myController.isGrounded) ? 1 : 0;

            boostFuel += (Time.deltaTime * boostValue);
        } else {
            var boostValue = booster ? -1 : 1;

            boostFuel += (Time.deltaTime * boostValue);
        }

        if (boostFuel < .01f) {
            boostFuel = 0;
            boostDepleted = true;
        }

        if (boostFuel > GM.PlayerMaxBoost) {
            boostFuel = GM.PlayerMaxBoost;
            boostDepleted = false;
        }

        var curr = BoostIndicator.transform.localScale;
        var width = (boostFuel/GM.PlayerMaxBoost) * 2;
        BoostIndicator.transform.localScale = new Vector3(width, curr.y, curr.z);
    }

    private void UpdateO2() {
        if (chargingO2) {
            remainingOxygen += Time.deltaTime * GM.PlayerMaxOxygen;
        } else {
            remainingOxygen -= Time.deltaTime;
        }

        if (remainingOxygen > GM.PlayerMaxOxygen) remainingOxygen = GM.PlayerMaxOxygen;
        if (remainingOxygen < 0.01f) remainingOxygen = 0f;

        var cur = OxygenIndicator.transform.localScale;
        var scaled = remainingOxygen / GM.PlayerMaxOxygen;

        OxygenIndicator.transform.localScale = new Vector3(cur.x, scaled, cur.z);

        if (remainingOxygen == 0f) Destroy(gameObject);
    }

    void GetHurt() {
        playerHealth -= 1;

        if (playerHealth < 1) {
            GM.GameOver();
            Destroy(this);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "SpaceShip") {
            chargingO2 = true;
        }
        if (col.gameObject.tag == "EnemyShot") {
            Destroy(col.gameObject);
            GetHurt();
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "SpaceShip") {
            chargingO2 = false;
        }
    }
}
