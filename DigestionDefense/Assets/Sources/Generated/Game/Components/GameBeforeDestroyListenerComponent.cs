//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public BeforeDestroyListenerComponent beforeDestroyListener { get { return (BeforeDestroyListenerComponent)GetComponent(GameComponentsLookup.BeforeDestroyListener); } }
    public bool hasBeforeDestroyListener { get { return HasComponent(GameComponentsLookup.BeforeDestroyListener); } }

    public void AddBeforeDestroyListener(System.Collections.Generic.List<IBeforeDestroyListener> newValue) {
        var index = GameComponentsLookup.BeforeDestroyListener;
        var component = CreateComponent<BeforeDestroyListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBeforeDestroyListener(System.Collections.Generic.List<IBeforeDestroyListener> newValue) {
        var index = GameComponentsLookup.BeforeDestroyListener;
        var component = CreateComponent<BeforeDestroyListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBeforeDestroyListener() {
        RemoveComponent(GameComponentsLookup.BeforeDestroyListener);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherBeforeDestroyListener;

    public static Entitas.IMatcher<GameEntity> BeforeDestroyListener {
        get {
            if (_matcherBeforeDestroyListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BeforeDestroyListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBeforeDestroyListener = matcher;
            }

            return _matcherBeforeDestroyListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddBeforeDestroyListener(IBeforeDestroyListener value) {
        var listeners = hasBeforeDestroyListener
            ? beforeDestroyListener.value
            : new System.Collections.Generic.List<IBeforeDestroyListener>();
        listeners.Add(value);
        ReplaceBeforeDestroyListener(listeners);
    }

    public void RemoveBeforeDestroyListener(IBeforeDestroyListener value, bool removeComponentWhenEmpty = true) {
        var listeners = beforeDestroyListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveBeforeDestroyListener();
        } else {
            ReplaceBeforeDestroyListener(listeners);
        }
    }
}
