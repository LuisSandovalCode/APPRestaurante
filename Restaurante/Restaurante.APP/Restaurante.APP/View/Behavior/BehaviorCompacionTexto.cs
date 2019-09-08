using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Restaurante.APP.View.Behavior
{
    public class BehaviorCompacionTexto : Behavior<Entry>
    {
        public static BindableProperty TextProperty = BindableProperty.Create<BehaviorCompacionTexto, string>(tc => tc.Text, string.Empty, BindingMode.TwoWay);

        public static readonly BindablePropertyKey EsContrasenaIgualPropertyKey = BindableProperty.CreateReadOnly(nameof(EsContrasenaIgual), typeof(bool), typeof(BehaviorCompacionTexto), false);

        public static readonly BindableProperty EsContrasenaIgualProperty = EsContrasenaIgualPropertyKey.BindableProperty;
        public bool EsContrasenaIgual
        {
            get => (bool)GetValue(EsContrasenaIgualProperty);
            set => SetValue(EsContrasenaIgualPropertyKey, value);
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static BindableProperty EntProperty = BindableProperty.Create<BehaviorCompacionTexto, Entry>(tc => tc.Ent, null, BindingMode.TwoWay);

        public Entry Ent
        {
            get { return (Entry)GetValue(EntProperty); }
            set { SetValue(EntProperty, value); }
        }

        // Compara que el texto de este control sea el mismo que el de otro control
        void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null)
            {
                EsContrasenaIgual = (e.NewTextValue.Equals(Text));
                ((Ent == null) ? (Entry)sender : Ent).TextColor = EsContrasenaIgual ? Color.Default : Color.Red;
            }
        }


        protected override void OnAttachedTo(Entry Entry)
        {
            Entry.TextChanged += TextChanged;
            base.OnAttachedTo(Entry);
        }

        protected override void OnDetachingFrom(Entry Entry)
        {
            Entry.TextChanged -= TextChanged;
            base.OnDetachingFrom(Entry);
        }
    }
}
