using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    public List<Transform> sheepEndPoints = new List<Transform>();

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Sheep")) {
            Sheep sheep = other.gameObject.GetComponent<Sheep>();
            sheep.ChangeDirection(sheepEndPoints);
        }
    }
}
