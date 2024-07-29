using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class YearLessThanOrEqualToCurrentYearAttribute(string nomeCampo) : ValidationAttribute
{
    private string NomeCampo = nomeCampo;
    public override bool IsValid(object value)
    {
        if (!int.TryParse(value.ToString(), out int year))
        {
            return false;
        }

        var currentYear = DateTime.Now.Year;

        return year <= currentYear;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"O {NomeCampo} não pode ser maior que o ano atual.";
    }
}
