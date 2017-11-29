using System;
using Entitas;
using UnityEngine;

// XXX Cannot instantiate MonoBehaviour out of a game object.
// XXX Expensive to attach to a game object or get from a game object.
[Serializable]
[Game]
public sealed class GridPositionBehaviour : MonoBehaviour, IComponent
{
    public int x;
    public int y;
}
