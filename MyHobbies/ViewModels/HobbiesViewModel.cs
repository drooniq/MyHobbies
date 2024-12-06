using MyHobbies.Commands;
using MyHobbies.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyHobbies.ViewModels
{
    public class HobbiesViewModel : ViewModelBase
    {
        public ObservableCollection<Hobby> Hobbies { get; set; }

        public DelegateCommand AddHobbyCommand { get; set; }
        public DelegateCommand DeleteHobbyCommand { get; set; }

        private string? _newHobbyName;
        public string? NewHobbyName
        {
            get => _newHobbyName;
            set
            {
                _newHobbyName = value;
                AddHobbyCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        private Hobby? _selectedHobby;
        public Hobby? SelectedHobby
        {
            get => _selectedHobby;
            set
            {
                _selectedHobby = value;
                DeleteHobbyCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public HobbiesViewModel()
        {
            Hobbies = new ObservableCollection<Hobby>
            {
                new("Discgolf"),
                new("Computers"),
                new("Travelling")
            };

            AddHobbyCommand = new DelegateCommand(AddNewHobby, CanAdd);
            DeleteHobbyCommand = new DelegateCommand(DeleteHobby, CanDelete);
        }

        private bool CanAdd(object? arg) => (NewHobbyName?.Length > 0);
        private bool CanDelete(object? arg) => SelectedHobby is not null;

        private void AddNewHobby(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(_newHobbyName))
                return;

            Hobbies.Add(new Hobby(_newHobbyName));

            NewHobbyName = string.Empty;
        }

        private void DeleteHobby(object? parameter)
        {
            // already checked that _selectedHobby is not null
            Hobbies.Remove(_selectedHobby! );
            _selectedHobby = null!;
        }
    }
}