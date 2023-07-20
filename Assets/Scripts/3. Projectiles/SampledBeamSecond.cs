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

    [SerializeField] private float damage = 6;
    private PlayerController2DTopDown playerController2DTopDown;

    // DEBUG AG
    Vector2 hitPositionForGizmoDrawing;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        playerController2DTopDown = GameObject.Find("Player").GetComponent<PlayerController2DTopDown>();
    }

    public void ShootLaser()
    {
        float beamDamage = (damage * playerController2DTopDown.secondaryDamageMultiplier) * Time.deltaTime;
        Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);

        if (Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right);
            hitPositionForGizmoDrawing = _hit.point;

            if (_hit.collider.tag == "Enemy")
            {
                _hit.collider.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(beamDamage);
            }

            if (_hit.collider.tag == "DestroyableObject")
            {
                _hit.collider.gameObject.GetComponent<ObjectHealthManager>().DamageObject(beamDamage);
            }

            if (_hit.collider.tag == "Enemy Bullet")
            {
                _hit.collider.gameObject.GetComponent<Bullet>().DestroySelf();
            }

            if (_hit.collider.tag == "StrongObject")
            {
                _hit.collider.gameObject.GetComponent<ObjectHealthManager>().DamageObject(beamDamage);
                Draw2DRay(laserFirePoint.position, _hit.point);
            }

            if (_hit.collider.tag == "Simple Collider")
            {
                Draw2DRay(laserFirePoint.position, _hit.point);
            }

            else
            {
                Debug.LogWarning("BEAM DID NOT HIT ANYTHING");
            }
        }
    }
    //TODO 20/07/2023: Fix the beam attack
    //2 raycasts, the first being a normal raycast as i have it but only detecting the wall, this would set the visuals of the line renderer start and end.
    //The second is a raycastall that raycasts for a number of units in that direction, detecting hits on all the creatures it hits.
    //Both woud be cast when using the beam.


    private void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
        // AG solution to avoid the "erratic beam direction" issue... just fire it 100 units forward!
        //m_lineRenderer.SetPosition(1, (startPos - (Vector2)transform.position) * 100);
    }

    private void OnDrawGizmos()
    {
        // Draw green wire sphere at beam origin
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f);

        // Draw green wire sphere at beam end
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPositionForGizmoDrawing, 2f);

        // Draw yellow line along beam length
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, hitPositionForGizmoDrawing);
    }
}