using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    // movement
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;
    // drop sheep
    public float dropDestroyerDelay;
    private Collider myCollider;
    private Rigidbody myRigidbody;
    // spawn
    private SheepSpawner sheepSpawner;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay() {
        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true;
        runSpeed = 0;
        Destroy(gameObject, gotHayDestroyDelay);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Hay") && !hitByHay) {
            Destroy(other.gameObject);
            HitByHay();
        } else if (other.CompareTag("DropSheep")) {
            Drop();
        }
    }

    private void Drop() {
        sheepSpawner.RemoveSheepFromList(gameObject);
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyerDelay);
    }

    public void SetSpawner(SheepSpawner spawner) {
        sheepSpawner = spawner;
    }

}
