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
        public int type;
        public String Direction;
        public int user;
        public double harm;

    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        int screenWidth;
        int screenHeight;
        int numberOfPlayers;
        PlayerData[] playerArray;
        Texture2D pixel;
        Texture2D backgroundTexture;
        private Texture2D carriageTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Console.WriteLine("updating");
            // TODO: Add your update logic here
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            base.Update(gameTime);
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

            // Call our method (also defined in this blog-post)
            DrawBorder(titleSafeRectangle, 4, Color.Silver);
            spriteBatch.End();

            base.Draw(gameTime);
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
           
            
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {

//                        if ((net.map[i][j].type) != 0)
  //                      {
                            /*PlayerData player = new PlayerData();
                            String str = null;
                            player.type = net.map[i][j].type;
                            player.user=net.map[i][j].user;
                            player.Direction=net.map[i][j].Direction;
                            player.Position = new Vector2((i * 45)+4, ((j * 45)+5));
                            if (player.type == 1)
                            {
                                str = "brick";
                            }
                            else if (player.type == 2)
                            {
                                str = "stone";
                            }
                            else if (player.type == 3)
                            {
                                str = "water";
                            }
                            else if (player.type == 4)
                            {
                                if (player.user == 1)
                                {
                                    if (player.Direction.Equals("West"))
                                    {
                                        str = "tank_left";
                                    }
                                    else if (player.Direction.Equals("South"))
                                    {
                                        str = "tank_down";
                                    }
                                    else if (player.Direction.Equals("East"))
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
                                    if (player.Direction.Equals("West"))
                                    {
                                        str = "enemy_left";
                                    }
                                    else if (player.Direction.Equals("South"))
                                    {
                                        str = "enemy_down";
                                    }
                                    else if (player.Direction.Equals("East"))
                                    {
                                        str = "enemy_right";
                                    }
                                    else
                                    {
                                        str = "enemy_up";
                                    }
                                }

                            }
                            else if (player.type == 5)
                            {
                                str = "coins";
                            }
                            else if (player.type == 6)
                            {
                                str = "life";
                            }
                            else if (player.type == 7)
                            {
                                str = "bullet";
                            }
                            carriageTexture = Content.Load<Texture2D>(str);
                            spriteBatch.Draw(carriageTexture, player.Position, Color.White);
                            

                        }
                        else
                        {*/

                            PlayerData player = new PlayerData();
                            //String str = null;
                           // player.type = net.map[i][j].type;
                            player.Position = new Vector2((i * 45)+4, ((j * 45)+5));
                            carriageTexture = Content.Load<Texture2D>("blank");
                            try
                            {
                                //spriteBatch.Begin();
                                spriteBatch.Draw(carriageTexture, player.Position, Color.White);
                                //spriteBatch.End();
                            }
                            catch (Exception e) { Console.WriteLine(e); }
                        
                    }
                }

            }
    }
}
