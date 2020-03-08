namespace GiantCroissant.FollowCodeMonkey.GridSystemInUnity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    using CodeMonkey.Utils;

    public class Grid
    {
        private readonly int _width;
        private readonly int _height;

        private int[,] _gridArray;
        private TextMesh[,] _debugTextArray;
        private float _cellSize;
        private Vector3 _originPosition;

        public Grid(int width, int height, float cellSize, Vector3 originPosition)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;

            _gridArray = new int [width, height];
            _debugTextArray = new TextMesh[width, height];
            
            Debug.Log($"Grid - width: {width} height: {height}");

            for (var x = 0; x < _gridArray.GetLength(0); ++x)
            {
                for (var y = 0; y < _gridArray.GetLength(1); ++y)
                {
                    // Debug.Log($"x:  {x} y: {y}");

                    _debugTextArray[x, y] =
                        UtilsClass.CreateWorldText(
                            _gridArray[x, y].ToString(), 
                            null, 
                            GetWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * 0.5f, 
                            20, 
                            Color.white,
                            TextAnchor.MiddleCenter);
                    
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100.0f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100.0f);
                }
            }
            
            Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100.0f);
            Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100.0f);
            
            SetValue(2, 1, 56);
        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return (new Vector3(x, y) * _cellSize + _originPosition);
        }

        private void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            var wp = worldPosition - _originPosition;
            
            x = Mathf.FloorToInt(wp.x / _cellSize);
            y = Mathf.FloorToInt(wp.y / _cellSize);
        }

        public void SetValue(int x, int y, int value)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                _gridArray[x, y] = value;
                _debugTextArray[x, y].text = _gridArray[x, y].ToString();
            }
        }

        public void SetValue(Vector3 worldPosition, int value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetValue(x, y, value);
        }

        public int GetValue(int x, int y)
        {
            var v = -1;
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                v = _gridArray[x, y];
            }

            return v;
        }

        public int GetValue(Vector3 worldPosition)
        {
            int x;
            int y;
            
            GetXY(worldPosition, out x, out y);
            return GetValue(x, y);
        }
    }
}
