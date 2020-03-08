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
}