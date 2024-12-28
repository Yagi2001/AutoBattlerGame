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

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _unitAttack = GetComponent<UnitAttack>();

        if (CompareTag( "AllyUnit" ))
        {
            _targetTag = "EnemyUnit";
        }
        else if (CompareTag( "EnemyUnit" ))
        {
            _targetTag = "AllyUnit";
        }

        FindClosestUnit();
    }

    private void Update()
    {
        if (StateManager.Instance.CurrentGameState != GameState.BattlePhase || !transform.parent.CompareTag( "Tile" ))
        {
            _animator.SetBool( "isRunning", false );
            return;
        }
        if (_closestUnit == null || _closestUnit.GetComponent<UnitHealth>().IsDead())
        {
            FindClosestUnit();
        }

        if (_closestUnit != null)
        {
            float distanceToTarget = Vector3.Distance( transform.position, _closestUnit.transform.position );
            if (distanceToTarget > _attackRange)
            {
                MoveTowardUnit();
                RotateTowardsTarget();
            }
            else
            {
                _animator.SetBool( "isRunning", false );
                _unitAttack.Attack( _closestUnit );
            }
        }
        else
        {
            _animator.SetBool( "isRunning", false );
        }
    }


    private void FindClosestUnit()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag( _targetTag );
        float closestDistance = Mathf.Infinity;

        foreach (GameObject unit in units)
        {
            if (unit.transform.parent.CompareTag( "Tile" ))
            {
                float distance = Vector3.Distance( transform.position, unit.transform.position );
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _closestUnit = unit;
                }
            }
        }
    }

    private void MoveTowardUnit()
    {
        _animator.SetBool( "isRunning", true );
        if (_closestUnit != null)
        {
            Vector3 direction = (_closestUnit.transform.position - transform.position).normalized;
            transform.position += _moveSpeed * Time.deltaTime * direction;
        }
    }

    private void RotateTowardsTarget()
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
