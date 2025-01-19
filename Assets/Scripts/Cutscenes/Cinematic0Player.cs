using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cinematic0Player : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _waitTimeAtWaypoint = 2f;

    private int _currentWaypointIndex = 0;
    private bool _isMoving = true;

    private static readonly int _VelocityXHash = Animator.StringToHash("VelocityX");
    private static readonly int _VelocityZHash = Animator.StringToHash("VelocityZ");
    
    private void Update()
    {
        if (_isMoving && !_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.2f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
        UpdateAnimatorParameters();
    }

    private IEnumerator WaitAtWaypoint()
    {
        _isMoving = false;
        _navMeshAgent.isStopped = true;

        yield return new WaitForSeconds(_waitTimeAtWaypoint);

        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
    }

    public void MoveToNextWaypoint()
    {
        _isMoving = true;
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_waypoints[_currentWaypointIndex].position);
    }
    public void WorkSometing()
    {
        bool currentState = _animator.GetBool("isWorking");
        _animator.SetBool("isWorking", !currentState);
    }

    private void UpdateAnimatorParameters()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(_navMeshAgent.velocity);

        float velocityX = localVelocity.x / _navMeshAgent.speed;
        float velocityZ = localVelocity.z / _navMeshAgent.speed;

        velocityX = Mathf.Clamp(velocityX, -0.5f, 0.5f);
        velocityZ = Mathf.Clamp(velocityZ, 0f, 0.5f);

        _animator.SetFloat(_VelocityXHash, velocityX, 0.1f, Time.deltaTime);
        _animator.SetFloat(_VelocityZHash, velocityZ, 0.1f, Time.deltaTime);
    }
}