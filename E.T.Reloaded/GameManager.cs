using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E.T.Reloaded
{
    class GameManager
    {
        public Window window { get; private set; }
       

        private static GameManager instance = null;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        private GameManager()
        {
            window = new Window(1280, 720, "oldE_T");
            
        }
        public void Update()
        {
            while(window.opened)
            {
                
                window.Update();
            }
    }
}
