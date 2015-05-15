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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // audio support
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;

        MusicManager musicManager;


        // game objects
        Player player;
     
        List<Enemy> enemies;
        
        List<Hatch> hatches = new List<Hatch>();
        ParallaxingBackground bgLayer1;
        List<Explosion> explosions;
        static List<Projectile> projectiles;
        List<Text> texts;
        Text textHelper;
        //string scoreText = GameConstants.SCORE_PREFIX;

        InfoWindow infoWindow;
        int backup_score = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public SpaceShipGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameConstants.WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = GameConstants.WINDOW_HEIGHT;

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

            // sound support
            audioEngine = new AudioEngine(AssetsConstants.AUDIO_ENGINE);
            waveBank = new WaveBank(audioEngine, AssetsConstants.WAVE_BANK);
            soundBank = new SoundBank(audioEngine, AssetsConstants.SOUND_BANK);
            musicManager = new MusicManager(Content, soundBank, GraphicsDevice);
            infoWindow = new InfoWindow(Content, GraphicsDevice, soundBank, new Vector2(20, 35));

            // Load the parallaxing background
            bgLayer1.Initialize(Content, AssetsConstants.STARTFIELD, GraphicsDevice.Viewport.Width, -1);
            textHelper = new Text(Content, GraphicsDevice);

            player = new Player(Content, GraphicsDevice, new Vector2(60, 300), this, soundBank);
            enemies = new List<Enemy>();
            
            SpawnEnemy();
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
            musicManager.Update(Keyboard.GetState());

            if (player.Score > 100)
            {
                player.BackupScore += player.Score;
                infoWindow.IsActive = true;
                player.Score = 0;
            }

            infoWindow.Update(gameTime);

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

            UpdateExplosions(gameTime);
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


            while (enemies.Count <= GameConstants.ENEMY_MAX_COUNT)
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
        void UpdateExplosions(GameTime gameTime)
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
            
            infoWindow.Draw(spriteBatch, gameTime);
            textHelper.DrawText(spriteBatch, GameConstants.SCORE, TextColor.Red, 20, 10);

            musicManager.Draw(spriteBatch, gameTime);
            
            bgLayer1.Draw(spriteBatch);
            player.Draw(spriteBatch, gameTime);

            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch, gameTime);
            }


            foreach (var hatch in hatches)
            {
                hatch.Draw(spriteBatch, gameTime);
            }

            

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