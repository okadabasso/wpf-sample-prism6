using System;
using System.Windows;
using System.Windows.Interactivity;
namespace WpfSamples.Infrastructure.Presentation.Behaviors
{
    public class FollowedMaxSizeBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty TargetMaxHeightProperty = DependencyProperty.Register(
            "TargetMaxHeight", typeof(Double), typeof(FollowedMaxSizeBehavior),
            new FrameworkPropertyMetadata(0.0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public Double TargetMaxHeight
        {
            get { return (double)GetValue(TargetMaxHeightProperty); }
            set { SetValue(TargetMaxHeightProperty, value); }
        }

        public static readonly DependencyProperty TargetMaxWidthProperty = DependencyProperty.Register(
            "TargetMaxWidth", typeof(Double), typeof(FollowedMaxSizeBehavior),
            new FrameworkPropertyMetadata(0.0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public double TargetMaxWidth
        {
            get { return (double)GetValue(TargetMaxWidthProperty); }
            set { SetValue(TargetMaxWidthProperty, value); }
        }

        public static readonly DependencyProperty TargetMinHeightProperty = DependencyProperty.Register(
            "TargetMinHeight", typeof(Double), typeof(FollowedMaxSizeBehavior),
            new FrameworkPropertyMetadata(0.0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public Double TargetMinHeight
        {
            get { return (double)GetValue(TargetMinHeightProperty); }
            set { SetValue(TargetMinHeightProperty, value); }
        }

        public static readonly DependencyProperty TargetMinWidthProperty = DependencyProperty.Register(
            "TargetMinWidth", typeof(Double), typeof(FollowedMaxSizeBehavior),
            new FrameworkPropertyMetadata(0.0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public Double TargetMinWidth
        {
            get { return (double)GetValue(TargetMinWidthProperty); }
            set { SetValue(TargetMinWidthProperty, value); }
        }

        /// <summary>
        /// ビヘイビアーが AssociatedObject にアタッチされた後で呼び出されます。 
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.SizeChanged += OnWindowSizeChanged;
        }

        /// <summary>
        /// ビヘイビアーが AssociatedObject からデタッチされるとき、その前に呼び出されます。
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.SizeChanged -= OnWindowSizeChanged;
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            TargetMaxHeight += e.NewSize.Height - e.PreviousSize.Height;
            TargetMaxWidth += e.NewSize.Width - e.PreviousSize.Width;

            // 最大サイズが最小サイズより小さくなったら、最小サイズを変更する
            if (TargetMinHeight > TargetMaxHeight)
            {
                TargetMinHeight = TargetMaxHeight;
            }
            if (TargetMinWidth > TargetMaxWidth)
            {
                TargetMinWidth = TargetMaxWidth;
            }
        }
    }
}
