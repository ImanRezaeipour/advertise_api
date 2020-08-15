using System;
using System.Collections.Generic;
using System.Linq;
using Advertise.Core.Extensions;

namespace Advertise.Core.Managers.ExportImport.Help
{
    public class PropertyByName<T>
    {
        #region Private Fields

        private object _propertyValue;

        #endregion Private Fields

        #region Public Constructors

        public PropertyByName(string propertyName, Func<T, object> func = null, bool ignore = false)
        {
            this.PropertyName = propertyName;
            this.GetProperty = func;
            this.PropertyOrderPosition = 1;
            this.Ignore = ignore;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool AllowBlank { get; set; }

        public bool BooleanValue
        {
            get
            {
                bool rez;
                if (PropertyValue == null || !bool.TryParse(PropertyValue.ToString(), out rez))
                    return default(bool);
                return rez;
            }
        }

        public DateTime? DateTimeNullable => PropertyValue == null ? null : DateTime.FromOADate(DoubleValue) as DateTime?;

        public decimal DecimalValue
        {
            get
            {
                decimal rez;
                if (PropertyValue == null || !decimal.TryParse(PropertyValue.ToString(), out rez))
                    return default(decimal);
                return rez;
            }
        }

        public decimal? DecimalValueNullable
        {
            get
            {
                decimal rez;
                if (PropertyValue == null || !decimal.TryParse(PropertyValue.ToString(), out rez))
                    return null;
                return rez;
            }
        }

        public double DoubleValue
        {
            get
            {
                double rez;
                if (PropertyValue == null || !double.TryParse(PropertyValue.ToString(), out rez))
                    return default(double);
                return rez;
            }
        }

        public IList<SelectListModel> DropDownElements { get; set; }

        public Func<T, object> GetProperty { get; private set; }

        public bool Ignore { get; set; }

        public int IntValue
        {
            get
            {
                int rez;
                if (PropertyValue == null || !int.TryParse(PropertyValue.ToString(), out rez))
                    return default(int);
                return rez;
            }
        }

        public bool IsCaption => PropertyName == StringValue || PropertyName == _propertyValue.ToString();

        public bool IsDropDownCell => DropDownElements != null;

        public string PropertyName { get; private set; }

        public int PropertyOrderPosition { get; set; }

        public object PropertyValue
        {
            get
            {
                return IsDropDownCell ? GetItemId(_propertyValue) : _propertyValue;
            }
            set
            {
                _propertyValue = value;
            }
        }

        public string StringValue => PropertyValue == null ? string.Empty : Convert.ToString(PropertyValue);

        public Guid? GuidValue
        {
            get
            {
                Guid result;
                Guid.TryParse(Convert.ToString(PropertyValue), out result);
                return result;
            }
        }

        #endregion Public Properties

        #region Public Methods


        public string[] GetDropDownElements()
        {
            return IsDropDownCell ? DropDownElements.Select(ev => ev.Text).ToArray() : new string[0];
        }

        public Guid? GetItemId(object name)
        {
            return DropDownElements.FirstOrDefault(ev => ev.Text.Trim() == name.Return(s => s.ToString(), String.Empty).Trim()).Return(ev => Guid.Parse(ev.Value), Guid.Empty);
        }

        public string GetItemText(object id)
        {
            return DropDownElements.FirstOrDefault(ev => ev.Value == id.ToString()).Return(ev => ev.Text, String.Empty);
        }

        public override string ToString()
        {
            return PropertyName;
        }

        #endregion Public Methods
    }
}