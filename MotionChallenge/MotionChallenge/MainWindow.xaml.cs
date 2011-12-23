﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Timers;

namespace MotionChallenge
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // singleton instance
        static MainWindow instance;

        // GLControl component used for displaying OpenGL graphics on Windows Forms (not on WPF windows)
        // For use in WPF : The GLControl component has to be included in a WindowsFormsHost first
        // Then the WindowsFormsHost component should be included in the WPF window
        // Because of airspace limitations, nothing can draw on top of the WindowsFormHost component
        GLControl glControl;

        // Game resources
        Game game;

        int playerCount;

        public MainWindow(int playerCount)
        {
            InitializeComponent();
            instance = this;
            this.playerCount = playerCount;
        }

        // Use to access UI elements from everywhere in the namespace
        public static MainWindow getInstance()
        {
            return instance;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the GLControl component
            glControl = new OpenTK.GLControl();
            glControl.SetBounds(0, 0, 640, 480);

            // Add the GLControl component into the WindowsFormHost
            windowsFormsHost.Child = glControl;

            // Create game resources
            game = new Game(glControl, playerCount);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // stop game threads
            game.stopGame();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    {
                        game.togglePause();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
