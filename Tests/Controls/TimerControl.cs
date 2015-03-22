using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tests.Controls
{
    public class TimerControl : Control
    {
        private int currentTimeInSeconds = 0;

		#region CurrentTimeProperty

		public static readonly DependencyProperty CurrentTimeProperty = DependencyProperty.Register(
			"CurrentTime",
			typeof(int),
			typeof(TimerControl),
			new PropertyMetadata(null));

		public int CurrentTime
		{
			get { return (int)GetValue(CurrentTimeProperty); }
			set { SetValue(CurrentTimeProperty, value); }
		}

		#endregion CurrentTimeProperty

        #region IsRunningProperty

        public static readonly DependencyProperty IsRunningProperty = DependencyProperty.Register(
            "IsRunningProperty",
            typeof(bool),
            typeof(TimerControl),
            new PropertyMetadata(null, OnIsRunningPropertyChanged));

        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        private static void OnIsRunningPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((TimerControl)sender).OnIsRunningChanged();
        }

        #endregion IsRunningProperty

        private TextBlock text;
        private Timer timer;

        public TimerControl()
        {
            this.DefaultStyleKey = typeof(TimerControl);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            text = GetTemplateChild("text") as TextBlock;

        }

        private void OnIsRunningChanged()
        {
            if (IsRunning)
            {
                startTimer();
            }
            else
            {
                timer.Dispose();
                currentTimeInSeconds = 0;
				CurrentTime = currentTimeInSeconds;
                timer = null;
            }
        }

        private void startTimer()
        {
            timer = new Timer(new TimerCallback((state) =>
            {
                OnTick();
            }), null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1));
        }

        private async void OnTick()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
					if (text != null)
					{
						currentTimeInSeconds++;
						CurrentTime = currentTimeInSeconds;
						text.Text = TimeSpan.FromSeconds(currentTimeInSeconds).ToString("mm\\:ss");
					}
                });            
        }
    }
}
