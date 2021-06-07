using GraphicalEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicalEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameEngine Engine;

        public MainWindow()
        {
            InitializeComponent();
            Thread th = new Thread(new ParameterizedThreadStart(StartEngine));
            Engine = new GameEngine(MainImage, SetInfoText);
            th.Start(Engine);
        }

        private void StartEngine(Object o)
        {
            while (true)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(100));


                this.Dispatcher.BeginInvoke(new Action(() =>
                {

                    ((GameEngine)o).Update(AnaliseMovement(), AnaliseRotation());

                }));
            }
        }
        private Vector2 AnaliseMovement()
        {
            Vector2 Movement = new Vector2(0, 0);
            if (Keyboard.IsKeyDown(Key.Up)) Movement.X = 1;
            else if (Keyboard.IsKeyDown(Key.Down)) Movement.X = -1;
            else Movement.X = 0;
            if (Keyboard.IsKeyDown(Key.Left)) Movement.Y = 1;
            else if (Keyboard.IsKeyDown(Key.Right)) Movement.Y = -1;
            else Movement.Y = 0;
            return Movement;
        }
        Vector2 PrevPos = Vector2.Zero;
        private Vector2 AnaliseRotation()
        {
            Vector2 Rotation = new Vector2(0, 0);
            Vector2 CurrentPos = new Vector2((int)Mouse.GetPosition((IInputElement)(MainImage)).X, (int)Mouse.GetPosition((IInputElement)(MainImage)).Y);
            if (PrevPos !=Vector2.Zero)
            {
                Rotation = CurrentPos - PrevPos;

            }
            PrevPos = CurrentPos;
            return Rotation;
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Engine.ReSize((int)Math.Ceiling(e.NewSize.Width), (int)Math.Ceiling(e.NewSize.Height));
        }

        public String SetInfoText(String s)
        {
            InfoBox.Text = s;
            return null;
        }

       
    }
}
