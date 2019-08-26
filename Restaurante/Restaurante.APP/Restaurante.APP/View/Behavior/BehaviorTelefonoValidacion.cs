using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Restaurante.APP.View.Behavior
{
    public class BehaviorTelefonoValidacion : Behavior<Entry>
    {
        private static string RegexDigitos = @"^[0-9]+$";

        protected override void OnAttachedTo(Entry Entry)
        {
            Entry.TextChanged += TextChanged;
            base.OnAttachedTo(Entry);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            bool Valido = (Regex.IsMatch(e.NewTextValue, RegexDigitos, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));

            (sender as Entry).TextColor = (!Valido) ? Color.Red : (sender as Entry).TextColor;
        }

        protected override void OnDetachingFrom(Entry Entry)
        {
            Entry.TextChanged -= TextChanged;
            base.OnDetachingFrom(Entry);
        }
    }
}
