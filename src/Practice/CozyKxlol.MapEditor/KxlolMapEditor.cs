using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Starbound.UI.Resources;
using Starbound.UI.XNA.Resources;

namespace CozyKxlol.MapEditor
{
    public class KxlolMapEditor : CozyGame
    {
        IResourceManager resourceManager;

        public KxlolMapEditor()
        {
            WindowSize              = new Point(960, 640);
            Content.RootDirectory   = "Content";
            IsMouseVisible          = true;

            CozyDirector.Instance.WindowSize = WindowSize;
        }

        protected override void Initialize()
        {
            base.Initialize();
            CozyDirector.Instance.RunWithScene(new MapEditorScene());
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            resourceManager = new XNAResourceManager(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
