﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MahApps.Metro.Controls
{
    /// <summary>
    /// This class eats little children.
    /// </summary>
    internal static class MetroWindowHelpers
    {
        public static void ChangeAllWindowCommandsBrush(this MetroWindow window, Brush brush)
        {
            window.ChangeWindowCommandButtonsBrush(brush);

            window.WindowButtonCommands.SetValue(Control.ForegroundProperty, brush);
        }

        public static void ResetAllWindowCommandsBrush(this MetroWindow window)
        {
            window.InvokeCommandButtons(x => x.ClearValue(Control.ForegroundProperty));

            window.WindowButtonCommands.ClearValue(Control.ForegroundProperty);
        }

        public static void ChangeWindowCommandButtonsBrush(this MetroWindow window, string resourceName)
        {
            window.InvokeCommandButtons(x => x.SetResourceReference(Control.ForegroundProperty, resourceName));
        }

        public static void ChangeWindowCommandButtonsBrush(this MetroWindow window, Brush brush)
        {
            window.InvokeCommandButtons(x => x.SetValue(Control.ForegroundProperty, brush));
        }

        private static void InvokeCommandButtons(this MetroWindow window, Action<Button> action)
        {
            foreach (Button b in ((WindowCommands)window.WindowCommandsPresenter.Content).FindChildren<Button>())
            {
                action(b);
            }
        }

        public static void HandleFlyout(this MetroWindow window, Flyout flyout, Brush darkThemeBrush = null)
        {
            Brush brush = null;

            if (flyout.Theme == FlyoutTheme.Accent)
            {
                brush = (Brush)flyout.FindResource("IdealForegroundColorBrush");
            }

            else if (flyout.ActualTheme == Theme.Light)
            {
                brush = (Brush)ThemeManager.LightResource["BlackBrush"];
            }

            else if(flyout.ActualTheme == Theme.Dark && darkThemeBrush != null)
            {
                brush = darkThemeBrush;
            }

            if (brush != null)
            {
                window.ChangeAllWindowCommandsBrush(brush);
            }
        }
    }
}
