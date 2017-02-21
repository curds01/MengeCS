using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MengeCS
{
    /// <summary>
    /// Wrapper for Menge::SimulatorBase
    /// </summary>
    public class Simulator
    {   
        /// <summary>
        /// Constructor.
        /// </summary>
        public Simulator() {
            _agents = new List<Agent>();
            _obstacles = new List<Obstacle>();
        }

        public bool Initialize(String behaveXml, String sceneXml, String model)
        {
            if (MengeWrapper.InitSimulator(behaveXml, sceneXml, model, null))
            {
                InitializeAgents();
                InitializeObstacles();
                return true;
            }
            else
            {
                System.Console.WriteLine("Failed to initialize simulator.");
            }
            return false;
        }

        /// <summary>
        /// The number of agents in the simulation.
        /// </summary>
        public int AgentCount { get { return _agents.Count;} }

        /// <summary>
        /// Returns the ith agent.
        /// </summary>
        /// <param name="i">Index of the agent to retrieve.</param>
        /// <returns>The ith agent.</returns>
        public Agent GetAgent( int i ) {
            return _agents[i];
        }

        /// <summary>
        /// The simulation time step.
        /// </summary>
        public float TimeStep
        {
            get { return _timeStep; }
            set { _timeStep = value; MengeWrapper.SetTimeStep(_timeStep); }
        }

        /// <summary>
        /// Advances the simulation by the current time step.
        /// </summary>
        /// <returns>True if evaluation is successful and the simulation can proceed.</returns>
        public bool DoStep() {
            bool running = MengeWrapper.DoStep();
            for (int i = 0; i < _agents.Count; ++i) {
                float x = 0, y = 0, z = 0;
                MengeWrapper.GetAgentPosition((uint)i, ref x, ref y, ref z);
                _agents[i].Position.Set(x, y, z);
                MengeWrapper.GetAgentOrient((uint)i, ref x, ref y);
                _agents[i].Orientation.Set(x, y);
            }
            return true;
        }

        /// <summary>
        /// Reports the number of obstacles.
        /// </summary>
        /// <returns>The number of obstacles in the simulation.</returns>
        public int GetObstacleCount() { return _obstacles.Count; }

        /// <summary>
        /// Gets the ith obstacle from the set.
        /// </summary>
        /// <param name="i">Index of the desired obstacle.</param>
        /// <returns>The ith obstacle.</returns>
        public Obstacle GetObstacle(int i) { return _obstacles[i]; }

        /// <summary>
        /// Initializes the agents from the properly initialized simulator.
        /// </summary>
        private void InitializeAgents()
        {
            uint count = MengeWrapper.AgentCount();
            for (uint i = 0; i < count; ++i)
            {
                Agent agt = new Agent();
                float x = 0;
                float y = 0;
                float z = 0;
                MengeWrapper.GetAgentPosition(i, ref x, ref y, ref z);
                agt._pos = new Vector3(x, y, z);
                MengeWrapper.GetAgentOrient(i, ref x, ref y);
                agt._orient = new Vector2(x, y);
                agt._class = MengeWrapper.GetAgentClass(i);
                agt._radius = MengeWrapper.GetAgentRadius(i);
                _agents.Add(agt);
            }
        }

        /// <summary>
        /// Initializes the obstacles from the properly initialized simulator.
        /// </summary>
        private void InitializeObstacles()
        {
            uint count = MengeWrapper.ObstacleCount();
            List<int> visited = Enumerable.Repeat<int>(-1, (int)count).ToList<int>();
            // Create set of all menge obstacles visited (to recognize loops).
            uint idx = 0;
            while (idx < count && visited[(int)idx] == -1)
            {
                int oId = _obstacles.Count;
                visited[(int)idx] = oId;
                float x = 0;
                float y = 0;
                float z = 0;
                MengeWrapper.GetObstacleP0(idx, ref x, ref y, ref z);
                Obstacle o = new Obstacle();
                o.AddPoint(new Vector3(x, y, z));
                uint next = MengeWrapper.GetNextObstacle(idx);
                while (true)
                {
                    int state = visited[(int)next];
                    if (state < 0)
                    {
                        // Unvisited obstacle that belongs to this point.
                        visited[(int)next] = oId;
                        MengeWrapper.GetObstacleP0(next, ref x, ref y, ref z);
                        o.AddPoint(new Vector3(x, y, z));
                        next = MengeWrapper.GetNextObstacle(next);
                    }
                    else if (state == oId)
                    {
                        // Done collecting points; I've closed the loop.
                        break;
                    }
                    else
                    {
                        // This is a problem; I'm building an Obstacle using a Menge obstacle
                        // that was used by another Obstacle.  Throw an exception?
                        break; // break for now.
                    }
                }
                o.ComputeWinding();
                _obstacles.Add(o);
                // Scan forward to the next obstacle
                while (idx < count && visited[(int)idx] != -1)
                {
                    ++idx;
                }
            }
        }

        /// <summary>
        /// The current simulation time step.
        /// </summary>
        private float _timeStep = 0.1f;

        /// <summary>
        /// The agents in the simulation.
        /// </summary>
        private List<Agent> _agents;

        /// <summary>
        /// The obstacles in the simulation.
        /// </summary>
        private List<Obstacle> _obstacles;
    }
}
