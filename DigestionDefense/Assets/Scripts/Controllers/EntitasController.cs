using Entitas;

namespace Finegamedesign.Entitas
{
    public sealed class EntitasController
    {
        private readonly Contexts m_Contexts;

        public Contexts contexts
        {
            get { return m_Contexts; }
        }

        private readonly Systems m_Systems;

        public Systems systems
        {
            get { return m_Systems; }
        }

        public EntitasController(Contexts contexts, Systems systems)
        {
            m_Contexts = contexts;
            ContextUtils.Subscribe(m_Contexts, true);

            m_Systems = systems;
        }

        ~EntitasController()
        {
            ContextUtils.Subscribe(m_Contexts, false);
        }

        public void Initialize()
        {
            m_Systems.Initialize();
        }

        public void TearDown()
        {
            if (m_Systems != null)
                m_Systems.TearDown();
        }

        public void Update()
        {
            if (m_Systems == null)
                return;

            m_Systems.Execute();
            m_Systems.Cleanup();
        }
    }
}
