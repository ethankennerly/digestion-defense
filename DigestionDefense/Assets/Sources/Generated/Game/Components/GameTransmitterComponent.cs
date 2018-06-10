//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TransmitterComponent transmitter { get { return (TransmitterComponent)GetComponent(GameComponentsLookup.Transmitter); } }
    public bool hasTransmitter { get { return HasComponent(GameComponentsLookup.Transmitter); } }

    public void AddTransmitter(int[] newInputIds, int[] newOutputIds) {
        var index = GameComponentsLookup.Transmitter;
        var component = CreateComponent<TransmitterComponent>(index);
        component.inputIds = newInputIds;
        component.outputIds = newOutputIds;
        AddComponent(index, component);
    }

    public void ReplaceTransmitter(int[] newInputIds, int[] newOutputIds) {
        var index = GameComponentsLookup.Transmitter;
        var component = CreateComponent<TransmitterComponent>(index);
        component.inputIds = newInputIds;
        component.outputIds = newOutputIds;
        ReplaceComponent(index, component);
    }

    public void RemoveTransmitter() {
        RemoveComponent(GameComponentsLookup.Transmitter);
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

    static Entitas.IMatcher<GameEntity> _matcherTransmitter;

    public static Entitas.IMatcher<GameEntity> Transmitter {
        get {
            if (_matcherTransmitter == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Transmitter);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTransmitter = matcher;
            }

            return _matcherTransmitter;
        }
    }
}