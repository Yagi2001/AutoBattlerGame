using UnityEngine;

public class ShopUnit : MonoBehaviour
{
    [SerializeField] private int _cost;
    [SerializeField] private GameObject _unit;
    public void PurchaseUnit()
    {
        Debug.Log( "Bought this Unit" );
    }
}
