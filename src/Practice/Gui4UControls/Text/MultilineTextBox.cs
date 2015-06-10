// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultilineTextBox.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Shows multiple lines of text. But has no scroll bar.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Text
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows multiple lines of text. But has no scroll bar.
    /// </summary>
    public class MultilineTextBox : Control
    {
        /// <summary>
        /// The text that must be shown on this control.
        /// </summary>
        private string text;

        /// <summary>
        /// The text that must be shown on this control. But now has split in separate lines.
        /// </summary>
        private List<string> textLines;

        /// <summary>
        /// Whether we must redraw the control during Update().
        /// </summary>
        private bool mustRedraw;

        /// <summary>
        /// The font used for this Control.
        /// </summary>
        private string configFont;

        /// <summary>
        /// The unique name creator, used for each internal line.
        /// </summary>
        private readonly UniqueNameCreator uniqueNameCreator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultilineTextBox"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MultilineTextBox(string name) : base(name)
        {
            this.text = "I am still empty";
            this.textLines = new List<string>();
            this.uniqueNameCreator = UniqueNameCreator.CreateInstance();
            this.MyLines = new Collection<Label>();
        }

        /// <summary>
        /// Gets my lines. These are a collection of Label-Controls.
        /// </summary>
        /// <value>
        /// My lines.
        /// </value>
        public Collection<Label> MyLines { get; private set; }

        /// <summary>
        /// Gets or sets the text that should be shown on the Control.
        /// </summary>
        /// <value>
        /// The configuration text.
        /// </value>
        public string ConfigText
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.mustRedraw = true;
            }
        }

        /// <summary>
        /// Gets or sets the font that should be used for this Control.
        /// </summary>
        /// <value>
        /// The configuration font.
        /// </value>
        public string ConfigFont
        {
            get
            {
                return this.configFont;
            }

            set
            {
                this.configFont = value;
                this.MustReload = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are debugging this Control.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [configuration debug]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigDebug { get; set; }

        /// <summary>
        /// Called when graphics resources need to be loaded.
        /// Use this for the usage of :
        /// - creation of the internal embedded controls.
        /// - setting of the variables and resources in this control
        /// - to load any game-specific graphics resources
        /// - take over the config width and height and use it into State
        /// - overriding how this item looks like , by settings its texture or theme
        /// Call base.LoadContent before you do your override code, this will cause :
        /// - State.SourceRectangle to be reset to the Config.Size
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawSourceRectangleByConfig();

            this.mustRedraw = true;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawSourceRectangleByConfig();

            if (this.mustRedraw)
            {
                this.Redraw();
                this.mustRedraw = false;
            }
        }

        /// <summary>
        /// Draw the texture from CurrentTextureName at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
        }

        /// <summary>
        /// Redraws this instance.
        /// </summary>
        private void Redraw()
        {
            // first going  to find out how many lines can be shown , and remember it into a simple list :  _textLines
            this.textLines = ParseTextIntoLines(this.ConfigText);

            // make sure we have the same number of lines shown , as the number of lines of text that we have
            this.CreateRemoveLabels();

            // populate all the lines with the correct configurations
            this.PopulateTheLines();

            // and resize me
            Config.Height = this.textLines.Count * Theme.ControlHeight;
        }

        /// <summary>
        /// A utility function that parses the text into lines.
        /// </summary>
        /// <param name="textToRead">The text to read.</param>
        /// <returns>The text lines.</returns>
        private static List<string> ParseTextIntoLines(string textToRead)
        {
            // validate the text
            if (string.IsNullOrEmpty(textToRead))
            {
                return new List<string>();
            }

            // lets populate a list
            var list = new List<string>();
            list.Clear();

            // use a string reader to read separate lines
            using (var reader = new StringReader(textToRead))
            {
                string line;

                // keep on reading lines
                while ((line = reader.ReadLine()) != null)
                {
                    // Do something with the line
                    list.Add(line);
                }
            }

            return list;
        }

        /// <summary>
        /// Creates or removes labels , to become in sync with the number of found Lines that we need to show.
        /// </summary>
        private void CreateRemoveLabels()
        {
            // add lines if needed
            while (this.MyLines.Count < this.textLines.Count)
            {
                var labelName = this.uniqueNameCreator.GetUniqueName(Name + "-Line");
                var newLine = new Label(labelName);
                this.AddControl(newLine);
                this.MyLines.Add(newLine);
            }

            // remove lines if needed
            while (this.MyLines.Count > this.textLines.Count)
            {
                var count1 = this.MyLines.Count;
                var lastLine1 = this.MyLines[count1 - 1];

                this.RemoveControl(lastLine1);
                this.MyLines.Remove(lastLine1);
                lastLine1.UnloadContent();
            }
        }

        /// <summary>
        /// Populates the lines.
        /// First clean up the labels , by removing the text.
        /// Then add the text..
        /// Its separated into two phases just for future possibilities
        /// </summary>
        private void PopulateTheLines()
        {
            var lines = this.MyLines;

            // update the text for each line
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var txtLine = this.textLines[i];

                line.ConfigText = txtLine;
                line.ConfigFont = this.configFont;
                line.ConfigHorizontalAlignment = HorizontalAlignment.Left;
                line.ConfigVerticalAlignment = VerticalAlignment.Center;
                line.Config.PositionY = i * line.Config.Height;
                line.Config.PositionX = 0;
                line.Config.Width = this.Config.Width;
                line.Config.Height = Theme.ControlHeight * 0.4f;
            }
        }
    }
}

// code for showing the lines work.. but it is totally not flexible.. i am going to fix that
// then add or remove new Labels that represent lines
// then position all those lines into the control
// then add text to each of those controls