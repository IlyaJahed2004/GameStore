using System.ComponentModel.DataAnnotations;

namespace GameStore.Entities;

/// <summary>
/// DataAnnotations in ASP.NET Core provide declarative rules for validating model properties.
/// When a request is made (POST/PUT), ASP.NET Core automatically:
///
/// 1. **Model Binding**: Maps incoming JSON or form data to the model properties.
/// 2. **Model Validation**:
///     - `[Required]` → Rejects null or empty values.
///     - `[MaxLength(n)]` → Rejects strings longer than 'n' characters.
///     - `[Range(min, max)]` → Rejects numeric values outside the specified range.
///     - `[Url]` → Validates that the string is a proper URL (http/https).
/// 3. **Validation Result Handling**:
///     - If validation fails, ModelState.IsValid becomes false.
///     - With `[ApiController]` or `WithParameterValidation()`, ASP.NET Core automatically
///       returns HTTP 400 Bad Request with JSON error details.
/// 4. **Optional EF Core Integration** (if using a database):
///     - `[Required]` → Column becomes NOT NULL.
///     - `[MaxLength]` → Sets the maximum column size (e.g., VARCHAR(n)).
///     - `[Url]` and `[Range]` are **runtime validations** only, not enforced in DB.
///
/// Using data annotations ensures consistent validation between client input, API handling,
/// and database constraints (if EF Core is used), without writing manual checks in each endpoint.
/// </summary>


public class Game
{
    public int Id { get; set; }

    [Required] // Must have a value (null/empty rejected)
    [MaxLength(50)] // String length ≤ 50 chars
    public required string Name { get; set; }

    [Required] // Must have a value
    [MaxLength(20)] // String length ≤ 20 chars
    public required string Genre { get; set; }

    [Range(1, 100)] // Decimal value must be between 1 and 100
    public decimal Price { get; set; }

    public DateTime ReleaseDate { get; set; }

    [Required] // Must have a value
    [Url] // Must be a valid URL format (http/https)
    public required string imageuri { get; set; }
}
