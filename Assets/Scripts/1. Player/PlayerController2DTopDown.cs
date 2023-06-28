using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController2DTopDown : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField] private float moveSpeed = 5;
    private Vector2 moveDirection;
    //dash
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashPower = 30f;
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
    [SerializeField] private GameObject dashStartEffect;
    [SerializeField] private GameObject dashEndEffect;
    [SerializeField] private float flashDuration = 2f;
    [SerializeField] private int numberOfFlashes = 4;
    [SerializeField] private Collider2D triggerCollider;

    [Header("Sprites and Colours")]
    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private SpriteRenderer hatSprite;
    [SerializeField] private SpriteRenderer hatRimSprite;
    [SerializeField] private Color regularColor;
    [SerializeField] private Color hatColor;
    [SerializeField] private Color flashColor;
    [SerializeField] private Color dashColor;
    [SerializeField] private GameObject dashParticles;


    //declarations
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject target;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform shootRotate;
    private Camera mainCamera;
    private Quaternion rotation;
    private int fr = 60;

    private void Start()
    {
        Application.targetFrameRate = fr;
        rb = GetComponent<Rigidbody2D>();
        canCharge = true;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        ProcessInputs();

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        shootRotate.eulerAngles = new Vector3(0, 0, angle);

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
        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

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
        mySprite.color = new Color(dashColor.r, dashColor.g, dashColor.b, dashColor.a);
        hatSprite.color = new Color(dashColor.r, dashColor.g, dashColor.b, dashColor.a);
        hatRimSprite.color = new Color(dashColor.r, dashColor.g, dashColor.b, dashColor.a);
        Instantiate(dashStartEffect, rb.position, Quaternion.identity);
        GameObject particles = Instantiate(dashParticles, rb.position, Quaternion.identity);
        particles.transform.SetParent(gameObject.transform);
        Physics2D.IgnoreLayerCollision(3, 6, true);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.velocity = new Vector2(0, 0);
        mySprite.color = new Color(regularColor.r, regularColor.g, regularColor.b, regularColor.a);
        hatSprite.color = new Color(hatColor.r, hatColor.g, hatColor.b, hatColor.a);
        hatRimSprite.color = new Color(hatColor.r, hatColor.g, hatColor.b, hatColor.a);
        Instantiate(dashStartEffect, rb.position, Quaternion.identity);
        Destroy(particles);
        Physics2D.IgnoreLayerCollision(3, 6, false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public IEnumerator FlashCo()
    {
        Physics2D.IgnoreLayerCollision(3, 8, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            mySprite.color = new Color(flashColor.r, flashColor.g, flashColor.b, flashColor.a);
            yield return new WaitForSeconds(flashDuration / (numberOfFlashes * flashDuration));
            mySprite.color = new Color(regularColor.r, regularColor.g, regularColor.b, regularColor.a);
            yield return new WaitForSeconds(flashDuration / (numberOfFlashes * flashDuration));
        }
        Physics2D.IgnoreLayerCollision(3, 8, false);
    }
}
