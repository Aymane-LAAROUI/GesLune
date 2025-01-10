using GesLune.Models;
using System.Windows;
using GesLune.Repositories;
using GesLune.Sdk.Commands;

namespace GesLune.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        // Properties
        public string Title { get; } = "تسجيل الدخول";
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;

        // Events
        public event EventHandler<Model_Utilisateur>? LoginSucceded;
        public event EventHandler<string>? LoginFailed;
        
        // Commands
        public RelayCommand LoginCommand { get; set; }

        // Ctor
        public LoginViewModel()
        {
            LoginCommand = new(Login, CanLogin);
        }

        public bool CanLogin() => !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password));

        public void Login()
        {
            try
            {
                Model_Utilisateur? utilisateur = UtilisateurRepository.Authenticate(Username, Password);
                if (utilisateur != null)
                    LoginSucceded?.Invoke(this, utilisateur);
                else
                {
                    //LoginFailed?.Invoke(this, "Nom d'utilisateur ou mot de passe incorrect");
                    Error = "اسم مستخدم أو كلمة مرور خطأ";
                    OnPropertyChanged(nameof(Error));
                }
            }
            catch (Exception ex)
            {
                //LoginFailed?.Invoke(this, $"Une erreur innatendu est survenue!\nMessage d'erreur:\n{ex.Message}");
                Error = $"حدث خطأ غير منتضر:" + '\n' + ex.Message;
                OnPropertyChanged(nameof(Error));
            }
        }
    }
}
