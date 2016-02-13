using System;
using OpenTK;

namespace HdGame
{
    // this class is supposed to create all world/level's objects 
    // e.g. trees, candies, phones, decorations
    public class World
    {
        public Bounds Boundings { get; }
        public World(Bounds boundings = null)
        {
            Boundings = boundings ?? new Bounds(Vector2.Zero, new Vector2(200f, 200f));
            SpawnTrees();
            SpawnRoads();
            SpawnCandies();
            SpawnPhones();
            SpawnDecorations();
        }

        // this *should* be procedural
        private void SpawnDecorations()
        {
        }

        // procedural through the world?
        private void SpawnPhones()
        {
        }

        // procedural through the world?
        private void SpawnCandies()
        {
        }

        // static, maybe procedural tree parts (NOT positions)
        private void SpawnTrees()
        {
        }

        // static
        private void SpawnRoads()
        {
        }
    }
}