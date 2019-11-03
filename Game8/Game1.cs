using Microsoft.Xna.Framework;
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
        Obstacles palm;
        List<Items> allItems;
        List<Obstacles> allObstacles;
        Texture2D background;
        GridSquares gridSquare;
        SpriteFont font;


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

            // add all the necessary sprites
            this.background = Content.Load<Texture2D>("NewBackground");
            Texture2D avatar_img = Content.Load<Texture2D>("starting_avatar");
            Texture2D block_img = Content.Load<Texture2D>("crab");
            Texture2D coconut_img = Content.Load<Texture2D>("coconut");
            Texture2D seagull = Content.Load<Texture2D>("seagull");
            Texture2D palm_img = Content.Load<Texture2D>("palm");
            Texture2D charmeleon = Content.Load<Texture2D>("charmeleon");
            Texture2D charizard = Content.Load<Texture2D>("charizard");

            font = Content.Load<SpriteFont>("Font");

            //add the objects with their texture
            avatar = new Avatar(GraphicsDevice, avatar_img, charmeleon, charizard, 1.5);
            coco = new Items(coconut_img, 1, 0.3);
            allItems.Add(coco);
            allObstacles.Add(new Obstacles(block_img, 2, 0.3, 0));
            allObstacles.Add(new Obstacles(palm_img, 0.5, 1, 0));
            allObstacles.Add(new Obstacles(seagull, 1.5, 0.3, 1));
            //collision initialization
            gridSquare = new GridSquares(avatar);
            gridSquare.AddCollidable(allItems[0]);
            gridSquare.AddCollidable(allObstacles[0]);
            gridSquare.AddCollidable(allObstacles[1]);
            gridSquare.AddCollidable(allObstacles[2]);


            //add commands to the controller
            controller.AddKey(Keys.Q, new QuitCommand(this));
            controller.AddKey(Keys.Space, new JumpCommand(avatar));
            controller.AddKey(Keys.R, new ResetCommand(this));

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
        public void LevelUpScreen(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GraphicsDevice.Clear(Color.MonoGameOrange);
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            controller.Update();
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
                if (item.isVisible)
                {
                    item.Draw(spriteBatch);
                }
            }
            foreach (Obstacles obstacle in allObstacles)
            {
                obstacle.Draw(spriteBatch);
            }
            avatar.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Distance: " + avatar.PlayerPoints, new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(font, "Coconut Points: " + avatar.PlayerCoconuts, new Vector2(0, 20), Color.Black);
            if (avatar.PlayerCoconuts == 30)
            {
                LevelUpScreen(gameTime, spriteBatch);
            }
            //Draw legend
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
