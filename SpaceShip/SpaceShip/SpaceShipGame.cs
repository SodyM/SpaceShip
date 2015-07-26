using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShip.Classes;
using SpaceShip.Objects;
using SpaceShip.Objects.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpaceShip
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SpaceShipGame : Microsoft.Xna.Framework.Game
    {
        int super_cool = GameConstants.SUPERCOOL_SCORE;
        int super_cool_step = GameConstants.SUPERCOOL_SCORE;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // audio support
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;

        MusicManager musicManager;

        GameState gameState;

        bool escPressed = false;
        bool escReleased = false;

        MainMenuView mainMenu;
        GameOverView gameOverView;
        SettingsView settingsView;
        CreditsView creditsView;

        bool GameWasStated = false;

        // game objects
        Player player;
        bool playerCanTakeDamage = true;
        int elapsedInvulTime = 0;
     
        List<Enemy> enemies;
        
        List<Hatch> hatches = new List<Hatch>();
        ParallaxingBackground bgLayer1;
        ParallaxingBackground bgLayer2;

        List<Explosion> explosions;
        static List<Projectile> projectiles;
        List<Text> texts;
        Text textHelper;
        //string scoreText = GameConstants.SCORE_PREFIX;

        Number numberHelper;

        InfoWindow infoWindow;

        LifeInfo lifeInfo;


        //Sprites
        Dictionary<string, Texture2D> textures = new Dictionary<string,Texture2D>();

        /// <summary>
        /// Constructor
        /// </summary>
        public SpaceShipGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameConstants.WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = GameConstants.WINDOW_HEIGHT;
            //graphics.ToggleFullScreen();

            Content.RootDirectory = "Content";
        }

        public void SetFullScreen()
        {
            graphics.ToggleFullScreen();
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
            bgLayer2 = new ParallaxingBackground();

            texts = new List<Text>();
            gameState = GameState.MENU_MAIN;
            base.Initialize();
        }

        /// <summary>
        /// Central function - loads the textures only once on this main position
        /// Pleace every Content.Load call only here
        /// </summary>
        private void LoadTextures()
        {
            textures.Add(AssetsConstants.LASER, Content.Load<Texture2D>(AssetsConstants.LASER));
            textures.Add(AssetsConstants.ENEMY_LASER, Content.Load<Texture2D>(AssetsConstants.ENEMY_LASER));
            textures.Add(AssetsConstants.ENEMY_YELLOW, Content.Load<Texture2D>(AssetsConstants.ENEMY_YELLOW));
            textures.Add(AssetsConstants.ENEMY_RED, Content.Load<Texture2D>(AssetsConstants.ENEMY_RED));
            textures.Add(AssetsConstants.ENEMY_CYAN, Content.Load<Texture2D>(AssetsConstants.ENEMY_CYAN));
            textures.Add(AssetsConstants.ENEMY_BLUE, Content.Load<Texture2D>(AssetsConstants.ENEMY_BLUE));
            textures.Add(AssetsConstants.ENEMY_GREEN, Content.Load<Texture2D>(AssetsConstants.ENEMY_GREEN));
            textures.Add(AssetsConstants.EXPLOSION, Content.Load<Texture2D>(AssetsConstants.EXPLOSION));
            textures.Add(AssetsConstants.HATCH, Content.Load<Texture2D>(AssetsConstants.HATCH));
            textures.Add(AssetsConstants.GAME_OVER, Content.Load<Texture2D>(AssetsConstants.GAME_OVER));

            // menu pages
            textures.Add(AssetsConstants.MENU_RESOLUTION, Content.Load<Texture2D>(AssetsConstants.MENU_RESOLUTION));
            textures.Add(AssetsConstants.MENU_SOUND, Content.Load<Texture2D>(AssetsConstants.MENU_SOUND));
            textures.Add(AssetsConstants.MENU_MUSIC, Content.Load<Texture2D>(AssetsConstants.MENU_MUSIC));
            textures.Add(AssetsConstants.MENU_BACK, Content.Load<Texture2D>(AssetsConstants.MENU_BACK));
            textures.Add(AssetsConstants.MENU_FULLSCREEN, Content.Load<Texture2D>(AssetsConstants.MENU_FULLSCREEN));
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
            infoWindow = new InfoWindow(Content, GraphicsDevice, soundBank, new Vector2(20, 35), AssetsConstants.RESPECT, AssetsConstants.RESPECT_SOUND);

            LoadTextures();

            // Load the parallaxing background            
            bgLayer1.Initialize(Content, AssetsConstants.STARTFIELD, GraphicsDevice.Viewport.Width, -2);
            bgLayer2.Initialize(Content, AssetsConstants.FARBACK, GraphicsDevice.Viewport.Width, -1);            

            textHelper = new Text(Content, GraphicsDevice);
            numberHelper = new Number(Content, GraphicsDevice);

            player = new Player(Content, GraphicsDevice, new Vector2(60, 300), this, soundBank, GameConstants.PLAYER_DEFAULT_HEALTH);
            lifeInfo = new LifeInfo(Content, GraphicsDevice, this);

            enemies = new List<Enemy>();
            mainMenu = new MainMenuView(Content, GraphicsDevice, this, soundBank, musicManager);
            gameOverView = new GameOverView(Content, GraphicsDevice, this, soundBank);
            settingsView = new SettingsView(Content, GraphicsDevice, this, soundBank);
            creditsView = new CreditsView(Content, GraphicsDevice, this, soundBank, musicManager);
            

            SpawnEnemy();
            enemies[0].SetTarget(player);
        }

        /// <summary>
        /// Add new explosion
        /// </summary>
        /// <param name="position">Start position</param>
        private void AddExplosion(Vector2 position, string spriteName)
        {
            Texture2D texture = null;
            if (textures.TryGetValue(spriteName, out texture))
            {
                explosions.Add(new Explosion(texture, Content, GraphicsDevice, position));
                soundBank.PlayCue(AssetsConstants.EXPLOSION);
            }
        }

        /// <summary>
        /// Returns a texture from the texturelist for a given name
        /// </summary>
        /// <param name="spriteName"></param>
        /// <returns></returns>
        public Texture2D GetTextureForName(string spriteName)
        { 
            Texture2D texture = null;
            textures.TryGetValue(spriteName, out texture);
            return texture; 
        }

        /// <summary>
        /// Add new projectile
        /// </summary>
        /// <param name="position">Start position</param>
        public void AddProjectile(Vector2 position, Vector2 velocity, string spriteName, double lifespan, ProjectileSource source)
        {
            Texture2D texture = null;
            if (textures.TryGetValue(spriteName, out texture))
            {
                projectiles.Add(new Projectile(texture, GraphicsDevice, position, velocity, lifespan, source));
            }
        }

        public GameState GetCurrentGameState()
        {
            return gameState;
        }

        public void ChangeGameState(GameState state)
        {
            //GameIsPaused = true;
            this.gameState = state;
        }

        public int GetPlayerHealth()
        {
            return player.Health;
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
            if (gameState == GameState.MENU_MAIN)
            {
                UpdateMainMenu(gameTime);
            }
            else if (gameState == GameState.MENU_DISPLAY_SETTINGS)
            {
                UpdateSettings(gameTime);
            }
            else if (gameState == GameState.MENU_CREDITS)
            {                
                UpdateCredits(gameTime);
            }
            else if(gameState == GameState.START_NEW_GAME)
            {                
                player.Score = 0;
                ReInitGame(gameTime);

                musicManager.PlayMainTheme();
                gameState = GameState.PLAY;
            }
            else if (gameState == GameState.PLAY)
            {
                UpdateGame(gameTime);
                UpdateLifeInfo(gameTime);
            }
            else if (gameState == GameState.PLAYER_DIED)
            {
                ReInitGame(gameTime);
            }
            else if (gameState == GameState.GAME_OVER)
            {
                UpdateGameOver(gameTime);
            }
            else if (gameState == GameState.QUIT)
            {
                this.Exit();
            }
            else if (gameState == GameState.GAME_PAUSED)
            {                
                musicManager.StopMusic();
                UpdateGamePaused(gameTime);
            }

            base.Update(gameTime);
        }

        private void UpdateLifeInfo(GameTime gameTime)
        {
            lifeInfo.Update(gameTime);
        }

        #region will be implemeted with better architecture later
        /// <summary>
        /// Updates the game over.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void UpdateGameOver(GameTime gameTime)
        {
            gameOverView.Update(gameTime);
        }

        /// <summary>
        /// Updates the credits.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void UpdateCredits(GameTime gameTime)
        {
            creditsView.Update(gameTime);
        }

        /// <summary>
        /// Updates the display settings.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void UpdateSettings(GameTime gameTime)
        {
            settingsView.Update(gameTime);
        }

        /// <summary>
        /// Updates the game paused.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void UpdateGamePaused(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
            {
                escPressed = true;
                escReleased = false;
            }
            else if (keyState.IsKeyUp(Keys.Escape))
            {
                escReleased = true;
                if (escPressed && escReleased)
                {
                    escPressed = false;
                    escPressed = false;
                    ChangeGameState(GameState.PLAY);
                }
            }

            UpdateMainMenu(gameTime);
        }

        /// <summary>
        /// Removes all enemies (example: after player died)
        /// </summary>
        private void ClearEnemies()
        {
        }

        /// <summary>
        /// Restarts the game after the player died if there are lives left
        /// Game over if no lives left
        /// </summary>
        /// <param name="gameTime"></param>
        public void ReInitGame(GameTime gameTime)
        {
            GameWasStated = true;
            enemies.Clear();
            projectiles.Clear();
            hatches.Clear();

            if (player.Lives > 0)
            {                
                player.Reset();
                gameState = GameState.PLAY;
            }
            else
            {
                // stop everything
                musicManager.StopMusic();
                gameState = GameState.GAME_OVER;    //later: use state GameOver
            }                
        }

        /// <summary>
        /// Updates the main menu.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void UpdateMainMenu(GameTime gameTime)
        {
            mainMenu.Update(gameTime);
        }

        /// <summary>
        /// Updates runnig sgame.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void UpdateGame(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
            {
                escPressed = true;
                escReleased = false;
            }
            else if (keyState.IsKeyUp(Keys.Escape))
            {
                escReleased = true;
                if (escPressed && escReleased)
                {
                    escPressed = false;
                    escPressed = false;                    
                    ChangeGameState(GameState.GAME_PAUSED);
                }
            }
            

            musicManager.Update(Keyboard.GetState());
            if (player.Score >= super_cool)
            {
                infoWindow.SetDisplayContent(Content, AssetsConstants.RESPECT, AssetsConstants.RESPECT_SOUND);
                infoWindow.IsActive = true;
                super_cool = super_cool_step + super_cool;
            }

            infoWindow.Update(gameTime);            
            bgLayer1.Update();
            bgLayer2.Update();
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

            //collision between enemies and projectiles and between player and projectiles from enemies
            if (player.IsActive)
            {
                for (int i = 0; i < projectiles.Count; i++)
                {
                    var curProjectTile = projectiles[i];
                    if (!curProjectTile.IsActive)
                        continue;

                    //collision between player and enemy projectiles
                    if (curProjectTile.SourceOfProjectile == ProjectileSource.Enemy)
                    {
                        Rectangle collisionRectangle = Rectangle.Intersect(player.ObjRectangle, curProjectTile.ObjRectangle);
                        if (!collisionRectangle.IsEmpty)
                        {
                            player.Health -= GameConstants.ENEMY_PROJECTILE_DAMAGE;

                            AddExplosion(new Vector2(player.Location.X + GameConstants.EXPLOSION_OFFSET, player.Location.Y + GameConstants.EXPLOSION_OFFSET), AssetsConstants.EXPLOSION);
                            curProjectTile.IsActive = false;
                        }

                        continue;
                    }

                    //collision between enemies and player projectiles
                    for (int j = 0; j < enemies.Count; j++)
                    {
                        if (curProjectTile.SourceOfProjectile != ProjectileSource.Player)
                            continue;

                        var curEnemy = enemies[j];
                        if (!curEnemy.IsActive)
                            continue;

                        Rectangle collisionRectangle = Rectangle.Intersect(curEnemy.ObjRectangle, curProjectTile.ObjRectangle);
                        if (!collisionRectangle.IsEmpty)
                        {
                            AddExplosion(new Vector2(curEnemy.Location.X + GameConstants.EXPLOSION_OFFSET, curEnemy.Location.Y + GameConstants.EXPLOSION_OFFSET), AssetsConstants.EXPLOSION);
                            curProjectTile.IsActive = false;
                            curEnemy.IsActive = false;//TODO: Health

                            SpawnHatch(new Vector2(curEnemy.Location.X, curEnemy.Location.Y+20), curEnemy.GetScore(), AssetsConstants.HATCH);
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

                        AddExplosion(new Vector2(curEnemy.ObjRectangle.Center.X - 50, curEnemy.ObjRectangle.Center.Y - 50), AssetsConstants.EXPLOSION);
                        curEnemy.IsActive = false;
                        player.Health -= GameConstants.ENEMY_COLLISION_DAMAGE;


                        infoWindow.SetDisplayContent(Content, AssetsConstants.COLLISION_INFO_PIC, AssetsConstants.COLLISION_SOUND);
                        infoWindow.IsActive = true;
                    }
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


            while (enemies.Count < GameConstants.ENEMY_MAX_COUNT)
            {
                SpawnEnemy();
            }

            CheckPlayerStatus();

            //for (int i = projectiles.Count - 1; i >= 0; i--)
            //{
            //    if (!projectiles[i].IsActive)
            //        projectiles.RemoveAt(i);
            //}
        }

        #endregion
        
        /// <summary>
        /// Checks whether player died
        /// </summary>
        private void CheckPlayerStatus()
        {
            if (player.IsActive && (player.Health <= 0))
            {
                player.Lives -= 1;
                player.IsActive = false;
                AddExplosion(new Vector2(player.Location.X, player.Location.Y), AssetsConstants.EXPLOSION);

                gameState = GameState.PLAYER_DIED;
                soundBank.PlayCue(AssetsConstants.EXPLOSION_PLAYER);
            }
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

            if (gameState == GameState.MENU_MAIN)
            {
                DrawGame(gameTime);
                DrawMainMenu(gameTime);
            }
            else if (gameState == GameState.MENU_DISPLAY_SETTINGS)
            {
                DrawGame(gameTime);
                DrawSettings(gameTime);
            }
            else if (gameState == GameState.MENU_CREDITS)
            {
                DrawCredits(gameTime);
            }            
            else if (gameState == GameState.PLAY)
            {
                DrawGame(gameTime);
            }
            else if (gameState == GameState.GAME_OVER)
            {
                DrawGameOver(gameTime);
            }
            else if (gameState == GameState.GAME_PAUSED)
            {
                DrawGame(gameTime);
                DrawGamePaused(gameTime);
            }

            spriteBatch.End();
            base.Draw(gameTime);            
        }

        private void DrawGamePaused(GameTime gameTime)
        {
            mainMenu.Draw(spriteBatch, gameTime);
        }

        #region will be implemeted with better architecture later
        /// <summary>
        /// Draws the game over.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void DrawGameOver(GameTime gameTime)
        {
            bgLayer2.Draw(spriteBatch);
            gameOverView.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Draws the credits.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void DrawCredits(GameTime gameTime)
        {
            if (gameState != GameState.GAME_PAUSED)
                bgLayer2.Draw(spriteBatch);

            creditsView.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Draws the display settings.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void DrawSettings(GameTime gameTime)
        {
            if (!GameWasStated)
                bgLayer2.Draw(spriteBatch);

            settingsView.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Draws the main menu.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void DrawMainMenu(GameTime gameTime)
        {
            if (!GameWasStated)
                bgLayer2.Draw(spriteBatch);
            
            mainMenu.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Draws the game.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void DrawGame(GameTime gameTime)
        {
            bgLayer2.Draw(spriteBatch);
            bgLayer1.Draw(spriteBatch);

            infoWindow.Draw(spriteBatch, gameTime);
            textHelper.DrawText(spriteBatch, GameConstants.SCORE, TextColor.Red, GameConstants.SCORE_TEXT_LEFT, GameConstants.INFOLINE_TOP);
            numberHelper.DrawNumber(spriteBatch, player.Score, GameConstants.SCORE_VALUE_LEFT, GameConstants.INFOLINE_TOP);
            musicManager.Draw(spriteBatch, gameTime);


            if (player.IsActive)
            {
                player.Draw(spriteBatch, gameTime);                
                lifeInfo.Draw(spriteBatch, gameTime);
            }

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
        }

        #endregion

        /// <summary>
        /// Spawns a new hatch
        /// </summary>
        /// <param name="position">position of the hatch (example: position of destroyed enemy)</param>
        /// <param name="value">the value of the hatch that will be added to the player's score</param>
        private void SpawnHatch(Vector2 position, int value, string spriteName)
        {
            //set velocity of hatch
            float vox = GameConstants.HATCH_SPEED;
            var voy = 0;
            Vector2 velocityVec = new Vector2(vox, voy);

            Texture2D texture = null;
            if (textures.TryGetValue(spriteName, out texture))
            {
                hatches.Add(new Hatch(texture, Content, GraphicsDevice, position, velocityVec, value));
            }            
        }

        /// <summary>
        /// Spawns a new enemy and adds it to the enemy list
        /// Current implementation: random location, random enemy type
        /// </summary>
        private void SpawnEnemy()
        {
            var x = GameConstants.WINDOW_WIDTH + 50;//spawn enemies outside of visible area // GetRandomLocation(GameConstants.SPAWN_BORDER_SIZE, GameConstants.WINDOW_WIDTH - GameConstants.SPAWN_BORDER_SIZE * 2);
            var y = GetRandomLocation(GameConstants.SPAWN_BORDER_SIZE, GameConstants.WINDOW_HEIGHT - GameConstants.SPAWN_BORDER_SIZE * 2);

            Array values = Enum.GetValues(typeof(EnemyType));
            var randomEnemyType = (EnemyType)values.GetValue(RandomNumberGenerator.Next(values.Length));

            //set random velocity
            var velocity = RandomNumberGenerator.NextFloat(GameConstants.ENEMY_SPEED_RANGE) + GameConstants.MIN_ENEMY_SPEED;

            // get a random value for the angle
            var randAng = RandomNumberGenerator.NextFloat((float)Math.PI * 2);

            //Calculate the movements in the x- and y-direction
            var vox = -Math.Abs(velocity * (float)Math.Cos(randAng));//from right to left
            var voy = 0;// velocity * (float)Math.Sin(randAng); //
            Vector2 velocityVec = new Vector2(vox, voy);


            Enemy newEnemy = new Enemy(Content, GraphicsDevice, new Vector2(x, y), velocityVec, randomEnemyType, this, soundBank);


            var collisionRectangles = GetCollisionRectangles();
            while (!CollisionUtils.IsCollisionFree(newEnemy.ObjRectangle, collisionRectangles))
            {
                //if collision was found, try a new random location for the teddy
                newEnemy.X = GetRandomLocation(GameConstants.SPAWN_BORDER_SIZE, GameConstants.WINDOW_WIDTH - GameConstants.SPAWN_BORDER_SIZE * 2);
                newEnemy.Y = GetRandomLocation(GameConstants.SPAWN_BORDER_SIZE, GameConstants.WINDOW_HEIGHT - GameConstants.SPAWN_BORDER_SIZE * 2);
            }

            if (GameConstants.ENEMIES_TARGET_PLAYER)
                newEnemy.SetTargetLocation(new Vector2(player.Location.X, player.Location.Y));
            enemies.Add(newEnemy);
        }

        /// <summary>
        /// Returns the rectangles from all objects
        /// </summary>
        /// <returns></returns>
        private List<Rectangle> GetCollisionRectangles()
        {
            List<Rectangle> collisionRectangles = new List<Rectangle>();
            collisionRectangles.Add(player.ObjRectangle);
            foreach (var enemy in enemies)
            {
                collisionRectangles.Add(enemy.ObjRectangle);
            }

            return collisionRectangles;
        }

        /// <summary>
        /// Returns a random integer value
        /// </summary>
        /// <param name="min"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        private int GetRandomLocation(int min, int range)
        {
            return min + RandomNumberGenerator.Next(range);
        }
    }
}