using UnityEngine;

public class Zombie : Unit
{
    private HealthSystem _health;

    public override void Init()
    {
        base.Init();

        _health = GetSystem<HealthSystem>();
        _health.OnTakedDamage += OnTakedDamage;

        AISystem ai = new(this);
        ai.Init();
        AddSystem(ai);

        OnInit?.Invoke();
    }

    private void OnTakedDamage(int damage)
    {
        if (_health.Health > 0)
        {
            Animator.SetTrigger(AnimCache.HitReactionIndex);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();
        GetSystem<AISystem>().CurrentState.Exit();
    }

    private void AttackTarget() // For animation event
    {
        GetSystem<AISystem>()?.AttackTarget();
    }
}
