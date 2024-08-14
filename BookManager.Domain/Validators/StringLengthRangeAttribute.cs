using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class StringLengthRangeAttribute(string nomeCampo, int minimumLenght, int maximumLength) : ValidationAttribute
{
    private string NomeCampo = nomeCampo;
    private int MinimumLength = minimumLenght;
    private int MaximumLength = maximumLength;
    public override bool IsValid(object value)
    {
        int lenght = value.ToString().Length;
        return lenght >= MinimumLength && lenght <= MaximumLength;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{NomeCampo} deve ter no mínimo {MinimumLength} e no máximo {MaximumLength} caracteres.";
    }
}
