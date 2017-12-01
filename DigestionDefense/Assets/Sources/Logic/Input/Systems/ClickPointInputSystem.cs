using Entitas;

public sealed class ClickPointInputSystem : IInitializeSystem, ICleanupSystem, ITearDownSystem
{
    private readonly InputContext m_Context;
    private readonly IGroup<InputEntity> m_Inputs;

    public ClickPointInputSystem(Contexts contexts)
    {
        m_Context = contexts.input;
        m_Inputs = m_Context.GetGroup(InputMatcher.Input);
    }

    public void Initialize()
    {
        AddListeners();
    }

    public void TearDown()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        ClickPoint.onClickXY += AddInput;
    }

    private void RemoveListeners()
    {
        ClickPoint.onClickXY -= AddInput;
    }

    private void AddInput(float worldX, float worldY)
    {
        m_Context.CreateEntity()
            .AddInput((int)worldX, (int)worldY);
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
