﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using New_Physics.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Frogs.src.Entities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Frogs.src
{
    public static class GameHandler
    {
        public static string gamestate = "startScreen";

        private static StartScreen startScreen;
        private static HelpScreen helpScreen;
        private static CursorHandler cursorHandler;

        private static SoundEffect effect;
        private static SoundEffect fail;
        private static Song song;

        private static Boolean first = true;

        public static void Init(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice)
        {
            Camera.SetDimensions(graphics, 1024, 576);
            //Camera.SetDimensions(graphics, graphicsDevice);
            EntityHandler.Init();

            startScreen = new StartScreen();
            helpScreen = new HelpScreen();
            cursorHandler = new CursorHandler();
        }

        public static void Update()
        {
            MediaPlayer.IsRepeating = true;
            if (first) MediaPlayer.Play(song);
            first = false;

            switch (gamestate)
            {
                case "startScreen":
                    startScreen.Update();
                    if (startScreen.Begin) gamestate = "help";
                    break;
                case "help":
                    helpScreen.Update();
                    if (helpScreen.Begin)
                    {
                        gamestate = "initLevel";
                        effect.Play();
                    }
                    break;
                case "initLevel":
                    Level_Loader.LoadLevel();
                    gamestate = "level";
                    break;
                case "level":
                    EntityHandler.Update();
                    Camera.Update();
                    //if (((GoalHandler)(EntityHandler.entities[0])).score == ((GoalHandler)(EntityHandler.entities[0])).maxScore) gamestate = "win";
                    break;
                case "die":
                    gamestate = "initLevel";
                    fail.Play();
                    break;
                case "win":
                    //TODO: probably never, but this is where it would be impelmented
                    break;
            }
            cursorHandler.Update();
            //Level_Loader.LoadLevel();
        }

        public static void LoadContent(ContentManager Content)
        {
            startScreen.LoadContent(Content);
            PlayerSprites.LoadContent(Content);
            GoalSprites.LoadContent(Content);
            cursorHandler.LoadContent(Content);
            PlatformSprites.LoadContent(Content);
            helpScreen.LoadContent(Content);
            ArrowSprites.LoadContent(Content);

            effect = Content.Load<SoundEffect>(@"Achive");
            fail = Content.Load<SoundEffect>(@"Fail");
            song = Content.Load<Song>(@"LuckyRubberDucky");
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            EntityHandler.Draw(spriteBatch, graphicsDevice);
            if (gamestate == "startScreen") startScreen.Draw(spriteBatch, graphicsDevice);
            else if (gamestate == "help") helpScreen.Draw(spriteBatch, graphicsDevice);
            cursorHandler.Draw(spriteBatch, graphicsDevice);
        }
    }

    public class CursorHandler
    {
        private Texture2D cursor;

        MouseState mouse;

        public void LoadContent(ContentManager Content)
        {
            cursor = Content.Load<Texture2D>("Cursor");
        }

        public void Update()
        {
            mouse = Mouse.GetState();
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(cursor,
                new Rectangle(mouse.X, mouse.Y, 30, 30),
                Color.White);

            spriteBatch.End();
        }

    }
}
