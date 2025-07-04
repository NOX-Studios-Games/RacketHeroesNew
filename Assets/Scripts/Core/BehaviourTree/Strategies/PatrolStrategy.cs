using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Core.BehaviourTree.Strategies
{
    public class PatrolStrategy : IStrategy
    {
        private readonly Transform _entity;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly List<Transform> _patrolPoints;
        private readonly float _patrolSpeed;
        private int _currentIndex;
        private bool _isPathCalculated;

        public PatrolStrategy(Transform entity, NavMeshAgent navMeshAgent, List<Transform> patrolPoints, float patrolSpeed = 2f)
        {
            _entity = entity;
            _navMeshAgent = navMeshAgent;
            _patrolPoints = patrolPoints;
            _patrolSpeed = patrolSpeed;
        }

        private void SetTarget()
        {
            var target = _patrolPoints[_currentIndex];
            _navMeshAgent.SetDestination(target.position);
            _entity.LookAt(target);
        }
        
        public Node.Status Process()
        {
            if(_currentIndex == _patrolPoints.Count) return Node.Status.Success;

            SetTarget();

            if (_isPathCalculated && _navMeshAgent.remainingDistance < 0.1f)
            {
                _currentIndex++;
                _isPathCalculated = false;
            }

            if (_navMeshAgent.pathPending) _isPathCalculated = true;
            
            return Node.Status.Running;
        }

        public void Reset() => _currentIndex = 0;
    }
}