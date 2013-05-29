﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace Mt.Common.UiCore.Controls
{
	/// <summary>
	/// Represents an extended <see cref="RadControl"/> class that adds the "Header" notation.
	/// Typically header is considered a label on top of the control that hints for the control's purpose in the UI.
	/// </summary>
	public class HeaderedContentControl : ContentControl
	{
		public HeaderedContentControl()
		{
			this.DefaultStyleKey = typeof(HeaderedContentControl);
		}

		/// <summary>
		/// Identifies the <see cref="Header"/> property.
		/// </summary>
		public static readonly DependencyProperty HeaderProperty =
				DependencyProperty.Register("Header", typeof(object), typeof(HeaderedContentControl), new PropertyMetadata(null));

		/// <summary>
		/// Identifies the <see cref="HeaderTemplate"/> property.
		/// </summary>
		public static readonly DependencyProperty HeaderTemplateProperty =
				DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(HeaderedContentControl), new PropertyMetadata(null));

		/// <summary>
		/// Identifies the <see cref="HeaderStyle"/> property.
		/// </summary>
		public static readonly DependencyProperty HeaderStyleProperty =
				DependencyProperty.Register("HeaderStyle", typeof(Style), typeof(HeaderedContentControl), new PropertyMetadata(null));

		/// <summary>
		/// Gets or sets the object that represents the header content.
		/// </summary>
		public object Header
		{
			get
			{
				return this.GetValue(HeaderProperty);
			}
			set
			{
				this.SetValue(HeaderProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> instance that defines the appearance of the header.
		/// </summary>
		public DataTemplate HeaderTemplate
		{
			get
			{
				return this.GetValue(HeaderTemplateProperty) as DataTemplate;
			}
			set
			{
				this.SetValue(HeaderTemplateProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Style"/> object that defines the appearance of the Header part of the Control.
		/// Typically that part will be represented by a <see cref="ContentControl"/> instance.
		/// </summary>
		public Style HeaderStyle
		{
			get
			{
				return this.GetValue(HeaderStyleProperty) as Style;
			}
			set
			{
				this.SetValue(HeaderStyleProperty, value);
			}
		}
	}
}
