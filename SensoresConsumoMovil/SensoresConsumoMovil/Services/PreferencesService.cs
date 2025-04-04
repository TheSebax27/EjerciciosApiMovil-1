using System;
using Microsoft.Maui.Storage;

namespace SensoresConsumoMovil.Services
{
    public interface IPreferencesService
    {
        // Nivel de alerta
        string GetNivelAlerta();
        void SaveNivelAlerta(string nivel);

        // GPS
        bool GetActivarGPS();
        void SaveActivarGPS(bool activar);
        string GetMensajeGPS();
        void SaveMensajeGPS(string mensaje);

        // Sonido
        string GetMensajeEmergencia();
        void SaveMensajeEmergencia(string mensaje);
    }

    public class PreferencesService : IPreferencesService
    {
        private const string KEY_NIVEL_ALERTA = "nivel_alerta";
        private const string KEY_ACTIVAR_GPS = "activar_gps";
        private const string KEY_MENSAJE_GPS = "mensaje_gps";
        private const string KEY_MENSAJE_EMERGENCIA = "mensaje_emergencia";

        // Nivel de alerta
        public string GetNivelAlerta()
        {
            return Preferences.Get(KEY_NIVEL_ALERTA, string.Empty);
        }

        public void SaveNivelAlerta(string nivel)
        {
            Preferences.Set(KEY_NIVEL_ALERTA, nivel);
        }

        // GPS
        public bool GetActivarGPS()
        {
            return Preferences.Get(KEY_ACTIVAR_GPS, false);
        }

        public void SaveActivarGPS(bool activar)
        {
            Preferences.Set(KEY_ACTIVAR_GPS, activar);
        }

        public string GetMensajeGPS()
        {
            return Preferences.Get(KEY_MENSAJE_GPS, string.Empty);
        }

        public void SaveMensajeGPS(string mensaje)
        {
            Preferences.Set(KEY_MENSAJE_GPS, mensaje);
        }

        // Sonido
        public string GetMensajeEmergencia()
        {
            return Preferences.Get(KEY_MENSAJE_EMERGENCIA, string.Empty);
        }

        public void SaveMensajeEmergencia(string mensaje)
        {
            Preferences.Set(KEY_MENSAJE_EMERGENCIA, mensaje);
        }
    }
}