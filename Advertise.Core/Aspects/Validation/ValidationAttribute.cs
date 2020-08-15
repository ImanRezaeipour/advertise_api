using System;

namespace Advertise.Core.Aspects.Validation
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidationAttribute : Attribute
    {
        #region Public Constructors

        public ValidationAttribute(Type className, string viewName = null, string actionName = null)
        {
            ClassName = className;
            ViewName = viewName;
            ActionName = actionName;
        }

        #endregion Public Constructors

        #region Public Properties

        public string ActionName { get; set; }
        public Type ClassName { get; set; }
        public string ViewName { get; set; }

        #endregion Public Properties
    }
}