using Microsoft.Xaml.Behaviors;
using System;
using System.Windows.Controls;

namespace IconFitter.Behaviors
{
    /// <summary>
    /// TextBox with behavior SelectAllOnFocus
    /// </summary>
    public class TextBoxSelectAll : TextBox
    {
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            var b = Interaction.GetBehaviors(this);
            b.Add(new SelectAllOnFocus());
        }
    }
}
