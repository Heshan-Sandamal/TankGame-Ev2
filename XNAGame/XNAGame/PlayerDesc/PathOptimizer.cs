﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAGame.PlayerDesc
{
    class PathOptimizer
    {
        public void FireAtWill(){
            
        }

        private Player getCurrentPlayerDetails()
        {
            return new Player();
        }
        private Player[] getOtherPlayerDetails()
        {
            return new Player[4];
        }

        private bool checkReachability(int cost, LifePack lifePack)
        {
            
            if (cost+3 > lifePack.LeftTime)
            {
                return false;
            }
            foreach (Player p in getOtherPlayerDetails())
            {
                if (IsInZone(p, lifePack))
                {
                    return false;
                }
                else return true;
            }
            return false;

        }

        private bool IsInZone(GameObject a, GameObject b)
        {

            if (Math.Abs(a.LocationX - b.LocationX) < 2 && Math.Abs(a.LocationY - b.LocationY) < 2)
            {
                return true;
            }
            else return false;
        }

        private bool IsSafeToTravel(int cost)
        {
            if (getCurrentPlayerDetails().health < 40 && cost > 7)
            {
                return false;
            } return true;
        }

        private bool IsEvasive()
        {
            if (getCurrentPlayerDetails().health < 10)
            {
                return true;
            }
            else return false;
        }

        public bool IntereptX()
        {
            if (!IsEvasive())
            {
                int x=getCurrentPlayerDetails().LocationX;
                foreach(Player p in getOtherPlayerDetails()){
                    if (x == p.LocationX)
                    {
                        if (checkDLF(x, p.LocationX))
                        {
                            return true;
                        }
                        else return false;
                    }
                }

            } return false;
        }

        public bool IntereptY()
        {
            if (!IsEvasive())
            {
                int y = getCurrentPlayerDetails().LocationY;
                foreach (Player p in getOtherPlayerDetails())
                {
                    if (y == p.LocationY)
                    {
                        if (checkDLF(y, p.LocationY))
                        {
                            return true;
                        }
                        else return false;
                    }
                }

            } return false;
        }
        private bool checkDLF(int x, int p)
        {
            
            return false;
        }


        public static void findPath(GameObject self, int x, int y,String[,] map)
        {
            Player player=(Player) self;
            int playerX = player.LocationX;
            int playerY = player.LocationY;

            if (map[playerX, playerY + 1] == "N")
            {
                if (!player.Direction.Equals(Enums.Directions.SOUTH)) {
                    XNAGame.Client.ClientConnectionInit.sendData("SOUTH#");
                }
                XNAGame.Client.ClientConnectionInit.sendData("SOUTH#");
            }
            else if (map[playerX+1,playerY]=="N")
            {
                if (!player.Direction.Equals(Enums.Directions.EAST))
                {
                    XNAGame.Client.ClientConnectionInit.sendData("EAST#");
                }
                XNAGame.Client.ClientConnectionInit.sendData("EAST#");
            }

            else if (map[playerX, playerY + 1] == "B")
            {
                if (!player.Direction.Equals(Enums.Directions.SOUTH))
                {
                    XNAGame.Client.ClientConnectionInit.sendData("SOUTH#");
                }

                XNAGame.Client.ClientConnectionInit.sendData("SHOOT#");
                XNAGame.Client.ClientConnectionInit.sendData("SHOOT#");
                XNAGame.Client.ClientConnectionInit.sendData("SHOOT#");
                XNAGame.Client.ClientConnectionInit.sendData("SHOOT#");
                XNAGame.Client.ClientConnectionInit.sendData("SOUTH#");
            }
            else if (map[playerX + 1, playerY] == "B")
            {
                if (!player.Direction.Equals(Enums.Directions.EAST))
                {
                    XNAGame.Client.ClientConnectionInit.sendData("EAST#");
                }

                XNAGame.Client.ClientConnectionInit.sendData("SHOOT#");
                XNAGame.Client.ClientConnectionInit.sendData("SHOOT#");
                XNAGame.Client.ClientConnectionInit.sendData("SHOOT#");
                XNAGame.Client.ClientConnectionInit.sendData("SHOOT#");
                XNAGame.Client.ClientConnectionInit.sendData("EAST#");
            }
            else if (!(map[playerX - 1, playerY] == "W" || map[playerX - 1, playerY] == "S"))
            {
                if (!player.Direction.Equals(Enums.Directions.WEST))
                {
                    XNAGame.Client.ClientConnectionInit.sendData("WEST#");
                }
                XNAGame.Client.ClientConnectionInit.sendData("WEST#");

            }

        }
        private double findDistance(int originX, int originY, int desX, int desY)
        {
            return Math.Sqrt((double)((originX - desX) << 2 + (originX - desX) << 2));
        }
    }
}
