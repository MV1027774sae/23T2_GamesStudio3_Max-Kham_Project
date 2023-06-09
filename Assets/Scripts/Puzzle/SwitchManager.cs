using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [SerializeField] private GameObject gameSwitch1;
    [SerializeField] private GameObject gameSwitch2;
    [SerializeField] private GameObject gate;

    void Start()
    {
        
    }

    void Update()
    {
        if (gameSwitch1.GetComponent<SwitchController>().isPowered && gameSwitch2.GetComponent<SwitchController>().isPowered)
        {
            Destroy(gate);
        }
    }
}
