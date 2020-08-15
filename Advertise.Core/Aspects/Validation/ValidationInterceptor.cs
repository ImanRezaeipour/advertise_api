using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Exceptions;
using Advertise.Core.Infrastructure.DependencyManagement;
using FluentValidation.Results;
using StructureMap.DynamicInterception;

namespace Advertise.Core.Aspects.Validation
{
    public class ValidationInterceptor : IAsyncInterceptionBehavior
    {
        #region Public Methods

        public async Task<IMethodInvocationResult> InterceptAsync(IAsyncMethodInvocation methodInvocation)
        {
            var validationAttribute = (ValidationAttribute)Attribute.GetCustomAttribute(methodInvocation.InstanceMethodInfo, typeof(ValidationAttribute), true);
            if (validationAttribute == null)
            {
                return await methodInvocation.InvokeNextAsync();
            }

            await CallValidatorWithDependencyInjectionAsync(methodInvocation, validationAttribute);

            return await methodInvocation.InvokeNextAsync();
        }

        #endregion Public Methods

        #region Private Methods

        private static async Task CallValidatorWithDependencyInjectionAsync(IAsyncMethodInvocation methodInvocation, ValidationAttribute validationAttribute)
        {
            var validatorClass = Type.GetType(validationAttribute.ClassName.ToString());
            if (validatorClass == null)
                return;

            if (!string.IsNullOrEmpty(validationAttribute.ViewName))
                if(!HttpContext.Current.Items.Contains("ViewName"))
                    HttpContext.Current.Items["ViewName"] = validationAttribute.ViewName;

            if (!string.IsNullOrEmpty(validationAttribute.ActionName))
                if (!HttpContext.Current.Items.Contains("ActionName"))
                    HttpContext.Current.Items["ActionName"] = validationAttribute.ActionName;

            var validatorObject = ContainerManager.Container.GetInstance(validatorClass);
            var methodInfo = validatorClass.GetMethod("ValidateAsync", new[] { methodInvocation.Arguments[0].Value.GetType(), typeof(CancellationToken) });
            if (methodInfo == null)
                return;

            var validateResult = await (Task<ValidationResult>)methodInfo.Invoke(validatorObject, new[] { methodInvocation.Arguments[0].Value, default(CancellationToken) });
            if (validateResult.IsValid == false)
                throw new ValidationException(validateResult.Errors.Select(failure => failure.ErrorMessage).ToList(), validationAttribute.ViewName);
        }

        private static void CallValidatorWithReflection(IAsyncMethodInvocation methodInvocation, ValidationAttribute validationAttribute)
        {
            var validatorClass = Type.GetType(validationAttribute.ClassName.ToString());
            if (validatorClass == null)
                return;

            var validatorObject = Activator.CreateInstance(validatorClass);
            var methodInfo = validatorClass.GetMethod("Validate", new[] { methodInvocation.Arguments[0].Value.GetType() });
            if (methodInfo == null)
                return;

            var validateResult = (ValidationResult)methodInfo.Invoke(validatorObject, new[] { methodInvocation.Arguments[0].Value });
            if (validateResult.IsValid == false)
                throw new ValidationException(validateResult.Errors.First().ErrorMessage, validationAttribute.ViewName);
        }

        #endregion Private Methods
    }
}