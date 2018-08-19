using Entitas;
using System;
using UnityEngine;

public class BeforeDestroyMonoListener: AMonoGameListener<BeforeDestroyListenerComponent>
{
    public override void OnComponent(GameEntity entity, IComponent changedComponent)
    {
        Debug.Log("CustomListener.OnComponent: TODO entity=" + entity + " changedComponent=" + changedComponent);
    }
}

/// <summary>
/// Example to show requested API.
/// <a href="https://github.com/sschmid/Entitas-CSharp/issues/769">How can I write less boilerplate code for an Entitas event listener?</a>
/// Without the API, one idea would be to add a system that checks for a changed component with the matching index that is being listened to.
/// </summary>
public abstract class AMonoGameListener<TListenerComponent> : MonoBehaviour // , IComponentListener <-- Requested API
    where TListenerComponent : IComponent
{
    protected GameContext m_Context;
    protected GameEntity m_Publisher;
    protected int m_PublisherId;
    protected int m_ListenerIndex;

    protected virtual void OnEnable()
    {
        AddListener();
    }

    protected virtual void AddListener()
    {
        m_ListenerIndex = Array.IndexOf(GameComponentsLookup.componentTypes, typeof(TListenerComponent));
        m_Publisher = GetOrCreatePublisher();
        /* Requested API:
        if (m_Publisher.HasComponentListener(m_ListenerIndex, this))
            return;

        m_Publisher.AddComponentListener(m_ListenerIndex, this);
         */
    }

    // TODO: Likewise implement the reverse for OnDisable / RemoveListener

    public abstract void OnComponent(GameEntity entity, IComponent changedComponent);

    /// <summary>
    /// Stores ID, not reference to entity to avoid stale reference.
    /// </summary>
    private GameEntity GetOrCreatePublisher()
    {
        if (m_Context == null)
            m_Context = Contexts.sharedInstance.game;

        GameEntity publisher = m_Context.GetEntityWithId(m_PublisherId);
        if (publisher == null)
            publisher = m_Context.CreateEntity();

        return publisher;
    }
}

