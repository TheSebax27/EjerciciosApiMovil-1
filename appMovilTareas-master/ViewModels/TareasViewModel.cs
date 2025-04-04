using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using appMovilTareas.Models;
using appMovilTareas.Service;
using Microsoft.Maui.Controls;

namespace appMovilTareas.ViewModels
{
    public class TareasViewModel : BindableObject
    {
        private readonly ApiService _apiService;
        private ObservableCollection<Tarea> _tareas;
        private int _usuarioId;

        public ObservableCollection<Tarea> Tareas
        {
            get => _tareas;
            set
            {
                _tareas = value;
                OnPropertyChanged();
            }
        }

        public ICommand CargarTareasCommand { get; }

        public TareasViewModel(int usuarioId)
        {
            _apiService = new ApiService();
            _usuarioId = usuarioId;
            Tareas = new ObservableCollection<Tarea>();

            CargarTareasCommand = new Command(async () => await CargarTareas());
            _ = CargarTareas(); // Cargar tareas al iniciar
        }

        private async Task CargarTareas()
        {
            try
            {
                Console.WriteLine($"🟢 Cargando tareas para usuario {_usuarioId}");
                var tareas = await _apiService.ObtenerTareasDelDia(_usuarioId);
                Tareas.Clear();
                foreach (var tarea in tareas)
                {
                    Tareas.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 Error al cargar tareas: {ex.Message}");
            }
        }
    }
}
