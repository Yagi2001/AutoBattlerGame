using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 100f;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage( float damage )
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public bool IsDead()
    {
        return _currentHealth <= 0;
    }

    private void Die()
    {
        gameObject.SetActive( false );
    }
}
