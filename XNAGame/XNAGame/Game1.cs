using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XNAGame.PlayerDesc;
using XNAGame.Client;
using XNAGame.util;
using System.Threading;

namespace XNAGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public struct PlayerData
    {
        public Vector2 Position;
        public bool IsAlive;
        public Color Color;
        public Enums.Type type;
        public Enums.Directions Direction;
        public int user;
        public double harm;

    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private static TokenizerMain torkenizerMain;
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        int screenWidth;
        int screenHeight;
        int numberOfPlayers;
        long lastPress;
        PlayerData[] playerArray;
        Texture2D pixel;
        Texture2D backgroundTexture;
        private Texture2D carriageTexture;
        private GameObject[,] map = null;
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            

            map = torkenizerMain.getTerrainInitializationArray();

            
            



            
            

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            graphics.PreferredBackBufferWidth = 650;
            graphics.PreferredBackBufferHeight = 454;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Tank Game- Eliminators";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;

            backgroundTexture=Content.Load<Texture2D>("tankbackground");

            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight= device.PresentationParameters.BackBufferHeight;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        private static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

       
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Console.WriteLine("updating");
            // TODO: Add your update logic here
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            base.Update(gameTime);


            //
            

            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawScenery();
            DrawPlayers();

            Rectangle titleSafeRectangle = GraphicsDevice.Viewport.TitleSafeArea;

            DrawBorder(titleSafeRectangle, 4, Color.Silver);
            spriteBatch.End();

            base.Draw(gameTime);


            Thread thread = new Thread(new ThreadStart(() => updatePlayerLocation()));
            thread.Start();

            
        }


        private void updatePlayerLocation() {
            if (lastPress + 1200 < CurrentTimeMillis())
            {
                //msgSender.left();
                //gameAI.move()//////////;

                //ClientConnectionInit.sendData(Constant.RIGHT);
                //callDigis();
               
                try {
                    Console.WriteLine(Graph.vertexes.Count + "this is countttttttttttttttttttttttttttttttt");
                    callDigis();
                    
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                    //throw e;
                }
                
                lastPress = CurrentTimeMillis();
            }
        }

        private void DrawBorder(Rectangle titleSafeRectangle, int thickness, Color color)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(480, 35, 150, thickness), color);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(480, 35, thickness, 170), color);

            spriteBatch.Draw(pixel, new Rectangle(480, 205, 150, thickness), color);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(630, 35, thickness, 174), color);
        }

        private void DrawScenery()
        {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
        }


       public static void setTorkenizerMain(TokenizerMain t) {
            torkenizerMain = t;
        }



       private void addEdges(int i,int j,GameObject gameOb) {
           GameObject g;
           try{
               if (map[i - 1, j] == null)
               {
                   g=new GameObject();
                   g.LocationX = i - 1;
                   g.LocationY = j;
                   Graph.edges.Add(new Edge(i+100*j, gameOb, g, 1));
               }
               else if (map[i - 1, j].Type == Enums.Type.BRICKS)
               {
                   Graph.edges.Add(new Edge(i, gameOb, map[i - 1, j], 5));
               }

               if (map[i + 1, j] == null)
               {
                   g = new GameObject();
                   g.LocationX = i + 1;
                   g.LocationY = j;
                   Graph.edges.Add(new Edge(i, gameOb, g, 1));
               }
               else if (map[i + 1, j].Type == Enums.Type.BRICKS)
               {
                   Graph.edges.Add(new Edge(i, gameOb, map[i + 1, j], 5));
               }

               if (map[i, j - 1] == null)
               {
                   g = new GameObject();
                   g.LocationX = i;
                   g.LocationY = j-1;
                   Graph.edges.Add(new Edge(i, gameOb, g, 1));
               }
               else if (map[i, j-1].Type == Enums.Type.BRICKS)
               {
                   Graph.edges.Add(new Edge(i, gameOb, map[i, j - 1], 5));
               }

               if (map[i, j + 1] == null)
               {
                   g = new GameObject();
                   g.LocationX = i;
                   g.LocationY = j + 1;
                   Graph.edges.Add(new Edge(i, gameOb, g, 1));
               }
               else if (map[i, j+1].Type == Enums.Type.BRICKS)
               {
                   Graph.edges.Add(new Edge(i, gameOb, map[i, j + 1], 5));
               }
           }catch(IndexOutOfRangeException e){}
       }

        private void DrawPlayers()
        {
            /* foreach (PlayerData player in players)
             {
                 if (player.IsAlive)
                 {
                     carriageTexture = Content.Load<Texture2D>("brick");
                     spriteBatch.Draw(carriageTexture, player.Position, Color.White);
                 }
             }*/
            //Console.WriteLine("this is drawing");

            map = torkenizerMain.getTerrainInitializationArray();
            Graph.vertexes = new List<GameObject>();
            Graph.edges = new List<Edge>();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    


                    if ((map[i, j] != null))
                    {

                        


                        PlayerData player = new PlayerData();
                        String str = null;
                        player.type = map[i, j].Type;
                        
                        //player.user=
                        //

                        player.Position = new Vector2((i * 45) + 4, ((j * 45) + 5));
                        if (player.type == Enums.Type.LIFE)
                        {
                            str = "lifepack";
                            Graph.vertexes.Add(map[i, j]);
                            addEdges(i, j,map[i,j]);
                        }
                        else if (player.type == Enums.Type.BRICKS)
                        {
                            addEdges(i, j, map[i, j]);
                            Graph.vertexes.Add(map[i, j]);
                            str = "bricks";
                        }
                        else if (player.type == Enums.Type.STONE)
                        {
                            str = "Stones";
                        }
                        else if (player.type == Enums.Type.PLAYER)
                        {
                            addEdges(i, j, map[i, j]);
                            Graph.vertexes.Add(map[i, j]);

                            player.Direction = ((Player)map[i, j]).Direction;
                            player.user = ((Player)map[i, j]).playerType;

                            if (player.user == 1)
                            {
                                if (player.Direction == Enums.Directions.WEST)
                                {
                                    str = "tank_left";
                                }
                                else if (player.Direction == Enums.Directions.SOUTH)
                                {
                                    str = "tank_down";
                                }
                                else if (player.Direction == Enums.Directions.EAST)
                                {
                                    str = "tank_right";
                                }
                                else
                                {
                                    str = "tank_up";
                                }
                            }
                            if (player.user == 0)
                            {
                                if (player.Direction==Enums.Directions.WEST)
                                {
                                    str = "enemy_left";
                                }
                                else if (player.Direction == Enums.Directions.SOUTH)
                                {
                                    str = "enemy_down";
                                }
                                else if (player.Direction == Enums.Directions.EAST)
                                {
                                    str = "enemy_right";
                                }
                                else
                                {
                                    str = "enemy_up";
                                }
                                //Console.WriteLine(str);
                            }

                        }
                        else if (player.type == Enums.Type.COIN)
                        {
                            str = "coins";
                            Graph.vertexes.Add(map[i, j]);
                            addEdges(i, j, map[i, j]);
                        }
                        
                        else if (player.type == Enums.Type.WATER)
                        {
                            str = "Water";
                        }
                        /* else if (player.type == Enums.Type.)
                         {
                             str = "bullet";
                         }*/
                        carriageTexture = Content.Load<Texture2D>(str);
                        spriteBatch.Draw(carriageTexture, player.Position, Color.White);


                    }
                    else
                    {

                        GameObject gameOb = new GameObject();
                        gameOb.LocationX = i;
                        gameOb.LocationY = j;
                        gameOb.id = i + ":" + j;
                        Graph.vertexes.Add(gameOb);

                        PlayerData player = new PlayerData();
                        //String str = null;
                        // player.type = net.map[i][j].type;
                        player.Position = new Vector2((i * 45) + 4, ((j * 45) + 5));
                        carriageTexture = Content.Load<Texture2D>("blank");
                        try
                        {
                            //spriteBatch.Begin();
                            spriteBatch.Draw(carriageTexture, player.Position, Color.White);
                            //spriteBatch.End();

                        }

                        catch (Exception e) { Console.WriteLine(e); }
                        map = torkenizerMain.getTerrainInitializationArray();

                        addEdges(i,j,gameOb);
                        //ClientConnectionInit.sendData(Constant.RIGHT);
                        //ClientConnectionInit.sendData(Constant.sh);

                    }
                }
               

            }

            
            
          
            
            
        }
        private void callDigis() {
            XNAGame.PlayerDesc.AI.DijkstraAlgorithm dij = new PlayerDesc.AI.DijkstraAlgorithm(new Graph(null, null));

            GameObject g = new GameObject();
            g.LocationX = 0;
            g.LocationY = 0;
            g.id = g.LocationX + ":" + g.LocationY;

            GameObject des = new GameObject();
            des.LocationX = 4;
            des.LocationY = 4;
            des.id = des.LocationX + ":" + des.LocationY;

            dij.execute(g);

            LinkedList<GameObject> path = dij.getPath(des);
            Console.WriteLine("ddddddddddddddddddddddd"+path.ElementAt(0));
        }
    }
}
