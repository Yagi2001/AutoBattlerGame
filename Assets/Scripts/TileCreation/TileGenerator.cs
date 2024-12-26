using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _tilePrefab;
    [SerializeField]
    private int _gridWidth = 8;
    [SerializeField]
    private int _gridHeight = 8;
    [SerializeField]
    private float _tileSize = 1f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Calculate offsets to center
        float xOffset = (_gridWidth * _tileSize) / 2f;
        float zOffset = (_gridHeight * _tileSize) / 2f;

        for (int x = 0; x < _gridWidth; x++)
        {
            for (int z = 0; z < _gridHeight; z++)
            {
                GameObject tile = Instantiate( _tilePrefab, transform );
                tile.transform.position = new Vector3( (x * _tileSize) - xOffset + _tileSize / 2,0, (z * _tileSize) - zOffset + _tileSize / 2 );
            }
        }
    }
}