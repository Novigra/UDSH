using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for CSFUserControl.xaml
    /// </summary>
    public partial class CSFUserControl : UserControl
    {
        private Point InitialMousePos;
        private bool IsMousePressed = false;
        private double InitialRectangleWidth;

        public CSFUserControl()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitialMousePos = e.GetPosition(this);
            InitialRectangleWidth = TestCapture.Width;
            IsMousePressed = true;
            TestCapture.CaptureMouse();
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsMousePressed = false;
            TestCapture.ReleaseMouseCapture();
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if(IsMousePressed)
            {
                Point CurrentMousePos = e.GetPosition(this);

                double DeltaX = (CurrentMousePos.X - InitialMousePos.X);
                double NewWidth = InitialRectangleWidth + DeltaX;

                if(NewWidth > 65)
                {
                    TestCapture.Width = NewWidth;
                }
            }
        }

        private void TestCapture_MouseEnter(object sender, MouseEventArgs e)
        {
            double LastX = 400.0;
            if(!IsMousePressed)
            {
                TestExpander.IsExpanded = true;
                AnimationTest(TestContent, 0, LastX, TimeSpan.FromSeconds(0.3));
            }
        }

        private void AnimationTest(Rectangle rec, double from, double to, TimeSpan duration)
        {
            DoubleAnimation WidthAnim = new DoubleAnimation();
            WidthAnim.From = from;
            WidthAnim.To = to;
            WidthAnim.Duration = new Duration(duration);
            var Easing = new QuadraticEase { EasingMode = EasingMode.EaseOut };
            WidthAnim.EasingFunction = Easing;

            rec.BeginAnimation(Rectangle.WidthProperty, WidthAnim);
        }
    }
}
