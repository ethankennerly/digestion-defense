//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly AccepterComponent accepterComponent = new AccepterComponent();

    public bool isAccepter {
        get { return HasComponent(GameComponentsLookup.Accepter); }
        set {
            if (value != isAccepter) {
                var index = GameComponentsLookup.Accepter;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : accepterComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherAccepter;

    public static Entitas.IMatcher<GameEntity> Accepter {
        get {
            if (_matcherAccepter == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Accepter);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAccepter = matcher;
            }

            return _matcherAccepter;
        }
    }
}
