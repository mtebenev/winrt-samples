using System.Windows.Input;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// The attached behavior executes a bound on item click.
	/// It passes bound item as object parameter
	/// </summary>
	public class ListViewItemClickToCommandBehavior : Behavior<ListViewBase>
	{
		/// <summary>
		/// Bound command
		/// </summary>
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register("Command", typeof(ICommand), typeof(ListViewItemClickToCommandBehavior),
			new PropertyMetadata(DependencyProperty.UnsetValue));

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.ItemClick += HandleItemClick;
			AssociatedObject.IsItemClickEnabled = true; // Enable item click
		}

		protected override void OnDetaching()
		{
			AssociatedObject.ItemClick -= HandleItemClick;
			base.OnDetaching();
		}


		private void HandleItemClick(object sender, ItemClickEventArgs e)
		{
			ICommand command = Command;
			if(command != null && command.CanExecute(e.ClickedItem))
			{
				command.Execute(e.ClickedItem);
			}
		}
	}
}
