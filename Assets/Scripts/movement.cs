using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
      Rigidbody rb;
     [SerializeField] float movementspeed = 6f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalinput * movementspeed, rb.velocity.y, verticalinput * movementspeed);
    }
}
