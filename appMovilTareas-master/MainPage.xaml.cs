using Microsoft.Maui.Controls;
using appMovilTareas.ViewModels;

namespace appMovilTareas
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }
    }
}
