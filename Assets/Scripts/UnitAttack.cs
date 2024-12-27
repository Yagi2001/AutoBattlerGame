using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    [SerializeField]
    private float _attackCooldown = 1f;
    private float _lastAttackTime = 0f;
    private Animator _animator;
    [SerializeField]
    private float _attackDamage = 10f;
    private GameObject _currentTarget;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack( GameObject target )
    {
        if (target == null)
        {
            _animator.SetBool( "isAttacking", false ); // Stop attacking if no target
            return;
        }

        _currentTarget = target;

        if (Time.time - _lastAttackTime >= _attackCooldown)
        {
            _animator.SetBool( "isAttacking", true );
            _lastAttackTime = Time.time;
            DealDamageToTarget();
        }
    }

    private void DealDamageToTarget()
    {
        if (_currentTarget != null)
        {
            UnitHealth targetHealth = _currentTarget.GetComponent<UnitHealth>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage( _attackDamage );
                if (targetHealth.IsDead())
                {
                    _currentTarget = null;
                    _animator.SetBool( "isAttacking", false );
                }
            }
        }
    }
}
