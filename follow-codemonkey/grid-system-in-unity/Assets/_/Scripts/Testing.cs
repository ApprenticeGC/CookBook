namespace GiantCroissant.FollowCodeMonkey.GridSystemInUnity
{
    using System.Collections;
    using System.Collections.Generic;
    using CodeMonkey.Utils;
    using UnityEngine;

    public class Testing : MonoBehaviour
    {
        private Grid _grid;
        
        void Start()
        {
            _grid = new Grid(10, 4, 10.0f, new Vector3(20.0f, 0, 0));
            new Grid(2, 3, 5.0f, Vector3.zero);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var worldPostion = UtilsClass.GetMouseWorldPosition();
                _grid.SetValue(worldPostion, 10);
            }

            if (Input.GetMouseButtonDown(1))
            {
                var worldPositon = UtilsClass.GetMouseWorldPosition();
                var v = _grid.GetValue(worldPositon);
                Debug.Log($"value at {worldPositon} is {v}");
            }
        }
    }
    
    internal class HeatMapVisual
    {
        private Grid _grid;

        private Mesh _mesh;

        public HeatMapVisual(Grid grid, MeshFilter meshFilter)
        {
            _grid = grid;
            
            _mesh = new Mesh();
            meshFilter.mesh = _mesh;
            
            _grid.OnGridValueChanged += GridOnOnGridValueChanged;
        }

        private void GridOnOnGridValueChanged(object sender, OnGridValueChangedEventArgs e)
        {
            UpdateHeatMapVisual();
        }

        private void UpdateHeatMapVisual()
        {
            Vector3[] vertices;
            Vector2[] uvs;
            int[] triangles;
            
            // Mesh Util
            for (var x = 0; x < _grid.GetWidth; ++x)
            {
                for (var y = 0; y < _grid.GetHeight; ++y)
                {
                    var index = x * _grid.GetHeight + y;
                    var baseSize = new Vector3(1, 1) * _grid.GetCellSize;
                    var gridValue = _grid.GetValue(x, y);
                    var maxGridValue = 100;
                    var gridValueNormalized = Mathf.Clamp01((float) gridValue / (float) maxGridValue);
                    var gridCellUV = new Vector2(gridValueNormalized, 0);
                    
                    //
                }
            }

            // _mesh.vertices = vertices;
            // _mesh.uv = uvs;
            // _mesh.triangles = triangles;
        }
    }
}