﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Interfaces;
using MudBlazor.Utilities;

namespace MudBlazor
{
    public partial class MudMenu : MudBaseButton, IActivatable
    {
        protected string Classname =>
        new CssBuilder("mud-menu")
        .AddClass(Class)
       .Build();

        protected string MenuClassname =>
        new CssBuilder("mud-menu-container")
        .AddClass("mud-menu-fullwidth", FullWidth)
       .Build();

        private bool _isOpen;

        [Parameter] public string Label { get; set; }
        [Parameter] public string Icon { get; set; }
        [Parameter] public string StartIcon { get; set; }
        [Parameter] public string EndIcon { get; set; }
        [Parameter] public Color Color { get; set; } = Color.Default;
        [Parameter] public Size Size { get; set; } = Size.Medium;
        [Parameter] public Variant Variant { get; set; } = Variant.Text;


        /// <summary>
        /// If true, compact vertical padding will be applied to all menu items.
        /// </summary>
        [Parameter] public bool Dense { get; set; }
        [Parameter] public bool DisableElevation { get; set; }
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool DisableRipple { get; set; }

        /// <summary>
        /// If true, the list menu will be same width as the parent.
        /// </summary>
        [Parameter] public bool FullWidth { get; set; }

        /// <summary>
        /// Sets the maxheight the menu can have when open.
        /// </summary>
        [Parameter] public int? MaxHeight { get; set; }

        /// <summary>
        /// If true, instead of positioning the menu at the left upper corner, position at the exact cursor location.
        /// This makes sense for larger activators
        /// </summary>
        [Parameter] public bool PositionAtCurser { get; set; }

        /// <summary>
        /// Place a MudButton, a MudIconButton or any other component capable of acting as an activator. This will
        /// override the standard button and all parameters which concern it.
        /// </summary>
        [Parameter] public RenderFragment ActivatorContent { get; set; }

        /// <summary>
        /// Sets the direction the select menu should be.
        /// </summary>
        [Parameter] public Direction Direction { get; set; } = Direction.Top;

        /// <summary>
        /// If true, the select menu will open either before or after the input.
        /// </summary>
        [Parameter] public bool OffsetY { get; set; }

        [Parameter] public bool OffsetX { get; set; }

        /// <summary>
        /// Add menu items here
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        public string PopoverStyle { get; set; }

        public void CloseMenu()
        {
            _isOpen = false;
            PopoverStyle = null;
            StateHasChanged();
        }

        public void OpenMenu(MouseEventArgs args)
        {
            if (Disabled)
                return;
            PopoverStyle = PositionAtCurser ? $"position:fixed; left:{args.ClientX}px; top:{args.ClientY}px;" : null;
            _isOpen = true;
            StateHasChanged();
        }

        public void ToggleMenu(MouseEventArgs args)
        {
            if (Disabled)
                return;
            if (_isOpen)
                CloseMenu();
            else
                OpenMenu(args);
        }

        /// <summary>
        /// Implementation of IActivatable.Activate, toggles the menu.
        /// </summary>
        /// <param name="activator"></param>
        /// <param name="args"></param>
        public void Activate(object activator, MouseEventArgs args)
        {
            ToggleMenu(args);
        }
    }
}
