using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamChargeSlider : MonoBehaviour
{
    [SerializeField] Slider chargeSlider;
    private GameObject player;

    private void Start()
    {
        chargeSlider = GetComponent<Slider>();
        player = GameObject.Find("Player");
    }

    public void SetSliderMax(float maxValue)
    {
        chargeSlider.maxValue = maxValue;
        chargeSlider.value = 0;
    }

    public void SetCharge(float charge)
    {
        chargeSlider.value = charge;
    }
}