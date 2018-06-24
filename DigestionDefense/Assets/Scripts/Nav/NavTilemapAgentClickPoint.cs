using Finegamedesign.Utils;
using System;
using UnityEngine;

namespace Finegamedesign.Nav
{
    public sealed class NavTilemapAgentClickPoint : MonoBehaviour
    {
        [SerializeField]
        private NavTilemapView m_NavTilemapView;

        public NavTilemapView navTilemapView
        {
            get
            {
                return m_NavTilemapView;
            }
            set
            {
                m_NavTilemapView = value;
            }
        }

        [SerializeField]
        private NavTilemapAgent m_Agent = new NavTilemapAgent();

        private Vector3 m_ClickPoint;

        private void Setup()
        {
            if (m_Agent.nav != null)
            {
                return;
            }
            m_Agent.nav = m_NavTilemapView.controller;
            m_Agent.position = transform.position;
        }

        private void OnEnable()
        {
            Setup();
            UpdatePosition(m_Agent.position);
            m_Agent.onPositionChanged += UpdatePosition;
            ClickSystem.instance.onWorld += UpdateDestination;
        }

        private void OnDisable()
        {
            m_Agent.onPositionChanged -= UpdatePosition;
            ClickSystem.instance.onWorld -= UpdateDestination;
        }

        private void Update()
        {
            ClickSystem.instance.Update();
            m_Agent.Update(Time.deltaTime);
        }

        private void UpdateDestination(Vector3 destination)
        {
            m_Agent.destination = destination;
        }

        private void UpdatePosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
