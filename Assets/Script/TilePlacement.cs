using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacement : MonoBehaviour
{
    [SerializeField] private RectTransform greenArea; // Reference to the green area RectTransform
    [SerializeField] private Tile tilePrefab;  // The tile prefab (yellow tile)
    [SerializeField] private RectTransform parentTransform; // Parent for the tiles (e.g., BG)

    private List<Tile> spawnedTilesList = new List<Tile>();

    void Start()
    {
        PlaceTiles();
        UIManager.ResetGame += ResetTiles;
    }

    private void ResetTiles()
    {
        foreach (Tile tile in spawnedTilesList)
        {
            Destroy(tile.gameObject);
        }
        spawnedTilesList.Clear();

        PlaceTiles();
    }

    private void PlaceTiles()
    {
        if (greenArea == null || tilePrefab == null || parentTransform == null)
        {
            Debug.LogError("Missing references! Please assign the greenArea, tilePrefab, and parentTransform.");
            return;
        }

        // Get the size of the green area
        Vector2 greenAreaSize = greenArea.rect.size;

        // Get the size of the tile
        RectTransform tileRect = tilePrefab.GetComponent<RectTransform>();
        Vector2 tileSize = tileRect.rect.size;

        // Calculate the number of tiles needed in each direction
        int tilesX = Mathf.CeilToInt(greenAreaSize.x / tileSize.x);
        int tilesY = Mathf.CeilToInt(greenAreaSize.y / tileSize.y);

        // Loop through and place tiles
        for (int y = 0; y < tilesY; y++)
        {
            for (int x = 0; x < tilesX; x++)
            {
                // Instantiate the tile
                Tile tile = Instantiate(tilePrefab, parentTransform);
                spawnedTilesList.Add(tile);

                // Set its position
                RectTransform tileTransform = tile.GetComponent<RectTransform>();
                tileTransform.anchoredPosition = new Vector2(x * tileSize.x, y * tileSize.y);

                // Ensure it matches the size of the prefab
                tileTransform.sizeDelta = tileSize;
                tileTransform.localScale = Vector3.one;
            }
        }

        Debug.Log($"Placed {tilesX * tilesY} tiles to cover the green area.");
    }

    public void RemoveTile(Tile tile)
    {
        spawnedTilesList.Remove(tile);
        if (spawnedTilesList.Count <= 0)
        {
            Debug.Log("All tiles have been removed!");
            UIManager.GameOver?.Invoke();
        }
    }

    public Vector3 GetRandomTilePosition()
    {
        if (spawnedTilesList.Count == 0)
        {
            Debug.LogError("No tiles available!");
            return Vector3.zero;
        }

        Tile randomTile = spawnedTilesList[UnityEngine.Random.Range(0, spawnedTilesList.Count)];
        return randomTile.transform.position;
    }

    private void OnDestroy()
    {
        UIManager.ResetGame -= ResetTiles;
    }
}
