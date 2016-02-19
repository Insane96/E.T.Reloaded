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
            var pitPositions = new List<Vector2>
            {
                new Vector2(5f, 5f), new Vector2(-5f, 1f), new Vector2(-5f, -5f)
            };
            // 3 meters width? et is 1meter
            var genericPit = new Pit(3f, 3f * aratio);
            ((MeshRenderer) genericPit.GetComponent<MeshRenderer>()).CurrentTexture = pitTexture;

            for (var index = 0; index < pitPositions.Count; index++)
            {
                var position = pitPositions[index];
                var gobj = genericPit.Clone();
                gobj.Transform.Position = position;
                GameManager.Instance.AddObject($"pit{index}", gobj);
            }
        }

        private void MultipleSpawn(string name, GameObject generic, List<Vector2> positions)
        {
            for (var index = 0; index < positions.Count; index++)
            {
                var position = positions[index];
                var gobj = generic.Clone();
                gobj.Transform.Position = position;
                GameManager.Instance.AddObject($"{name}{index}", gobj);
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
        
        private void SpawnTrees()
        {
            var treeBottomTexture = new TexturePart("tree_1_bottom.png");
            var treeTopTextures = new List<TexturePart>
            {
                new TexturePart("tree_1_top_1.png"),
                new TexturePart("tree_1_top_2.png"),
                new TexturePart("tree_1_top_3.png"),
                new TexturePart("tree_1_top_4.png")
            };

            var bottomAratio = treeBottomTexture.Height / treeBottomTexture.Width;
            var treePositions = new List<Vector2>
            {
                new Vector2(9f, 1f), new Vector2(-1f, 11f), new Vector2(1.2f, -6f)
            };
            var treeSize = 3f;
            var genericTreeBottom = new GameObject { Order = 9 };
            var mr = (MeshRenderer) genericTreeBottom.AddComponent(new MeshRenderer(treeSize, treeSize*bottomAratio));
            var rigid = (RigidBody) genericTreeBottom.AddComponent(new RigidBody());
            rigid.Hitboxes.Add(
                new Bounds(
                    new Vector2(mr.Width / 2, mr.Height * 0.85f),
                    new Vector2(mr.Width * 0.4f, mr.Height * 0.15f)));
            mr.CurrentTexture = treeBottomTexture;

            for (var index = 0; index < treePositions.Count; index++)
            {
                var treePosition = treePositions[index];

                var treeTop = new GameObject
                {
                    Order = genericTreeBottom.Order
                };
                var treeMr = (MeshRenderer) treeTop.AddComponent(new MeshRenderer(treeSize, treeSize*bottomAratio));
                treeTop.Transform.Position = treePosition;
                treeMr.CurrentTexture = Utils.ChooseRandom(treeTopTextures);

                var treeBottom = genericTreeBottom.Clone();
                treeBottom.Transform.Position = treePosition;

                GameManager.Instance.AddObject($"treeBottom{index}", treeBottom);
                GameManager.Instance.AddObject($"treeTop{index}", treeTop);
            }
        }

        // static
        private void SpawnRoads()
        {
        }
    }
}