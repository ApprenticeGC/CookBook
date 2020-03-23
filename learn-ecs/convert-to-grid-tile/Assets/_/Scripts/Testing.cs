using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private void Start()
    {
        {
            var hGridCellCount = 20;
            var vGridCellCount = 30;
            var hGridCellSize = 1.0f;
            var vGridCellSize = 1.0f;
            var hPosition = 5.7f;
            var vPosition = 12.3f;
            var gridIndex =
                Utility.MapGridHelper.GridIndex(
                    hGridCellCount, vGridCellCount,
                    hGridCellSize, vGridCellSize,
                    hPosition, vPosition);
            
            Debug.Log($"hGridCellCount: {hGridCellCount} vGridCellCount: {vGridCellCount} pos: ({hPosition}, {vPosition}) Grid Index: {gridIndex}");
        }

        {
            var hGridCellCount = 20;
            var vGridCellCount = 30;
            var hGridCellSize = 1.0f;
            var vGridCellSize = 1.0f;
            var hTileCellCount = 6;
            var vTileCellCount = 7;
            var hTileCellSize = 1.0f * hGridCellSize;
            var vTileCellSize = 1.0f * vGridCellSize;
            var hPosition = 5.7f;
            var vPosition = 12.3f;
            var tileCount =
                Utility.PathTileHelper.TileCount(
                    hGridCellCount, vGridCellCount,
                    hGridCellSize, vGridCellSize,
                    hTileCellCount, vTileCellCount,
                    hTileCellSize, vTileCellSize);
            
            Debug.Log($"hGridCellCount: {hGridCellCount} vGridCellCount: {vGridCellCount} hTileCellCount: {hTileCellCount} vTileCellCount: {vTileCellCount} hTileCellSize: {hTileCellSize} vTileCellSize: {vTileCellSize} Tile Count: {tileCount}");
        }
        
        {
            var hGridCellCount = 20;
            var vGridCellCount = 30;
            var hGridCellSize = 1.0f;
            var vGridCellSize = 1.0f;
            var hTileCellCount = 6;
            var vTileCellCount = 7;
            var hTileCellSize = 1.0f * hGridCellSize;
            var vTileCellSize = 1.0f * vGridCellSize;
            var hPosition = 5.7f;
            var vPosition = 12.3f;
            var tileIndex =
                Utility.PathTileHelper.TileIndex(
                    hGridCellCount, vGridCellCount,
                    hGridCellSize, vGridCellSize,
                    hTileCellCount, vTileCellCount,
                    hTileCellSize, vTileCellSize,
                    hPosition, vPosition);
            
            Debug.Log($"hGridCellCount: {hGridCellCount} vGridCellCount: {vGridCellCount} hTileCellCount: {hTileCellCount} vTileCellCount: {vTileCellCount} hTileCellSize: {hTileCellSize} vTileCellSize: {vTileCellSize} pos: ({hPosition}, {vPosition}) TileIndex: {tileIndex}");
        }

        {
            var hGridCellCount = 20;
            var vGridCellCount = 30;
            var hGridCellSize = 1.0f;
            var vGridCellSize = 1.0f;
            var hTileCellCount = 6;
            var vTileCellCount = 7;
            var hTileCellSize = 1.0f * hGridCellSize;
            var vTileCellSize = 1.0f * vGridCellSize;
            var hPosition = 5.7f;
            var vPosition = 12.3f;
            var tileIndexWithTileCellIndex =
                Utility.PathTileHelper.TileIndexWithTileCellIndex(
                    hGridCellCount, vGridCellCount,
                    hGridCellSize, vGridCellSize,
                    hTileCellCount, vTileCellCount,
                    hTileCellSize, vTileCellSize,
                    hPosition, vPosition);
            
            Debug.Log($"hGridCellCount: {hGridCellCount} vGridCellCount: {vGridCellCount} hTileCellCount: {hTileCellCount} vTileCellCount: {vTileCellCount} hTileCellSize: {hTileCellSize} vTileCellSize: {vTileCellSize} pos: ({hPosition}, {vPosition}) tileIndexWithTileCellIndex: {tileIndexWithTileCellIndex}");
        }
        
        {
            var hGridCellCount = 20;
            var vGridCellCount = 30;
            var hGridCellSize = 1.0f;
            var vGridCellSize = 1.0f;
            var hTileCellCount = 6;
            var vTileCellCount = 7;
            var hTileCellSize = 1.0f * hGridCellSize;
            var vTileCellSize = 1.0f * vGridCellSize;

            var path = new NativeList<float2>(Allocator.Temp);
            path.Add(new float2(4.5f, 1.0f));
            path.Add(new float2(5.5f, 1.0f));
            path.Add(new float2(6.5f, 1.0f));
            path.Add(new float2(6.5f, 1.5f));
            path.Add(new float2(6.5f, 2.5f));
            
            var tileIndices =
                Utility.PathTileHelper.OnPathTiles(
                    hGridCellCount, vGridCellCount,
                    hGridCellSize, vGridCellSize,
                    hTileCellCount, vTileCellCount,
                    hTileCellSize, vTileCellSize,
                    path);

            for (var i = 0; i < tileIndices.Length; ++i)
            {
                var index = tileIndices[i];
                Debug.Log($"Tile index: {index}");
            }

            path.Dispose();
            tileIndices.Dispose();
            // Debug.Log($"hGridCellCount: {hGridCellCount} vGridCellCount: {vGridCellCount} hTileCellCount: {hTileCellCount} vTileCellCount: {vTileCellCount} hTileCellSize: {hTileCellSize} vTileCellSize: {vTileCellSize} pos: ({hPosition}, {vPosition}) TileIndex: {tileIndex}");
        }
    }
}
