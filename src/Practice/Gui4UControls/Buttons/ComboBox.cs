// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComboBox.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Combo box drop-down list.
//   A control for the text, a button for drop-down, and a ListBox for the drop-down itself.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Buttons
{
    using System;
    using System.Diagnostics;

    using GUI4UControls.Images;
    using GUI4UControls.Text;

    using GUI4UFramework.EventArgs;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Combo box drop-down list.
    /// A control for the text, a button for drop-down, and a ListBox for the drop-down itself.
    /// </summary>
    public class ComboBox : Control
    {
        /// <summary>
        /// The image that is shown in the combo-box.
        /// </summary>
        private ImageControl imageControlBoxForValue;

        /// <summary>
        /// The text value that is shown in the combo-box.
        /// </summary>
        private Label textValue;

        /// <summary>
        /// The button that toggles the drop-down menu.
        /// </summary>
        private ToggleButton dropDownButton;

        /// <summary>
        /// Occurs when there is a change in the selected item.
        /// </summary>
        public event EventHandler<StringEventArgs> OnSelectedItemChanged;

        /// <summary>
        /// Occurs when the drop-down is shown/hidden.
        /// </summary>
        public event EventHandler<BooleanEventArgs> OnShowHide;

        /// <summary>
        /// True when the drop-down is shown.
        /// </summary>
        private bool dropDownListShown;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBox"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ComboBox(string name)
            : base(name)
        {
            this.ImagePath = null;
            this.Text = null;
            this.Config.Width = this.Theme.ControlWidth;
            this.Config.Height = this.Theme.ControlHeight;
            this.DropDownList = new ListBox(this.Name + "-DropDownList");
            this.SetText(string.Empty);
        }

        /// <summary>
        /// Gets or sets the drop down list control.
        /// </summary>
        /// <value>
        /// The drop down list.
        /// </value>
        public ListBox DropDownList { get; set; }

        /// <summary>
        /// Gets or sets the image path for the image shown.
        /// </summary>
        /// <value>
        /// The image path.
        /// </value>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the text for the item shown.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether [the drop-down-list is shown].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [drop-down-list is shown]; otherwise, <c>false</c>.
        /// </value>
        public bool DropDownListShown
        {
            get { return this.dropDownListShown; }
            set { this.dropDownListShown = value; }
        }

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
            // float textXOffset = Padding;
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawSourceRectangleByConfig();

            // add a image-item if image name is set 
            if (this.ImagePath != null)
            {
                this.imageControlBoxForValue = new ImageControl(this.Name + "-" + this.ImagePath)
                {
                    Config =
                    {
                        PositionX = this.Theme.ControlSmallSpacing,
                        PositionY = this.Theme.ControlSmallSpacing,
                        Width = (int)(this.State.Width - (2 * this.Theme.ControlSmallSpacing)),
                        Height = (int)(this.State.Height - (2 * this.Theme.ControlSmallSpacing)),
                    },
                    ImagePath = this.ImagePath,
                    Manager = this.Manager
                };

                this.imageControlBoxForValue.Initialize();

                this.Children.Add(this.imageControlBoxForValue);

                // textXOffset += _imageBoxForValue.State.DrawPosition.X + _imageBoxForValue.State.Width;
            }

            // add a text-box
            this.textValue = new Label(this.Name + "-TextBox")
            {
                ConfigText = this.Text,
                ConfigHorizontalAlignment = HorizontalAlignment.Left,
                ConfigVerticalAlignment = VerticalAlignment.Center,
                Config =
                {
                    Width = this.Config.Width - this.Config.Height,
                    Height = this.Config.Height,
                },
                ConfigDebugLayout = false
            };
            this.textValue.Initialize();
            this.AddControl(this.textValue);

            // add the drop-down button
            this.dropDownButton = new ToggleButton(this.Name + "-DropDownButton")
            {
                ConfigText = string.Empty,
                Config =
                {
                    Width = this.Config.Height,
                    Height = this.Config.Height
                }
            };
            this.dropDownButton.Config.PositionX = this.Config.Width - this.dropDownButton.Config.Width;
            this.AddControl(this.dropDownButton);
            this.dropDownButton.OnToggle += this.OnToggleDropDown;

            // add the drop down list
            this.DropDownList = new ListBox(this.Name + "-FlapOut")
            {
                Config =
                {
                    Width = this.Config.Width,
                    Height = this.Config.Width,
                    Visible = false,
                    PositionY = this.Config.PositionY + this.Config.Height
                }
            };
            this.AddControl(this.DropDownList);
            this.DropDownList.OnItemSelect += this.OnItemSelect;

            this.CurrentTextureName = this.Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-Background", (int)this.Config.Width, (int)this.Config.Height, this.Theme.BorderWidth, this.Theme.ContainerFillColor, this.Theme.BorderColor);

            this.dropDownListShown = false;

            base.LoadContent();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            if (this.imageControlBoxForValue != null)
            {
                this.imageControlBoxForValue.UnloadContent();
            }

            this.dropDownButton.OnToggle -= this.OnToggleDropDown;
            this.textValue.UnloadContent();
            this.dropDownButton.UnloadContent();
            this.DropDownList.UnloadContent();
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var leftPressed = this.Manager.InputManager.ReadLeftMousePressed();
            if (leftPressed)
            {
                Debug.WriteLine("ComboBox pressed !");
            }

            ////if (!_dropDownButton.State.MouseHoveringOver && !DropDownList.State.MouseHoveringOver && leftPressed && _listShown)
            ////{
            ////    _dropDownButton.Toggle(this, new ButtonStateEventArgs(ButtonState.Off));
            ////    OnToggleDropDown(this, new ButtonStateEventArgs(ButtonState.Off));
            ////}
        }

        /// <summary>
        /// Sets the text shown on the combo-box.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetText(string value)
        {
            this.Text = value;

            // Also try select the item in the drop down list
            if (this.DropDownList == null)
            {
                return;
            }

            foreach (var item in this.DropDownList.ListBoxItems)
            {
                if (item.Text != value)
                {
                    continue;
                }

                item.Selected = true;
                break;
            }
        }

        /// <summary>
        /// Add an item to the combo box.
        /// </summary>
        /// <param name="value">ConfigText of the item.</param>
        /// <param name="imagePath">Optional image to load.</param>
        public void AddItem(string value, string imagePath)
        {
            var item = new ListBoxItem(value, value)
                           {
                               ImagePath = imagePath
                           };

            this.DropDownList.AddListItem(item);
        }

        /// <summary>
        /// Called when a item is selected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnItemSelect(object sender, EventArgs eventArgs)
        {
            this.dropDownButton.Toggle(this, new ButtonStateEventArgs(ButtonState.Off));
            this.DropDownList.Config.Visible = false;
            this.Manager.ForegroundSceneNodes.Children.Remove(this.DropDownList);

            if (this.dropDownListShown)
            {
                this.dropDownListShown = false;
            }

            if (this.OnShowHide != null)
            {
                this.OnShowHide(this, new BooleanEventArgs(false));
            }

            var index = this.DropDownList.SelectedIndex;
            if (index > -1 && index < this.DropDownList.ListBoxItems.Count)
            {
                this.textValue.ConfigText = this.DropDownList.ListBoxItems[index].Text;
                this.ImagePath = this.DropDownList.ListBoxItems[index].ImagePath;
                this.SetText(this.textValue.ConfigText);

                if (this.OnSelectedItemChanged != null)
                {
                    this.OnSelectedItemChanged(this, new StringEventArgs(this.textValue.ConfigText));
                }

                // Also set our image value if we can
                if (this.imageControlBoxForValue != null)
                {
                    this.Children.Remove(this.imageControlBoxForValue);
                }

                this.imageControlBoxForValue = new ImageControl(this.Name + "-ImageBoxForValue")
                                                {
                                                    Config =
                                                    {
                                                        PositionX = 3,
                                                        PositionY = 3,
                                                        Width = (int)(this.State.Width - 6),
                                                        Height = (int)(this.State.Height - 6)
                                                    },
                                                    ImagePath = this.ImagePath
                                                };
                this.Children.Add(this.imageControlBoxForValue);

                // Shift text over
                const float Tolerance = 0.001f;
                if (this.ImagePath != null && Math.Abs(this.textValue.State.DrawPosition.X - 5f) < Tolerance)
                {
                    this.textValue.State.DrawPosition = new DVector2(this.imageControlBoxForValue.State.DrawPosition.X + this.imageControlBoxForValue.State.Width + 5f, this.textValue.State.DrawPosition.Y);
                }
            }

            this.Manager.SetFocusedControl(this.dropDownButton);
        }

        /// <summary>
        /// Called when the drop down button is pressed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="buttonStateEventArgs">The <see cref="ButtonStateEventArgs"/> instance containing the event data.</param>
        private void OnToggleDropDown(object sender, ButtonStateEventArgs buttonStateEventArgs)
        {
            var state = buttonStateEventArgs.ButtonState;
            if (state == ButtonState.On)
            {
                // Display the drop down list
                this.DropDownList.Config.PositionY = this.Config.Height;

                // DropDownList.State.DrawPosition = new DVector2(State.DrawPosition.X, State.DrawPosition.Y + State.Height);
                this.Manager.AddForegroundControl(this.DropDownList);

                this.dropDownListShown = true;

                this.DropDownList.Config.Visible = true;

                if (this.OnShowHide != null)
                {
                    this.OnShowHide(this, new BooleanEventArgs(true));
                }
            }
            else
            {
                this.DropDownList.Config.Visible = false;

                this.Manager.ForegroundSceneNodes.Children.Remove(this.DropDownList);

                if (this.dropDownListShown)
                {
                    this.dropDownListShown = false;
                }

                if (this.OnShowHide != null)
                {
                    this.OnShowHide(this, new BooleanEventArgs(false));
                }
            }
        }
    }
}
