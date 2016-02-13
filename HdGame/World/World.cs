using System;
using OpenTK;

namespace HdGame
{
    // this class is supposed to create all world/level's objects 
    // e.g. trees, candies, phones, decorations
    public class World
    {
        public Tuple<Vector2, Vector2> Boundings { get; }
        public World(Tuple<Vector2, Vector2> boundings = null)
        {
            Boundings = boundings ?? Tuple.Create(new Vector2(-500f, -500f), new Vector2(500f, 500f));
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