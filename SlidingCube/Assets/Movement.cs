using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float sensitivity;
    Rigidbody rb;
    float mouseX;
    bool move = false;

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
            move = true;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, mouseX, transform.eulerAngles.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void FixedUpdate()
    {
        if(move)
        {
            rb.AddForce(transform.forward * speed);
            move = false;
        }
    }
}
