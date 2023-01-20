using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float sensitivity;
    Rigidbody rb;
    float mouseX;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * sensitivity;

        if (Input.GetKey(KeyCode.W))
        {
           rb.AddForce(transform.forward * speed);
        }

        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }
}
