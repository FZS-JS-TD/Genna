using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genna
{
    public class ParticleTimer
    {
        public int time;
        bool up = false;
     
        public ParticleTimer()
        {
            time = 0;
        }

        public void Update()
        {
            if(up)
                time++;
            if (!up)
                time--;
            if (time >= 100)
            {
                time--;
                up = !up;
            }
            if (time <= -100)
            {
                time++;
                up = !up;
            }

        }
    }
}
