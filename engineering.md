# Architecture

Options:
- Entity-Component-System
- Model-View-Controller
- Entity-Component

## Entity-Component-System

Entitas

### Navigation

1. Click point input system updates click point and publishes click position in the world.
1. Tilemap position system reacts to click position by setting selected destination to that grid cell.
1. Nav tilemap agent system reacts to selected destination by asking each spatial A-star to find a path for each selected nav agent.
1. Nav tilemap agent system takes a step on the path.
1. Tween position system reacts to step for each entity with a tween position component and adds tween progress component.
1. When tween progress component is removed, nav tilemap agent system reacts by finding a path.

## Model-View-Controller

### Navigation

1. Nav tilemap agent updates click point and asks nav tilemap controller to convert to grid cell destination.
1. Nav tilemap agent asks spatial A-star to find a path.
1. Nav tilemap agent tweens to next step.
1. After each step, nav tilemap agent finds a path to its current destination.

# Navigation

Options:
- 2D pathfinding with tilemap.
- 3D NavMesh
- Waypoints
- Physics forces

## 2D pathfinding with tilemap

Tilemap is limited to 2D.  It can be aligned to another axis, but the terrain collider 2D is limited. to XY plane.

I downloaded pathfinding asset on store:

2D Grid Based A\* Pathfinding - Tower Defense Scripting/AI Saad Khawaja
<https://www.assetstore.unity3d.com/en/#!/content/20087>

Bind the tilemap to the grid.


## 3D NavMesh

Of these, NavMesh seems like the least work to get started.

But NavMesh is limited to 3D.

The navmesh does not rotate.

When an agent is in the trigger volume of an attractor,
the attractor checks if the the type of agent matches the attractor,
and the attractor reassigns the agent's destination.

Workflow:

1. Draw walls of the maze on an image.
1. Export image to Photoshop Raw file.
1. Import to terrain.
1. Bake terrain.

Alternative workflow:

1. In Unity editor, Place walls.
1. Place objects.

## Filter collision

Set layer of colliders.
- Sugar1
- Sugar2
<https://docs.unity3d.com/Manual/LayerBasedCollision.html>

## Tilemap

Options:
- Unity 2017.2 Tilemap
<https://docs.unity3d.com/Manual/Tilemap.html>
- <https://www.assetstore.unity3d.com/en/#!/content/90420>
- <http://www.seanba.com/tiled2unity>

Since it's built-in, I'll use 2017.2 tilemap.

- Tilemaps in Unity 2017.2
<https://www.youtube.com/watch?v=o_eZFKCCng0>
- Unity 2017 - Tilemap tutorial
<https://www.youtube.com/watch?v=70sLhuE1sbc>


