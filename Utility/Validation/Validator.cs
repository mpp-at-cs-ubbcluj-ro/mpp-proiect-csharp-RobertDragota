namespace Lab3.Utility.Validation;

public interface Validator<TE>
{
    // In C#, method documentation is typically added using XML comments.
    /// <summary>
    /// Validates the specified entity.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <exception cref="ValidException">Thrown when the validation fails.</exception>
    void validate(TE entity);
}