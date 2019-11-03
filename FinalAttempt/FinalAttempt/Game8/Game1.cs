﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game8.Commands;
using System.Collections.Generic;
using Game8.Stuff;
using Game8.Collisions;

namespace Game8
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Controller controller;
        Avatar avatar;
        Items coco;
        Obstacles crab;
        List<Items> allItems;
        List<Obstacles> allObstacles;
        Texture2D background;
        GridSquares gridSquare;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            allItems = new List<Items>();
            allObstacles = new List<Obstacles>();
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures, and the controller
            spriteBatch = new SpriteBatch(GraphicsDevice);
            controller = new Controller();

            //add commands to the controller
            controller.AddKey(Keys.Q, new QuitCommand(this));
            controller.AddKey(Keys.Space, new JumpCommand(avatar));
            controller.AddKey(Keys.R, new ResetCommand(this));

            // add all the necessary sprites
            this.background = Content.Load<Texture2D>("beach");
            Texture2D avatar_img = Content.Load<Texture2D>("coconut");
            Texture2D block_img = Content.Load<Texture2D>("crab");
            Texture2D coconut_img = Content.Load<Texture2D>("coconut");
            
            //add the objects with their texture
            avatar = new Avatar(GraphicsDevice, avatar_img);
            coco = new Items(coconut_img, 1);
            crab = new Obstacles(block_img, 2);
            allItems.Add(coco);
            allObstacles.Add(new Obstacles(block_img, 2));
            //collision initialization
            gridSquare = new GridSquares(avatar);
            gridSquare.AddCollidable(allItems[0]);
            gridSquare.AddCollidable(allObstacles[0]);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void ResetGame()
        {
            spriteBatch.Dispose();

            controller = new Controller();

            LoadContent();

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            foreach (Items item in allItems)
            {
                item.Update(gameTime);
            }
            foreach(Obstacles obstacle in allObstacles)
            {
                obstacle.Update(gameTime);
            }
            avatar.Update(gameTime);
            gridSquare.HandleCollisions();
            if (avatar.IsDead())
            {
                this.Exit();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            foreach (Items item in allItems)
            {
                item.Draw(spriteBatch);
            }
            foreach (Obstacles obstacle in allObstacles)
            {
                obstacle.Draw(spriteBatch);
            }
            avatar.Draw(spriteBatch);
            //Draw legend
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
