using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace IconFitter.Behaviors
{
    public class SelectAllOnFocus : NavigateNextOnEnterKey
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
                AssociatedObject.GotFocus += OnGotFocus;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
                AssociatedObject.GotFocus -= OnGotFocus;
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox && !string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                TextBox textBox = sender as TextBox;
                var tmr = new DispatcherTimer()
                {
                    Interval = new System.TimeSpan(1000 * 500) //500ms
                };
                tmr.Tick += (o,ev) =>
                {
                    textBox.SelectAll();
                    tmr.Stop();
                };
                tmr.Start();
            }
        }
    }
}
