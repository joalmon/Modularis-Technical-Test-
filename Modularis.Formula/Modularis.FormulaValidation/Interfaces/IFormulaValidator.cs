namespace Modularis.FormulaValidation.Interfaces
{
    public interface IFormulaValidator
    {
        bool IsWellFormed(string formula);
    }
}