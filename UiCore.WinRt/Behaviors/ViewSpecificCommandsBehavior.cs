using System;
using System.Collections.ObjectModel;
using Mt.Common.AppCore.Utils;
using Mt.Common.UiCore.Platform;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// Use behavior to define view-specific commands and put them in shared AppBar
	/// </summary>
	[ContentProperty(Name = "CommandGroups")]
	public class ViewSpecificCommandsBehavior : Behavior<Page>
	{
		public ViewSpecificCommandsBehavior()
		{
			CommandGroups = new Collection<AppBarCommandGroup>();
		}

		/// <summary>
		/// Use the property to define command groups
		/// </summary>
		public Collection<AppBarCommandGroup> CommandGroups
		{
			get { return (Collection<AppBarCommandGroup>)GetValue(CommandGroupsProperty); }
			set { SetValue(CommandGroupsProperty, value); }
		}

		public static readonly DependencyProperty CommandGroupsProperty =
			DependencyProperty.Register("CommandGroups", typeof(Collection<AppBarCommandGroup>), typeof(ViewSpecificCommandsBehavior),
			new PropertyMetadata(null));

		protected override void OnAttached()
		{
			base.OnAttached();

			// Add commands from each of defined command groups to panels
			foreach(AppBarCommandGroup commandGroup in CommandGroups)
			{
				Panel groupPanel = FindGroupPanel(commandGroup.Name);
				groupPanel.Children.Clear();

				// Design note: this is essential to bind commands view-specific commands to the view model
				groupPanel.DataContext = this.DataContext;

				commandGroup.CommandElements.ForEach(e => groupPanel.Children.Add(e));
			}
		}

		protected override void OnDetaching()
		{
			// Clear panels with view-specific commands
			foreach(AppBarCommandGroup commandGroup in CommandGroups)
			{
				Panel groupPanel = FindGroupPanel(commandGroup.Name);
				groupPanel.Children.Clear();
			}

			base.OnDetaching();
		}

		/// <summary>
		/// Looks for a shared application's AppBar. Assuming the following structure of the visual tree:
		/// this_Page ..->..Frame..->..Page. (according to MS guidelines)
		/// topBar - true to obtain top bar and false to obtain bottom bar
		/// </summary>
		private Panel FindGroupPanel(string groupName)
		{
			Frame frame = this.AssociatedObject.Frame;

			if(frame == null)
				throw new InvalidOperationException("Cannot find shared AppBar. Please make sure that the view is in Frame.");

			Page rootPage = VisualTreeEnumerator.FindParentElement<Page>(frame);

			if(rootPage == null)
				throw new InvalidOperationException("Cannot find shared AppBar. Please make sure that the view is in frame which in turn in a root page.");

			Panel result = null;

			// Try find a panel with specified name
			if(rootPage.BottomAppBar != null)
				result = rootPage.FindName(groupName) as Panel;

			if(result == null)
				throw new InvalidOperationException("Cannot find group in shared AppBar.");

			return result;
		}
	}

	/// <summary>
	/// Contains commands collection for a single group
	/// </summary>
	[ContentProperty(Name = "CommandElements")]
	public class AppBarCommandGroup : DependencyObject
	{
		public AppBarCommandGroup()
		{
			CommandElements = new Collection<UIElement>();
		}

		/// <summary>
		/// Shared AppBar must contain a Panel-derived element with x:Name==Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Use the property to define command groups
		/// </summary>
		public Collection<UIElement> CommandElements
		{
			get { return (Collection<UIElement>)GetValue(CommandElementsProperty); }
			set { SetValue(CommandElementsProperty, value); }
		}

		public static readonly DependencyProperty CommandElementsProperty =
			DependencyProperty.Register("CommandElements", typeof(Collection<UIElement>), typeof(AppBarCommandGroup),
			new PropertyMetadata(null));
	}
}
