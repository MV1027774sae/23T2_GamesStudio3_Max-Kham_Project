using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        Cursor.visible = false;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }
}
