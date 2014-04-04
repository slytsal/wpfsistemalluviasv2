using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace Protell.UI.v2
{
    public class BindablePasswordBox : Decorator
    {
        /// <summary>
        /// The password dependency property.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty;

        private bool isPreventCallback;
        private RoutedEventHandler savedCallback;

        /// <summary>
        /// Static constructor to initialize the dependency properties.
        /// </summary>
        static BindablePasswordBox()
        {
            PasswordProperty = DependencyProperty.Register(
                "Password",
                typeof(string),
                typeof(BindablePasswordBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnPasswordPropertyChanged))
            );
        }

        /// <summary>
        /// Guarda el callback contraseña cambiada y establece el elemento secundario a la caja de contraseña.
        /// </summary>
        public BindablePasswordBox()
        {
            savedCallback = HandlePasswordChanged;

            PasswordBox passwordBox = new PasswordBox();
            passwordBox.PasswordChanged += savedCallback;
            Child = passwordBox;
            //ResourceDictionary rd = SetDictionaryStyle("InventoryApp.View;component/Style/WindowTheme.xaml");
            //if (rd.Count > 0)
            //{
            //    Style stPass = rd["PassLogin"] as Style;
            //    passwordBox.Style = stPass;

            //}
            passwordBox.MaxLength = 20;
            passwordBox.TabIndex = 2;
        }

        /// <summary>
        /// La propiedad de dependencia contraseña.
        /// </summary>
        public string Password
        {
            get { return GetValue(PasswordProperty) as string; }
            set { SetValue(PasswordProperty, value); }
        }

        /// <summary>
        /// Maneja los cambios en la propiedad de dependencia contraseña.
        /// </summary>
        /// <param name="d">el objeto de dependencia</param>
        /// <param name="eventArgs">los argumentos del evento</param>
        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs eventArgs)
        {
            BindablePasswordBox bindablePasswordBox = (BindablePasswordBox)d;
            PasswordBox passwordBox = (PasswordBox)bindablePasswordBox.Child;

            if (bindablePasswordBox.isPreventCallback)
            {
                return;
            }
            passwordBox.PasswordChanged -= bindablePasswordBox.savedCallback;
            passwordBox.Password = (eventArgs.NewValue != null) ? eventArgs.NewValue.ToString() : "";
            passwordBox.PasswordChanged += bindablePasswordBox.savedCallback;
        }

        /// <summary>
        /// Controla el evento contraseña cambiada.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="eventArgs">the event args</param>
        private void HandlePasswordChanged(object sender, RoutedEventArgs eventArgs)
        {
            PasswordBox passwordBox = (PasswordBox)sender;

            isPreventCallback = true;
            Password = passwordBox.Password;
            isPreventCallback = false;

        }

        /// <summary>
        /// Devuelve un Diccionario de recursos Style
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ResourceDictionary SetDictionaryStyle(string path)
        {
            ResourceDictionary rd = new ResourceDictionary();
            try
            {
                rd.Source = new Uri(path, UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {

            }
            return rd;
        }
    }
}
