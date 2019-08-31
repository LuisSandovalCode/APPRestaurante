using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Restaurante.APP.View.Behavior
{
    public class BehaviorTelefonoValidacion : Behavior<Entry>
    {

        public static readonly BindablePropertyKey EsTelefonoValidoPropertyKey = BindableProperty.CreateReadOnly(nameof(EsTelefonoValido), typeof(bool), typeof(BehaviorTelefonoValidacion), false);

        public static readonly BindableProperty EsTelefonoValidoProperty = EsTelefonoValidoPropertyKey.BindableProperty;

        private static string RegexDigitos = @"^[0-9]+$";


        public bool EsTelefonoValido
        {
            get => (bool)GetValue(EsTelefonoValidoProperty);
            set => SetValue(EsTelefonoValidoPropertyKey, value);
        }
        protected override void OnAttachedTo(Entry Entry)
        {
            Entry.TextChanged += TextChanged;
            base.OnAttachedTo(Entry);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            EsTelefonoValido = (Regex.IsMatch(e.NewTextValue, RegexDigitos, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));

            (sender as Entry).TextColor = (!EsTelefonoValido) ? Color.Red : (sender as Entry).TextColor;
        }

        protected override void OnDetachingFrom(Entry Entry)
        {
            Entry.TextChanged -= TextChanged;
            base.OnDetachingFrom(Entry);
        }
    }
}
