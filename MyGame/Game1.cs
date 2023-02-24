using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace MyGame;

public class Game1 : Game
{   
    Texture2D[] _backGrounds = new Texture2D[5];
    Texture2D _spaceShip;
    Texture2D _asteroid;
    Texture2D _earth;
    private int[][] _astoridPositions = new int[25][];
    private int _currentBackGround;
    private int[] _shipPosition;
    private Random _random;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;



    public Game1()
    {
        _random = new Random();
        _currentBackGround = _random.Next(0,5);
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
       _graphics.PreferredBackBufferWidth = 1280;
       _graphics.PreferredBackBufferHeight = 720;
       _graphics.ApplyChanges();
       this.Window.Title = "Space The Final Frontier";

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spaceShip = Content.Load<Texture2D>("SpaceShip");
        _asteroid = Content.Load<Texture2D>("Asteroid");
        _earth = Content.Load<Texture2D>("Earth");
        for(int i = 0; i <= 4;i++)
        {
            _backGrounds[i] = Content.Load<Texture2D>($"StarScapeBackGround{i + 1}");
        }

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if(gameTime.TotalGameTime.Seconds % 10 == 0 && gameTime.TotalGameTime.Milliseconds <= 200)
        {
            _shipPosition = new int[2] {_random.Next(0, 1280 - 175), _random.Next(0, 720 - 98)};
             _currentBackGround = _random.Next(0,5);
            // when warping gens astorids positions
            int xBase = _random.Next(0, 800);
            int yBase = _random.Next(0, 300);
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 0; y <= 4;y++)
                { 
                    _astoridPositions[x * 5  + y] = new int[2] {xBase + x * 80 + _random.Next(10, 30),yBase + y * 80 +  _random.Next(10, 30)};
                }
            }
        }
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        _spriteBatch.Draw(_backGrounds[_currentBackGround], new Vector2(0,0), Color.White);
        _spriteBatch.Draw(_earth, new Vector2(0, 0), Color.White);
        foreach(int[] asteroidPos in _astoridPositions)
        {
            _spriteBatch.Draw(_asteroid, new Vector2(asteroidPos[0], asteroidPos[1]), Color.White);
        }
        _spriteBatch.Draw(_spaceShip, new Vector2(_shipPosition[0], _shipPosition[1]), Color.White);
        _spriteBatch.End();
        base.Draw(gameTime);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
