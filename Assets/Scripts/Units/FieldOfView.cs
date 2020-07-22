using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewDistance;
    public float viewAngle;
    public LayerMask targetLayer;
    public LayerMask viewMask;
    Collider2D[] targetsInRadius;
    public List<Transform> visibleTargets;
    Transform pivot;

    private void Awake()
    {
        pivot = transform.Find("Pivot");
    }

    void FindVisiblePlayer()
    {
        targetsInRadius = Physics2D.OverlapCircleAll(transform.position, viewDistance, targetLayer);

        visibleTargets.Clear();

        for (int i = 0; i < targetsInRadius.Length; i++)
        {
            Transform target = targetsInRadius[i].transform;
            Vector2 dirTarget = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);

            if (Vector2.Angle(dirTarget, pivot.up * -1) < viewAngle / 2)
            {
                float distanceTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, dirTarget, distanceTarget, viewMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindVisiblePlayer();
    }
}
