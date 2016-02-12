using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace E.T.Reloaded
{
    class CityLevel : GameLevel
    {
        private Texture city;
        private Sprite cityMesh;
        public CityLevel():base()
        {
            city = new Texture("../../Assets/City.png");
            cityMesh = new Sprite(city.Width, city.Height);
        }
        public override void Update()
        {
            base.Update();
            Draw();
        }
        public override void Draw()
        {
            cityMesh.DrawTexture(city);

        }
    }
}
