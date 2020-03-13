namespace GiantCroissant.LearnECS.IntegrateWithUiButton
{
    using System;
    using Unity.Entities;
    using UnityEngine;

    public class PlayerStatus : MonoBehaviour
    {
        public UnityEngine.UI.Text hpText;
        
        public UnityEngine.UI.Button addHp;
        public UnityEngine.UI.Button subtractHp;

        private EntityManager _entityManager;
        private EntityQuery _statusQuery;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            var queryDesc = new EntityQueryDesc
            {
                All = new ComponentType[] {typeof(HudEventBase)},
                None = new ComponentType[] {typeof(HpButtonPressed)}
            };
            
            _statusQuery = _entityManager.CreateEntityQuery(queryDesc);
        }

        public void UpdateHpCount(int value)
        {
            hpText.text = $"{value}";
        }

        public void HandleAddHp(int value)
        {
            var entity = _statusQuery.GetSingletonEntity();
            _entityManager.AddComponentData(entity, new HpButtonPressed
            {
                Kind = EButtonKind.Add,
                Value = value
            });
        }

        public void HandleSubtractHp(int value)
        {
            var entity = _statusQuery.GetSingletonEntity();
            _entityManager.AddComponentData(entity, new HpButtonPressed
            {
                Kind = EButtonKind.Subtract,
                Value = value
            });
        }
    }    
}
