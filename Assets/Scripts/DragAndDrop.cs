using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _isDragging = false;
    private Vector3 _offset;
    private Vector3 _originalPosition;
    [SerializeField]
    private LayerMask _tileLayer;
    private Vector3 _initialLocalPosition;

    private void Start()
    {
        _initialLocalPosition = transform.localPosition;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_isDragging)
        {
            DragObject();
        }
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        _originalPosition = transform.position;
        _offset = transform.position - GetMouseWorldPosition();
        if (transform.parent != null)
        {
            transform.SetParent( null );
        }
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        bool snappedToTile = SnapToTile();
        if (!snappedToTile)
        {
            transform.position = _originalPosition;
        }
    }

    private void DragObject()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        transform.position = new Vector3( mouseWorldPosition.x + _offset.x, transform.position.y, mouseWorldPosition.z + _offset.z );
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = _mainCamera.ScreenPointToRay( Input.mousePosition );
        if (Physics.Raycast( ray, out RaycastHit hit ))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private bool SnapToTile()
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
                    transform.localPosition = _initialLocalPosition; //This might not be the best solution but currently Im using it solve the position problem
                    return true;
                }
            }
        }
        return false;
    }
}
