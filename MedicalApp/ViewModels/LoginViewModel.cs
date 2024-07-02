using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication;
using MedicalApp.Messages;
using MedicalDatabase;
using ReactiveUI;
using Tools.Messaging;

namespace MedicalApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _errorMessage = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private DummyUser? _selectedUser;
        private IReadOnlyList<DummyUser> _availableUsers = Array.Empty<DummyUser>();

        private readonly MedicalProject _project;
        private readonly ILoginService _loginService;

        public LoginViewModel(MedicalProject project, ILoginService loginService)
        {
            _project = project;
            _loginService = loginService;
            _ = GetUsers();
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public DummyUser? SelectedUser
        {
            get => _selectedUser;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedUser, value);

                if (value is null)
                {
                    return;
                }
                Username = value.Username;
                Password = value.Password;
            }
        }

        public IReadOnlyList<DummyUser> AvailableUsers  
        {
            get => _availableUsers;
            set => this.RaiseAndSetIfChanged(ref _availableUsers, value);
        }

        public async Task LoginCommand()
        {
            var authResult = await _loginService.Authenticate(Username, Password);

            if (authResult is null)
            {
                ErrorMessage = "Invalid username or password";
                return;
            }

            await _project.MessageBus.SendAsync<LoginSuccess>(new LoginSuccess());
            ErrorMessage = "";
        }

        private async Task GetUsers()
        {
            AvailableUsers = await _loginService.GetUsers();
        }
    }
}
