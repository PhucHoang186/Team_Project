using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

namespace AI
{
    public class SeekTargetBehaviour : MonoBehaviour, ISteering
    {
        [SerializeField] float targetReachThreshold = 0.5f;
        [SerializeField] bool showGizmos;
        [SerializeField] bool reachTarget = true;
        private float[] interestsTempList;
        private Vector3 targetPositionCached;

        public (float[], float[]) GetSteering(float[] dangers, float[] interests, AIData aIData)
        {
            if (reachTarget)
            {

                if (aIData.targets == null || aIData.targets.Count <= 0)
                {
                    aIData.currentTarget = null;
                    return (dangers, interests);
                }
                else
                {
                    reachTarget = false;
                    aIData.currentTarget = aIData.targets[0];
                }

            }
            if (aIData.currentTarget != null && aIData.targets != null && aIData.targets.Contains(aIData.currentTarget))
            {
                targetPositionCached = aIData.currentTarget.transform.position;
            }

            if (Vector3.Distance(transform.position, targetPositionCached) <= targetReachThreshold)
            {
                aIData.currentTarget = null;
                reachTarget = true;
                return (dangers, interests);
            }
            Vector3 directionToTarget = (targetPositionCached - transform.position).normalized;

            var eightNormalizedDirectionsList = Direction3D.eightNormalizedDirectionsList;
            for (int i = 0; i < eightNormalizedDirectionsList.Count; i++)
            {
                var result = Vector3.Dot(directionToTarget, eightNormalizedDirectionsList[i]);
                if (result > 0)
                {
                    if (interests[i] < result)
                    {
                        interests[i] = result;
                    }
                }
            }
            interestsTempList = interests;
            return (dangers, interests);
        }


        void OnDrawGizmos()
        {
            if (!showGizmos || !Application.isPlaying || interestsTempList == null)
                return;
            Gizmos.color = Color.red;
            for (int i = 0; i < interestsTempList.Length; i++)
            {
                Gizmos.DrawRay(transform.position, interestsTempList[i] * Direction3D.eightNormalizedDirectionsList[i]);
            }
            if (!reachTarget)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(targetPositionCached, 2f);
            }
        }
    }
}

 public static class Direction3D
    {
        public enum DirectionType
        {
            Forward,
            Back,
            Right,
            Left,
            Up,
            Down,
        }

        public static List<Vector3Int> eightDirectionsList = new List<Vector3Int>
        {
        Vector3Int.forward,
        Vector3Int.back,
        Vector3Int.right,
        Vector3Int.left,
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.forward + Vector3Int.right, // top right
        Vector3Int.forward + Vector3Int.left, // top left
        Vector3Int.back + Vector3Int.right, // bottom right
        Vector3Int.back + Vector3Int.left, // bottom right
        };

        public static List<Vector3> eightNormalizedDirectionsList = new()
        {
            new Vector3(0, 0,1).normalized,
            new Vector3(0, 0,-1).normalized,
            new Vector3(1, 0, 0).normalized,
            new Vector3(-1, 0, 0).normalized,
            new Vector3(1, 0,1).normalized,
            new Vector3(-1, 0,1).normalized,
            new Vector3(1, 0,-1).normalized,
            new Vector3(-1, 0,-1).normalized,
        };

        public static Vector3Int GetRandomCardinalDirection(bool isThreeDemension = true)
        {
            var length = isThreeDemension ? eightDirectionsList.Count : eightDirectionsList.Count - 6;
            return eightDirectionsList[Random.Range(0, length)];
        }

        public static List<Vector3Int> GetCardinalDirectionsListIgnoreY()
        {
            return eightDirectionsList.GetRange(0, 4);
        }

        public static List<Vector3Int> GetDiagonalDirectionList()
        {
            return eightDirectionsList.GetRange(6, 4);
        }

        public static List<Vector3> GetNormalizeEightDirection()
        {
            List<Vector3> normalizedDirection = new();
            for (int i = 0; i < normalizedDirection.Count; i++)
            {
                normalizedDirection[i] = ((Vector3)eightDirectionsList[i]).normalized;
            }
            return normalizedDirection;
        }
    }