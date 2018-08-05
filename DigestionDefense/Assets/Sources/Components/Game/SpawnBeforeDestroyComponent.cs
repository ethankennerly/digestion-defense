using Entitas;
using UnityEngine;

[Game]
public sealed class SpawnBeforeDestroyComponent : IComponent
{
    public GameObject prefab;
}
