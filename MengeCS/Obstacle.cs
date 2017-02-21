using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MengeCS
{
    /// <summary>
    /// Definition of an "obstacle"; a closed loop of vertices. The obstacle knows its winding
    /// and can report if the *outside* or *inside* of the obstacle is the traversible region.
    /// </summary>
    public class Obstacle
    {
        internal Obstacle()
        {
            _points = new List<Vector3>();
            _walkOutside = true;
        }

        /// <summary>
        /// Reports the number of points in the obstacle
        /// </summary>
        /// <returns></returns>
        public int GetPointCount() { return _points.Count; }

        /// <summary>
        /// Returns the ith point in the obstacle.
        /// </summary>
        /// <param name="i">The index of the desired point.</param>
        /// <returns>The ith point.</returns>
        public Vector3 GetPoint(int i)
        {
            return _points[i];
        }

        public bool AgentsWalkOutside() { return _walkOutside; }

        /// <summary>
        /// Adds a point to the Obstacle; appending it to the end of the sequence.
        /// </summary>
        /// <param name="pt">The point to append.</param>
        internal void AddPoint(Vector3 pt) {
            _points.Add(pt);
        }

        /// <summary>
        /// Sets the walk outside state by computing the winding of the obstacle.
        /// </summary>
        internal void ComputeWinding()
        {
            if (_points.Count > 2)
            {
                Vector2 prev = GetXZ(_points[_points.Count - 1]);
                Vector2 curr = GetXZ(_points[0]);
                Vector2 next = GetXZ(_points[1]);
                float totalWinding = 0.0f;
                for (int i = 0; i < _points.Count - 1; ++i)
                {
                    totalWinding += ComputeAngle(prev, curr, next);
                    prev = curr;
                    curr = next;
                    next = GetXZ(_points[(i + 2) % _points.Count]);
                    //  If the value is > 0, I'm counter clockwise (and walk outsde = true), otherwise false.
                }
                totalWinding += ComputeAngle(prev, curr, next);
                _walkOutside = totalWinding >= 0.0f;
            }
            else
            {
                _walkOutside = true;
            }
        }

        /// <summary>
        /// Given three planar points p0, p1, and p2, computes the signed angle from the edge
        /// (p2, p1) to the edge (p0, p1).  It will be positive if it is counter-clockwise and
        /// negative if clockwise.
        /// </summary>
        /// <param name="p0">The vertex at the end of one edge.</param>
        /// <param name="p1">The common vertex to the two edges.</param>
        /// <param name="p2">The vertex at the end of the second edge.</param>
        /// <returns>The sine of the angle between the two implied edges.</returns>
        private float ComputeAngle(Vector2 p0, Vector2 p1, Vector2 p2)
        {
            Vector2 A = new Vector2(p2.X - p1.X, p2.Y - p1.Y);
            Vector2 B = new Vector2(p0.X - p1.X, p0.Y - p1.Y);
            float A_mag2 = A.X * A.X + A.Y * A.Y;
            float B_mag2 = B.X * B.X + B.Y * B.Y;
            float det = A.X * B.Y - A.Y * B.X;
            return (float)Math.Asin(det / Math.Sqrt(A_mag2 * B_mag2));
        }

        /// <summary>
        /// Creates a Vector2 from the x- and z-components of the given Vector3.
        /// </summary>
        /// <param name="v">The input vector.</param>
        /// <returns>The resultant 2D vector.</returns>
        private Vector2 GetXZ(Vector3 v)
        {
            return new Vector2(v.X, v.Z);
        }

        /// <summary>
        /// The sequence of points that form the obstacle.
        /// </summary>
        private List<Vector3> _points;

        /// <summary>
        /// If true, the obstacle defines a region *in* which agents cannot walk.  If false,
        /// the interior region is where agents *must* walk.
        /// </summary>
        private bool _walkOutside;
    }
}
