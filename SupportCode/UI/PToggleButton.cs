/*
 * Based on work by Peter Han, under the following license:
 * https://github.com/peterhaneve/ONIMods/blob/236a0bcb9e0d84e03ac617b38ae29bd8a22cb3de/PLibUI/PToggle.cs
 *
 * Copyright 2024 Peter Han
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 * and associated documentation files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 * BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using UnityEngine.UI;

namespace PeterHan.PLib.UI {
    /// <summary>
    /// A custom UI toggled button factory class, controlling the button rather than
    /// the image, unlike PToggle.
    /// </summary>
    public sealed class PToggleButton : PTextComponent {
        /// <summary>
        /// The default margins around a toggle button.
        /// </summary>
        internal static readonly RectOffset TOGGLE_MARGIN = new(7, 7, 5, 5);

        /// <summary>
        /// Gets a realized toggle button's state.
        /// </summary>
        /// <param name="realized">The realized toggle button.</param>
        /// <returns>The toggle button state.</returns>
        public static bool GetToggleState(GameObject realized) {
            return realized?.GetComponent<KToggle>()?.isOn ?? false;
        }

        /// <summary>
        /// Sets a realized toggle button's state.
        /// </summary>
        /// <param name="realized">The realized toggle button.</param>
        /// <param name="on">Whether the button should be on or off.</param>
        public static void SetToggleState(GameObject realized, bool on) {
            realized?.GetComponent<KToggle>()?.isOn = on;
        }

        /// <summary>
        /// The toggle's color.
        /// </summary>
        public ColorStyleSetting Color { get; set; }

        /// <summary>
        /// The initial state of the toggle button.
        /// </summary>
        public bool InitialState { get; set; }

        /// <summary>
        /// The action to trigger when the state changes. It is passed the realized source
        /// object.
        /// </summary>
        public PUIDelegates.OnToggleButton? OnStateChanged { get; set; }

        /// <summary>
        /// The size to scale the toggle images. If 0x0, it will not be scaled.
        /// </summary>
        public Vector2 Size { get; set; }

        public PToggleButton() : this(null) { }

        public PToggleButton(string? name): base(name ?? "Toggle") {
            Color = PUITuning.Colors.ComponentDarkStyle;
            InitialState = false;
            Margin = TOGGLE_MARGIN;
            ToolTip = "";

            // Like PButton.
            Sprite = null;
            Text = null;
        }

        /// <summary>
        /// Adds a handler when this toggle button is realized.
        /// </summary>
        /// <param name="onRealize">The handler to invoke on realization.</param>
        /// <returns>This toggle button for call chaining.</returns>
        public PToggleButton AddOnRealize(PUIDelegates.OnRealize onRealize) {
            OnRealize += onRealize;
            return this;
        }

        private static ToggleSoundPlayer _toggleSounds = new();

        public override GameObject Build() {
            var toggle = PUIElements.CreateUI(null, Name);
            // Set on click event
            var kToggle = toggle.AddComponent<KToggle>();
            var evt = OnStateChanged;
            if (evt != null)
                kToggle.onValueChanged += (on) => evt?.Invoke(toggle, on);

            kToggle.artExtension = new KToggleArtExtensions();
            // kToggle.soundPlayer  = PUITuning.ToggleSounds;
            kToggle.soundPlayer = _toggleSounds;  // since PUITuning.ToggleSounds is not public, this'll substitute.

            // Background image
            var bgImage = toggle.AddComponent<KImage>();
            bgImage.color  = InitialState ? Color.activeColor : Color.inactiveColor;
            bgImage.sprite = PUITuning.Images.ButtonBorder;  // friendly button look.
            bgImage.type   = Image.Type.Sliced;
            bgImage.ApplyColorStyleSetting();
            kToggle.bgImage = bgImage;

            toggle.SetActive(false);

            // Toggled images
            var toggleImage = toggle.AddComponent<ImageToggleState>();
            toggleImage.TargetImage = bgImage;

            // do *NOT* use the sprites, instead, this just controls the color.
            toggleImage.useSprites = false;

            toggleImage.startingState = InitialState ? ImageToggleState.State.Active : ImageToggleState.State.Inactive;
            toggleImage.useStartingState = true;
            toggleImage.ActiveColour = Color.activeColor;
            toggleImage.DisabledActiveColour = Color.disabledActiveColor;
            toggleImage.InactiveColour = Color.inactiveColor;
            toggleImage.DisabledColour = Color.disabledColor;
            toggleImage.HoverColour = Color.hoverColor;
            toggleImage.DisabledHoverColor = Color.disabledhoverColor;
            toggleImage.colorStyleSetting = Color;

            kToggle.isOn = InitialState;

            toggle.SetActive(true);

            // the sprite and label text contained within the button control.
            GameObject? sprite = null;
            if (Sprite != null) {
                var fgImage = ImageChildHelper(toggle, this);
                kToggle.bgImage = fgImage;
                sprite = fgImage.gameObject;
            }

            GameObject? text = null;
            if (!string.IsNullOrEmpty(Text))
                text = TextChildHelper(toggle, TextStyle ?? PUITuning.Fonts.UILightStyle, Text).gameObject;

            // Arrange the icon and text
            var layout = toggle.AddComponent<RelativeLayoutGroup>();
            layout.Margin = Margin;
            ArrangeComponent(layout, WrapTextAndSprite(text, sprite), TextAlignment);
            if (!DynamicSize) layout.LockLayout();
            layout.flexibleWidth = FlexSize.x;
            layout.flexibleHeight = FlexSize.y;
            DestroyLayoutIfPossible(toggle);

            // Set size
            if (Size.x > 0.0f && Size.y > 0.0f)
                toggle.SetUISize(Size, true);
            else
                PUIElements.AddSizeFitter(toggle, DynamicSize);

            // Add tooltip
            PUIElements.SetToolTip(toggle, ToolTip).SetFlexUISize(FlexSize).SetActive(true);

            kToggle.ForceUpdateVisualState();

            InvokeRealize(toggle);
            return toggle;
        }

        public override string ToString() {
            return string.Format("PToggleButton[Name={0}]", Name);
        }
    }
}
