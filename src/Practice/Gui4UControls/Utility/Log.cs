// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Log.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the Log type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Utility
{
    using System;

    using GUI4UControls.Text;

    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// A log contains multiple lines that update like a scrolling log-box
    /// </summary>
    public class Log : Control
    {
        /// <summary>
        /// The text box that is used to show the text
        /// </summary>
        private MultilineTextBox textBox;

        /// <summary>
        /// The last update time
        /// </summary>
        private float lastUpdateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Log(string name) : base(name)
        {
            this.Config.Width = 5 * Theme.ControlWidth;

            this.Config.Height = 5 * Theme.ControlHeight;
        }

        /// <summary>
        /// Gets or sets the text shown
        /// </summary>
        /// <value>
        /// The configuration text.
        /// </value>
        public string ConfigText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we are testing [debug text generation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug text generation]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugTextGeneration { get; set; }

        /// <summary>
        /// Called when graphics resources need to be loaded. 
        /// 
        /// Use this for the usage of :
        /// - creation of the internal embedded controls.
        /// - setting of the variables and resources in this control
        /// - to load any game-specific graphics resources
        /// - take over the config width and height and use it into State
        /// - overriding how this item looks like , by settings its texture or theme
        /// 
        /// Call base.LoadContent before you do your override code, this will cause :
        /// - State.SourceRectangle to be reset to the Config.Size 
        /// </summary>
        public override void LoadContent()
        {
            // do the basic stuff 
            base.LoadContent();

            // update state by using config
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawSourceRectangleByConfig();

            // create the text to show
            this.textBox = new MultilineTextBox(this.Name + "-Text")
                               {
                                   Config =
                                       {
                                           Width = this.Config.Width,
                                           Height = this.Config.Height,
                                           PositionX = 0,
                                           PositionY = 0
                                       },
                                   ConfigFont = "Fonts\\LucidaConsole"
                               };
            this.AddControl(this.textBox);

            // create the background
            this.CurrentTextureName = this.Manager.ImageCompositor.CreateRectangleTexture(
                                                                                    this.Name + "-Background",
                                                                                    (int)this.Config.Width,
                                                                                    (int)this.Config.Height,
                                                                                    1,
                                                                                    Theme.ContainerFillColor,
                                                                                    Theme.BorderColor);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // do the basic stuff
            base.Update(gameTime);

            // update state by using config
            if (Config.Changed)
            {
                this.UpdateDrawPositionByConfigAndParent();
                this.UpdateDrawSizeByConfig();
            }

            // update text when needed
            this.textBox.ConfigText = this.ConfigText;

            if (this.DebugTextGeneration == true)
            {
                this.CreateSomeDebugText(gameTime);
            }
        }

        /// <summary>
        /// A utility that creates some debug text.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void CreateSomeDebugText(GameTime gameTime)
        {
            // convert milliseconds into sec
            var t = gameTime.TotalGameTime.TotalMilliseconds / 1000f;

            // if a interval passed , do stuff
            if (t > this.lastUpdateTime + 0.04f)
            {
                this.lastUpdateTime = (float)t;

                var myText = string.Empty;

                // create 10 lines of bull
                var rnd = new Random();
                for (int i = 0; i < 12; i++)
                {
                    var debugText = GUI4UFramework.Utility.CreateRandomText(rnd, 70);
                    myText = myText + debugText + "\n";
                }

                this.ConfigText = myText;
            }
        }
    }
}
