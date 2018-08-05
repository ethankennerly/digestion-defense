using Entitas;
using NUnit.Framework;

namespace Finegamedesign.Entitas
{
    public class TestGameContextWithId
    {
        protected Contexts m_Contexts;
        protected GameContext m_Context;

        [SetUp]
        public void SetUpId()
        {
            m_Contexts = Contexts.sharedInstance;
            ContextUtils.Subscribe(m_Contexts, true);
            m_Context = m_Contexts.game;
        }

        [TearDown]
        public void TearDownId()
        {
            ContextUtils.Subscribe(m_Contexts, false);
            m_Contexts = null;
            m_Context = null;
        }
    }
}
