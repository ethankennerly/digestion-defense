using Entitas.Unity;
using Finegamedesign.Entitas;
using Finegamedesign.Utils;
using System;
using UnityEngine;

public sealed class ClickSelectedController : MonoBehaviour
{
    private Action<Collider2D> m_OnCollisionEnter2D;

    private string m_ComponentName = "Selected";

    private GameContext m_Context;

    private void OnEnable()
    {
        m_Context = Contexts.sharedInstance.game;

        AddListener();
    }

    private void OnDisable()
    {
        RemoveListener();
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

        if (!FilterReceiver(entity.receiver, m_ComponentName))
            return;

        GameEntity selectedEntity = m_Context.CreateEntity();
        selectedEntity.isSelected = true;
        ReplaceReceiver(entity, selectedEntity);
    }

    /// <returns>
    /// If receiver is empty and accepts a component of the given name.
    /// </returns>
    private static bool FilterReceiver(ReceiverComponent receiver, string componentName)
    {
        if (receiver.occupantId >= 0)
            return false;

        string[] filterNames = receiver.filterComponentNames;
        if (filterNames == null)
            return false;

        foreach (string filterName in filterNames)
        {
            if (filterName != componentName)
                continue;

            return true;
        }

        return false;
    }

    private static void ReplaceReceiver(GameEntity receiver, GameEntity occupant)
    {
        receiver.ReplaceReceiver(
            receiver.receiver.filterComponentNames,
            occupant.id.value
        );
    }
}
