using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MengeCS
{
    class MengeWrapper
    {
        [DllImport("MengeCore")]
        public static extern bool InitSimulator([MarshalAs(UnmanagedType.LPStr)] String behaveFile,
                                                [MarshalAs(UnmanagedType.LPStr)] String sceneFile,
                                                [MarshalAs(UnmanagedType.LPStr)] String model,
                                                [MarshalAs(UnmanagedType.LPStr)] String pluginPath
                                                );

        [DllImport("MengeCore")]
        public static extern uint AgentCount();
        
        [DllImport("MengeCore")]
        public static extern float SetTimeStep(float timestep);

        [DllImport("MengeCore")]
        public static extern bool DoStep();

        [DllImport("MengeCore")]
        public static extern bool GetAgentPosition(uint i, ref float x, ref float y, ref float z);

        [DllImport("MengeCore")]
        public static extern bool GetAgentVelocity(uint i, ref float x, ref float y, ref float z);

        [DllImport("MengeCore")]
        public static extern bool GetAgentOrient(uint i, ref float x, ref float y );

        [DllImport("MengeCore")]
        public static extern int GetAgentClass(uint i);

        [DllImport("MengeCore")]
        public static extern float GetAgentRadius(uint i);

        [DllImport("MengeCore")]
        public static extern uint ObstacleCount();

        [DllImport("MengeCore")]
        public static extern uint GetNextObstacle(uint i);

        [DllImport("MengeCore")]
        public static extern bool GetObstacleEndPoints(uint i, ref float x0, ref float y0,
            ref float z0, ref float x1, ref float y1, ref float z1);

        [DllImport("MengeCore")]
        public static extern bool GetObstacleP0(uint i, ref float x0, ref float y0, ref float z0);

        [DllImport("MengeCore")]
        public static extern bool GetObstacleP1(uint i, ref float x1, ref float y1, ref float z1);
    }
}
