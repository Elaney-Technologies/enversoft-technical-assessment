using Microsoft.AspNetCore.Mvc;
using SupplierPortal.Application.Interfaces;
using SupplierPortal.Application.Models;
using SupplierPortal.Web.Models;

namespace SupplierPortal.Web.Controllers;

public class SuppliersController : Controller
{
    private readonly ISupplierService _supplierService;
    private const int PageSize = 5;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new SupplierPageViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SupplierPageViewModel model, CancellationToken cancellationToken)
    {
        model.ActionSource = ActionSource.Create;

        // Ignore search validation during creation
        ModelState.Remove(nameof(model.SearchCompanyName));

        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        try
        {
            var response = await _supplierService.CreateAsync(new CreateSupplierRequest
            {
                CompanyName = model.CreateCompanyName ?? string.Empty,
                TelephoneNo = model.CreateTelephoneNo ?? string.Empty
            }, cancellationToken);

            TempData["SuccessMessage"] = $"Supplier '{response.CompanyName}' saved successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("Index", model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Search(SupplierPageViewModel model, int page = 1, CancellationToken cancellationToken = default)
    {
        model.ActionSource = ActionSource.Search;

        // Ignore create validation during searching
        ModelState.Remove(nameof(model.CreateCompanyName));
        ModelState.Remove(nameof(model.CreateTelephoneNo));

        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        if (string.IsNullOrWhiteSpace(model.SearchCompanyName))
        {
            ModelState.AddModelError(nameof(model.SearchCompanyName), "Please enter a company name to search.");
            return View("Index", model);
        }

        model.SearchResults = await _supplierService.SearchByCompanyNameAsync(model.SearchCompanyName, page, PageSize, cancellationToken);
        if (!model.SearchResults.Items.Any())
        {
            ModelState.AddModelError(string.Empty, "No suppliers found matching that company name.");
        }

        return View("Index", model);
    }
}
