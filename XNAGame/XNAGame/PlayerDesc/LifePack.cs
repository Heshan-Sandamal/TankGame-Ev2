using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace XNAGame.PlayerDesc
{
    public class LifePack : GameObject
    {

        System.Timers.Timer timer;
        GameObject[,] map;
        
        public int LeftTime { get; set; }

        public void updateLifeTime(GameObject[,] map)
        {
            this.map = map;
            Thread thread = new Thread(new ThreadStart(() =>destroyOb() ));
            thread.Start();
        }

        private void destroyOb()
        {

            timer = new System.Timers.Timer();
            timer.Interval =LeftTime ;//set interval of one day
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            start_timer();
        }
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            map[this.LocationX, this.LocationY] = null;
            

        }
        private  void start_timer()
        {
            timer.Start();
        }
        

    }
}
