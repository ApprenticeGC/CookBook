namespace Game
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using UnityEngine;
    using Random = Unity.Mathematics.Random;

    public class Bootstrap : MonoBehaviour
    {
        private EntityManager _entityManager;
        private Random _rnd;
        
        // private NativeHashMap<Entity, >
        private NativeHashMap<Entity, Entity> _cachedOrderedPathTiles;

        void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _rnd = new Unity.Mathematics.Random((uint)System.DateTime.UtcNow.Ticks);

            using (_cachedOrderedPathTiles =
                new NativeHashMap<Entity, Entity>(1000, Allocator.Persistent))
            {
                CreateMapGrid();
                var orderedPathTile = CreateOrderedPathTile();
                var canAdd = _cachedOrderedPathTiles.TryAdd(orderedPathTile, orderedPathTile);
                CreateUnit(orderedPathTile);
            }
        }

        private void CreateMapGrid()
        {
            var archetype =
                _entityManager.CreateArchetype(
                    typeof(MapGrid),
                    typeof(PathTileBuffer));

            var entity = _entityManager.CreateEntity(archetype);
            _entityManager.SetComponentData(entity, new MapGrid
            {
                Width = 100,
                Height = 120
            });
            
            _entityManager.AddBuffer<PathTileBuffer>(entity);
        }

        private Entity CreateOrderedPathTile()
        {
            var archetype =
                _entityManager.CreateArchetype(
                    typeof(OrderedPathTile),
                    typeof(OrderedPathTileBuffer));
            
            var entity = _entityManager.CreateEntity(archetype);
            
            _entityManager.SetComponentData(entity, new OrderedPathTile
            {
                Id = _rnd.NextInt()
            });
            

            var buffer = _entityManager.AddBuffer<OrderedPathTileBuffer>(entity);

            buffer.ResizeUninitialized(20);
            for (var i = 0; i < buffer.Length; ++i)
            {
                buffer[i] = new int3(-1, -1, -1);
            }

            return entity;
        }

        private void CreateUnit(Entity orderedPathTile)
        {
            var archetype =
                _entityManager.CreateArchetype(
                    typeof(Unit),
                    typeof(PathfindingGroup));

            using (var entities = _entityManager.CreateEntity(archetype, 5, Allocator.Persistent))
            {
                for (var i = 0; i < entities.Length; ++i)
                {
                    var entity = entities[i];
                    _entityManager.SetComponentData(entity, new PathfindingGroup
                    {
                        Value = orderedPathTile
                    });
                }
            }
        }
    }
}
