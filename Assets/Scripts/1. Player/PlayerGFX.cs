using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFX : MonoBehaviour
{
    void Update()
    {
        if (gameObject.GetComponentInParent<Rigidbody2D>().velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (gameObject.GetComponentInParent<Rigidbody2D>().velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
