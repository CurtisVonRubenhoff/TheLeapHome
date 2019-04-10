using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    public int Health = 3;
    public int Value = 10;
    private GameManager GM;
    public bool canUse = false;
    [SerializeField]
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (canUse) {
            var ticked = Input.GetButtonDown("Use");

            if (ticked) HitMineral();
        }    
    }

    void HitMineral() {
        Health -= 1;
        particles.Play();

        if (Health < 1) {
            GM.AwardMinerals(Value);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            canUse = true;
        }
    }
    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Player") {
         canUse = false;
        }
    }

}
