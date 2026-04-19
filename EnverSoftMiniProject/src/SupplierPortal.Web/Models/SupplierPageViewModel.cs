using System.ComponentModel.DataAnnotations;
using SupplierPortal.Application.Models;

namespace SupplierPortal.Web.Models;

public enum ActionSource
{
    None,
    Create,
    Search
}

public class SupplierPageViewModel
{
    // Form Source tracking
    public ActionSource ActionSource { get; set; } = ActionSource.None;

    [Display(Name = "Company Name")]
    [Required(ErrorMessage = "Company name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Company name must be between 2 and 100 characters.")]
    public string? CreateCompanyName { get; set; }

    [Display(Name = "Telephone Number")]
    [Required(ErrorMessage = "Telephone number is required.")]
    [Phone(ErrorMessage = "Please enter a valid telephone number.")]
    [StringLength(20, MinimumLength = 7, ErrorMessage = "Telephone number must be between 7 and 20 characters.")]
    public string? CreateTelephoneNo { get; set; }

    [Display(Name = "Search Company Name")]
    [StringLength(100, ErrorMessage = "Search term is too long.")]
    public string? SearchCompanyName { get; set; }

    // Paged Results
    public PagedResult<SupplierResponse> SearchResults { get; set; } = new();
}
