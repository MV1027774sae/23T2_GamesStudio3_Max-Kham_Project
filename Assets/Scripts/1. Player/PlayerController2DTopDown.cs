using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2DTopDown : MonoBehaviour
{
    [SerializeField] private LayerMask interactableObject;

    [Header ("Movement")]
    [SerializeField] private float moveSpeed = 5;
    private Vector2 moveDirection;
    //dash
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashPower = 40f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 1f;

    [Header ("Primary Fire")]
    [SerializeField] private GameObject lightningBolt;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shotVelocity = 20;
    [SerializeField] private float timeBetweenFiring = 0.4f;
    private bool alreadyFired;

    [Header("IFrames")]
    [SerializeField] private Color flashColor;
    [SerializeField] private Color regularColor;
    [SerializeField] private float flashDuration = 0.7f;
    [SerializeField] private int numberOfFlashes = 4;
    [SerializeField] private Collider2D triggerCollider;
    [SerializeField] private SpriteRenderer mySprite;

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
        if (isDashing)
        {
            return;
        }
        
        ProcessInputs();

        //firing
        if (Input.GetButton("Fire1") && !alreadyFired)
        {
            PrimaryFire();
        }

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

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

        if (Input.GetButtonDown("Jump") && canDash && moveDirection != new Vector2(0,0))
        {
            StartCoroutine(Dash());
        }
    }

    private void PrimaryFire()
    {   
        Vector2 targetPosition = target.transform.localPosition;

        Vector2 aimDirection = targetPosition - rb.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        Rigidbody2D ball = Instantiate(lightningBolt, shootPoint.position, Quaternion.Euler(0, 0, angle)).GetComponent<Rigidbody2D>();
        ball.velocity = new Vector2(targetPosition.x, targetPosition.y).normalized * shotVelocity;

        alreadyFired = true;
        Invoke(nameof(ResetFire), timeBetweenFiring);
    }

    private void ResetFire()
    {
        alreadyFired = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * dashPower;
        triggerCollider.enabled = false;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.velocity = new Vector2(0, 0);
        triggerCollider.enabled = true;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
}
