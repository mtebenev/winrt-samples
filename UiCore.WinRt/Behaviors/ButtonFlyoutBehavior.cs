using System;
using Mt.Common.UiCore.Controls;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// The behavior binds flyout to a button click. The most evident scenario is displaying flyouts for AppBar
	/// </summary>
	[ContentProperty(Name = "Content")]
	public class ButtonFlyoutBehavior : Behavior<ButtonBase>
	{
		protected override void OnAttached()
		{
			base.OnAttached();

			this.AssociatedObject.Click += HandleButtonClick;
		}

		public static readonly DependencyProperty ContentProperty =
			DependencyProperty.Register("Content", typeof(object), typeof(ButtonFlyoutBehavior), 
			new PropertyMetadata(null));

		/// <summary>
		/// Set content to be displayed in the flyout
		/// </summary>
		public object Content
		{
			get
			{
				return this.GetValue(ContentProperty);
			}
			set
			{
				this.SetValue(ContentProperty, value);
			}
		}

		protected override void OnDetaching()
		{
			this.AssociatedObject.Click -= HandleButtonClick;
			base.OnDetaching();
		}

		private void HandleButtonClick(object sender, RoutedEventArgs e)
		{
			Flyout flyout = new Flyout
				{
					PlacementTarget = sender as UIElement,
					Placement = PlacementMode.Top,
					DataContext = this.DataContext,
					Content = this.Content
				};

			// Reset flyout's content on close to let the content be used the next time
			EventHandler<object> closedHandler = null;
			closedHandler = (s, args) =>
				{
					flyout.Closed -= closedHandler;
					flyout.Content = null;
				};

			flyout.Closed += closedHandler;
			flyout.IsOpen = true;

		}
	}
}
