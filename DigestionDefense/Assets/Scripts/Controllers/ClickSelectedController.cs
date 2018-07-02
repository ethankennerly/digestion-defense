using Entitas.Unity;
using Finegamedesign.Entitas;
using Finegamedesign.Utils;
using System;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class ClickSelectedController : MonoBehaviour
    {
        private Action<Collider2D> m_OnCollisionEnter2D;

        private string m_ComponentName = "Selected";
        private int m_ComponentIndex = -1;

        private GameContext m_Context;

        private void OnValidate()
        {
            SetComponentIndex();
        }

        private void OnEnable()
        {
            m_Context = Contexts.sharedInstance.game;
            SetComponentIndex();

            AddListener();
        }

        private void OnDisable()
        {
            RemoveListener();
        }

        private void SetComponentIndex()
        {
            m_ComponentIndex = ReceiverUtils.ToComponentIndex(m_ComponentName);
        }

        private void AddListener()
        {
            if (m_OnCollisionEnter2D == null)
                m_OnCollisionEnter2D = ReplaceSelectedIfReceiver;

            ClickSystem.instance.onCollisionEnter2D += m_OnCollisionEnter2D;
        }

        private void RemoveListener()
        {
            ClickSystem.instance.onCollisionEnter2D -= m_OnCollisionEnter2D;
        }

        private void ReplaceSelectedIfReceiver(Collider2D collider)
        {
            if (collider == null || collider.gameObject == null)
                return;

            GameEntity entity = GameLinkUtils.GetEntity(collider.gameObject);
            if (entity == null)
                return;

            if (!entity.hasReceiver)
                return;

            if (!ReceiverUtils.Filter(entity.receiver, m_ComponentIndex))
                return;

            GameEntity selectedEntity = m_Context.CreateEntity();
            selectedEntity.isSelected = true;
            ReceiverUtils.AddOccupant(entity, selectedEntity.id.value);
        }
    }
}
