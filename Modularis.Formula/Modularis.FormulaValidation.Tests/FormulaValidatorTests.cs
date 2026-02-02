using Modularis.FormulaValidation;
using System.Collections.Generic;
using System.IO;
using Xunit;

public class FormulaValidatorTests
{
    private readonly FormulaValidator _validator;

    public FormulaValidatorTests()
    {
        _validator = new FormulaValidator();
    }

    [Fact]
    public void ValidateWellFormedFormulas_FromFile_ShouldAllBeTrue()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Well formed formulas.txt");
        var formulas = File.ReadAllLines(path);

        foreach (var formula in formulas)
        {
            if (string.IsNullOrWhiteSpace(formula)) continue;
            bool result = _validator.IsWellFormed(formula.Trim());
            Assert.True(result, $"Error: The formula '{formula}' should be VALID.");
        }
    }

    [Fact]
    public void ValidateBadFormedFormulas_FromFile_ShouldAllBeFalse()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Bad formed formulas.txt");
        var formulas = File.ReadAllLines(path);

        foreach (var formula in formulas)
        {
            if (string.IsNullOrWhiteSpace(formula)) continue;
            bool result = _validator.IsWellFormed(formula.Trim());
            Assert.False(result, $"Error: The formula '{formula}' should be INVALID.");
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void IsWellFormed_EmptyOrNull_ReturnsTrue(string formula)
    {
        bool result = _validator.IsWellFormed(formula);
        Assert.True(result);
    }
}