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

## Designer flow

1. [ ] Select transmitter.
1. [ ] Connect receiver.
1. [ ] See debug line showing connection.  Deselect transmitter. Debug line disappears.
1. [x] Select filter component names from dropdown list of components.

## Player flow

1. [ ] Three consumable deployment receivers.
1. [ ] Three consumables transmit:  Grape, Cod, Oil.
1. [x] Player taps collider of selected receiver in node hierarchy containing grape.
1. [x] Transmitter transforms selected and grape to transmit grape to connected entrance that receives consumable.
    1. [x] A view listening to this event reparents the game object to the new receiver.
1. [x] consumable receiver at end of tunnel has trigger region covering all caves.
1. [x] Nav agent in grape finds path to end of tunnel, and begins moving.
1. [x] Grape moves in trigger region of white teeth.
    1. [x] One of the following:
        1. [x] Patrol:
            1. [x] Bottom white teeth loop potential destinations of top and bottom.
        1. [ ] Animation:
            1. [ ] Bottom white teeth animate in a loop.
        1. [ ] Transmitter / receiver:
            1. [ ] Bottom white teeth change to closing.
            1. [ ] Bottom white closing teeth find path to receiver in top white teeth.
            1. [ ] Bottom white teeth change to opening.
            1. [ ] Bottom white opening teeth find path to receiver below.
    1. [x] One of the following:
        1. [x] Separate systems:
            1. [x] Trigger reaction system filters reaction to add to trigger.
            1. [x] Attraction reaction system
            1. [x] Loop destinations reaction system
        1. [ ] Utility to filter two systems:
            1. [ ] Trigger nav target system
            1. [ ] Trigger loop destinations system
    1. [x] After grape exits trigger, loop stops at beginning.
1. [ ] White teeth contact grape. They transmit negative health to grape.
    1. [ ] Grape has a quantity receiver for negative health and the health is depleted.
        1. [ ] Damage. One of the following:
            1. [ ] Petri transmission pattern:
                1. [ ] Health with quantity occupies health receiver.
                1. [ ] Health with quantity entity has a trigger and an accepter.
                1. [ ] Teeth have a collider.
                1. [ ] Teeth has transmitter of negative health.
                1. [ ] Example test steps:
                    1. [ ] Receiver accepts health.
                    1. [ ] Occupant has health and quantity 1.
                    1. [ ] Target has receiver.
                    1. [ ] Target has trigger and accepter.
                    1. [ ] Source has receiver with health and quantity -2.
                    1. [ ] Publish trigger of source and target.
                    1. [ ] Execute trigger systems.
                    1. [ ] Expect target receiver occupant has health and quantity -1.
                1. [ ] Test reversed roles: expect target health 1.
            1. [ ] Entity-component-system pattern:
                1. [ ] Grape has health component with a positive value.
                1. [ ] Tooth has health changer component with a negative value.
                1. [ ] Trigger component view publishes trigger enter component.
                1. [ ] Health system reacts to trigger enter filtered to targets that have health and sources that have health changer.
                1. [ ] Example test steps:
                    1. [ ] Source has health changer -2.
                    1. [ ] Target has health 1.
                    1. [ ] Publish trigger of source and target.
                    1. [ ] Execute health system.
                    1. [ ] Expect target health -1.
                1. [ ] Test reverse steps.
                    1. [ ] Setup. Reverse source and target, and publish.
                    1. [ ] Execute health system.
                    1. [ ] Expect target health 1.
            1. [ ] Petri Net pattern:
                1. [ ] Grape health petri net.
                    1. [ ] Petri net tree view.
                    1. [ ] Negative health transitions to white hexagon spawner.
                    1. [ ] Find health place in tree.
                1. [ ] Teeth collider places negative health in network.
                1. [ ] Example test steps:
                    1. [ ] Place has health type and quantity 1.
                    1. [ ] Petri net has place.
                    1. [ ] Target has petri net.
                    1. [ ] Source has changer with health type and quantity -2.
                    1. [ ] Publish trigger of source and target.
                    1. [ ] Execute petri net systems.
                    1. [ ] Expect target place health quantity -1.
        1. [ ] Only attack once in a period.  One of the following:
            1. [ ] Teeth have a timer to recharge negative health, analogous to attack speed in Clash Royale.
                1. [ ] One of the following:
                    1. [ ] Petri Net:
                        1. [ ] Timer inhibits transition to negative health.
                        1. [ ] Negative health inhibits timer.
                        1. [ ] Delta time transitions negative time into timer.
                        1. [ ] Collision is an input to transmitter.
                    1. [ ] Petri Transmission:
                        1. [ ] Timer inhibits negative health spawner.
                        1. [ ] Negative health inhibits timer.
                        1. [ ] Delta time transmits negative time into timer.
                        1. [ ] Collision is an input to transmitter.
                    - Petri Net and Petri Transmission are similar. Transmission needs explicit receiver to transmit from.
            1. [ ] Collision transmits negative health only if vectors of travel are incoming.
            1. [ ] Teeth inhibit negative health transmitter, until loop destination.
