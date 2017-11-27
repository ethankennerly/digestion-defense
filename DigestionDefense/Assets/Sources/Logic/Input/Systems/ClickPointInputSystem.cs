using Entitas;
using Vector3 = UnityEngine.Vector3;

public sealed class ClickPointInputSystem : ICleanupSystem
{
    private readonly InputContext m_Context;
    private readonly IGroup<InputEntity> m_Inputs;

    public ClickPointInputSystem(Contexts contexts)
    {
        m_Context = contexts.input;
        m_Inputs = m_Context.GetGroup(InputMatcher.Input);
        AddListeners();
    }

    ~ClickPointInputSystem()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        ClickPoint.onClick += AddInput;
    }

    private void RemoveListeners()
    {
        ClickPoint.onClick -= AddInput;
    }

    private void AddInput(Vector3 position)
    {
        m_Context.CreateEntity()
            .AddInput((int)position.x, (int)position.y);
    }

    public void Cleanup()
    {
        InputEntity[] inputEntities = m_Inputs.GetEntities();
        foreach (InputEntity entity in inputEntities)
        {
            entity.Destroy();
        }
    }
}
