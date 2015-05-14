using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShip.Classes;
using SpaceShip.Objects;
using System;
using System.Collections.Generic;

namespace SpaceShip
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SpaceShipGame : Microsoft.Xna.Framework.Game
    {
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;
        

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // game objects
        Player player;
        //Enemy enemy_blue;
        //Enemy enemy_cyan;
        //Enemy enemy_green;
        //Enemy enemy_red;
        //Enemy enemy_yellow;
        List<Enemy> enemies;
        int maxEnemies = 5;
        Hatch hatch1;
        Hatch hatch2;
        List<Hatch> hatches = new List<Hatch>();
        Head head;
        ParallaxingBackground bgLayer1;
        List<Explosion> explosions;
        static List<Projectile> projectiles;
        List<Text> texts;
        Text text;
        string scoreText = GameConstants.SCORE_PREFIX;

        /// <summary>
        /// Constructor
        /// </summary>
        public SpaceShipGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;

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
            RandomNumberGenerator.Initialize();

            explosions = new List<Explosion>();
            projectiles = new List<Projectile>();
            bgLayer1 = new ParallaxingBackground();
            texts = new List<Text>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the parallaxing background
            bgLayer1.Initialize(Content, "starfield", GraphicsDevice.Viewport.Width, -1);

            text = new Text(Content, GraphicsDevice);
            player = new Player(Content, GraphicsDevice, new Vector2(60, 300), this);
            //enemy_blue = new Enemy(Content, GraphicsDevice, new Vector2(200, 200), EnemyType.Blue);
            //enemy_cyan = new Enemy(Content, GraphicsDevice, new Vector2(300, 200), EnemyType.Cyan);
            //enemy_green = new Enemy(Content, GraphicsDevice, new Vector2(400, 200), EnemyType.Green);
            //enemy_red = new Enemy(Content, GraphicsDevice, new Vector2(200, 400), EnemyType.Red);
            //enemy_yellow = new Enemy(Content, GraphicsDevice, new Vector2(300, 400), EnemyType.Yellow);
            //hatch1 = new Hatch(Content, GraphicsDevice, new Vector2(100, 100));
            //hatch2 = new Hatch(Content, GraphicsDevice, new Vector2(120, 100));
            head = new Head(Content, GraphicsDevice, new Vector2(550, 220));
            enemies = new List<Enemy>();

            SpawnEnemy();

            //AddTestExplosion(new Vector2(270, 250));
        }

        /// <summary>
        /// Add new explosion
        /// </summary>
        /// <param name="position">Start position</param>
        private void AddExplosion(Vector2 position)
        {
            explosions.Add(new Explosion(Content, GraphicsDevice, position));
        }

        /// <summary>
        /// Add new projectile
        /// </summary>
        /// <param name="position">Start position</param>
        public void AddProjectile(Vector2 position)
        {
            projectiles.Add(new Projectile(Content, GraphicsDevice, position));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
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

            bgLayer1.Update();          
            player.Update(gameTime);

            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            foreach (var hatch in hatches)
            {
                hatch.Update(gameTime);
            }

            //hatch1.Update(gameTime);
            //hatch2.Update(gameTime);
            head.Update(gameTime);
            UpdateExplisions(gameTime);
            UpdateProjectiles(gameTime);

            //collision between enemies and projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                var curProjectTile = projectiles[i];
                if (!curProjectTile.IsActive)
                    continue;
                for (int j = 0; j < enemies.Count; j++)
                {
                    var curEnemy = enemies[j];
                    if (!curEnemy.IsActive)
                        continue;

                    Rectangle collisionRectangle = Rectangle.Intersect(curEnemy.ObjRectangle, curProjectTile.ObjRectangle);
                    if (!collisionRectangle.IsEmpty)
                    {
                        AddExplosion(new Vector2(curEnemy.Location.X - 50, curEnemy.Location.Y - 50));
                        curProjectTile.IsActive = false;
                        curEnemy.IsActive = false;

                        var newHatch = new Hatch(Content, GraphicsDevice, new Vector2(curEnemy.Location.X, curEnemy.Location.Y), curEnemy.GetScore());
                        hatches.Add(newHatch);
                    }
                }
            }

            //collision between player and enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                var curEnemy = enemies[i];
                if (!curEnemy.IsActive)
                    continue;

                Rectangle collisionRectangle = Rectangle.Intersect(curEnemy.ObjRectangle, player.ObjRectangle);
                if (!collisionRectangle.IsEmpty)
                {
                    
                    AddExplosion(new Vector2(curEnemy.ObjRectangle.Center.X-50, curEnemy.ObjRectangle.Center.Y-50));
                    curEnemy.IsActive = false;

                    var newHatch = new Hatch(Content, GraphicsDevice, new Vector2(curEnemy.Location.X, curEnemy.Location.Y), curEnemy.GetScore());
                    hatches.Add(newHatch);
                    
                }
            }

            for (int i = 0; i < hatches.Count; i++)
            {
                var curHatch = hatches[i];
                if (!curHatch.IsActive)
                    continue;

                Rectangle collisionRectangle = Rectangle.Intersect(curHatch.ObjRectangle, player.ObjRectangle);
                if (!collisionRectangle.IsEmpty)
                {
                    curHatch.IsActive = false;
                    player.Score += curHatch.Value;
                }
            }


            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (!enemies[i].IsActive)
                    enemies.RemoveAt(i);
            }

            for (int i = hatches.Count - 1; i >= 0; i--)
            {
                if (!hatches[i].IsActive)
                    hatches.RemoveAt(i);
            }


            while (enemies.Count <= maxEnemies)
            {
                SpawnEnemy();
            }

            //for (int i = projectiles.Count - 1; i >= 0; i--)
            //{
            //    if (!projectiles[i].IsActive)
            //        projectiles.RemoveAt(i);
            //}

            base.Update(gameTime);
        }
       
        /// <summary>
        /// Update all existing explosions
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        void UpdateExplisions(GameTime gameTime)
        {
            foreach (Explosion explosion in explosions)
            {
                explosion.Update(gameTime);
            }

            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                if (!explosions[i].IsActive)
                    explosions.RemoveAt(i);
            }
        }

        /// <summary>
        /// Update all existing projectiles
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        void UpdateProjectiles(GameTime gameTime)
        {
            // Update the Projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                Projectile projectile = projectiles[i];
                projectile.Update(gameTime);
                if (projectile.X_Position + projectile.TextureWidth / 2 > GraphicsDevice.Viewport.Width)
                    projectile.IsActive = false;

                if (projectile.IsActive == false)
                    projectiles.RemoveAt(i);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            text.DrawText(spriteBatch, "SCORE", TextColor.Red, 20, 10);
            
            bgLayer1.Draw(spriteBatch);
            player.Draw(spriteBatch, gameTime);

            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch, gameTime);
            }

            //enemy_blue.Draw(spriteBatch, gameTime);
            //enemy_cyan.Draw(spriteBatch, gameTime);
            //enemy_green.Draw(spriteBatch, gameTime);
            //enemy_red.Draw(spriteBatch, gameTime);
            //enemy_yellow.Draw(spriteBatch, gameTime);
            //hatch1.Draw(spriteBatch, gameTime);
            //hatch2.Draw(spriteBatch, gameTime);

            foreach (var hatch in hatches)
            {
                hatch.Draw(spriteBatch, gameTime);
            }

            head.Draw(spriteBatch, gameTime);

            // draw all existing explosions
            foreach (Explosion expl in explosions)
            {
                expl.Draw(spriteBatch, gameTime);
            }

            // draw all existing projectiles
            foreach (Projectile proj in projectiles)
            {
                proj.Draw(spriteBatch, gameTime);
            }            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void SpawnEnemy()
        {
            var x = GetRandomLocation(GameConstants.SPAWN_BORDER_SIZE, GameConstants.WINDOW_WIDTH - GameConstants.SPAWN_BORDER_SIZE * 2);
            var y = GetRandomLocation(GameConstants.SPAWN_BORDER_SIZE, GameConstants.WINDOW_HEIGHT - GameConstants.SPAWN_BORDER_SIZE * 2);

            Array values = Enum.GetValues(typeof(EnemyType));
            var randomEnemyType = (EnemyType)values.GetValue(RandomNumberGenerator.Next(values.Length));


            Enemy newEnemy = new Enemy(Content, GraphicsDevice, new Vector2(x, y), randomEnemyType);
            enemies.Add(newEnemy);
        }

        private int GetRandomLocation(int min, int range)
        {
            return min + RandomNumberGenerator.Next(range);
        }
    }
}