using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rb;
    float mouseX;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X");

        if (Input.GetKey(KeyCode.W))
        {
           rb.AddForce(transform.forward * speed);
        }

        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }
}
