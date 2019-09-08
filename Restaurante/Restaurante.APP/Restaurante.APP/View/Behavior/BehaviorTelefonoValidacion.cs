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

        private static string RegexDigitos = @"^[\(]?[\+]?(\d{2}|\d{3})[\)]?[\s]?((\d{6}|\d{8})|(\d{3}[\*\.\-\s]){3}|(\d{2}[\*\.\-\s]){4}|(\d{4}[\*\.\-\s]){2})|\d{8}|\d{10}|\d{12}$";


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

            (sender as Entry).TextColor = (!EsTelefonoValido) ? Color.Red : Color.Default;
        }

        protected override void OnDetachingFrom(Entry Entry)
        {
            Entry.TextChanged -= TextChanged;
            base.OnDetachingFrom(Entry);
        }
    }
}
