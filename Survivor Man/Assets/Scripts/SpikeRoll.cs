using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRoll : MonoBehaviour
{
    float rotateSpeed = 120;
    float speed = 2;

    private GameObject player;
    private Rigidbody2D spikeRb;

    // Start is called before the first frame update
    void Start()
    {
        spikeRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Make spikes follow the player

        Vector2 lookDirection = (player.transform.position - transform.position).normalized;
        spikeRb.AddForce(lookDirection * speed);
        int multiplier = lookDirection.x > 0 ? -1 : 1;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed * multiplier);
    }
}
