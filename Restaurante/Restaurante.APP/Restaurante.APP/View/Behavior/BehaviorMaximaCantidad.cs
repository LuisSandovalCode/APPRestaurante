using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Restaurante.APP.View.Behavior
{
    public class BehaviorMaximaCantidad : Behavior<Entry>
    {

        private static readonly BindableProperty MaxLenghtProperty = BindableProperty.Create(nameof(MaxLength)
            , typeof(int), typeof(BehaviorMaximaCantidad), 0);
        public int MaxLength { get; set; }

        protected override void OnAttachedTo(Entry Entry)
        {
            base.OnAttachedTo(Entry);
        }

        protected override void OnDetachingFrom(Entry Entry)
        {
            base.OnDetachingFrom(Entry);
        }

        protected void Unfocused(object sender, FocusEventArgs e)
        {
            Entry EntryLenght = sender as Entry;
            if (string.IsNullOrEmpty(EntryLenght.Text))
                return;
            EntryLenght.Text = EntryLenght.Text.Substring(0, MaxLength);
        }
    }
}
