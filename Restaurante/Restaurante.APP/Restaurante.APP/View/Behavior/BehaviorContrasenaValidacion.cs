using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Restaurante.APP.View.Behavior
{
    public class BehaviorContrasenaValidacion : Behavior<Entry>
    {
        private static string ContrasenaRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{10,}$";

        public static readonly BindablePropertyKey EsContrasenaValidoPropertyKey = BindableProperty.CreateReadOnly(nameof(EsContrasenaValido), typeof(bool), typeof(BehaviorContrasenaValidacion), false);

        public static readonly BindableProperty EsContrasenaValidoProperty = EsContrasenaValidoPropertyKey.BindableProperty;

        public bool EsContrasenaValido
        {
            get => (bool)base.GetValue(EsContrasenaValidoProperty);

            private set => SetValue(EsContrasenaValidoPropertyKey, value);
        }
        protected override void OnAttachedTo(Entry Entry)
        {
            Entry.TextChanged += TextChanged;
        }

        protected void TextChanged(object sender, TextChangedEventArgs e)
        {
            EsContrasenaValido = (Regex.IsMatch(e.NewTextValue, ContrasenaRegex));

            (sender as Entry).TextColor = (!EsContrasenaValido) ? Color.Red : Color.Default;
        }

        protected override void OnDetachingFrom(Entry Entry)
        {
            Entry.TextChanged -= TextChanged;
        }
    }
}
