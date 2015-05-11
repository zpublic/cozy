using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Starbound.UI.Bindings
{
    public enum BindingType { TwoWay, OneWay, OneWayToSource };

    public class Binding
    {
        private BindingType type;
        private IPropertyChanged source;
        private IPropertyChanged target;
        private string sourcePropertyName;
        private string targetPropertyName;
        private IValueConverter converter;

        private bool sourceToTarget;
        private bool targetToSource;

        public Binding(IPropertyChanged source, string sourcePropertyName, IPropertyChanged target, string targetPropertyName, BindingType type = BindingType.TwoWay, IValueConverter converter = null)
        {
            this.source = source;
            this.sourcePropertyName = sourcePropertyName;
            this.target = target;
            this.targetPropertyName = targetPropertyName;
            this.type = type;

            this.converter = converter == null ? new NoOpCoverter() : converter;

            if (type == BindingType.TwoWay || type == BindingType.OneWay)
            {
                sourceToTarget = true;
                source.PropertyChanged += SourceChanged;
            }

            if (type == BindingType.TwoWay || type == BindingType.OneWayToSource)
            {
                targetToSource = true;
                target.PropertyChanged += TargetChanged;
            }

            ValidateBinding();
        }

        private void ValidateBinding()
        {
            PropertyInfo sourceProperty = source.GetType().GetProperty(sourcePropertyName);
            if(sourceProperty == null)
            {
                throw new InvalidBindingException(String.Format(
                    "Source object of type {1} does not have a property names {0}.", sourcePropertyName, source.GetType()));
            }

            if (sourceToTarget && !sourceProperty.CanRead) 
            {
                throw new InvalidBindingException(String.Format(
                    "Source property {0} on object of type is set to {1} but property does not expose a public getter.", sourcePropertyName, source.GetType()));
            }

            PropertyInfo targetProperty = target.GetType().GetProperty(targetPropertyName);
            if (targetProperty == null)
            {
                throw new InvalidBindingException(String.Format(
                    "Target object of type {1} does not have a property names {0}.", targetPropertyName, target.GetType()));
            }

            if (targetToSource && !targetProperty.CanRead)
            {
                throw new InvalidBindingException(String.Format(
                    "Target property {0} on object of type is set to {1} but property does not expose a public getter.", targetPropertyName, target.GetType()));
            }

            if(converter == null && sourceProperty.PropertyType != targetProperty.PropertyType)
            {
                throw new InvalidBindingException(String.Format(
                    "Target property type {0} and source property type {1} do not match, and no converter was supplied to allow the binding.", targetProperty.PropertyType, sourceProperty.PropertyType));
            }
        }

        private void SourceChanged(string property)
        {
            if (property != sourcePropertyName) { return; }

            object value = GetSourceValue();
            value = converter.Convert(value, null);
            SetTargetValue(value);
        }

        private void TargetChanged(string property)
        {
            if (property != targetPropertyName) { return; }

            object value = GetTargetValue();
            value = converter.Convert(value, null);
            SetSourceValue(value);
        }

        private object GetSourceValue()
        {
            return source.GetType().GetProperty(sourcePropertyName).GetValue(source, new object[0]);
        }

        private object GetTargetValue()
        {
            return target.GetType().GetProperty(targetPropertyName).GetValue(target, new object[0]);
        }

        private void SetSourceValue(object value)
        {
            source.GetType().GetProperty(sourcePropertyName).SetValue(source, value, new object[0]);
        }

        private void SetTargetValue(object value)
        {
            target.GetType().GetProperty(targetPropertyName).SetValue(target, value, new object[0]);
        }
    }

    public class NoOpCoverter : IValueConverter
    {
        public object Convert(object value, Type type)
        {
            return value;
        }

        public object ConvertBack(object value, Type type)
        {
            return value;
        }
    }
}
