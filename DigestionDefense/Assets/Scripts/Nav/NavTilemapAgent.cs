using Finegamedesign.Utils;
using SettlersEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Finegamedesign.Nav
{
    [Serializable]
    public sealed class NavTilemapAgent
    {
        [SerializeField]
        private bool m_AllowsDiagonals = true;

        public bool allowsDiagonals
        {
            get { return m_AllowsDiagonals; }
            set
            {
                m_AllowsDiagonals = value;
                if (m_Solver == null)
                {
                    return;
                }
                CreateSolver();
            }
        }

        private bool m_HasDestination = false;

        public bool hasDestination
        {
            get { return m_HasDestination; }
            set { m_HasDestination = value; }
        }

        private Vector3 m_Destination;

        // Does not interrupt a tween of a step.
        public Vector3 destination
        {
            get { return m_Destination; }
            set
            {
                if (m_Destination == value)
                    return;

                m_Destination = value;
                SetDestinationCell(m_Destination);
                if (m_Path != null)
                {
                    return;
                }
                FindPathInWorld();
            }
        }

        public event Action<Vector3> onPositionChanged;

        private Vector3 m_Position;

        public Vector3 position
        {
            get { return m_Position; }
            set
            {
                if (m_Position == value)
                {
                    return;
                }
                m_Position = value;
                SetCurrentCell(value);
                if (onPositionChanged == null)
                {
                    return;
                }
                onPositionChanged(value);
            }
        }

        private NavTilemapController m_Nav;

        public NavTilemapController nav
        {
            get { return m_Nav; }
            set { m_Nav = value; }
        }

        private Vector2Int m_CurrentCell = new Vector2Int();

        private Vector2Int m_DestinationCell = new Vector2Int();

        private SpatialAStar<MyPathNode, object> m_Solver;

        private IEnumerable<MyPathNode> m_Path;

        public bool hasPath
        {
            get { return m_Path != null; }
        }

        /// <summary>
        /// Error of path does not exist.
        /// </summary>
        public int pathDistance
        {
            get { return GetNumSteps(m_Path); }
        }

        private float m_TweenTime = 0f;
        private float m_StepDuration = 0.25f;
        private Vector3 m_PreviousStep;
        private Vector3 m_NextStep;

        private bool m_IsVerbose = true;

        private void SetCurrentCell(Vector3 positionInWorld)
        {
            if (m_Nav == null || m_Nav.tilemap == null)
                return;

            m_CurrentCell = m_Nav.WorldToGrid(positionInWorld);
        }

        private void SetDestinationCell(Vector3 destinationInWorld)
        {
            if (m_Nav == null || m_Nav.tilemap == null)
                return;

            m_DestinationCell = m_Nav.WorldToGrid(destinationInWorld);
            m_HasDestination = true;
            if (m_IsVerbose)
                DebugUtil.Log(this + ".SetDestinationCell: " + m_DestinationCell +
                    " destinationInWorld=" + destinationInWorld +
                    " current position=" + position
                );
        }

        private void FindPathInWorld()
        {
            if (m_IsVerbose)
                Debug.Log("NavTilemapAgent.FindPath: From "
                    + m_Position + " to " + m_Destination);

            SetCurrentCell(m_Position);
            FindPath();
        }

        private void CreateSolver()
        {
            m_Solver = new SpatialAStar<MyPathNode, object>(m_Nav.grid);
            m_Solver.allowsDiagonals = m_AllowsDiagonals;
            m_PreviousStep = position;
            m_NextStep = position;
        }

        public static int GetNumSteps<T>(IEnumerable<T> path)
        {
            int numSteps = 0;
            foreach (T step in path)
                ++numSteps;

            return numSteps;
        }

        public IEnumerable<MyPathNode> GetPath(Vector3 destination)
        {
            if (m_Solver == null)
                CreateSolver();

            return m_Solver.Search(
                (Vector2)m_CurrentCell,
                (Vector2)m_DestinationCell,
                null);
        }

        private void FindPath()
        {
            if (m_Solver == null)
                CreateSolver();

            m_Path = m_Solver.Search(
                (Vector2)m_CurrentCell,
                (Vector2)m_DestinationCell,
                null);
            if (m_Path == null)
                return;

            SetNextStep();
            if (m_IsVerbose)
                Debug.Log("NavTilemapAgent.FindPath: From "
                    + m_CurrentCell + " to " + m_DestinationCell);
        }

        public void Update(float deltaTime)
        {
            if (!m_HasDestination)
                return;

            if (deltaTime <= 0.0f)
                return;

            if (!TweenStep(deltaTime))
                return;

            FindPath();
        }

        private bool TweenStep(float deltaTime)
        {
            if (m_Path == null)
                return false;

            if (deltaTime <= 0.0f)
                return false;

            position = Vector3.Lerp(m_PreviousStep, m_NextStep, m_TweenTime / m_StepDuration);
            m_TweenTime += deltaTime;
            if (m_TweenTime < m_StepDuration)
                return false;

            m_TweenTime -= m_StepDuration;
            return true;
        }

        private void SetNextStep()
        {
            int index = 0;
            MyPathNode nextNode = null;
            // XXX It would be more efficient if path were a list or an array rather than IEnumerable.
            foreach (MyPathNode node in m_Path)
            {
                if (index == 1)
                {
                    nextNode = node;
                    break;
                }
                ++index;
            }
            if (nextNode == null)
            {
                position = m_NextStep;
                m_Path = null;
                m_HasDestination = false;
                return;
            }
            Vector2Int cell = new Vector2Int(nextNode.X, nextNode.Y);
            m_PreviousStep = m_NextStep;
            m_NextStep = m_Nav.GridToWorld(cell);
        }
    }
}
