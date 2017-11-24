using System;
using UnityEngine;

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

	private NavTilemapAgent m_Agent = new NavTilemapAgent();

	private Vector3 m_ClickPoint;

	private void Start()
	{
		m_Agent.nav = m_NavTilemapView.controller;
		m_Agent.position = transform.position;
	}

	private void Update()
	{
		UpdateDestination();
		m_Agent.Update(Time.deltaTime);
	}

	private void UpdateDestination()
	{
		if (!ClickPoint.Screen(out m_ClickPoint))
		{
			return;
		}
		m_Agent.destination = m_ClickPoint;
	}
}
