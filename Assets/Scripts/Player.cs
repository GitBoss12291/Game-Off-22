using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float walkLimiter = 1.7f;
    private float moveSpeed;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;

    //Follow Cam
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private float camDist;

    //Gun
    [SerializeField]
    private Transform gun;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical"); 
        moveSpeed = walkSpeed;
        gun.position = transform.position;
    }

    void FixedUpdate()
    {
        //Check if horizontal movement or vertical movement is not zero
        if (horizontal != 0 || vertical != 0)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
            if (horizontal != 0 && vertical != 0)
            {
                rb.velocity = new Vector2(horizontal * moveSpeed * walkLimiter, vertical * moveSpeed * walkLimiter);
            }
        } else
        {
            rb.velocity = Vector2.zero;
        }
        
        cam.position = new Vector3(transform.position.x, transform.position.y, camDist);
    }
}