using UnityEngine;

namespace GamePlay.Scripts
{
    public class JellyVertex
    {
        public int ID;
        public Vector3 Position;
        public Vector3 Velocity, Force;

        public JellyVertex(int id, Vector3 pos)
        {
            ID = id;
            Position = pos;
        }
        public void Shake(Vector3 target, float m, float s, float d)
        {
            Force = (target - Position) * s;
            Velocity = (Velocity + Force / m) * d;
            Position += Velocity;
            if ((Velocity + Force +Force /m).magnitude < 0.0001f)
            {
                Position = target;
            }
        }
    }
}
