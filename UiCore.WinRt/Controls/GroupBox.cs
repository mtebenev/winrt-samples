using Windows.UI.Xaml;

namespace Mt.Common.UiCore.Controls
{
	public enum GroupBoxPane
	{
		/// <summary>
		/// Flat white pane with round corners.
		/// </summary>
		Standard,
	}

	/// <summary>
	/// Represents a control that displays a frame around a group of controls with an optional caption.
	/// </summary>
	[TemplateVisualState(GroupName = VisualStates.PaneStates, Name = VisualStates.Standard)]
	public class GroupBox : HeaderedContentControl
	{
		private static class VisualStates
		{
			internal const string PaneStates = "PaneStates";
			internal const string Standard = "Standard";
		}

		public GroupBoxPane Pane
		{
			get { return (GroupBoxPane)GetValue(PaneProperty); }
			set { SetValue(PaneProperty, value); }
		}

		public static readonly DependencyProperty PaneProperty =
			DependencyProperty.Register("Pane", typeof(GroupBoxPane), typeof(GroupBox),
			new PropertyMetadata(GroupBoxPane.Standard, new PropertyChangedCallback(OnPaneChanged)));

		private static void OnPaneChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((GroupBox)d).OnPaneChanged(e);
		}

		protected virtual void OnPaneChanged(DependencyPropertyChangedEventArgs e)
		{
			UpdateVisualState();
		}

		public GridLength HeaderLength
		{
			get { return (GridLength)GetValue(HeaderLengthProperty); }
			set { SetValue(HeaderLengthProperty, value); }
		}
		public static readonly DependencyProperty HeaderLengthProperty =
			DependencyProperty.Register("HeaderLength", typeof(GridLength), typeof(GroupBox),
			  new PropertyMetadata(GridLength.Auto));

		public HorizontalAlignment HorizontalHeaderAlignment
		{
			get { return (HorizontalAlignment)GetValue(HorizontalHeaderAlignmentProperty); }
			set { SetValue(HorizontalHeaderAlignmentProperty, value); }
		}
		public static readonly DependencyProperty HorizontalHeaderAlignmentProperty =
			DependencyProperty.Register("HorizontalHeaderAlignment", typeof(HorizontalAlignment), typeof(GroupBox),
			  new PropertyMetadata(HorizontalAlignment.Left));

		public VerticalAlignment VerticalHeaderAlignment
		{
			get { return (VerticalAlignment)GetValue(VerticalHeaderAlignmentProperty); }
			set { SetValue(VerticalHeaderAlignmentProperty, value); }
		}
		public static readonly DependencyProperty VerticalHeaderAlignmentProperty =
			DependencyProperty.Register("VerticalHeaderAlignment", typeof(VerticalAlignment), typeof(GroupBox),
			  new PropertyMetadata(VerticalAlignment.Top));

		public GridLength ContentLength
		{
			get { return (GridLength)GetValue(ContentLengthProperty); }
			set { SetValue(ContentLengthProperty, value); }
		}
		public static readonly DependencyProperty ContentLengthProperty =
			DependencyProperty.Register("ContentLength", typeof(GridLength), typeof(GroupBox),
			  new PropertyMetadata(new GridLength(1, GridUnitType.Star)));

		public GroupBox()
		{
			DefaultStyleKey = typeof(GroupBox);
		}

		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			UpdateVisualState(useTransitions: false);
		}

		private void UpdateVisualState(bool useTransitions = true)
		{
			switch (this.Pane)
			{
				case GroupBoxPane.Standard:
					GoToVisualState(VisualStates.Standard, useTransitions);
					break;
			}
		}

		private bool GoToVisualState(string stateName, bool useTransitions)
		{
			return VisualStateManager.GoToState(this, stateName, useTransitions);
		}
	}
}
