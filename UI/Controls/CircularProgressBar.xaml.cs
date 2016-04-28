using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для CircularProgressBar.xaml
    /// </summary>
    public partial class CircularProgressBar : UserControl
    {
        public double Radius { get; set; }

        public CircularProgressBar()
        {
            InitializeComponent();
            Radius = Height/3;
        }

        private void test()
        {
            var a = new Storyboard
            {
                RepeatBehavior = RepeatBehavior.Forever
            };
            var t = new DoubleAnimationUsingKeyFrames();
            var s = new SplineDoubleKeyFrame(Radius/6, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100)));
            t.KeyFrames.Add(s);

            var frameNumber = Enumerable.Range(0, 8).ToArray();
            var test = frameNumber
                .Select(i => frameNumber
                    .Select(
                        n => new SplineDoubleKeyFrame(Radius/n, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(i*100)))));
        }
    }
}
