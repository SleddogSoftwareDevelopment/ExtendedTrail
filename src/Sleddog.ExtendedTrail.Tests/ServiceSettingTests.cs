using System;
using Ploeh.AutoFixture.Xunit;
using Ploeh.SemanticComparison.Fluent;
using Xunit;
using Xunit.Extensions;

namespace Sleddog.ExtendedTrail.Tests
{
	public class ServiceSettingTests
	{
		[Theory]
		[InlineData(null, "randomValue")]
		[InlineData("randomValue", null)]
		public void TwoParamConstructorDoesNotAcceptNull(string serviceName, string displayName)
		{
			Assert.Throws<ArgumentNullException>(() => new ServiceSettings(serviceName, displayName));
		}

		[Theory, AutoData]
		public void TwoParamConstructorSetsPropertiesRight(string serviceName, string displayName)
		{
			var sut = new ServiceSettings(serviceName, displayName);

			var expected = new ServiceSettingResult {ServiceName = serviceName, DisplayName = displayName};

			sut.AsSource().OfLikeness<ServiceSettingResult>().Equals(expected);
		}

		[Theory]
		[InlineData(null, "randomValue", "randomValue")]
		[InlineData("randomValue", null, "randomValue")]
		[InlineData("randomValue", "randomValue", null)]
		public void ThreeParamConstructorDoesNotAcceptNull(string serviceName, string displayName, string description)
		{
			Assert.Throws<ArgumentNullException>(() => new ServiceSettings(serviceName, displayName, description));
		}

		[Theory, AutoData]
		public void ThreeParamConstructorSetsPropertiesRight(string serviceName, string displayName, string description)
		{
			var sut = new ServiceSettings(serviceName, displayName, description);

			var expected = new ServiceSettingResult {ServiceName = serviceName, DisplayName = displayName, Description = description};

			sut.AsSource().OfLikeness<ServiceSettingResult>().Equals(expected);
		}

		private class ServiceSettingResult
		{
			public string ServiceName { get; set; }
			public string DisplayName { get; set; }
			public string Description { get; set; }
		}
	}
}