using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Models;
using Tests.Helpers;
using Windows.UI.Xaml;

namespace Tests.ViewModel
{
	/// <summary>
	/// This class contains properties that the main View can data bind to.
	/// <para>
	/// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
	/// </para>
	/// <para>
	/// You can also use Blend to data bind with the tool's support.
	/// </para>
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class MainViewModel : ViewModelBase
	{
		private const string ResultTextFormat = "{0}, ������ �� ������!\n\r�� ������ �������� �� �� ������� �� {1} ������.";

		#region FIELDS

		private List<Question> questions;
		private string userName;
		private NavigationProvider navigationProvider;
		private Question currentQuestion;
		private string currentAnswer = string.Empty;
		private int currentTime = 0;
		private bool isTimerRunning = false;
		private string userResultText = string.Empty;
		private Visibility imageQuestionVisibility = Visibility.Collapsed;

		#endregion FIELDS

		#region CONSTRUCTOR

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel(NavigationProvider provider)
		{
			this.navigationProvider = provider;
			this.Initialize();
		}

		#endregion CONSTRUCTOR

		#region PROPERTIES

		public List<Question> Questions
		{
			get
			{
				return this.questions;
			}
		}

		public Visibility ImageQuestionVisibility
		{
			get { return imageQuestionVisibility; }
			set
			{
				imageQuestionVisibility = value;
				RaisePropertyChanged();
			}
		}

		public string UserResultText
		{
			get { return userResultText; }
			set
			{
				userResultText = value;
				RaisePropertyChanged();
			}
		}

		public bool IsTimerRunning
		{
			get { return isTimerRunning; }
			set
			{
				isTimerRunning = value;
				RaisePropertyChanged();
			}
		}

		public Question CurrentQuestion
		{
			get { return this.currentQuestion; }
			set
			{
				this.currentQuestion = value;
				this.RaisePropertyChanged();
				if (this.currentQuestion.QuestionImage != null)
				{
					ImageQuestionVisibility = Visibility.Visible;
				}
				else
				{
					ImageQuestionVisibility = Visibility.Collapsed;
				}
			}
		}

		public int CurrentTime
		{
			get { return this.currentTime; }
			set
			{
				this.currentTime = value;
				RaisePropertyChanged();
			}
		}

		public string CurrentAnswer
		{
			get { return currentAnswer; }
			set
			{
				currentAnswer = value;
				RaisePropertyChanged();
			}
		}

		public string UserName
		{
			get
			{
				return this.userName;
			}

			set
			{
				this.userName = value;
				this.GoToQuestionsCommand.RaiseCanExecuteChanged();
				this.RaisePropertyChanged();
			}
		}

		#endregion PROPERTIES

		#region COMMANDS

		public RelayCommand GoToUserCommand { get; private set; }
		public RelayCommand GoToQuestionsCommand { get; private set; }
		public RelayCommand<string> SetAnswerCommand { get; private set; }
		public RelayCommand GoToNextQuestionCommand { get; private set; }
		public RelayCommand GoToStartCommand { get; private set; }

		#endregion COMMANDS

		#region METHODS

		private async void Initialize()
		{
			this.GoToUserCommand = new RelayCommand(this.GoToUserCommandExecute);
			this.GoToQuestionsCommand = new RelayCommand(this.GoToQuestionsCommndExecute, this.GoToQuestionsCommandCanExecute);
			this.SetAnswerCommand = new RelayCommand<string>(this.SetAnswerCommndExecute);
			this.GoToNextQuestionCommand = new RelayCommand(this.GoToNextQuestionCommandExecute);
			this.GoToStartCommand = new RelayCommand(this.GoToStartExecute);

			await Task.Run(() => this.GenerateQuestions());

		}

		private void SetAnswerCommndExecute(string answer)
		{
			this.CurrentAnswer = answer;
		}

		private void Clear()
		{
			CurrentQuestion = questions[0];
			CurrentTime = 0;
			IsTimerRunning = true;
			UserResultText = string.Empty;
		}

		private void GoToNextQuestionCommandExecute()
		{
			if (this.currentAnswer == currentQuestion.CorrectAnswer)
			{
				int newQuestionIndex = questions.IndexOf(this.currentQuestion) + 1;
				if (newQuestionIndex < questions.Count)
				{
					this.CurrentQuestion = this.questions[newQuestionIndex];
				}
				else
				{
					this.navigationProvider.Navigate(typeof(ResultPage));
					IsTimerRunning = false;
					UserResultText = string.Format(ResultTextFormat, UserName, CurrentTime);
					GoogleDriveSDK.GoogleDriveManager.CreateFile(CurrentTime + "_" + UserName + ".txt");
				}

				CurrentAnswer = string.Empty;
			}
			else
			{
				CurrentAnswer = string.Empty;
			}
		}
		private void GoToUserCommandExecute()
		{
			this.navigationProvider.Navigate(typeof(UserDataPage));
		}

		private void GoToQuestionsCommndExecute()
		{
			this.Clear();
			this.navigationProvider.Navigate(typeof(QuestionsPage));
		}

		private bool GoToQuestionsCommandCanExecute()
		{
			return !string.IsNullOrEmpty(UserName);
		}

		private void GoToStartExecute()
		{
			navigationProvider.Navigate(typeof(StartPage));
			UserName = string.Empty;
		}

		private void GenerateQuestions()
		{
			this.questions = new List<Question>();

			questions.Add((new Question
			{
				CategoryImage = "ms-appx:///Images/math.png",
				QuestionText = "��������� �����������: 1, 1, 2, 3, 5, 8, ...",
				CorrectAnswer = "13",
				Answer0 = "12",
				Answer1 = "13",
				Answer2 = "15"
			}));

			questions.Add(new Question
			{
				CategoryImage = "ms-appx:///Images/chemistry.png",
				QuestionText = "������� ���������� ������� ��������?",
				CorrectAnswer = "����",
				Answer0 = "�",
				Answer1 = "�/��",
				Answer2 = "����"
			});

			questions.Add(new Question
			{
				CategoryImage = "ms-appx:///Images/biology.png",
				QuestionText = "������� ����� ������� ������� ����� ������� ����?",
				CorrectAnswer = "��������",
				Answer0 = "Գ�������",
				Answer1 = "��������",
				Answer2 = "����������"
			});

			questions.Add(new Question
			{
				CategoryImage = "ms-appx:///Images/physics.png",
				QuestionText = "���� � ���������� ������� ���������� ����������� ������� ������?",
				CorrectAnswer = "9,8 �/�2",
				Answer0 = "9,8 �/�2",
				Answer1 = "101325 ��",
				Answer2 = "6,6 * 10-34 ��*�"
			});

			questions.Add(new Question
			{
				CategoryImage = "ms-appx:///Images/literature.png",
				QuestionText = "��� ���������� �� �������?",
				CorrectAnswer = "����� ������",
				Answer0 = "����� ������",
				Answer1 = "������� ������������",
				Answer2 = "����� ������",
				QuestionImage = "ms-appx:///Images/panas.png",
			});

			questions.Add(new Question
			{
				CategoryImage = "ms-appx:///Images/geography.png",
				QuestionText = "������ ����� ���� ������� �� �� ����������� �� ...",
				CorrectAnswer = "24 ������",
				Answer0 = "12 �����",
				Answer1 = "24 ������",
				Answer2 = "365 ���"
			});

			questions.Add(new Question
			{
				CategoryImage = "ms-appx:///Images/hystory.png",
				QuestionText = "�� � ������������� ������ �� � ������� �������� �������?",
				CorrectAnswer = "�����",
				Answer0 = "������",
				Answer1 = "�����",
				Answer2 = "�����"
			});
		}

		#endregion METHODS

	}
}
