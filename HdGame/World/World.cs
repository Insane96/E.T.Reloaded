using System;
using System.Collections.Generic;
using OpenTK;

namespace HdGame
{
    // this class is supposed to create all world/level's objects 
    // e.g. trees, candies, phones, decorations
    public class World
    {
        public static Bounds Boundings { get; private set; }
        public World(Bounds boundings = null)
        {
            Boundings = boundings ?? new Bounds(Vector2.Zero, new Vector2(200f, 200f));
            SpawnTrees();
            SpawnRoads();
            SpawnCandies();
            SpawnPhones();
            SpawnDecorations();
            SpawnPits();
        }

        private void SpawnPits()
        {
            // static?
            var pitTexture = new TexturePart("pit.png");
            var aratio = pitTexture.Height/pitTexture.Width;
            var pitPositions = new List<Vector2>()
            {
                new Vector2(5f, 5f), new Vector2(-5f, 1f), new Vector2(-5f, -5f)
            };
            // 3 meters width? et is 1meter
            var genericPit = new Pit(3f, 3f * aratio);
            genericPit.States.Add(new StateManager(5) { Textures = new List<TexturePart> {pitTexture} });
            for (var index = 0; index < pitPositions.Count; index++)
            {
                var position = pitPositions[index];
                var pit = genericPit.Clone();
                pit.position = position;
                GameManager.Instance.AddObject($"pit{index}", pit);
            }
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