using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IconFitter.Behaviors
{
    public class NavigateNextOnEnterKey : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
                AssociatedObject.KeyDown += OnKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
                AssociatedObject.KeyDown -= OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (sender != null && sender is FrameworkElement && (e.Key == Key.Return || e.Key == Key.Enter))
            {
                ((FrameworkElement)sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
