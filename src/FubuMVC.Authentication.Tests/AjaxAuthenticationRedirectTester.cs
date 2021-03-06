﻿using System.Collections.Generic;
using FubuCore.Binding;
using FubuCore.Util;
using FubuMVC.Core;
using FubuTestingSupport;
using NUnit.Framework;


namespace FubuMVC.Authentication.Tests
{
	[TestFixture]
	public class AjaxAuthenticationRedirectTester
	{
		private AjaxAuthenticationRedirect theRedirect;
		private IRequestData theRequestData;

		[SetUp]
		public void SetUp()
		{
			theRequestData = new InMemoryRequestData();
			theRedirect = new AjaxAuthenticationRedirect(theRequestData, null, null, null);
		}

		[Test]
		public void applies_to_ajax_requests_positive()
		{
			var data = new Dictionary<string, string>();
			data.Add(AjaxExtensions.XRequestedWithHeader, AjaxExtensions.XmlHttpRequestValue);
			theRequestData.AddValues("test", new DictionaryKeyValues(data));

			theRedirect.Applies().ShouldBeTrue();
		}

		[Test]
		public void applies_to_ajax_requests_negative()
		{
			theRedirect.Applies().ShouldBeFalse();
		}
	}
}