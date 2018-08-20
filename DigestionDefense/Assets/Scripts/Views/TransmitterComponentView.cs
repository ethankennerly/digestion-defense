using UnityEngine;

namespace FineGameDesign.Entitas
{
    public sealed class TransmitterComponentView : AGameComponentView<TransmitterComponent>
    {
        [SerializeField]
        private GameObject[] m_InputObjects = null;
        [SerializeField]
        private GameObject[] m_OutputObjects = null;

        public override void Initialize()
        {
            if (m_InputObjects != null)
                m_Component.inputIds = GameLinkUtils.TryLinkIds(m_InputObjects);
            if (m_OutputObjects != null)
                m_Component.outputIds = GameLinkUtils.TryLinkIds(m_OutputObjects);

            base.Initialize();
        }
    }
}
