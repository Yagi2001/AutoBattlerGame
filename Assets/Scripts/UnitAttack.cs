using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    [SerializeField]
    private float _attackCooldown = 1f;
    private float _lastAttackTime = 0f;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (Time.time - _lastAttackTime >= _attackCooldown)
        {
            _animator.SetBool( "isAttacking",true );
            _lastAttackTime = Time.time;
        }
    }
}
