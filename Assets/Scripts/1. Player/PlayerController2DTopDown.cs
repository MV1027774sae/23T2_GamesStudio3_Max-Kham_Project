using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2DTopDown : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField] private float moveSpeed = 6;
    private Vector2 moveDirection;
    //dash
    private bool canDash = true;
    private bool _isDashing;
    [SerializeField] private float dashPower = 30f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 1f;

    [Header ("Primary Fire")]
    [SerializeField] private GameObject primaryFireObject;
    [SerializeField] private float shotVelocity = 20;
    [SerializeField] private float timeBetweenFiring = 0.4f;
    private bool _alreadyFired;

    [Header("Secondary Fire")]
    [SerializeField] private float beamRechargeTime = 2f;
    [SerializeField] private float secondaryCharge;
    public float secondaryDamageMultiplier;
    [SerializeField] private GameObject secondaryFireObject;
    [SerializeField] private float secondaryVelocity = 8;
    private float _secondaryChargeInterval = 0.02f;
    private bool _beamFired, _canCharge;
    [SerializeField] private BeamChargeSlider beamChargeSlider;
    [SerializeField] private float chargeToFire = 30f;
    public int secondaryMana = 0;
    private int numMana = 2;
    [SerializeField] private Sprite fullMana, emptyMana;
    [SerializeField] private Image[] mana;
    private GameObject _chargeSlider;

    [Header("IFrames")]
    [SerializeField] private GameObject dashStartEffect;
    [SerializeField] private GameObject dashEndEffect;
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private int numberOfFlashes = 5;
    [SerializeField] private Collider2D triggerCollider;

    [Header("Sprites and Colours")]
    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private Color regularColor;
    [SerializeField] private Color flashColor;
    [SerializeField] private Color dashColor;
    [SerializeField] private Color dashCooldownColor;
    [SerializeField] private GameObject dashParticles;

    [Header("Audio")]
    [SerializeField] private AudioClip primaryShootSFX;
    [SerializeField] private AudioClip secondaryShootSFX;
    [SerializeField] private AudioClip secondaryChargeSFX;
    [SerializeField] private AudioClip teleportStart;
    [SerializeField] private AudioClip teleportEnd;
    [SerializeField] private AudioSource audioSource;

    //object references
    private Rigidbody2D rb;
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
        _canCharge = true;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _chargeSlider = GameObject.Find("Circle Slider");
        Physics2D.IgnoreLayerCollision(3, 8, false);
        _chargeSlider.SetActive(false);
        secondaryMana = 0;
        Physics2D.IgnoreLayerCollision(3, 8, false);
        Physics2D.IgnoreLayerCollision(3, 9, false);
        Physics2D.IgnoreLayerCollision(3, 10, false);
    }

    void Update()
    {
        if (_isDashing)
        {
            return;
        }

        ProcessInputs();

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        shootRotate.eulerAngles = new Vector3(0, 0, angle);

        //firing
        if (Input.GetButton("Fire1") && !_alreadyFired && !_beamFired)
        {
            PrimaryFire();
        }

        if(Input.GetButton("Fire2") && !_alreadyFired && _canCharge && secondaryMana > 0)
        {
            ChargeSecondaryFire();
            _chargeSlider.SetActive(true);
        }

        if (Input.GetButtonUp("Fire2"))
        {
            if(secondaryCharge >= chargeToFire && !_alreadyFired && !_beamFired)
            {
                SecondaryFire();
                secondaryDamageMultiplier = secondaryCharge / 100;
            }

            moveSpeed = 6;
            secondaryCharge = 0;
            beamChargeSlider.SetCharge(secondaryCharge);
            _chargeSlider.SetActive(false);
        }

        if (secondaryMana > numMana)
        {
            secondaryMana = numMana;
        }

        for (int i = 0; i < mana.Length; i++)
        {
            if (i < secondaryMana)
            {
                mana[i].sprite = fullMana;
            }
            else
            {
                mana[i].sprite = emptyMana;
            }

            if (i < numMana)
            {
                mana[i].enabled = true;
            }
            else
            {
                mana[i].enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isDashing)
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

        _alreadyFired = true;
        Invoke(nameof(ResetPrimaryFire), timeBetweenFiring);

        audioSource.PlayOneShot(primaryShootSFX);
    }

    private void ChargeSecondaryFire()
    {
        beamChargeSlider.SetCharge(secondaryCharge);

        moveSpeed = 4;

        secondaryCharge++;
        _canCharge = false;
        Invoke(nameof(ResetChargeTime), _secondaryChargeInterval);

        if (secondaryCharge > 100)
        {
            secondaryCharge = 100;
        }

        //audioSource.PlayOneShot(secondaryChargeSFX);
    }

    private void ResetChargeTime()
    {
        _canCharge = true;
    }

    private void SecondaryFire()
    {
        Vector2 targetPosition = target.transform.localPosition;

        Vector2 aimDirection = targetPosition - rb.position;
        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

        Rigidbody2D ball = Instantiate(secondaryFireObject, shootPoint.position, Quaternion.Euler(0, 0, angle)).GetComponent<Rigidbody2D>();
        ball.velocity = new Vector2(targetPosition.x, targetPosition.y).normalized * secondaryVelocity;
        
        secondaryMana--;
        _alreadyFired = true;
        _beamFired = true;
        Invoke(nameof(ResetSecondaryFire), beamRechargeTime);

        audioSource.PlayOneShot(secondaryShootSFX);
    }

    private void ResetPrimaryFire()
    {
        _alreadyFired = false;
    }

    private void ResetSecondaryFire()
    {
        canDash = true;
        _alreadyFired = false;
        _beamFired = false;
        secondaryDamageMultiplier = 0;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        _isDashing = true;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * dashPower;
        mySprite.color = new Color(dashColor.r, dashColor.g, dashColor.b, dashColor.a);

        Instantiate(dashStartEffect, rb.position, Quaternion.identity);
        audioSource.PlayOneShot(teleportStart);
        GameObject particles = Instantiate(dashParticles, rb.position, Quaternion.identity);
        particles.transform.SetParent(gameObject.transform);

        Physics2D.IgnoreLayerCollision(3, 8, true);
        Physics2D.IgnoreLayerCollision(3, 9, true);
        Physics2D.IgnoreLayerCollision(3, 10, true);
        Physics2D.IgnoreLayerCollision(3, 12, true);
        yield return new WaitForSeconds(dashTime);

        _isDashing = false;
        rb.velocity = new Vector2(0, 0);
        mySprite.color = new Color(dashCooldownColor.r, dashCooldownColor.g, dashCooldownColor.b, dashCooldownColor.a);

        Instantiate(dashStartEffect, rb.position, Quaternion.identity);
        audioSource.PlayOneShot(teleportEnd);
        Destroy(particles);

        Physics2D.IgnoreLayerCollision(3, 8, false);
        Physics2D.IgnoreLayerCollision(3, 9, false);
        Physics2D.IgnoreLayerCollision(3, 10, false);
        Physics2D.IgnoreLayerCollision(3, 12, false);
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        mySprite.color = new Color(regularColor.r, regularColor.g, regularColor.b, regularColor.a);
    }

    public IEnumerator FlashCo()
    {
        Physics2D.IgnoreLayerCollision(3, 8, true);
        Physics2D.IgnoreLayerCollision(3, 10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            mySprite.color = new Color(flashColor.r, flashColor.g, flashColor.b, flashColor.a);
            yield return new WaitForSeconds(flashDuration / (numberOfFlashes * flashDuration));

            mySprite.color = new Color(regularColor.r, regularColor.g, regularColor.b, regularColor.a);
            yield return new WaitForSeconds(flashDuration / (numberOfFlashes * flashDuration));
        }
        Physics2D.IgnoreLayerCollision(3, 8, false);
        Physics2D.IgnoreLayerCollision(3, 10, false);
    }

    public void AddMana()
    {
        secondaryMana++;
    }
}
