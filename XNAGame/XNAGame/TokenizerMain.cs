﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using XNAGame.PlayerDesc;
using XNAGame.PlayerDesc.Client;
namespace XNAGame
{
    public class TokenizerMain
    {
        static char[] colonArray = { ':' };
        static char[] commaArray = { ',' };
        static char[] semicolonArray = { ';' };
        public static int PLAYER_NO;
        public static ArrayList playerList;
        string[,] terrain = new string[10, 10];
        static GameObject[,] map= new GameObject[10,10];
        private static List<Player> playerarray=null;
        public static int selfNumber;
        public Player JoinReply(String Reply)
        {
            Player self;    
            if (Reply.EndsWith("#"))
            {
                self = new Player();
                // --TODO--Remove the trailing # sign 
                Reply = Reply.Remove(Reply.Length - 1);
                //find the player number 
                String[] splitted = Reply.Split(colonArray);
                if (splitted[1].Substring(0).StartsWith("P", true, null))
                {
                    self.Number = int.Parse(splitted[1].Substring(1));
                    
                }
                // initial player Location x and y
                String[] Locations = splitted[2].Split(commaArray);
                self.LocationX = int.Parse(Locations[0]);
                self.LocationY = int.Parse(Locations[1]);
                //player initial direction.
                //--TODO-- directions must be mapped into enums with the correcty directions.
                self.Direction = (Enums.Directions)Enum.Parse(typeof(Enums.Directions), splitted[3]);
                return self;
            }
            else
            {
                return null;
            }

        }
        public Hashtable MapInitializer(String MapDetails)
        {
            Hashtable MapTerrain = new Hashtable();
            if (MapDetails.EndsWith("#"))
            {
                CreateTerrain();
                //--TODO-- Remove the trailing #
                MapDetails = MapDetails.Remove(MapDetails.Length - 1);
                String[] splitted = MapDetails.Split(colonArray);
                //Add Player number to HT 
                if (splitted[1].Substring(0).StartsWith("P", true, null))
                {
                    PLAYER_NO = int.Parse(splitted[1].Substring(1));
                    TokenizerMain.selfNumber = PLAYER_NO;
                    MapTerrain.Add("PlayerNumber",PLAYER_NO );
                }
                String[] bricksLocations = splitted[2].Split(semicolonArray);
                MapTerrain.Add("BrickLocations", bricksLocations);
                String[] stoneLocations = splitted[3].Split(semicolonArray);
                MapTerrain.Add("StoneLocations", stoneLocations);
                String[] waterLocations = splitted[4].Split(semicolonArray);
                MapTerrain.Add("WaterLocations", waterLocations);
                createObjects(bricksLocations, "B");
                createObjects(stoneLocations, "S");
                createObjects(waterLocations, "W");
                DisplayTerrain();
                return MapTerrain;
            }
            else
            {
                return null;
            }


        }
        public void createObjects(String[] locations,String type)
        {
            //map= new GameObject[10,10];
            String[] temp;
            foreach (String item in locations)
            {
                temp = item.Split(commaArray);
                if (type == "B")
                {
                    Brick brick = new Brick();
                    brick.LocationX = int.Parse(temp[0]);
                    brick.LocationY = int.Parse(temp[1]);
                    brick.health = 0;
                    brick.Type = Enums.Type.BRICKS;
                    brick.id = brick.LocationX + ":" + brick.LocationY;
                    map[int.Parse(temp[0]), int.Parse(temp[1])] =brick;
                    //Graph.vertexes.Add(brick);
                }
                else if (type == "S")
                {
                    Stone stone = new Stone();
                    stone.LocationX = int.Parse(temp[0]);
                    stone.LocationY = int.Parse(temp[1]);
                    stone.Type = Enums.Type.STONE;
                    map[int.Parse(temp[0]), int.Parse(temp[1])] = stone;
                    
                }
                else if(type=="W"){
                    Water water = new Water();
                    water.LocationX = int.Parse(temp[0]);
                    water.LocationY = int.Parse(temp[1]);
                    water.Type = Enums.Type.WATER;
                    map[int.Parse(temp[0]), int.Parse(temp[1])] = water;

                }
                
            }
            
        }
        public GameObject[,] getTerrainInitializationArray()
        {
            /*Brick brick = new Brick();
            brick.LocationX = 2;
            brick.LocationY = 3;
            brick.health = 7;
            map[2,3] = brick;

            CoinPile coins = new CoinPile();
            coins.LocationX = (2);
            coins.LocationY = (7);
            //Get the time left with the coin pile.
            coins.LeftTime = (12);
            coins.Type = Enums.Type.COIN;

            map[4, 5] = coins;
            */
            return map;
        }

