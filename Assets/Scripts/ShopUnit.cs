using UnityEngine;

public class ShopUnit : MonoBehaviour
{
    [SerializeField] private int _cost;
    [SerializeField] private GameObject _unit;
    private GoldManager _goldManager;
    private void Start()
    {
        _goldManager = FindObjectOfType<GoldManager>();
    }
    public void PurchaseUnit()
    {
        PlaceUnitOnIdle();
        _goldManager.DecreaseGold( _cost );
    }

    private void PlaceUnitOnIdle()
    {
        GameObject[] idleSpots = GameObject.FindGameObjectsWithTag( "IdleSpot" );

        foreach (GameObject idleSpot in idleSpots)
        {
            if (idleSpot.transform.childCount <= 1)
            {
                GameObject newUnit = Instantiate( _unit, idleSpot.transform.position, Quaternion.identity );
                newUnit.transform.SetParent( idleSpot.transform );
                newUnit.transform.localPosition = _unit.transform.position;
                newUnit.transform.localScale = _unit.transform.localScale;
                Destroy( gameObject );
                break;
            }
        }
    }
}
