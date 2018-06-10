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


# Example Flow

Breakdown of screenplay in [design.md](design.md)

Status:
- [ ] Todo
- [-] In-progress
- [x] Done
- Context
---

Designer flow:
1. [ ] Select transmitter.
1. [ ] Connect receiver.
1. [ ] See debug line showing connection.  Deselect transmitter. Debug line disappears.
1. [x] Select filter component names from dropdown list of components.

Player flow:
1. [ ] Three consumable deployment receivers.
1. [ ] Three consumables transmit:  Grape, Cod, Oil.
1. [x] Player taps collider of selected receiver in node hierarchy containing grape.
1. [ ] Transmitter transforms selected and grape to transmit grape to connected entrance that receives consumable.
1. [ ] consumable receiver at end of tunnel has trigger region covering all caves.
1. [ ] Nav agent in grape finds path to end of tunnel, and begins moving.
1. [ ] Grape moves in trigger region of white teeth.
1. [ ] Bottom white teeth change to closing.
1. [ ] Bottom white closing teeth find path to receiver in top white teeth.
1. [ ] White teeth contact grape. They transmit negative health to grape.
1. [ ] Bottom white teeth change to opening.
1. [ ] Bottom white opening teeth find path to receiver below.
1. [ ] Grape has a receiver for negative health and the health is depleted.
1. [ ] Health is destroyed.
1. [ ] Health was inhibiting Pair of white hexagon transmitter.
1. [ ] Pair of white hexagon transmitter transmits.
1. [ ] Pair of white hexagon is a consumable.
1. [ ] consumable receiver in tree that had received the destroyed grape receives consumable.
1. [ ] Nav agent in pair of white hexagon finds path to end of tunnel.
    - For path finding simplicity, small pair fits into one cell of the nav grid.
1. [ ] Pair of white hexagon flow out of purple cave.
1. [ ] Nav agent in pair of white hexagon finds path to end of tunnel.
1. [ ] Pair of white hexagon flow out of purple cave.
1. [ ] In white cave, pair of white hexagonal concavities detect pair of white hexagon in visible range.
1. [ ] Concavities target pair of white hexagon.
1. [ ] Concavities attract nav agent in pair of white hexagon.
1. [ ] Pair of white hexagons finds path to concavities.
1. [ ] Concavities convert pair of white hexagons into two separate white hexagons.
1. [ ] White hexagons find path to end of tunnel.
1. [ ] White hexagon hole detect white hexagon.
1. [ ] White hexagon hole attract nav agent of white hexagon.
1. [ ] White hexagon contacts white hexagon hole.
1. [ ] White hexagon hole receives white hexagon and transmits white fuel.
1. [ ] White fuel receiver accepts white fuel.
1. [ ] White fuel receiver transmits some red fuel.
1. [ ] red fuel receiver increases.

## Components

- [x] Receiver (similar to what a Petri Net calls a Place)
    - Filter component names
        - String for simple and stable serialization.
    - Occupant
        - Entity ID
- [ ] Transmitter (similar to what a Petri Net calls a Transition)
    - Input IDs
        - Input is a receiver with an occupant.
    - Output IDs
        - Available output is a receiver without an occupant.
- [ ] Transmitter inhibitor (similar to what a Petri Net calls a Transition)
    - Inhibitor IDs
        - Inhibitor is a receiver with an occupant.
- [x] Selected
- [x] Consumable
    - name
- [ ] Timer
    - Rate
    - Min
    - Max
    - Remove remaining
- [ ] Time remaining
    - Value
- [ ] Tree node (of a composition in a what graph theorists call a Tree)
    - Parent entity IDs
    - Children entity IDs

Navigation components:
- [ ] nav agent
    - nav agent targets
        - nearest receiver in trigger region
- [ ] Nav grid
- [ ] trigger region


## Reference: Machinations by Joris Dormans

Node types:
- Pool
- Label modifier
- Node modifier
- Gate
- Trigger
- Activator
- Source
- Drain
- Converter
- Trader

## Reference: Petri Net

- Place
- Arc
- Inhibitor arc
- Transition
- Token

Examples:
<https://www.techfak.uni-bielefeld.de/~mchen/BioPNML/Intro/pnfaq.html>

Pathway Analysis in Metabolic Databases via Differential Metabolic Display (DMD)
Robert Küffner, Ralf Zimmer and Thomas Lengauer
<http://www.bioinfo.de/isb/gcb99/talks/kueffner/main.html>

Transmitter and Receiver as a PetriNet

    ( )A -----> | -----> ( )A
    Transmitter      Receiver

When an A-token is delivered to the first place, it transitions to the second place.

One way to represent receiver that cannot be occupied if already occupied, is for the receiver place to have an arc that inhibits the transition that the transmitter is pointing toward.
