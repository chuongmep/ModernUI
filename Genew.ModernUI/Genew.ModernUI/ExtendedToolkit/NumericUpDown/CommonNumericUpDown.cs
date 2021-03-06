﻿/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： CommonNumericUpDown.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:19
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Globalization;
using System.IO;
using System.Windows;
using ModernUI.ExtendedToolkit.Primitives;

namespace ModernUI.ExtendedToolkit
{
    public abstract class CommonNumericUpDown<T> : NumericUpDown<T?> where T : struct, IFormattable, IComparable<T>
    {
        private readonly FromDecimal _fromDecimal;
        private readonly Func<T, T, bool> _fromGreaterThan;
        private readonly Func<T, T, bool> _fromLowerThan;
        private readonly FromText _fromText;

        #region ParsingNumberStyle

        public static readonly DependencyProperty ParsingNumberStyleProperty =
            DependencyProperty.Register("ParsingNumberStyle", typeof(NumberStyles), typeof(CommonNumericUpDown<T>),
                new UIPropertyMetadata(NumberStyles.Any));

        public NumberStyles ParsingNumberStyle
        {
            get { return (NumberStyles)GetValue(ParsingNumberStyleProperty); }
            set { SetValue(ParsingNumberStyleProperty, value); }
        }

        #endregion //ParsingNumberStyle

        protected CommonNumericUpDown(FromText fromText, FromDecimal fromDecimal, Func<T, T, bool> fromLowerThan,
            Func<T, T, bool> fromGreaterThan)
        {
            if (fromText == null)
                throw new ArgumentNullException("parseMethod");

            if (fromDecimal == null)
                throw new ArgumentNullException("fromDecimal");

            if (fromLowerThan == null)
                throw new ArgumentNullException("fromLowerThan");

            if (fromGreaterThan == null)
                throw new ArgumentNullException("fromGreaterThan");

            _fromText = fromText;
            _fromDecimal = fromDecimal;
            _fromLowerThan = fromLowerThan;
            _fromGreaterThan = fromGreaterThan;
        }

        protected static void UpdateMetadata(Type type, T? increment, T? minValue, T? maxValue)
        {
            DefaultStyleKeyProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(type));
            UpdateMetadataCommon(type, increment, minValue, maxValue);
        }

        internal static void UpdateMetadataInternal(Type type, T? increment, T? minValue, T? maxValue)
        {
            // DefaultStyleKey for internal type (eg. UShortUpDown) must be a ComponentResourceKey instead
            // of the type itself to allow external theme (ex. Office2007 theme) to redefine the default
            // style of the control.
            DefaultStyleKeyProperty.OverrideMetadata(type,
                new FrameworkPropertyMetadata(new ComponentResourceKey(typeof(InputBase), type.Name)));
            UpdateMetadataCommon(type, increment, minValue, maxValue);
        }

