using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    // movement
    private Vector3 direction;
    private bool directionChanged = false;
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
        direction = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * runSpeed * Time.deltaTime);
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

    public void ChangeDirection(List<Transform> sheepEndPoints) {
        // change direction only once
        if (directionChanged) return;
        // choose one random end point to direct to
        Vector3 randomPoint = sheepEndPoints[Random.Range(0, sheepEndPoints.Count)].position;
        // find the new walking direction
        direction = transform.position - randomPoint;
        direction.Normalize();
        // activate flag
        directionChanged = true;
    }

}
