using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] ForceReceiver forceReceiver;
    [SerializeField] Transform target;
    [SerializeField] CharacterAnimationController animator;

    public void MoveToTarget(Transform target)
    {
        navAgent.destination = target.position;
    }
    public void StopAllMovement()
    {
        navAgent.isStopped = true;
    }
}
