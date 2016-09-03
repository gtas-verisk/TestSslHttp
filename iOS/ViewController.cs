using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
					//string result = await ApiService.WebApi.GetInitialOrUpdatedData("f6f83e7d-8c8c-4a82-99b5-677d1a2ec608", 60136).ConfigureAwait(true);

					HttpClient hc = new HttpClient(new NSUrlSessionHandler());

					string url = "https://flightkitapi.iso.com/sync/Order?workerid=43303&clientid=" + Guid.NewGuid().ToString();

					hc.BaseAddress = new Uri(url);

					var x = hc.BaseAddress.PathAndQuery;

					hc.DefaultRequestHeaders.Accept.Clear();
					hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					hc.DefaultRequestHeaders.Add("access_token", "a0ee216f-621d-4065-a171-bf49c51af636");

					HttpResponseMessage result = await hc.GetAsync(url);

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
