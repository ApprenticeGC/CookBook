namespace GiantCroissant.LearnECS.ReadInTilemap
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class ConvertTilemap : MonoBehaviour
    {
        public GameObject gridPrefab;

        void Start()
        {
            var gridGo = Instantiate(gridPrefab);
            var grid = gridGo.GetComponent<Grid>();
            
            Debug.Log($"grid cellSize: {grid.cellSize}");
            
            foreach (Transform child in grid.transform)
            {
                var tilemap = child.GetComponent<Tilemap>();
                var tilemapOrigin = tilemap.origin;
                var tilemapSize = tilemap.size;
                var tilemapCellBounds = tilemap.cellBounds;

                var cellPos = new Vector3Int(0, 0, 0);
                var worldPos = tilemap.CellToWorld(cellPos);
                
                var specificWorldPos = new Vector3Int(2, 0, -12);
                var specificCellPos = tilemap.WorldToCell(specificWorldPos);
                
                Debug.Log($"tilemap: {tilemap.name} pos: {tilemap.transform.position} origin: {tilemapOrigin} size: {tilemapSize} cellBounds: {tilemapCellBounds}");
                Debug.Log($"cellPos {cellPos} at worldPos: {worldPos}");
                Debug.Log($"specificWorldPos {specificWorldPos} at specificCellPos: {specificCellPos}");
            }
        }
    }
    
}
