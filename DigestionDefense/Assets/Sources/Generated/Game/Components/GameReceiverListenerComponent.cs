//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ReceiverListenerComponent receiverListener { get { return (ReceiverListenerComponent)GetComponent(GameComponentsLookup.ReceiverListener); } }
    public bool hasReceiverListener { get { return HasComponent(GameComponentsLookup.ReceiverListener); } }

    public void AddReceiverListener(System.Collections.Generic.List<IReceiverListener> newValue) {
        var index = GameComponentsLookup.ReceiverListener;
        var component = CreateComponent<ReceiverListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceReceiverListener(System.Collections.Generic.List<IReceiverListener> newValue) {
        var index = GameComponentsLookup.ReceiverListener;
        var component = CreateComponent<ReceiverListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveReceiverListener() {
        RemoveComponent(GameComponentsLookup.ReceiverListener);
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

    static Entitas.IMatcher<GameEntity> _matcherReceiverListener;

    public static Entitas.IMatcher<GameEntity> ReceiverListener {
        get {
            if (_matcherReceiverListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ReceiverListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherReceiverListener = matcher;
            }

            return _matcherReceiverListener;
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

    public void AddReceiverListener(IReceiverListener value) {
        var listeners = hasReceiverListener
            ? receiverListener.value
            : new System.Collections.Generic.List<IReceiverListener>();
        listeners.Add(value);
        ReplaceReceiverListener(listeners);
    }

    public void RemoveReceiverListener(IReceiverListener value, bool removeComponentWhenEmpty = true) {
        var listeners = receiverListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveReceiverListener();
        } else {
            ReplaceReceiverListener(listeners);
        }
    }
}
