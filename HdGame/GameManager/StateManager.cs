using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HdGame
{
    public class StateManager
    {
        public int CurrentFrame { get; set; }
        public float Frequency { get; set; }
        public List<TexturePart> Textures { get; set; }
        public float LastTime { get; set; }
        public bool Loop { get; set; }
        public bool Playing { get; set; }
        public TexturePart CurrentTexture => Textures[CurrentFrame];

        public StateManager(float fps = 1f, bool loop = true)
        {
            Frequency = 1/fps;
            Loop = loop;
            Textures = new List<TexturePart>();
            Playing = true;
        }

        public StateManager Clone()
        {
            var clone = (StateManager) MemberwiseClone();
            if (Textures != null)
            {
                clone.Textures = new List<TexturePart>();
                foreach (var texture in Textures)
                {
                    clone.Textures.Add((TexturePart)texture.Clone());
                }
            }
            return clone;
        }
    }
}
