using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson05;

public class InputGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteFont _font;

    private string _message = "";

    // we will know what state the keyboard was in the last call to update
    // and the current call to updat
    private KeyboardState _kbPreviousState, _kbCurrentState;
    public InputGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _kbPreviousState = Keyboard.GetState();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // _spriteBatch = new SpriteBatch 
        _font = Content.Load<SpriteFont>("SystemArialFont");
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _kbCurrentState = Keyboard.GetState();
  
        _message = "";

        if (_kbCurrentState.IsKeyDown(Keys.Up))
        {
            _message += "Up ";
        }
        if (_kbCurrentState.IsKeyDown(Keys.Down))
        {
            _message += "Down ";
        }
        if (_kbCurrentState.IsKeyDown(Keys.Left))
        {
            _message += "Left ";
        }
        if (_kbCurrentState.IsKeyDown(Keys.Right))
        {
            _message += "Right ";
        }

        #region arrow keys
        // "&&" in c# means "and"
        // "||" in c# means "or"
        // if (_kbPreviousState.IsKeyUp(Keys.Space) && _kbCurrentState.IsKeyDown(Keys.Space))
        if (IsKeyPressed(Keys.Space))
        {
            _message += "\n";             
            _message += "Space pressed\n";            
            _message += "----------------------------------------\n";             
            _message += "----------------------------------------\n";             
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";             
            _message += "----------------------------------------\n";             
            _message += "----------------------------------------\n";
        }

        else if (_kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "\n";
            _message += "Space held";
        }
        else if (_kbPreviousState.IsKeyDown(Keys.Space))
        {
            _message += "\n";
            _message += "Space released\n";
            _message += "++++++++++++++++++++++++++++++++++++++++++\n";
            _message += "++++++++++++++++++++++++++++++++++++++++++\n";
            _message += "++++++++++++++++++++++++++++++++++++++++++\n";
            _message += "++++++++++++++++++++++++++++++++++++++++++\n";
            _message += "++++++++++++++++++++++++++++++++++++++++++\n";
            _message += "++++++++++++++++++++++++++++++++++++++++++\n";
            _message += "++++++++++++++++++++++++++++++++++++++++++\n";
            _message += "++++++++++++++++++++++++++++++++++++++++++\n";
        }
        _kbPreviousState = _kbCurrentState;
        #endregion
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.DrawString(_font, _message, Vector2.Zero, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
private bool IsKeyHeld(Keys key)
    {
        return _kbCurrentState.IsKeyDown(key);
    }
    private bool IsKeyPressed(Keys key)
    {
        return _kbPreviousState.IsKeyUp(key) && _kbCurrentState.IsKeyDown(key);
    }
}
