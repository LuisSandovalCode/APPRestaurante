﻿using System.ComponentModel;

namespace Restaurante.APP.ViewModel.Utilidades
{
    public class PropiedadNotificacion : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string PorperityName)
        {
            if (PropertyChanged is null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PorperityName));
            }
        }
    }
}
