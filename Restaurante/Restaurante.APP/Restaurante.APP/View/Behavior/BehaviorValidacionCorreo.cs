using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Restaurante.APP.View.Behavior
{
    public class BehaviorValidacionCorreo : Behavior<Entry>
    {
        const string emailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

        public static readonly BindablePropertyKey EsCorreoValidoPropertyKey = BindableProperty.CreateReadOnly(nameof(EsCorreoValido), typeof(bool), typeof(BehaviorValidacionCorreo), false);

        public static readonly BindableProperty EsCorreoValidoProperty = EsCorreoValidoPropertyKey.BindableProperty;

        public bool EsCorreoValido
        {
            get => (bool)base.GetValue(EsCorreoValidoProperty);

            private set => SetValue(EsCorreoValidoPropertyKey, value);
        }
        protected override void OnAttachedTo(Entry Entry)
        {
            Entry.TextChanged += TextChanged;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            EsCorreoValido = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));

            ((Entry)sender).TextColor = (!EsCorreoValido) ? Color.Red : Color.Default;

        }

        protected override void OnDetachingFrom(Entry Entry)
        {
            Entry.TextChanged -= TextChanged;
        }
    }
}
