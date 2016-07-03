using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace 小说更新提醒器.controls
{
    /// <summary>
    /// Loading.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingWait : UserControl
    {
        #region Data
        private readonly System.Timers.Timer animationTimer;
        #endregion

        #region Constructor
        public LoadingWait()
        {
            InitializeComponent();
            animationTimer = new System.Timers.Timer(90);
            animationTimer.Elapsed += animationTimer_Elapsed;
        }

        void animationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SpinnerRotate.Dispatcher.Invoke(new Action(() =>
            {
                SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
            }));

        }
        #endregion

        #region Private Methods
        public void Start()
        {
            animationTimer.Enabled = true;
            this.Visibility = System.Windows.Visibility.Visible;
            animationTimer.Start();
        }

        public void Stop()
        {
            animationTimer.Enabled = false;
            this.Visibility = System.Windows.Visibility.Collapsed;
            animationTimer.Stop();
        }
        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            const double offset = Math.PI;
            const double step = Math.PI * 2 / 10.0;

            SetPosition(C0, offset, 0.0, step);
            SetPosition(C1, offset, 1.0, step);
            SetPosition(C2, offset, 2.0, step);
            SetPosition(C3, offset, 3.0, step);
            SetPosition(C4, offset, 4.0, step);
            SetPosition(C5, offset, 5.0, step);
            SetPosition(C6, offset, 6.0, step);
            SetPosition(C7, offset, 7.0, step);
            SetPosition(C8, offset, 8.0, step);
        }
        private void SetPosition(Ellipse ellipse, double offset,
            double posOffSet, double step)
        {
            ellipse.SetValue(Canvas.LeftProperty, 50.0
                + Math.Sin(offset + posOffSet * step) * 50.0);

            ellipse.SetValue(Canvas.TopProperty, 50
                + Math.Cos(offset + posOffSet * step) * 50.0);
        }
        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            Stop();
        }
        #endregion
    }
}
