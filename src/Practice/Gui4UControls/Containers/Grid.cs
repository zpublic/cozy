// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Grid.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Creates a grid with controls in it
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Containers
{
    using System;

    using GUI4UControls.Buttons;
    using GUI4UControls.Text;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Creates a grid with controls in it
    /// </summary>
    public class Grid : Control
    {
        /// <summary>
        /// The fill type for each grid cell.
        /// </summary>
        private GridFillType configFillType = GridFillType.None;

        /// <summary>
        /// The cell widths.
        /// </summary>
        private float[] cellWidths;

        /// <summary>
        /// The cell heights.
        /// </summary>
        private float[] cellHeights;

        /// <summary>
        /// The column lines are texture, these are the names
        /// </summary>
        private string[] columnLineTextureNames;

        /// <summary>
        /// The row lines are textures, these are the names
        /// </summary>
        private string[] rowLineTextureNames;

        /// <summary>
        /// The cell height
        /// </summary>
        private int cellHeight;

        /// <summary>
        /// The cell width
        /// </summary>
        private int cellWidth;

        /// <summary>
        /// The grid color
        /// </summary>
        private readonly GUIColor gridColor;

        /// <summary>
        /// The control array
        /// </summary>
        private Control[][] controlArray;

        /// <summary>
        /// The line width
        /// </summary>
        private int configLineWidth;

        /// <summary>
        /// The default grid line width
        /// </summary>
        private const int DefaultGridLineWidth = 1;

        /// <summary>
        /// If we must redraw positions and stuff in update.
        /// </summary>
        private bool mustredraw;

        /// <summary>
        /// The row count
        /// </summary>
        private int configRowCount;

        /// <summary>
        /// The column count
        /// </summary>
        private int configColumnCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Grid(string name) : base(name)
        {
            this.cellHeight = Theme.ControlHeight;
            this.cellWidth = Theme.ControlWidth;
            this.configLineWidth = DefaultGridLineWidth;
            this.configColumnCount = 4;
            this.configRowCount = 4;

            Config.Width = this.cellWidth * this.ConfigRowCount;
            Config.Height = this.cellHeight * this.ConfigColumnCount;

            this.gridColor = Theme.FillColor;

            this.mustredraw = true;
        }

        /// <summary>
        /// Gets or sets the width of the line in between the grid items.
        /// </summary>
        /// <value>
        /// The width of the line.
        /// </value>
        public int ConfigLineWidth
        {
            get
            {
                return this.configLineWidth;
            }

            set
            {
                this.configLineWidth = value;
                this.mustredraw = true;
            }
        }

        /// <summary>
        /// Gets or sets the type of the fill.
        /// </summary>
        /// <value>
        /// The type of the fill.
        /// </value>
        public GridFillType ConfigFillType
        {
            get
            {
                return this.configFillType;
            }

            set
            {
                this.configFillType = value;
                this.mustredraw = true;
            }
        }

        /// <summary>Gets or sets the row count.</summary>
        /// <value>The row count.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Value;Cannot create a grid with zero or negative rowCount.</exception>
        public int ConfigRowCount
        {
            get
            {
                return this.configRowCount;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Cannot create a grid with zero or negative rowCount.");
                }

                if (value > 0)
                {
                    this.configRowCount = value;
                    this.mustredraw = true;
                }
            }
        }

        /// <summary>Gets or sets the column count.</summary>
        /// <value>The column count.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Value;Cannot create a grid with zero or negative ConfigColumnCount.</exception>
        public int ConfigColumnCount
        {
            get
            {
                return this.configColumnCount;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Cannot create a grid with zero or negative ConfigColumnCount.");
                }

                if (value > 0)
                {
                    this.configColumnCount = value;
                    this.mustredraw = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the cell.
        /// </summary>
        /// <value>
        /// The width of the configuration cell.
        /// </value>
        public int ConfigCellWidth
        {
            get
            {
                return this.cellWidth;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Cannot create a grid where the cell width is too small.");
                }

                this.cellWidth = value;
                this.mustredraw = true;
            }
        }

        /// <summary>
        /// Gets or sets the height of the cell.
        /// </summary>
        /// <value>
        /// The height of the configuration cell.
        /// </value>
        public int ConfigCellHeight
        {
            get
            {
                return this.cellHeight;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Cannot create a grid where the cell height is too small.");
                }

                this.cellHeight = value;
                this.mustredraw = true;
            }
        }

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
            // this needs to happen first
            base.LoadContent();

            // set my appearance values
            Theme.BorderWidth = 0;
            Theme.FillColor = GUIColor.Transparent();

            // make the internal stuff that i show
            this.mustredraw = true;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.mustredraw)
            {
                this.Calculate();
                this.Populate();

                this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-Background", (int)Config.Width, (int)Config.Height, 1, Theme.WindowFillColor, Theme.BorderColor);
                this.mustredraw = false;
            }
        }

        /// <summary>
        /// Draw the texture at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            if (this.columnLineTextureNames == null)
            {
                return;
            }

            // draw the background
            // Manager.ImageCompositor.Draw(CurrentTextureName, this.State, GUIColor.White());

            // draw lines if needed
            if (this.configLineWidth <= 0)
            {
                return;
            }

            var tempState = new DrawState();

            // draw the column lines
            for (var i = 0; i < this.columnLineTextureNames.Length; i++)
            {
                tempState.DrawPosition = new DVector2((float)i * (this.cellWidth + this.configLineWidth), 0) + State.DrawPosition;
                tempState.Width = this.configLineWidth;
                tempState.Height = Config.Height;
                tempState.SourceRectangle = new Rectangle(0, 0, tempState.Width, tempState.Height);

                var name = this.columnLineTextureNames[i];
                Manager.ImageCompositor.Draw(name, tempState, GUIColor.White());

                // System.Diagnostics.Debug.WriteLine("Drawing column line at : " + tempState.DrawPosition + " to " + (tempState.DrawPosition + new DVector2(tempState.Width, tempState.Height)));
            }

            // draw the row lines
            for (var i = 0; i < this.rowLineTextureNames.Length; i++)
            {
                tempState.DrawPosition = new DVector2(0, (float)i * (this.cellHeight + this.configLineWidth)) + State.DrawPosition;
                tempState.Width = Config.Width;
                tempState.Height = this.configLineWidth;
                tempState.SourceRectangle = new Rectangle(0, 0, tempState.Width, tempState.Height);

                var name = this.rowLineTextureNames[i];
                Manager.ImageCompositor.Draw(name, tempState, GUIColor.White());

                // System.Diagnostics.Debug.WriteLine("Drawing row line at : " + tempState.DrawPosition + " to " + (tempState.DrawPosition + new DVector2(tempState.Width, tempState.Height)));
            }
        }

        /// <summary>Adds the control to my grid.</summary>
        /// <param name="gridIndexX">The grid index position x.</param>
        /// <param name="gridIndexY">The grid index position y.</param>
        /// <param name="control">The control to add to given location.</param>
        /// <exception cref="System.ArgumentNullException">Control that was null.</exception>
        public void AddGridControl(int gridIndexX, int gridIndexY, Control control)
        {
#if DEBUG
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
#endif

            if (gridIndexX >= 0 && gridIndexX < this.ConfigColumnCount && gridIndexY >= 0 && gridIndexY < this.ConfigRowCount && this.controlArray[gridIndexX][gridIndexY] == null)
            {
                this.controlArray[gridIndexX][gridIndexY] = control;
                var position = this.GridPosition(gridIndexX, gridIndexY);

                control.Config.PositionX = position.X;
                control.Config.PositionY = position.Y;
                control.Config.Width = this.cellWidth;
                control.Config.Height = this.cellHeight;

                if (control is Label)
                {
                    var pos = new DVector2(control.Config.PositionX, control.Config.PositionY);
                    pos += new DVector2(this.cellWidth / 2.0f, this.cellHeight / 2.0f);
                    control.State.DrawPosition = pos;
                }

                this.AddControl(control);
                control.LoadContent();
            }
        }

        /// <summary>
        /// Removes the control at location x,y from the grid
        /// </summary>
        /// <param name="gridPositionX">The grid position x.</param>
        /// <param name="gridPositionY">The grid position y.</param>
        /// <exception cref="System.InvalidOperationException">Could not retrieve array from given grid-position</exception>
        public void RemoveGridControl(int gridPositionX, int gridPositionY)
        {
            if (gridPositionX >= 0 && gridPositionX < this.ConfigColumnCount && gridPositionY >= 0 && gridPositionY < this.ConfigRowCount && this.controlArray[gridPositionX][gridPositionY] == null)
            {
                var array = this.controlArray[gridPositionX][gridPositionY];
                if (array == null)
                {
                    throw new InvalidOperationException("Could not retrieve array from given grid-position");
                }

                Children.Remove(array);
                this.controlArray[gridPositionX][gridPositionY] = null;
            }
        }

        /// <summary>
        /// Gets the vector-position at grid-position x,y
        /// </summary>
        /// <param name="gridPositionX">The grid position x.</param>
        /// <param name="gridPositionY">The grid position y.</param>
        /// <returns>The position of the grid item.</returns>
        protected DVector2 GridPosition(int gridPositionX, int gridPositionY)
        {
            return new DVector2((gridPositionX * this.cellWidth) + ((gridPositionX + 1) * this.configLineWidth), (gridPositionY * this.cellHeight) + ((gridPositionY + 1) * this.configLineWidth));
        }

        /// <summary>
        /// Creates the grid.
        ///
        /// 0 Create a 2D array for the used Controls : __controlArray
        /// 1 Create a array for : _cellWidths , _cellHeights , _columnLines and _rowLines
        /// 2 Populate step 0 and 1
        /// 3 Change my Draw Size to the total size thanks to step 2
        /// 4 Create column- and row- line-textures if needed
        /// 5 Populate the _controlArray , by checking the ConfigFillType
        /// </summary>
        protected void Calculate()
        {
            // calculate the resulting size by using cellWidths , cellHeight , gridLineWidth
            // my state should be that size
            Config.Width = (this.ConfigColumnCount * this.cellWidth) + ((this.ConfigColumnCount + 1) * this.configLineWidth);
            Config.Height = (this.ConfigRowCount * this.cellHeight) + ((this.ConfigRowCount + 1) * this.configLineWidth);

            // create my array
            this.controlArray = new Control[this.ConfigRowCount][];
            for (var i = 0; i < this.ConfigColumnCount; i++)
            {
                this.controlArray[i] = new Control[this.ConfigColumnCount];
            }

            this.cellWidths = new float[this.ConfigColumnCount];
            this.cellHeights = new float[this.ConfigRowCount];

            // set the width and height for every row and column
            for (var i = 0; i < this.ConfigColumnCount; i++)
            {
                this.cellWidths[i] = this.cellWidth;
            }

            for (var i = 0; i < this.ConfigRowCount; i++)
            {
                this.cellHeights[i] = this.cellHeight;
            }

            this.UpdateDrawSourceRectangleByConfig();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

        }

        protected void Populate()
        {
            // if we have in between-lines with a size bigger then 0 , then create textures for the
            this.MakeGridLines();

            // walk by every square and populate it
            this.MakeGridControls();
        }

        /// <summary>
        /// Creates the in between lines.
        /// </summary>
        private void MakeGridLines()
        {
            if (this.configLineWidth <= 0)
            {
                return;
            }

            this.columnLineTextureNames = new string[this.ConfigColumnCount + 1];
            for (var i = 0; i < this.columnLineTextureNames.Length; i++)
            {
                var name = string.Format(Name + "-ColumnLine");
                this.columnLineTextureNames[i] = Manager.ImageCompositor.CreateFlatTexture(name + i, this.configLineWidth, (int)State.Height, this.gridColor);
            }

            this.rowLineTextureNames = new string[this.ConfigRowCount + 1];
            for (var i = 0; i < this.rowLineTextureNames.Length; i++)
            {
                var name = string.Format(Name + "-RowLine");
                this.rowLineTextureNames[i] = Manager.ImageCompositor.CreateFlatTexture(name, (int)State.Width, this.configLineWidth, this.gridColor);
            }
        }

        /// <summary>
        /// Populates the grid with controls.
        /// </summary>
        private void MakeGridControls()
        {
            for (var x = 0; x < this.ConfigColumnCount; x++)
            {
                for (var y = 0; y < this.ConfigRowCount; y++)
                {
                    // if no fill type , then continue
                    if (this.configFillType == GridFillType.None)
                    {
                        continue;
                    }

                    var gridPosition = this.GridPosition(x, y);

                    switch (this.configFillType)
                    {
                        case GridFillType.Button:
                            var buttonname = Name + string.Format(" {0}, {1}", x, y);

                            var button = new Button(buttonname)
                                             {
                                                 Config =
                                                     {
                                                         PositionX = gridPosition.X,
                                                         PositionY = gridPosition.Y,
                                                         Width = this.cellWidth,
                                                         Height = this.cellHeight,
                                                     },
                                                 ConfigText = string.Format("{0}, {1}", x, y),
                                                 Theme = this.Theme
                                             };
                            button.Initialize();

                            this.controlArray[x][y] = button;
                            this.AddControl(button);

                            break;

                        case GridFillType.Text:
                            var textname = Name + string.Format(" {0}, {1}", x, y);

                            var text = new Label(textname)
                                           {
                                               Config =
                                                   {
                                                       PositionX =
                                                           gridPosition.X + (this.cellWidth / 2f),
                                                       PositionY =
                                                           gridPosition.Y + (this.cellHeight / 2f),
                                                       Width = this.cellWidth,
                                                       Height = this.cellHeight,
                                                   },
                                               ConfigText = string.Format("{0}, {1}", x, y),
                                               ConfigHorizontalAlignment = HorizontalAlignment.Center,
                                               ConfigVerticalAlignment = VerticalAlignment.Center,
                                           };

                            text.Initialize();
                            this.controlArray[x][y] = text;
                            this.AddControl(text);

                            break;
                    }
                }
            }
        }
    }
}
