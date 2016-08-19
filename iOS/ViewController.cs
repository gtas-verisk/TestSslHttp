﻿using System;
using System.Diagnostics;
using UIKit;

namespace TestHttpSsl.iOS
{
	public partial class ViewController : UIViewController
	{
		protected ApiService ApiService { get; private set; }

		public ViewController(IntPtr handle) : base(handle)
		{
			ApiService = new ApiService();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
			Button.AccessibilityIdentifier = "myButton";
			Button.TouchUpInside += async delegate
			{
				try
				{
					string result = await ApiService.WebApi.GetInitialOrUpdatedData("f6f83e7d-8c8c-4a82-99b5-677d1a2ec608", 60136).ConfigureAwait(true);
					Debug.WriteLine("JSON Result: {0}", result);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex);
				}
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}