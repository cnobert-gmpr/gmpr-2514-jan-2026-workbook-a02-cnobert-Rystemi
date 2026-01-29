using System.Net.Http.Headers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson02;

public class IntroGame : Game
{
    // an object that represents the screen
    private GraphicsDeviceManager _graphics;
    // an object that batches up draw commands 
    // so that they can be sent to the screen all at once
    private SpriteBatch _spriteBatch;

    private Texture2D _pixel;

    private float _xPosition = 100, _yPosition = 150;
    private float _speed = 150;
    private int _width = 80, _height = 50;

    public IntroGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // new texture that is 1px by 1px
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new [] {Color.White});
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        _xPosition += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        int windowWidth = _graphics.PreferredBackBufferWidth;
        float maxX = windowWidth - _width;
        
        if (_xPosition > maxX);
        {
            _xPosition = maxX;
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Lavender);

        // all draw commands should always begin with SpriteBatch begin and end
        _spriteBatch.Begin();
        // writing (int) beforehand is called casting
        Rectangle rect = new Rectangle((int)_xPosition, (int)_yPosition, _width, _height);
        _spriteBatch.Draw(_pixel, rect, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
