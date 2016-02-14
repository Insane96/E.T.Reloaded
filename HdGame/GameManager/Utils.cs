using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace HdGame
{
    public static class Utils
    {
        public static readonly Random Random;
        static Utils()
        {
            Random = new Random();
        }

        public static float NextFloat(this Random random)
        {
            return (float) random.NextDouble();
        } 

        public static T ChooseRandom<T>(List<T> list)
        {
            return list[Random.Next(list.Count)];
        }

        public static Vector2 PickRandomPoint(Bounds chooseRandom)
        {
            return chooseRandom.Min + 
                new Vector2(Random.NextFloat()*chooseRandom.Size.X, Random.NextFloat() * chooseRandom.Size.Y);
        }

    }
}
