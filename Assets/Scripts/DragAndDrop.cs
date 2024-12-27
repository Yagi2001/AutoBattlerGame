using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _isDragging = false;
    private Vector3 _offset;
    private Vector3 _originalPosition;
    [SerializeField]
    private LayerMask _tileLayer;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (_isDragging)
        {
            DragObject();
        }
    }

    void OnMouseDown()
    {
        _isDragging = true;
        _originalPosition = transform.position;
        _offset = transform.position - GetMouseWorldPosition();
        if (transform.parent != null)
        {
            transform.SetParent( null );
        }
    }

    void OnMouseUp()
    {
        _isDragging = false;
        bool snappedToTile = SnapToTile();
        if (!snappedToTile)
        {
            transform.position = _originalPosition;
        }
    }

    void DragObject()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        transform.position = new Vector3( mouseWorldPosition.x + _offset.x, transform.position.y, mouseWorldPosition.z + _offset.z );
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = _mainCamera.ScreenPointToRay( Input.mousePosition );
        if (Physics.Raycast( ray, out RaycastHit hit ))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    bool SnapToTile()
    {
        Ray ray = _mainCamera.ScreenPointToRay( Input.mousePosition );
        if (Physics.Raycast( ray, out RaycastHit hit, Mathf.Infinity, _tileLayer ))
        {
            if (hit.collider.CompareTag( "Tile" ))
            {
                Transform tileTransform = hit.collider.transform;
                if (tileTransform.childCount == 0)
                {
                    transform.position = new Vector3(
                        tileTransform.position.x,
                        transform.position.y,
                        tileTransform.position.z
                    );
                    transform.SetParent( tileTransform );
                    return true;
                }
            }
        }
        return false;
    }
}
