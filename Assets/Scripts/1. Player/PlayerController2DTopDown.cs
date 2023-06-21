using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2DTopDown : MonoBehaviour
{
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
    [SerializeField] private GameObject primaryFireObject;
    [SerializeField] private float shotVelocity = 20;
    [SerializeField] private float timeBetweenFiring = 0.4f;
    private bool alreadyFired;

    [Header("Secondary Fire")]
    [SerializeField] private GameObject secondaryFireObject;
    [SerializeField] private float beamVelocity = 40;
    [SerializeField] private float beamRechargeTime = 2f;
    private bool beamFired, canCharge;
    [SerializeField] private float secondaryCharge;
    private float secondaryChargeInterval = 0.02f;

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
    [SerializeField] private Transform shootPoint;
    private Quaternion rotation;
    private int fr = 60;

    private void Start()
    {
        Application.targetFrameRate = fr;
        rb = GetComponent<Rigidbody2D>();
        canCharge = true;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        ProcessInputs();

        //firing
        if (Input.GetButton("Fire1") && !alreadyFired && !beamFired)
        {
            PrimaryFire();
        }

        if(Input.GetButton("Fire2") && !alreadyFired && canCharge)
        {
            ChargeSecondaryFire();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            if(secondaryCharge >= 50 && !alreadyFired && !beamFired)
            {
                SecondaryFire();
            }
            secondaryCharge = 0;
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

        Rigidbody2D ball = Instantiate(primaryFireObject, shootPoint.position, Quaternion.Euler(0, 0, angle)).GetComponent<Rigidbody2D>();
        ball.velocity = new Vector2(targetPosition.x, targetPosition.y).normalized * shotVelocity;

        alreadyFired = true;
        Invoke(nameof(ResetPrimaryFire), timeBetweenFiring);
    }

    private void ChargeSecondaryFire()
    {
        secondaryCharge++;
        canCharge = false;
        Invoke(nameof(ResetChargeTime), secondaryChargeInterval);

        if(secondaryCharge > 100)
        {
            secondaryCharge = 100;
        }
    }

    private void ResetChargeTime()
    {
        canCharge = true;
    }

    private void SecondaryFire()
    {
        Vector2 targetPosition = target.transform.localPosition;

        Vector2 aimDirection = targetPosition - rb.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        Rigidbody2D ball = Instantiate(secondaryFireObject, shootPoint.position, Quaternion.Euler(0, 0, angle)).GetComponent<Rigidbody2D>();
        ball.velocity = new Vector2(targetPosition.x, targetPosition.y).normalized * beamVelocity;

        alreadyFired = true;
        beamFired = true;
        Invoke(nameof(ResetSecondaryFire), beamRechargeTime);
    }

    private void ResetPrimaryFire()
    {
        alreadyFired = false;
    }

    private void ResetSecondaryFire()
    {
        alreadyFired = false;
        beamFired = false;
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
