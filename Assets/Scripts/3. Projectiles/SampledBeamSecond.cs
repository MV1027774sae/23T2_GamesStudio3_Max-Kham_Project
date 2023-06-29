using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampledBeamSecond : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 300f;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    private Transform m_transform;
    [SerializeField] private LayerMask simpleCollider;

    [SerializeField] private int damage = 1;
    private PlayerController2DTopDown playerController2DTopDown;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        playerController2DTopDown = GameObject.Find("Player").GetComponent<PlayerController2DTopDown>();
    }

    public void ShootLaser()
    {
        damage = (damage + playerController2DTopDown.bonusBeamDamage);

        if (Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right); 
            if (_hit.collider.tag == "Enemy")
            {
                _hit.collider.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(damage);
            }

            if (_hit.collider.tag == "DestroyableObject")
            {
                _hit.collider.gameObject.GetComponent<ObjectHealthManager>().DamageObject(damage);
            }

            if (_hit.collider.tag == "Simple Collider")
            {
                Draw2DRay(laserFirePoint.position, _hit.point);
            }
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);
        }
    }

    private void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }
}