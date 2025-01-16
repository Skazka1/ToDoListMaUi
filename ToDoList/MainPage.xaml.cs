using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ToDoViewModel();
        }
        public class ToDoViewModel : INotifyPropertyChanged
        {
            private ObservableCollection<ToDoItemViewModel> _itemList;
            public ObservableCollection<ToDoItemViewModel> ItemList => _itemList;
            private string _newText = "";
            public ICommand AddItemCommand { get; }
            public string NewText
            {
                get => _newText;
                set
                {
                    if (_newText == value) return;
                    _newText = value;
                    OnPropertyChanged();
                }
            }
            public ToDoViewModel()
            {
                _itemList = new ObservableCollection<ToDoItemViewModel>();
                AddItemCommand = new Command(() =>
                {
                    if (string.IsNullOrWhiteSpace(NewText))
                        return;
                    ItemList.Add(new ToDoItemViewModel(NewText));

                }, () => !string.IsNullOrWhiteSpace(NewText));
            }
            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                ((Command)AddItemCommand).ChangeCanExecute();
            }
            public class ToDoItemViewModel(string newText, bool newState = false) : INotifyPropertyChanged
            {
                private string _text = newText;
                private bool _isComplete = newState;

                public string Text
                {
                    get => _text;
                    set
                    {
                        if (_text == value) return;
                        _text = value;
                        OnPropertyChanged();
                    }
                }

                public bool IsComplete
                {
                    get => _isComplete;
                    set
                    {
                        if (_isComplete == value) return;
                        _isComplete = value;
                        OnPropertyChanged();
                    }
                }

                public event PropertyChangedEventHandler? PropertyChanged;

                protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}