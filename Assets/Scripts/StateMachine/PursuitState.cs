using UnityEngine.AI;
using UnityEngine;

public class PursuitState : UnitState
{
    private readonly NavMeshAgent _navMeshAgent;
    private readonly float _attackDistance = 1.7f;

    private Unit _target;
    private float _attackDelay;

    private bool _isAttack;

    public PursuitState(Unit unit) : base(unit)
    {
        _navMeshAgent = unit.GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        _navMeshAgent.enabled = true;
        _navMeshAgent.speed = Unit.Data.SprintSpeed;
    }

    public override void Update()
    {
        if (_target == null || !_navMeshAgent.isOnNavMesh || !_navMeshAgent.isActiveAndEnabled) return;

        float distance = Vector3.Distance(_target.transform.position, Unit.transform.position);
        if (distance <= _attackDistance)
        {
            TryAttack();
        }
        else
        {
            Pursuit(_target);
        }
    }

    public override void Exit()
    {
        Stop();
        _navMeshAgent.enabled = false;
    }

    public void Pursuit(Unit target)
    {
        _target = target;

        if (_navMeshAgent.isOnNavMesh && _navMeshAgent.isActiveAndEnabled)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_target.transform.position);

            Unit.Animator.SetBool(Unit.AnimCache.SprintIndex, true);
        }

        _isAttack = false;
    }

    private void TryAttack()
    {
        if (_isAttack) return;

        _isAttack = true;
        Stop();

        Unit.transform.LookAt(_target.transform);
        Unit.Animator.SetTrigger(Unit.AnimCache.ZombieAttackIndex);
    }

    public void AttackTarget()
    {
        _isAttack = false;

        if (_attackDelay > Time.time) return;

        float distance = Vector3.Distance(_target.transform.position, Unit.transform.position);
        if (distance <= _attackDistance)
        {
            HealthSystem targetHealth = _target.GetSystem<HealthSystem>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(Unit.transform.position, Unit.Data.AttackDamage);

                float delay = 1f;
                _attackDelay = Time.time + delay;
            }
        }
    }

    private void Stop()
    {
        if (_navMeshAgent.isOnNavMesh && _navMeshAgent.isActiveAndEnabled)
        {
            _navMeshAgent.isStopped = true;
        }

        Unit.Animator.SetBool(Unit.AnimCache.SprintIndex, false);
    }
}