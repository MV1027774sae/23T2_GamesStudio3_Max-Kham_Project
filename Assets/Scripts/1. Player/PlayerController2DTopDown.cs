using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2DTopDown : MonoBehaviour
{
    [SerializeField] private LayerMask interactableObject;

    //movement variables
    [SerializeField] private float moveSpeed = 5;
    private Vector2 moveDirection;

    //lightningbolt
    [SerializeField] private GameObject lightningBolt;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shotVelocity = 3;
    [SerializeField] private float timeBetweenFiring = 1;
    private bool alreadyFired;

    //declarations
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject target;
    private int fr = 60;

    private void Start()
    {
        Application.targetFrameRate = fr;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInputs();

        //firing
        if (Input.GetButtonDown("Fire1") && !alreadyFired)
        {
            LightningBolt();
        }

        //interaction
        if (Input.GetButtonDown("Jump"))
        {
            Interact();
        }
    }

    private void FixedUpdate()
    {
        Move();     
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Interact()
    {
        Collider2D[] interact = Physics2D.OverlapBoxAll(transform.position, new Vector2(1f, 1f), 0f, interactableObject);
        
        foreach(Collider2D interactable in interact)
        {
            interactable.GetComponent<SwitchController>().ChargeSwitch();
        }
    }

    private void LightningBolt()
    {   
        Vector2 targetPosition = target.transform.localPosition;

        Rigidbody2D ball = Instantiate(lightningBolt, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        ball.velocity = new Vector2(targetPosition.x, targetPosition.y).normalized * shotVelocity;

        alreadyFired = true;
        Invoke(nameof(ResetFire), timeBetweenFiring);
    }

    private void ResetFire()
    {
        alreadyFired = false;
    }
}
