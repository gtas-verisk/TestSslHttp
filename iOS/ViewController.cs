using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using UIKit;

namespace TestHttpSsl.iOS
{
	public partial class ViewController : UIViewController
	{
		protected ApiService ApiService { get; private set; }

		public ViewController(IntPtr handle) : base(handle)
		{
			ApiService = new ApiService(new TouchMessageHandler());
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate pCertificate, System.Security.Cryptography.X509Certificates.X509Chain pChain, System.Net.Security.SslPolicyErrors pSSLPolicyErrors)
			{
				Debug.WriteLine(this.GetType().Name, "Invoked ServerCertificateCallback");
				return true;
			};

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
