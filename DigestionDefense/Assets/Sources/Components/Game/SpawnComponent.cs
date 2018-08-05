using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Event(bindToEntity: false)]
[Game]
public sealed class SpawnComponent : IComponent
{
    public GameObject prefab;
}
