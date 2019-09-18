using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Restaurante.APP.View.Controll
{
    public class Gradiante : StackLayout
    {
        public static readonly BindableProperty StartColorProperty = BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(Gradiante),
                                    Color.Default, BindingMode.TwoWay, propertyChanged: StartColorPropertyChanged);

        public static readonly BindableProperty EndColorProperty = BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(Gradiante),
                                Color.Default, BindingMode.TwoWay, propertyChanged: EndColorPropertyChanged);
        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }


        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }

        public static void StartColorPropertyChanged(BindableObject value, object oldvalue, object newvalue)
        {
            if (!(value is Gradiante))
                return;

            if (newvalue != null)
            {
                Gradiante stack = (Gradiante)value;

                stack.StartColor = (Color)newvalue;
            }
        }

        public static void EndColorPropertyChanged(BindableObject value, object oldvalue, object newvlaue)
        {
            if (!(value is Gradiante))
                return;

            if (newvlaue != null)
            {
                Gradiante stack = (Gradiante)value;

                stack.EndColor = (Color)newvlaue;
            }
        }
    }
}
