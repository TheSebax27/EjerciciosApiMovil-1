using System;
using Microsoft.Maui.Controls;
using SensoresConsumoMovil.ViewModel;

namespace SensoresConsumoMovil.Views
{
    public partial class NivelSonido : ContentPage
    {
        public NivelSonido()
        {
            InitializeComponent();

            // Conectar el MediaElement con el ViewModel después de la inicialización
            if (BindingContext is AlertaSonidoViewModel viewModel)
            {
                viewModel.SetMediaElement(mediaPlayer);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Volver a conectar el MediaElement por si acaso
            if (BindingContext is AlertaSonidoViewModel viewModel)
            {
                viewModel.SetMediaElement(mediaPlayer);
            }
        }
    }
}