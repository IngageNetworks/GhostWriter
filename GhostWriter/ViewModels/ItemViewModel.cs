﻿using System;
using System.ComponentModel;

namespace GhostWriter
{
	public class ItemViewModel : INotifyPropertyChanged
	{
		private string _lineOne;

		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding.
		/// </summary>
		/// <returns></returns>
		public string LineOne
		{
			get
			{
				return _lineOne;
			}
			set
			{
				if (value != _lineOne)
				{
					_lineOne = value;
					NotifyPropertyChanged("LineOne");
				}
			}
		}

		private string _lineTwo;

		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding.
		/// </summary>
		/// <returns></returns>
		public string LineTwo
		{
			get
			{
				return _lineTwo;
			}
			set
			{
				if (value != _lineTwo)
				{
					_lineTwo = value;
					NotifyPropertyChanged("LineTwo");
				}
			}
		}

		private string _lineThree;

		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding.
		/// </summary>
		/// <returns></returns>
		public string LineThree
		{
			get
			{
				return _lineThree;
			}
			set
			{
				if (value != _lineThree)
				{
					_lineThree = value;
					NotifyPropertyChanged("LineThree");
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(String propertyName)
		{
			var handler = PropertyChanged;
			if (null != handler)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}