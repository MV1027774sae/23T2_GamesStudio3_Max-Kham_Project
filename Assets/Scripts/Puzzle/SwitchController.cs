using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    private bool isCharged = false;
    public bool isPowered = false;
    [SerializeField] private float unchargeTime = 0.65f;
    [SerializeField] private GameObject switchManager;
    [SerializeField] private GameObject gameSwitch;

    [SerializeField] private GameObject chargedColor;
    [SerializeField] private GameObject unchargedColor;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        chargedColor.SetActive(false);
    }

    void Update()
    {
        if (isCharged)
        {
            isPowered = true;
        }
        else isPowered = false;
    }

    public void ChargeSwitch()
    {
        isCharged = true;
        unchargedColor.SetActive(false);
        chargedColor.SetActive(true);

        StartCoroutine(UnchargeSwitch());
    }

    private IEnumerator UnchargeSwitch()
    {
        yield return new WaitForSeconds(unchargeTime);
        isCharged = false;

        unchargedColor.SetActive(true);
        chargedColor.SetActive(false);
    }
}
