using Entitas;
using System;
using UnityEngine;

[Game]
[Serializable]
public sealed class SpawnBeforeDestroyComponent : IComponent
{
    public GameObject prefab;
}
