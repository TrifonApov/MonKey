namespace MonKey.Web.ViewModels.Account
{
    using System.Globalization;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public sealed class CultureAwareDecimalModelBinder : IModelBinder
    {
        private const NumberStyles DecimalStyles = NumberStyles.AllowLeadingSign
                                                   | NumberStyles.AllowDecimalPoint
                                                   | NumberStyles.AllowLeadingWhite
                                                   | NumberStyles.AllowTrailingWhite;

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueResult == ValueProviderResult.None)
            {
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName,
                    $"The {bindingContext.ModelMetadata.GetDisplayName()} field is required.");
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueResult);
            string? value = valueResult.FirstValue;
            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName,
                    $"The {bindingContext.ModelMetadata.GetDisplayName()} field is required.");
                return Task.CompletedTask;
            }

            bool parsed = TryParseDecimal(value, out decimal balance);
            if (parsed)
            {
                bindingContext.Result = ModelBindingResult.Success(balance);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName,
                    $"The value '{value}' is not valid for {bindingContext.ModelMetadata.GetDisplayName()}.");
            }

            return Task.CompletedTask;
        }

        private static bool TryParseDecimal(string value, out decimal result)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            string currentDecimalSeparator = currentCulture.NumberFormat.NumberDecimalSeparator;

            if (value.Contains(currentDecimalSeparator, StringComparison.Ordinal))
            {
                return decimal.TryParse(value, DecimalStyles, currentCulture, out result)
                       || decimal.TryParse(value, DecimalStyles, CultureInfo.InvariantCulture, out result);
            }

            return decimal.TryParse(value, DecimalStyles, CultureInfo.InvariantCulture, out result)
                   || decimal.TryParse(value, DecimalStyles, currentCulture, out result);
        }
    }
}
