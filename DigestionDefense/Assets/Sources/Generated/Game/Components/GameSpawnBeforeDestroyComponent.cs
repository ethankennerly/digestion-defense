//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SpawnBeforeDestroyComponent spawnBeforeDestroy { get { return (SpawnBeforeDestroyComponent)GetComponent(GameComponentsLookup.SpawnBeforeDestroy); } }
    public bool hasSpawnBeforeDestroy { get { return HasComponent(GameComponentsLookup.SpawnBeforeDestroy); } }

    public void AddSpawnBeforeDestroy(UnityEngine.GameObject newPrefab) {
        var index = GameComponentsLookup.SpawnBeforeDestroy;
        var component = CreateComponent<SpawnBeforeDestroyComponent>(index);
        component.prefab = newPrefab;
        AddComponent(index, component);
    }

    public void ReplaceSpawnBeforeDestroy(UnityEngine.GameObject newPrefab) {
        var index = GameComponentsLookup.SpawnBeforeDestroy;
        var component = CreateComponent<SpawnBeforeDestroyComponent>(index);
        component.prefab = newPrefab;
        ReplaceComponent(index, component);
    }

    public void RemoveSpawnBeforeDestroy() {
        RemoveComponent(GameComponentsLookup.SpawnBeforeDestroy);
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

    static Entitas.IMatcher<GameEntity> _matcherSpawnBeforeDestroy;

    public static Entitas.IMatcher<GameEntity> SpawnBeforeDestroy {
        get {
            if (_matcherSpawnBeforeDestroy == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SpawnBeforeDestroy);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSpawnBeforeDestroy = matcher;
            }

            return _matcherSpawnBeforeDestroy;
        }
    }
}
