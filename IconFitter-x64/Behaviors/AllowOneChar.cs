using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace IconFitter.Behaviors
{
    public class AllowOneChar : SelectAllOnFocus
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
            {
                AssociatedObject.PreviewTextInput += OnPreviewTextInput;
                AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
            {
                AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
                AssociatedObject.PreviewKeyDown -= OnPreviewKeyDown;
            }
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                var textBox = sender as TextBox;
                char c = e.Text.First();
                if (IsValid(c))
                    textBox.Text = c.ToString();
                e.Handled = true;
                textBox.SelectionStart = 1;
            }
        }

        private bool IsValid(char c)
        {
            return c.Equals('-') || c.Equals('_') || c.Equals('.');
        }
    }
}
