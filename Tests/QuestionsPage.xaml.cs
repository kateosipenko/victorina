using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Tests.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Tests
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class QuestionsPage : Page
	{
		private Storyboard questionChanged;
		private MainViewModel dataContext;
		private Storyboard incorrectAnswerAnimation;

		public QuestionsPage()
		{
			this.InitializeComponent();
			questionChanged = ((Storyboard)Resources["QuestionChanged"]);
			incorrectAnswerAnimation = ((Storyboard)Resources["IncorrectAnswerAnimation"]);
			dataContext = (MainViewModel)this.DataContext;			
		}

		private void OnMainPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "CurrentQuestion")
			{
				questionChanged.Begin();
			}
			else if (e.PropertyName == "CurrentAnswer" && string.IsNullOrEmpty(dataContext.CurrentAnswer))
			{
				incorrectAnswerAnimation.Begin();
			}
		}

		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			Storyboard pageLoaded = ((Storyboard)Resources["PageLoaded"]);
			Storyboard timerAnimation = ((Storyboard)Resources["TimerAnimation"]);
			timerAnimation.Begin();
			await Task.Delay(400);
			pageLoaded.Begin();
			dataContext.PropertyChanged += OnMainPropertyChanged;
		}
	}
}
