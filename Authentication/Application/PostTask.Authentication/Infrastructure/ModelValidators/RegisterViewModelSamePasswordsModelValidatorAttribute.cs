using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PostTask.Authentication.ViewModels;

namespace PostTask.Authentication.Infrastructure.ModelValidators;
/// <summary>
///     Model validator for register view model on check by equality password and repeat password
///     model properties
/// </summary>
public class RegisterViewModelSamePasswordsModelValidatorAttribute : Attribute, IModelValidator
{
    /// <summary>
    ///     Password property name
    /// </summary>
    private readonly string _passwordName = nameof(RegisterViewModel.Password);
    /// <summary>
    ///     Repeat password property name
    /// </summary>
    private readonly string _repeatPasswordName = nameof(RegisterViewModel.RepeatPassword);

    /// <summary>
    ///     Implementation of model validator interface, validating model
    /// </summary>
    /// <param name="context">
    ///     Validation context provider
    /// </param>
    /// <returns>
    ///     Enumerable of validation result
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred if validated model is null (which cannot be in ordinary situation)
    /// </exception>
    public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
    {
        var properties = context.MetadataProvider
            .GetMetadataForProperties(modelType: typeof(RegisterViewModel));

        var passwordValue = GetValueOfObjectByName<string>(
            properties,
            name: _passwordName,
            model: context.Model ??  throw new InvalidOperationException(
                message: "Model cannot be null!"));
        var repeatPasswordValue = GetValueOfObjectByName<string>(
            properties,
            name: _repeatPasswordName,
            model: context.Model ?? throw new InvalidOperationException(
                message: "Model cannot be null!"));

        if (passwordValue != repeatPasswordValue)
            return new List<ModelValidationResult>()
            {
                new ModelValidationResult(
                    memberName: string.Empty,
                    message: "Password and Repeat password is not equals!")
            };

        return Enumerable.Empty<ModelValidationResult>();
    }

    /// <summary>
    ///     Get value of property by name and automatically cast in type T
    /// </summary>
    /// <typeparam name="T">
    ///     Type of requested property
    /// </typeparam>
    /// <param name="properties">
    ///     All enumerable properties metadata from container
    /// </param>
    /// <param name="name">
    ///     Property name
    /// </param>
    /// <param name="model">
    ///     Model object
    /// </param>
    /// <returns>
    ///     Property value
    /// </returns>
    private T? GetValueOfObjectByName<T>(
        IEnumerable<ModelMetadata> properties,
        string name,
        object model)
        where T : class
        => properties
            .FirstOrDefault(c => c.Name == name)?
            .PropertyGetter?.Invoke(model) as T;
}