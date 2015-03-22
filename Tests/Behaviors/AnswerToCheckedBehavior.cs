using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tests.Behaviors
{
	public class AnswerToCheckedBehavior : Behavior<RadioButton>
	{
		#region CurrentAnswer

		public static readonly DependencyProperty CurrentAnswerProperty = DependencyProperty.Register(
			"CurrentAnswer",
			typeof(string),
			typeof(AnswerToCheckedBehavior),
			new PropertyMetadata(null, OnCurrentAnswerChanged));

		public string CurrentAnswer
		{
			get { return (string)GetValue(CurrentAnswerProperty); }
			set { SetValue(CurrentAnswerProperty, value); }
		}

		private static void OnCurrentAnswerChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			((AnswerToCheckedBehavior)sender).UpdateChecked();
		}

		#endregion CurrentAnswer

		#region TargetAnswer

		public static readonly DependencyProperty TargetAnswerProperty = DependencyProperty.Register(
			"TargetAnswer",
			typeof(string),
			typeof(AnswerToCheckedBehavior),
			new PropertyMetadata(null, OnTargetAnswerChanged));

		public string TargetAnswer
		{
			get { return (string)GetValue(TargetAnswerProperty); }
			set { SetValue(TargetAnswerProperty, value); }
		}

		private static void OnTargetAnswerChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			((AnswerToCheckedBehavior)sender).UpdateChecked();
		}

		#endregion TargetAnswer

		private void UpdateChecked()
		{
			if (this.AssociatedObject != null)
			{
				this.AssociatedObject.IsChecked = CurrentAnswer == TargetAnswer;
			}
		}
	}
}
