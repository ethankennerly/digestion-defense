using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class ReceiverComponentView : AGameComponentView<ReceiverComponent>
    {
        [SerializeField]
        private GameObject m_OccupantObject = null;

        protected override void Initialize()
        {
            if (m_OccupantObject != null)
                m_Component.occupantId = GameLinkUtils.TryLink(m_OccupantObject).id.value;

            base.Initialize();
        }
    }
}
