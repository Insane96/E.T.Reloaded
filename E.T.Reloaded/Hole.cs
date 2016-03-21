using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.T.Reloaded
{
    public class Hole
    {
        public Room Room { get; private set; }
        public Vector2 SpawingPoint { get; private set; }

        private Rect m_vArea;

        public Hole(Vector2 vPosition, float width, float height, Room hConnected, Vector2 spaw)
        {
            m_vArea = new Rect();
            m_vArea.Position = vPosition;
            m_vArea.Width = width;
            m_vArea.Height = height;
            Room = hConnected;
            SpawingPoint = spaw;
        }

      
        public bool CheckFall(Player player)
        {
            if ((player.EtMesh.position.X + player.EtMesh.Width >= m_vArea.Position.X) &&
                (player.EtMesh.position.X + player.EtMesh.Width <= m_vArea.Position.X + m_vArea.Width) &&
                (player.EtMesh.position.Y + player.EtMesh.Height >= m_vArea.Position.Y) &&
                (player.EtMesh.position.Y + player.EtMesh.Height <= m_vArea.Position.Y + m_vArea.Height))
                return true;
            else
                return false;
        }



        private struct Rect
        {
            public Vector2 Position;
            public float Width;
            public float Height;
        }
    }
}