1. [ ] Health is destroyed.
1. [ ] Health was inhibiting pair of white hexagon transmitter.
1. [ ] Pair of white hexagon transmitter transmits two pair.
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
1. [ ] Player taps collider of selected receiver in node hierarchy containing oil.
1. [ ] Transmitter transforms selected and oil to transmit oil to connected entrance that receives consumable.
    1. [x] Develop one of the following for selectable card receiver:
        1. [x] Receiver configured to accept multiple entities; stored in a collection.
            1. [x] List of occupant IDs.  View sets element in list.
            1. [x] Maximum occupants sets capacity of list.  Filters not full instead of is empty.
        1. [ ] Receiver configured to accept infinite occupants.
        1. [ ] Component on selectable card receiver clears occupant ID after teleported.
        1. [ ] On time to spawn, receiver clears previous occupant ID.
        1. [ ] On distance, receiver has clears previous occupant ID.
        1. [ ] Spawn a new receiver for each selected card.
        1. [ ] Do not receive. Just teleport.
        1. [ ] Each tile receives occupant as the occupant passes through.
        1. [ ] Nav target clears occupant from selectable card receiver.
        1. [ ] Nav target takes occupant from selectable card receiver.
        1. [ ] Separate receiver for each transmitter.
        1. [ ] Each cell in nav grid has consumable receiver. Each cell adopts when consumable passes.

## Programmer flow

1. [x] Read relevant pathfinding classes.
1. [x] Read clear pathfinding variable name `WalkableNode` instead of `MyPathNode`.
1. [ ] Read more conventional static class suffix `Utils` instead of `Util`.
1. [ ] Bind net from editor to code.
1. [ ] Bind net from code to editor.

## Components

- [x] Receiver (similar to what a Petri Net calls a Place)
    - Filter component names
        - String for simple and stable serialization.
    - Occupant
        - Entity ID
- [x] Transmitter (similar to what a Petri Net calls a Transition)
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
- [x] nav agent
    - nav tilemap agent
- [x] trigger region 2D
    - kinematic rigid body <https://docs.unity3d.com/Manual/CollidersOverview.html>
    - collider 2D as trigger
        - mono behaviour listens to on trigger enter 2D.
    - event connects source entity ID and target entity ID of trigger.
Navigation system:
    - [x] reacts to trigger.
        - [ ] filters matching receiver.
            - [x] if no nav agent, adds nav agent.
            - [x] nav agent targets receiver, if nearer than current target.
            - [x] nav agent listener updates position.

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
    - Quantity
- Arc
    - Inhibitor arc
    - Quantity
- Transition
    - Inputs
    - Outputs
- Token

Introduction:
<https://en.wikipedia.org/wiki/Petri_net>

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

Modeling production with Petri Nets
<http://faculty.econ.ucdavis.edu/faculty/bonanno/PDF/petri>

Modeling Games with Petri Nets
<http://www.digra.org/wp-content/uploads/digital-library/09287.37256.pdf>

Petri Nets for Game Plot
<https://artemis.ms.mff.cuni.cz/main/papers/IVE-dramamanager-2006.pdf>

Petri Net Programming
<https://johncarlosbaez.wordpress.com/2012/10/01/petri-net-programming/>

Petri Nets Tools Database Quick Overview
<http://www.informatik.uni-hamburg.de/TGI/PetriNets/tools/quick.html>
