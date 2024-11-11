using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using UDSH.ViewModel;

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
            
            CSFUserControlViewModel ViewModel = new CSFUserControlViewModel();
            DataContext = ViewModel;
            TestGrid.DataContext = ViewModel.CSFData;

            /*TesstList.Items.Add(new ListItem());
            TesstList.Items.Add(new ListItem());*/
            //TestList.Items.Add(new ListItem());

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

        private void TestCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int changeTemplate = TestCombo.SelectedIndex;
        }

        private void TesstList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TestActive.Text = TesstList.SelectedIndex.ToString();
        }

        /*private void TestTextChange(object sender, RoutedEventArgs e)
        {
            var TB = sender as TextBox;
            if(TB.Text == "Scene Header")
            {
                TB.Text = "";
            }
        }*/
    }
}