        private static void UpdateMetadataCommon(Type type, T? increment, T? minValue, T? maxValue)
        {
            IncrementProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(increment));
            MaximumProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(maxValue));
            MinimumProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(minValue));
        }

        protected void TestInputSpecialValue(AllowedSpecialValues allowedValues, AllowedSpecialValues valueToCompare)
        {
            if ((allowedValues & valueToCompare) != valueToCompare)
            {
                switch (valueToCompare)
                {
                    case AllowedSpecialValues.NaN:
                        throw new InvalidDataException("Value to parse shouldn't be NaN.");
                    case AllowedSpecialValues.PositiveInfinity:
                        throw new InvalidDataException("Value to parse shouldn't be Positive Infinity.");
                    case AllowedSpecialValues.NegativeInfinity:
                        throw new InvalidDataException("Value to parse shouldn't be Negative Infinity.");
                }
            }
        }

        private bool IsLowerThan(T? value1, T? value2)
        {
            if (value1 == null || value2 == null)
                return false;

            return _fromLowerThan(value1.Value, value2.Value);
        }

        private bool IsGreaterThan(T? value1, T? value2)
        {
            if (value1 == null || value2 == null)
                return false;

            return _fromGreaterThan(value1.Value, value2.Value);
        }

        private bool HandleNullSpin()
        {
            if (!Value.HasValue)
            {
                T forcedValue = (DefaultValue.HasValue)
                    ? DefaultValue.Value
                    : default(T);

                Value = CoerceValueMinMax(forcedValue);

                return true;
            }
            if (!Increment.HasValue)
            {
                return true;
            }

            return false;
        }

        internal bool IsValid(T? value)
        {
            return !IsLowerThan(value, Minimum) && !IsGreaterThan(value, Maximum);
        }

        private T? CoerceValueMinMax(T value)
        {
            if (IsLowerThan(value, Minimum))
                return Minimum;
            if (IsGreaterThan(value, Maximum))
                return Maximum;
            return value;
        }

        #region Base Class Overrides

        protected override void OnIncrement()
        {
            if (!HandleNullSpin())
            {
                T result = IncrementValue(Value.Value, Increment.Value);
                Value = CoerceValueMinMax(result);
            }
        }

        protected override void OnDecrement()
        {
            if (!HandleNullSpin())
            {
                T result = DecrementValue(Value.Value, Increment.Value);
                Value = CoerceValueMinMax(result);
            }
        }

        protected override T? ConvertTextToValue(string text)
        {
            T? result = null;

            if (String.IsNullOrEmpty(text))
                return result;

            // Since the conversion from Value to text using a FormartString may not be parsable,
            // we verify that the already existing text is not the exact same value.
            string currentValueText = ConvertValueToText();
            if (object.Equals(currentValueText, text))
                return this.Value;

            //Don't know why someone would format a T as %, but just in case they do.
            result = FormatString.Contains("P")
                ? _fromDecimal(ParsePercent(text, CultureInfo))
                : _fromText(text, ParsingNumberStyle, CultureInfo);

            if (ClipValueToMinMax)
            {
                return GetClippedMinMaxValue();
            }

            ValidateDefaultMinMax(result);

            return result;
        }

        protected override string ConvertValueToText()
        {
            if (Value == null)
                return string.Empty;

            return Value.Value.ToString(FormatString, CultureInfo);
        }

        protected override void SetValidSpinDirection()
        {
            ValidSpinDirections validDirections = ValidSpinDirections.None;

            // Null increment always prevents spin.
            if ((Increment != null) && !IsReadOnly)
            {
                if (IsLowerThan(Value, Maximum) || !Value.HasValue)
                    validDirections = validDirections | ValidSpinDirections.Increase;

                if (IsGreaterThan(Value, Minimum) || !Value.HasValue)
                    validDirections = validDirections | ValidSpinDirections.Decrease;
            }

            if (Spinner != null)
                Spinner.ValidSpinDirection = validDirections;
        }

        private T? GetClippedMinMaxValue()
        {
            T? result = FormatString.Contains("P")
                ? _fromDecimal(ParsePercent(this.Text, CultureInfo))
                : _fromText(this.Text, ParsingNumberStyle, CultureInfo);

            if (IsGreaterThan(result, Maximum))
                return Maximum;
            if (IsLowerThan(result, Minimum))
                return Minimum;
            return result;
        }

        private void ValidateDefaultMinMax(T? value)
        {
            // DefaultValue is always accepted.
            if (object.Equals(value, DefaultValue))
                return;

            if (IsLowerThan(value, Minimum))
                throw new ArgumentOutOfRangeException("Minimum",
                    String.Format("Value must be greater than MinValue of {0}", Minimum));
            if (IsGreaterThan(value, Maximum))
                throw new ArgumentOutOfRangeException("Maximum",
                    String.Format("Value must be less than MaxValue of {0}", Maximum));
        }

        #endregion //Base Class Overrides

        #region Abstract Methods

        protected abstract T IncrementValue(T value, T increment);

        protected abstract T DecrementValue(T value, T increment);

        #endregion

        protected delegate T FromDecimal(decimal d);

        protected delegate T FromText(string s, NumberStyles style, IFormatProvider provider);
    }
}