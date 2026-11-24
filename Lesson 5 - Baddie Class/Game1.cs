using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Lesson_5___Baddie_Class //Maxym F.
{
    enum Screen 
    {
        Title,
        House,
        End
    }


    public class Game1 : Game
    {

        Screen screen; 
        Rectangle window;

        private GraphicsDeviceManager _graphics; 
        private SpriteBatch _spriteBatch; 

        List<Texture2D> booTextures; 
        Random generator; 

        Texture2D titleTexture; 
        Texture2D endTexture; 
        Texture2D backgroundTexture; 
        Texture2D marioTexture; 
        Rectangle marioRect; 


        KeyboardState keyboardState; 
        MouseState mouseState; 

        List<Ghost> ghosts; 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.Title;

            generator = new Random();
            booTextures = new List<Texture2D>();

            ghosts = new List<Ghost>();

            marioRect = new Rectangle(0, 0, 30, 30);
            base.Initialize();

            this.IsMouseVisible = false;
            for (int i = 0; i < 20; i++)
                ghosts.Add(new Ghost(booTextures, new Rectangle(generator.Next(500), generator.Next(500), 40, 40)));



        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            backgroundTexture = Content.Load<Texture2D>("Textures/haunted-background");
            endTexture = Content.Load<Texture2D>("Textures/haunted-end-screen");
            titleTexture = Content.Load<Texture2D>("Textures/haunted-title");
            booTextures.Add(Content.Load<Texture2D>("Images/boo-stopped"));
            marioTexture = Content.Load<Texture2D>("Images/mario");

            for (int i = 1; i <= 8; i++)
                booTextures.Add(Content.Load<Texture2D>("Images/boo-move-" + i));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            marioRect.Location = mouseState.Position;

            if (screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.House;
                }
            }

            else if (screen == Screen.House)
            {
                foreach (Ghost ghost in ghosts)
                {
                    ghost.Update(gameTime, mouseState);
                    if (ghost.Intersects(marioRect) && mouseState.LeftButton == ButtonState.Pressed)
                        screen = Screen.End;
                }
            }

             base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            _spriteBatch.Begin();

            if (screen == Screen.Title)
                _spriteBatch.Draw(titleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            else if (screen == Screen.House)
            {
                _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                foreach (Ghost ghost in ghosts)
                    ghost.Draw(_spriteBatch);
            }
            else
                _spriteBatch.Draw(endTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

            _spriteBatch.Draw(marioTexture, marioRect, Color.White);



            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
