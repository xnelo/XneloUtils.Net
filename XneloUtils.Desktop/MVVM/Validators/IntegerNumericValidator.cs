﻿#region Copyright (c) 2022 Spencer Hoffa
// \file IntegerNumericValidator.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System;
using System.Globalization;
using System.Windows.Controls;

namespace XneloUtils.Desktop.MVVM.Validators
{
	public class IntegerNumericValidator : ValidationRule
	{
		public bool AllowNegative { get; set; }
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string stringValue = Convert.ToString(value);
			if (string.IsNullOrEmpty(stringValue))
			{
				return new ValidationResult(false, "Cannot convert to string.");
			}

			// using float here gives the results I need in the UI
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
