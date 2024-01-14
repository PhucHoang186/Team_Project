using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIEnemy : MonoBehaviour
    {
        [SerializeField] NavMeshAgent agent;
        [SerializeField] Transform target;
        Vector3[] paths;

        public Vector3 GetMovementDirection()
        {
            if (!agent.hasPath)
                return Vector3.zero;
            paths = agent.path.corners;
            return (paths[1] - transform.position).normalized;
        }

        [Button]
        public void SetTarget()
        {
            agent.SetDestination(target.position);
            agent.updatePosition = false;
            agent.isStopped = true;
        }

        [Button]
        public void ClearTarget()
        {
            // agent.isStopped = false;
        }
    }
}
