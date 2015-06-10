// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListBox.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   List of selectable items that can be image/text
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Buttons
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using GUI4UControls.ScrollBar;

    using GUI4UFramework.Colors;
    using GUI4UFramework.EventArgs;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// List of selectable items that can be image/text
    /// </summary>
    public class ListBox : Control
    {
        /// <summary>
        /// The default line spacing
        /// </summary>
        private const float DefaultLineSpacing = 2;

        /// <summary>
        /// Occurs when a list-box item is selected.
        /// </summary>
        public event EventHandler OnItemSelect;

        /// <summary>
        /// The height of all the list items together
        /// </summary>
        private int totalListHeight;

        /// <summary>
        /// The scroll bar vertical
        /// </summary>
        private ScrollBarVertical scrollBarVertical;

        /// <summary>
        /// We must redraw the vertical scrollbar during Update()
        /// </summary>
        private bool mustRedrawVerticalScrollBar;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBox"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ListBox(string name) : base(name)
        {
            this.FontColor = GUIColor.MidnightBlue();
            this.ListBoxItems = new Collection<ListBoxItem>();
            this.SelectedIndex = -1;
            this.ItemHeight = this.Theme.ControlHeight;
            this.Theme.FillColor = GUIColor.White();
            this.Theme.BorderColor = GUIColor.Black();
            this.Config.Width = this.Theme.ControlWidth;
            this.Config.Height = this.Theme.ControlWidth;
        }

        /// <summary>
        /// Gets or sets the vertical scrollbar.
        /// </summary>
        /// <value>
        /// The scroll bar vertical.
        /// </value>
        protected ScrollBarVertical ScrollBarVertical
        {
            get { return this.scrollBarVertical; }
            set { this.scrollBarVertical = value; }
        }

        /// <summary>
        /// Gets or sets the height of the items that are shown.
        /// </summary>
        /// <value>
        /// The height of the item.
        /// </value>
        public float ItemHeight { get; set; }

        /// <summary>
        /// Gets or sets the index of the selected list-box-item.
        /// </summary>
        /// <value>
        /// The index of the selected.
        /// </value>
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Gets the ListBox items controls that are shown in this control.
        /// </summary>
        /// <value>
        /// The ListBox items.
        /// </value>
        public Collection<ListBoxItem> ListBoxItems { get; private set; }

        /// <summary>
        /// Gets or sets the name of the sprite font.
        /// </summary>
        /// <value>
        /// The name of the sprite font.
        /// </value>
        public string SpriteFontName { get; set; }

        /// <summary>
        /// Gets or sets the color of the font.
        /// </summary>
        /// <value>
        /// The color of the font.
        /// </value>
        public GUIColor FontColor { get; set; }

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
            // lineHeight = spriteFont.MeasureString("T").Y;
            base.LoadContent();

            this.UpdateDrawSizeByConfig();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSourceRectangleByConfig();

            const int VerticalScrollBarWidth = 24;
            this.State.Width = this.Config.Width - VerticalScrollBarWidth;

            // Load the scrollbar with the new position!
            this.scrollBarVertical = new ScrollBarVertical(this.Name + "-ScrollBarVertical")
            {
                Config =
                    {
                        PositionX = this.State.Width,
                        PositionY = 0,
                        Width = VerticalScrollBarWidth,
                        Height = this.State.Height
                    }
            };
            this.AddControl(this.scrollBarVertical);
            this.scrollBarVertical.ConfigDebugLayout = false;
            this.scrollBarVertical.EventScrolling += this.OnScrollBarVerticalScroll;

            this.Manager.ImageCompositor.CreateSpriteFont(this.Theme.FontName);

            // set my appearance values
            this.CurrentTextureName = this.Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-background", (int)this.State.Width, (int)this.State.Height, 1, this.Theme.ContainerFillColor, this.Theme.BorderColor);

            this.UpdateVisibility();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            this.scrollBarVertical.UnloadContent();
            foreach (var listItem in this.ListBoxItems)
            {
                listItem.UnloadContent();
            }

            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (this.Config.Changed)
            {
                this.UpdateVisibility();
                this.Config.ResetChanged();
            }

            this.UpdateDrawSizeByConfig();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSourceRectangleByConfig();

            const int VerticalScrollBarWidth = 24;
            this.State.Width = this.Config.Width - VerticalScrollBarWidth;

            if (this.mustRedrawVerticalScrollBar)
            {
                this.mustRedrawVerticalScrollBar = false;
                this.RedrawVerticalScrollBar();
            }
        }

        /// <summary>
        /// Draw the texture at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            if (this.Config.Visible == false)
            {
                return;
            }

            base.DrawMyData();
        }

        /// <summary>Called when the vertical scrollbar is been scrolled.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="floatEventArgs">The <see cref="FloatEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentNullException">FloatEventArgs is null.</exception>
        protected void OnScrollBarVerticalScroll(object sender, FloatEventArgs floatEventArgs)
        {
#if DEBUG
            if (floatEventArgs == null)
            {
                throw new ArgumentNullException("floatEventArgs");
            }
#endif

            var y = floatEventArgs.Value;
            foreach (var listBoxItem in this.ListBoxItems)
            {
                listBoxItem.State.DrawPosition = new DVector2(listBoxItem.State.DrawPosition.X, listBoxItem.State.DrawPosition.Y - y);
            }
        }

        /// <summary>Called when a list-box-item gets selected.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentNullException">EventArgs is null.</exception>
        protected void OnSelect(object sender, EventArgs eventArgs)
        {
#if DEBUG
            if (eventArgs == null)
            {
                throw new ArgumentNullException("eventArgs");
            }
#endif

            var i = 0;
            var item = sender;
            foreach (var listBoxItem in this.ListBoxItems)
            {
                if (item != listBoxItem)
                {
                    listBoxItem.Selected = false;
                }
                else
                {
                    this.SelectedIndex = i;
                }

                i++;
            }

            // Trigger change listBoxItem event
            if (this.OnItemSelect != null)
            {
                this.OnItemSelect(this, null);
            }
        }

        /// <summary>Adds a list-box-item to me,</summary>
        /// <param name="listBoxItem">The list box item.</param>
        /// <exception cref="System.ArgumentNullException">ListBoxItem is null.</exception>
        public void AddListItem(ListBoxItem listBoxItem)
        {
#if DEBUG
            if (listBoxItem == null)
            {
                throw new ArgumentNullException("listBoxItem");
            }
#endif
            // get where to place the next item
            var currentY = this.CalculateTotalListHeight();
            var newY = currentY + this.Theme.ControlLargeSpacing;
            var newWidth = this.Config.Width - (this.Theme.ControlLargeSpacing * 2);

            this.AddControl(listBoxItem);
            this.ListBoxItems.Add(listBoxItem);
            listBoxItem.Theme = this.Theme;
            listBoxItem.Config.Visible = false;
            listBoxItem.Config.PositionX = this.Theme.ControlLargeSpacing;
            listBoxItem.Config.PositionY = newY;
            listBoxItem.Config.Width = newWidth;
            listBoxItem.Config.Visible = true;

            // Ensure scrollbar uses relevant scroll area
            this.mustRedrawVerticalScrollBar = true;

            listBoxItem.ItemPressed += this.OnSelect;
        }

        /// <summary>
        /// Redraws the vertical scroll bar during Update()
        /// </summary>
        private void RedrawVerticalScrollBar()
        {
            this.totalListHeight = this.CalculateTotalListHeight();
            if (this.totalListHeight > (this.State.Height - DefaultLineSpacing))
            {
                this.scrollBarVertical.Config.Visible = true;
                this.ScaleScrollBar();
            }

            this.RefreshAllListBoxSizes();
        }

        /// <summary>
        /// Removes the given list item from my list.
        /// </summary>
        /// <param name="item">The item.</param>
        public void RemoveListItem(ListBoxItem item)
        {
            var removedItem = false;
            foreach (var cmbItem in this.ListBoxItems)
            {
                if (cmbItem == item)
                {
                    this.Children.Remove(cmbItem);

                    // Ensure scrollbar uses relevant scroll area
                    this.totalListHeight -= (int)(cmbItem.State.Height + DefaultLineSpacing);
                    if (this.totalListHeight < (this.State.Height - DefaultLineSpacing))
                    {
                        this.scrollBarVertical.Config.Visible = false;
                    }

                    this.ScaleScrollBar();

                    removedItem = true;
                    break;
                }
            }

            if (removedItem)
            {
                this.ListBoxItems.Remove(item);
                this.RefreshAllListBoxSizes();
            }
        }

        /// <summary>
        /// Clears the items in this list-view
        /// </summary>
        public void ClearItems()
        {
            foreach (var cmbItem in this.ListBoxItems)
            {
                this.Children.Remove(cmbItem);
                cmbItem.UnloadContent();
            }

            this.ListBoxItems.Clear();
            this.scrollBarVertical.Config.Visible = false;
            this.totalListHeight = this.Theme.ControlLargeSpacing;
        }

        /// <summary>
        /// Calculates the total height of all the list-box-items combined.
        /// </summary>
        /// <returns>The total list-box-items height</returns>
        private int CalculateTotalListHeight()
        {
            var height = 0;
            foreach (var listBoxItem in this.ListBoxItems)
            {
                height += (int)(listBoxItem.State.Height + DefaultLineSpacing);
            }

            return height;
        }

        /// <summary>
        /// Returns all the selected list-box-items.
        /// </summary>
        /// <returns>The selected items.</returns>
        public IEnumerable<ListBoxItem> SelectedItems()
        {
            var selectedItems = new List<ListBoxItem>();
            foreach (var listBoxItem in this.ListBoxItems)
            {
                if (listBoxItem.Selected)
                {
                    selectedItems.Add(listBoxItem);
                }
            }

            return selectedItems.Count > 0 ? 
                selectedItems : 
                null;
        }

        /// <summary>
        /// De-selects all. the list-box-items that were selected.
        /// </summary>
        public void UnselectAll()
        {
            foreach (var listBoxItem in this.ListBoxItems)
            {
                listBoxItem.Selected = false;
            }

            this.SelectedIndex = -1;
        }

        /// <summary>
        /// Scale max and step so we do not go over the max distance or under the min
        /// </summary>
        protected void ScaleScrollBar()
        {
            if (this.totalListHeight <= 0)
            {
                return;
            }

            // Scale max and step so we do not go over the max distance or under the min
            // var step = (State.Height - (DefaultPadding + DefaultLineSpacing));
            // float max = _totalListHeight;
            // var indicatorRatio = (max - step) / max;

            // _verticalScrollBar.SetStep(step * indicatorRatio);
            // _verticalScrollBar.SetMax(max * indicatorRatio);
        }

        /// <summary>
        /// Refresh the X size based on whether the vertical scrollbar is visible
        /// </summary>
        /// <param name="item">The item to resize.</param>
        private void RefreshListBoxItemSize(ListBoxItem item)
        {
            DVector2 size;
            if (this.scrollBarVertical.Config.Visible)
            {
                size = new DVector2(
                                    this.Config.Width - (2f * this.Theme.ControlLargeSpacing) - this.scrollBarVertical.Config.Width, 
                                    item.Config.Height);
            }
            else
            {
                size = new DVector2(
                                    this.Config.Width - (2f * this.Theme.ControlLargeSpacing), 
                                    item.Config.Height);
            }

            item.State.Width = size.X;
            item.State.Height = size.Y;
        }

        /// <summary>
        /// Refreshes all ListBox sizes.
        /// </summary>
        private void RefreshAllListBoxSizes()
        {
            foreach (var item in this.ListBoxItems)
            {
                this.RefreshListBoxItemSize(item);
            }
        }

        /// <summary>
        /// Updates the visibility of this control and the list-box items.
        /// </summary>
        public void UpdateVisibility()
        {
            this.scrollBarVertical.Config.Visible = this.Config.Visible;
            foreach (var node in this.Children)
            {
                var child = node as Control;
                if (child == null)
                {
                    return;
                }

                child.Config.Visible = this.Config.Visible;
            }
        }
    }
}
