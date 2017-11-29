## Linking MonoBehaviours

Serialize the Entitas component.

Create a component view.

In Unity editor, drag and drop serial components.

Example:  GridPositionComponentView.cs

For another example, GridPositionBehaviour.cs does not work, because:

- Cannot instantiate MonoBehaviour out of a game object.
- Expensive to attach to a game object or get from a game object.

## To-do

- [x] Attach example component and view in editor.
- [ ] Auto-link Entitas entity.
- [ ] Auto-update fields from Entitas to view.
- [ ] Auto-update fields from view to Entitas.
