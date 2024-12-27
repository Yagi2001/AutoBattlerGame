using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private GameObject _closestUnit;
    private Animator _animator;
    [SerializeField]
    private float _attackRange = 2f;
    private string _targetTag;
    [SerializeField]
    private float _moveSpeed = 5f;
    private float _rotationSpeed = 5f;
    private UnitAttack _unitAttack;

    void Start()
    {
        _animator = GetComponent<Animator>();
        if (CompareTag( "AllyUnit" ))
        {
            _targetTag = "EnemyUnit";
        }
        else if (CompareTag( "EnemyUnit" ))
        {
            _targetTag = "AllyUnit";
        }

        FindClosestUnit();
        _unitAttack = GetComponent<UnitAttack>();
    }

    void Update()
    {
        if (_closestUnit != null)
        {
            if (Vector3.Distance( transform.position, _closestUnit.transform.position ) > _attackRange)
            {
                MoveTowardUnit();
                RotateTowardsTarget();
            }
            if (Vector3.Distance( transform.position, _closestUnit.transform.position ) <= _attackRange)
            {
                _animator.SetBool( "isRunning", false );
                _unitAttack.Attack();
            }
        }
    }

    void FindClosestUnit()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag( _targetTag );
        float closestDistance = Mathf.Infinity;
        foreach (GameObject unit in units)
        {
            float distance = Vector3.Distance( transform.position, unit.transform.position );
            if (distance < closestDistance)
            {
                closestDistance = distance;
                _closestUnit = unit;
            }
        }
    }

    void MoveTowardUnit()
    {
        _animator.SetBool( "isRunning", true );
        if (_closestUnit != null)
        {
            Vector3 direction = (_closestUnit.transform.position - transform.position).normalized;
            transform.position += _moveSpeed * Time.deltaTime * direction;
        }
    }

    void RotateTowardsTarget()
    {
        if (_closestUnit != null)
        {
            Vector3 direction = (_closestUnit.transform.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation( new Vector3( direction.x, 0, direction.z ) );
                transform.rotation = Quaternion.Slerp( transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed );
            }
        }
    }
}
