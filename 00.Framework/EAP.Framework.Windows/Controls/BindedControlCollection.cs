using System;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace EAP.Framework.Windows.Controls
{
    public class BindedControlCollection : CollectionBase
    {
        public BindedControlCollection() { }

        public BindedControl Add(BindedControl value)
        {
            this.InnerList.Add(value);
            return value;
        }

        public void AddRange(BindedControl[] value)
        {
            this.InnerList.AddRange(value);
        }

        public void Remove(BindedControl value)
        {
            this.InnerList.Remove(value);
        }

        public bool Contains(BindedControl value)
        {
            return this.InnerList.Contains(value);
        }

        public BindedControl this[int index]
        {
            get { return (BindedControl)this.InnerList[index]; }
            set { this.InnerList[index] = value; }
        }

        public BindedControl[] GetValues()
        {
            BindedControl[] bc = new BindedControl[this.InnerList.Count];
            this.InnerList.CopyTo(0, bc, 0, this.InnerList.Count);
            return bc;
        }

    }

    public class BindedControlConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                BindedControl bc = (BindedControl)value;
                ConstructorInfo cInfo = typeof(BindedControl).GetConstructor(new Type[] { typeof(Control) });
                if (cInfo != null)
                {
                    return new InstanceDescriptor(cInfo, new object[] { bc.Control }, true);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
