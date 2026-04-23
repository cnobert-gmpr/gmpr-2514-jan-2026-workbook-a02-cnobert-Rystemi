using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson07Pong;

public class Pong : Game
{
    private const int _WindowWidth = 750, _WindowHeight = 450;
    private const int _PlayAreaEdgeLineWidth = 12;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _backgroundTexture;

    private Ball _ball;
    private Paddle _paddleR;
    private Paddle _paddleL;
    
    // C# properties are the "getters and setters" for C#
    // They are used to expose data in a controlled way.
    // PlayAreaBoundingBox is a "read only" property (there is no setter)
    internal Rectangle PlayAreaBoundingBox
    {
        get
        {
            return new Rectangle(0, _PlayAreaEdgeLineWidth, _WindowWidth, _WindowHeight - (2 * _PlayAreaEdgeLineWidth));
        }
    }

    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        _ball = new Ball();
        _ball.Initialize(new Vector2(150,195), new Vector2 (21,21), new Vector2(-1,-1), 60, PlayAreaBoundingBox);
        
        _paddleR = new Paddle();
        _paddleR.Initialize(new Vector2(690, 198), new Vector2(8, 124), 240, PlayAreaBoundingBox);
        
        _paddleL = new Paddle();        
        _paddleL.Initialize(new Vector2(54, 198), new Vector2(8, 124), 240, PlayAreaBoundingBox);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundTexture = Content.Load<Texture2D>("Court");
        _paddleR.LoadContent(Content);
        _paddleL.LoadContent(Content);
        _ball.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        #region Keyboard Input
        KeyboardState kbState = Keyboard.GetState();
        if(kbState.IsKeyDown(Keys.Up))
            _paddleR.Direction = new Vector2(0, -1);
        else if(kbState.IsKeyDown(Keys.Down))
            _paddleR.Direction = new Vector2(0, 1);
        else
            _paddleR.Direction = Vector2.Zero;

        if(kbState.IsKeyDown(Keys.W))
            _paddleL.Direction = new Vector2(0, -1);
        else if(kbState.IsKeyDown(Keys.S))
            _paddleL.Direction = new Vector2(0, 1);
        else
            _paddleL.Direction = Vector2.Zero;
        #endregion
        _ball.Update(gameTime);
        _paddleR.Update(gameTime);
        _paddleL.Update(gameTime);

        _ball.ProcessCollision(_paddleR.BoundingBox);
        _ball.ProcessCollision(_paddleL.BoundingBox);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _WindowWidth, _WindowHeight), Color.White);

        _ball.Draw(_spriteBatch);
        _paddleR.Draw(_spriteBatch);
        _paddleL.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}