using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI;
using UnityEngine;

public class TargetDetector : MonoBehaviour, IDetect
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] float detectRange = 20f;
    [SerializeField] bool showGizmos;
    private Collider[] colliders = new Collider[100];
    private int colliderFound;
    private Vector3 directionToTarget;

    public void Detect(AIData aiData)
    {
        colliderFound = Physics.OverlapSphereNonAlloc(transform.position, detectRange, colliders, targetLayer);
        if (colliderFound > 0 && colliders != null)
        {
            directionToTarget = (colliders[0].transform.position - transform.position).normalized;
            Physics.Raycast(transform.position + Vector3.up * 0.5f, directionToTarget, out RaycastHit hit, detectRange, obstacleLayer);
            if (hit.collider != null && (targetLayer & (1 << hit.collider.gameObject.layer)) != 0)
            {
                aiData.targets = new List<Transform> { hit.transform };
                aiData.currentTarget = aiData.targets[0];
                return;
            }
        }
        aiData.targets = null;
    }

    void OnDrawGizmos()
    {
        if (!showGizmos || !Application.isPlaying)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, directionToTarget);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
