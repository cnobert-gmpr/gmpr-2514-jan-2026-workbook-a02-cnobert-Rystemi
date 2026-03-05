using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson08MosquitoAttack;

public class MosquitoAttackGame : Game
{
    private const int _WindowWidth = 550;
    private const int _WindowHeight = 400;
 
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _background;
    private SpriteFont _font;
    private string _message = "";
    private KeyboardState _kbPrevState, _kbCurrentState;

    private enum GameState { Playing, Paused, Over }
    private GameState _gameState;

    Cannon _cannon;

    public MosquitoAttackGame()
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

        _cannon = new Cannon();
        _cannon.Initialize(new Vector2(50, 325), 150f);

        _gameState = GameState.Playing;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _background = Content.Load<Texture2D>("Background");
        _font = Content.Load<SpriteFont>("SystemArialFont");

        _cannon.LoadContent(Content);

    }

    protected override void Update(GameTime gameTime)
    {
        _kbCurrentState = Keyboard.GetState();

        switch(_gameState)
        {
            case GameState.Playing:
                #region keyboard input
                if(_kbCurrentState.IsKeyDown(Keys.Left) || _kbCurrentState.IsKeyDown(Keys.A)) 
                    _cannon.Direction = new Vector2(-1, 0);
                else if(_kbCurrentState.IsKeyDown(Keys.Right) || _kbCurrentState.IsKeyDown(Keys.D))
                    _cannon.Direction = new Vector2(1, 0);
                else
                    _cannon.Direction = Vector2.Zero;

                if (Pressed(Keys.P))
                    {
                        _gameState = GameState.Paused;
                        _message = "Game Paused, Press P to continue.";
                    }
                #endregion
                _cannon.Update(gameTime);
                break;
            case GameState.Paused:
                if (Pressed(Keys.P))
                    {
                        _gameState = GameState.Playing;
                        _message = "";
                    }
                break;
            case GameState.Over:
                break;
            
            
        }
        _kbPrevState = _kbCurrentState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();

        switch(_gameState)
        {
            case GameState.Playing:
                _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
                _cannon.Draw(_spriteBatch);
                break;
            case GameState.Paused:
                _spriteBatch.Draw(_background, Vector2.Zero, Color.Silver);
                _cannon.Draw(_spriteBatch);
                _spriteBatch.DrawString(_font, _message, new Vector2 (125,175), Color.White);
                break;
            case GameState.Over:
                break;
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
    private bool Pressed(Keys key)
    {
        return _kbCurrentState.IsKeyDown(Keys.P) && _kbPrevState.IsKeyUp(Keys.P);
    }
}
