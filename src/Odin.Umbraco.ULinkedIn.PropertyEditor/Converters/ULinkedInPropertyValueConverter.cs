using Odin.Umbraco.ULinkedIn.PropertyEditor.Extensions;
using System;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using website.Odin.Umbraco.ULinkedIn.PropertyEditor.Models;

namespace Odin.Umbraco.ULinkedIn.PropertyEditor.Converters
{
	[PropertyValueType(typeof(ULinkedInModel))]
	[PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
	public class ULinkedInPropertyValueConverter : PropertyValueConverterBase
	{
		public override bool IsConverter(PublishedPropertyType propertyType)
		{
			return propertyType.PropertyEditorAlias.Equals("Odin.Umbraco.ULinkedIn");
        }

		public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
		{
			try
			{
				if (source != null && !source.ToString().IsNullOrWhiteSpace())
				{
					return source.ToString().DeserializeJsonTo<ULinkedInModel>();
				}
			}
			catch (Exception e)
			{
				LogHelper.Error<ULinkedInPropertyValueConverter>("Error converting uLinkedIn value", e);
			}

			return null;
		}
	}
}