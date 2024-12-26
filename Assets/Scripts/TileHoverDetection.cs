using UnityEngine;

public class TileHoverDetection : MonoBehaviour
{
    private Camera _mainCamera;
    private RaycastHit _hit;
    private GameObject _currentHoveredTile;
    [SerializeField]
    private LayerMask _ignoreLayerMask;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        DetectTileHover();
    }

    void DetectTileHover()
    {
        Ray ray = _mainCamera.ScreenPointToRay( Input.mousePosition );

        if (Physics.Raycast( ray, out _hit, Mathf.Infinity, ~_ignoreLayerMask ))
        {
            if (_hit.collider.CompareTag( "Tile" ))
            {
                GameObject hoveredTile = _hit.collider.gameObject;

                if (_currentHoveredTile != hoveredTile)
                {
                    if (_currentHoveredTile != null)
                    {
                        ResetTileState( _currentHoveredTile );
                    }

                    _currentHoveredTile = hoveredTile;
                    HighlightTile( hoveredTile );
                }
            }
        }
        else
        {
            if (_currentHoveredTile != null)
            {
                ResetTileState( _currentHoveredTile );
                _currentHoveredTile = null;
            }
        }
    }

    void HighlightTile( GameObject tile )
    {
        MeshRenderer tileMeshRenderer = tile.GetComponent<MeshRenderer>();
        if (tileMeshRenderer != null)
        {
            tileMeshRenderer.enabled = true;
        }
    }

    void ResetTileState( GameObject tile )
    {
        MeshRenderer tileMeshRenderer = tile.GetComponent<MeshRenderer>();
        if (tileMeshRenderer != null)
        {
            tileMeshRenderer.enabled = false;
        }
    }
}
