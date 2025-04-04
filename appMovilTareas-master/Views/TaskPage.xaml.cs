using appMovilTareas.ViewModels;
using Microsoft.Maui.Controls;

namespace appMovilTareas.Views
{
    public partial class TaskPage : ContentPage
    {
        public TaskPage(int usuarioId)
        {
            InitializeComponent();
            BindingContext = new TareasViewModel(usuarioId);
        }
    }
}
