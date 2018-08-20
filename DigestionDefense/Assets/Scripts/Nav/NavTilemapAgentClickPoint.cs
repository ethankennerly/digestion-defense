using FineGameDesign.Utils;
using System;
using UnityEngine;

namespace FineGameDesign.Nav
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

        private void Initialize()
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
            Initialize();
            UpdatePosition(m_Agent.position);
            m_Agent.onPositionChanged += UpdatePosition;
            ClickInputSystem.instance.onWorld += UpdateDestination;
        }

        private void OnDisable()
        {
            m_Agent.onPositionChanged -= UpdatePosition;
            ClickInputSystem.instance.onWorld -= UpdateDestination;
        }

        private void Update()
        {
            ClickInputSystem.instance.Update();
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