        public CoinPile AquireCoins(String CoinDetails)
        {
            CoinPile Coins = new CoinPile();
            if (CoinDetails.EndsWith("#"))
            {
                CoinDetails = CoinDetails.Remove(CoinDetails.Length - 1);
                String[] CoinDrop = CoinDetails.Split(colonArray);
                //Get the coin location.
                String[] Locations = CoinDrop[1].Split(commaArray);
                Coins.LocationX = (int.Parse(Locations[0]));
                Coins.LocationY = (int.Parse(Locations[1]));
                //Get the time left with the coin pile.
                Coins.LeftTime = (int.Parse(CoinDrop[2]));
                //Value of the coin pile 
                Coins.Value = int.Parse(CoinDrop[3]);
                Coins.Type = Enums.Type.COIN;
                Coins.id = Coins.LocationX + ":" + Coins.LocationY;
                map[Coins.LocationX, Coins.LocationY] = Coins;
                Coins.updateLifeTime(map);
                Console.WriteLine("--------- Coins received at X: " + Coins.LocationX + " Y: " + Coins.LocationY + " --------------");
                return Coins;
            }
            else
            {
                return null;
            }

        }
        public LifePack LifePacks(String LifepackDetails)
        {
            LifePack lifepack = new LifePack();
            if (LifepackDetails.EndsWith("#"))
            {
                LifepackDetails = LifepackDetails.Remove(LifepackDetails.Length - 1);
                String[] LifePackDrop = LifepackDetails.Split(colonArray);
                String[] Locations = LifePackDrop[1].Split(commaArray);
                lifepack.LocationX = int.Parse(Locations[0]);
                lifepack.LocationY = int.Parse(Locations[1]);
                //Get the left time
                lifepack.LeftTime = int.Parse(LifePackDrop[2]);
                Console.WriteLine("--------- Life packs received at X: " + lifepack.LocationX + " Y: " + lifepack.LocationY + " --------------");
                lifepack.Type = Enums.Type.LIFE;
                lifepack.id = lifepack.LocationX + ":" + lifepack.LocationY;
                map[lifepack.LocationX, lifepack.LocationY] = lifepack;
                lifepack.updateLifeTime(map);
                return lifepack;
            }
            else
            {
                return null;
            }
        }
        public bool GameEnded(string End)
        {
            if (End.EndsWith("#") && End.Equals("GAME_FINISHED#"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ArrayList UpdatePlayerStats(String dataString)
        {
   
            if (dataString.EndsWith("#"))
            {
                String[] splitted = dataString.Split(colonArray);
                playerList = new ArrayList();
                //Add player details 
                

                if(playerarray!=null){
                    foreach (Player p in playerarray)
                    {
                        if(p!=null){
                            map[p.LocationX, p.LocationY] = null;                        
                        }
                        

                    }
                }


                playerarray = new List<Player>();


                for (int i = 1; i < splitted.Length; i++)
                {
                   
                    Player player = new Player();
                    String[] data = splitted[i].Split(semicolonArray);
                    String[] location = data[1].Split(commaArray);
                    player.LocationX = int.Parse(location[0]);
                    player.LocationY = int.Parse(location[1]);
                    player.Direction = (Enums.Directions)int.Parse(data[2]);
                    player.Shot = int.Parse(data[3]);// Dont know whether this is bool or not --TODO-- find type
                    player.health = int.Parse(data[4]);
                    player.Coins = int.Parse(data[5]);
                    player.points = int.Parse(data[6]);
                    player.id = player.LocationX + ":" + player.LocationY;
                    player.Type = Enums.Type.PLAYER;
                    playerList.Add(player);
                    playerarray.Add(player);

                    if((i-1)==selfNumber){
                        player.playerType = 1;
                    }
                    // For demo only


                    map[int.Parse(location[0]), int.Parse(location[1])] = player;

                    

                    Console.WriteLine("==========Player " + player.Number + " Details===========");
                    Console.WriteLine("Player Location X " + player.LocationX);
                    Console.WriteLine("Player Location Y " + player.LocationY);
                    Console.WriteLine("Player Direction " + player.Direction);
                    Console.WriteLine("Player Shot? " + player.Shot);
                    Console.WriteLine("Player health " + player.health);
                    Console.WriteLine("Player Coins " + player.Coins);
                    Console.WriteLine("Player points " + player.points);





                }
                return playerList;
            }
            else
            {
                return null;
            }

        }
        public ArrayList UpdateMap(String MapDetails)
        {
            ArrayList Map;
            Coordinate coord;
            try
            {
                if (MapDetails.EndsWith("#"))
                {
                    MapDetails = MapDetails.Remove(MapDetails.Length - 1);
                    String[] splitted = MapDetails.Split(colonArray);
                    Map = new ArrayList();
                    String[] temp = splitted[2].Split(semicolonArray);
                    for (int i = 0; i < temp.Length; i++)
                    {
                        coord = new Coordinate();
                        String[] location = temp[i].Split(commaArray);
                        coord.XCoordinate = int.Parse(location[0]);
                        coord.YCoordinate = int.Parse(location[1]);
                        coord.Damage = int.Parse(location[2]);
                        Console.WriteLine("Updating Location :" + coord.XCoordinate + "," + coord.YCoordinate + "Damage is :" + coord.Damage);
                        Map.Add(coord);
                    }
                    return Map;
                }
                else
                {
                    return null;
                }
            }catch(IndexOutOfRangeException e){
                Console.Write(e.StackTrace);
                return null;
            }


        }
        // Following methods are for testing and demonstration purposes only..
        //Should be and will be removed once the game engine integration comes.
        public void CreateTerrain()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    terrain[i, j] = "N";
                }
            }
        }

        public void DisplayTerrain()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(terrain[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void PopulateTerrain(String[] a, String sign)
        {
            String[] temp;
            foreach (String item in a)
            {
                temp = item.Split(commaArray);
                terrain[int.Parse(temp[1]), int.Parse(temp[0])] = sign;
            }

        }




    }
}

