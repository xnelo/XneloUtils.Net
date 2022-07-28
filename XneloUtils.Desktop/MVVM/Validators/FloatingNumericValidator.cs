#region Copyright (c) 2022 Spencer Hoffa
// \file FloatingNumericValidator.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System;
using System.Globalization;
using System.Windows.Controls;

namespace XneloUtils.Desktop.MVVM.Validators
{
	public class FloatingNumericValidator : ValidationRule
	{
		public bool AllowNegative { get; set; }
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string stringValue = Convert.ToString(value);
			if (string.IsNullOrEmpty(stringValue))
			{
				return new ValidationResult(false, "Cannot convert to string.");
			}

			if (float.TryParse(stringValue, out float floatNumber))
			{
				if (!AllowNegative && floatNumber < 0.0f)
				{
					return new ValidationResult(false, "Numeric value must be positive.");
				}
				else
				{
					return new ValidationResult(true, null);
				}
			}
			else
			{
				return new ValidationResult(false, "Cannot convert to a numeric value.");
			}
		}
	}
}
