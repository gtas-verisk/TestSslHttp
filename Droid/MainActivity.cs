﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using System;
using System.Net;

namespace TestHttpSsl.Droid
{
	[Activity(Label = "TestHttpSsl", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected ApiService ApiService { get; private set; }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			ApiService = new ApiService();

			ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate pCertificate, System.Security.Cryptography.X509Certificates.X509Chain pChain, System.Net.Security.SslPolicyErrors pSSLPolicyErrors)
			{
				Log.Debug(this.GetType().Name, "Invoked ServerCertificateCallback");
				return true;
			};

			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.githubApiButton);
			Button syncApiButton = FindViewById<Button>(Resource.Id.syncApiButton);

			button.Click += async delegate 
			{ 
				try
				{
					string result = await ApiService.GithubWebApi.GetOctokitReposAsync().ConfigureAwait(true);
					Log.Debug(this.GetType().Name, string.Format("JSON Result Github: {0}", result));
				}
				catch (Exception ex)
				{
					Log.Debug(this.GetType().Name, ex.ToString());
				}
			};

			syncApiButton.Click += async delegate
			{
				try
				{
					string result = await ApiService.WebApi.GetInitialOrUpdatedData("f6f83e7d-8c8c-4a82-99b5-677d1a2ec608", 60136).ConfigureAwait(true);
					Log.Debug(this.GetType().Name, string.Format("JSON Result Sync: {0}", result));
				}
				catch (Exception ex)
				{
					Log.Debug(this.GetType().Name, ex.ToString());
				}
			};
		}
	}
}


