using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson07Pong;

public class Paddle{
    private Texture2D _texture;
    private Vector2 _positionR, _dimensionsR, _directionR;
    private Vector2 _positionL, _dimensionsL, _directionL;
    private float _speed;
    private Rectangle _playAreaBoundingBox;


    internal void Initialize(Vector2 position, Vector2 dimensions, Vector2 direction, float speed, Rectangle playAreaBoundingBox)
    {
            _positionR = position;
            _dimensionsR = dimensions;
            _directionR = direction;
            _positionL = position;
            _dimensionsL = dimensions;
            _directionL = direction;
            _speed = speed;
            _playAreaBoundingBox = playAreaBoundingBox;
        }
    internal void LoadContent(ContentManager content)
    {
            _texture = content.Load<Texture2D>("Paddle");
        }

    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        KeyboardState kbState = Keyboard.GetState();
        if(kbState.IsKeyDown(Keys.Up))
        {
            _directionR = new Vector2(0, -1);
        }
        else if(kbState.IsKeyDown(Keys.Down))
        {
            _directionR = new Vector2(0, 1);
        }
        else
        {
            _directionR = Vector2.Zero;
        }

        _positionR += _directionR * _speed * dt;

        if(_positionR.Y <= _playAreaBoundingBox.Top)
        {
            _positionR.Y = _playAreaBoundingBox.Top;
        }
        else if( (_positionR.Y + _dimensionsR.Y) >= _playAreaBoundingBox.Bottom)
        {
            _positionR.Y = _playAreaBoundingBox.Bottom - _dimensionsR.Y;
        }

        if(kbState.IsKeyDown(Keys.W))
        {
            _directionL = new Vector2(0, -1);
        }
        else if(kbState.IsKeyDown(Keys.S))
        {
            _directionL = new Vector2(0, 1);
        }
        else
        {
            _directionL = Vector2.Zero;
        }

         _positionL += _directionL * _speed * dt;

        if(_positionL.Y <= _playAreaBoundingBox.Top)
        {
            _positionL.Y = _playAreaBoundingBox.Top;
        }
        else if((_positionL.Y + _dimensionsL.Y) >= _playAreaBoundingBox.Bottom)
        {
            _positionL.Y = _playAreaBoundingBox.Bottom - _dimensionsL.Y;
        }
        
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle paddleRectangleR = new Rectangle((int) _positionR.X, (int) _positionR.Y, (int) _dimensionsR.X, (int) _dimensionsR.Y);
        spriteBatch.Draw(_texture, paddleRectangleR, Color.White);

        Rectangle paddleRectangleL = new Rectangle((int) _positionL.X, (int) _positionL.Y, (int) _dimensionsL.X, (int) _dimensionsL.Y);
        spriteBatch.Draw(_texture, paddleRectangleL, Color.White);
    }
}