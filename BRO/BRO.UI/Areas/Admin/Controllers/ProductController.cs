using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Product;
using BRO.Domain.Command.ProductTaste;
using BRO.Domain.Query.Category;
using BRO.Domain.Query.DTO.Product;
using BRO.Domain.Query.Manufacturer;
using BRO.Domain.Query.Product;
using BRO.Domain.Query.ProductTaste;
using BRO.Domain.Query.Taste;
using BRO.Domain.Utilities.PaginationSortingRules;
using BRO.Domain.Utilities.StaticDetails;
using BRO.UI.Extensions;
using BRO.UI.Utilities;
using BRO.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace BRO.UI.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public ProductController(IMediator mediator, IMapper mapper, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _mapper = mapper;
            _env = env;
        }
        [HttpGet]

        [Authorize(Roles = Roles.RoleAdmin + "," + Roles.RoleEmployee)]
        public async Task<IActionResult> Index(SearchProductsQuery query)
        {
            return View(await Fetch(query));
        }
        private async Task<PagedResultViewModel<ProductDTO>> Fetch(SearchProductsQuery query)
        {
            var viewModel = new PagedResultViewModel<ProductDTO>();
            viewModel.Result = await _mediator.QueryAsync(query);
            viewModel.PageSizes = Pagination.GetPageSizesAndSelected(query.PageSize);
            viewModel.Sorting = Pagination.GetSortRulesAndSelected(new ProductSortingRules(), query.SortBy);
            viewModel.SearchName = query.SearchName;
            viewModel.SearchValue = query.SearchValue;
            return viewModel;
        }
        [HttpGet]
        [Authorize(Roles = Roles.RoleAdmin)]
        public async Task<IActionResult> Add()
        {
            var addProductViewModel = new AddProductViewModel()
            {
                Product = new AddProductCommand(),
                Categories = (await _mediator.QueryAsync(new GetCategoriesQuery())).Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() }),
                Tastes = (await _mediator.QueryAsync(new GetTastesQuery())).Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() }),
                Manufacturers = (await _mediator.QueryAsync(new GetManufacturersQuery())).Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() }),
            };
            return View(addProductViewModel);
        }

        [HttpPost]
        [Authorize(Roles = Roles.RoleAdmin)]
        public async Task<IActionResult> Add(AddProductViewModel command)
        {
            Tuple<string, string> fileName_filePath_ImageUrlLarge = null;
            Tuple<string, string> fileName_filePath_ImageUrlMain = null;
            if (command.ImageUrlLarge != null)
            {
                fileName_filePath_ImageUrlLarge = CreateFilePath(command.ImageUrlLarge);
                command.Product.ImageUrlLarge = fileName_filePath_ImageUrlLarge.Item1;
            }
            if (command.ImageUrlMain != null)
            {
                fileName_filePath_ImageUrlMain = CreateFilePath(command.ImageUrlMain);
                command.Product.ImageUrlMain = fileName_filePath_ImageUrlMain.Item1;
            }
            var result = await _mediator.CommandAsync(command.Product);
            if (result.IsSuccess)
            {
                CopyImageToFilePath(fileName_filePath_ImageUrlLarge.Item2, command.ImageUrlLarge);
                CopyImageToFilePath(fileName_filePath_ImageUrlMain.Item2, command.ImageUrlMain);
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie dodano produkt", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }
            command.Categories = (await _mediator.QueryAsync(new GetCategoriesQuery())).Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() });
            command.Tastes = (await _mediator.QueryAsync(new GetTastesQuery())).Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() });
            command.Manufacturers = (await _mediator.QueryAsync(new GetManufacturersQuery())).Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() });
            ModelState.PopulateValidation(result.Errors, result.Message, "Product.", new List<string>() { "ImageUrlLarge", "ImageUrlMain" });
            return View(command);
        }
        private void CopyImageToFilePath(string path, IFormFile form)
        {
            using (var FileStream = new FileStream(path, FileMode.Create))
            {
                form.CopyTo(FileStream);
            }
        }
        private Tuple<string, string> CreateFilePath(IFormFile form)
        {
            string uploadsFolder = Path.Combine(_env.WebRootPath, "images", "products");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + form.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            return new Tuple<string, string>(uniqueFileName, filePath);
        }

        [HttpGet]
        [Authorize(Roles = Roles.RoleAdmin)]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productDetailsDTO = await _mediator.QueryAsync(new GetProductWithDetailsQuery() { Id = id });
            var model = _mapper.Map<EditProductCommand>(productDetailsDTO);
            model.TasteIds = productDetailsDTO.ProductTastes.Select(n => n.TasteId);
            var editProductViewModel = new EditProductViewModel()
            {
                Product = model,
                Categories = (await _mediator.QueryAsync(new GetCategoriesQuery())).Select(n=> new SelectListItem() { Text=n.Name, Value=n.Id.ToString()}),
                Tastes = (await _mediator.QueryAsync(new GetTastesQuery())).Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString()}),
                Manufacturers = (await _mediator.QueryAsync(new GetManufacturersQuery())).Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString()}),
            };
            return View(editProductViewModel);
        }
        [HttpGet]
        [Authorize(Roles = Roles.RoleAdmin + "," + Roles.RoleEmployee)]
        public async Task<IActionResult> EditQuantity(Guid id)
        {
            var productDTO= await _mediator.QueryAsync(new GetProductQuery() { Id = id });
            var productTastesDTO = await _mediator.QueryAsync(new GetProductTastesQuery() { ProductId = id });
            var model = new EditProductQuantityCommand();
            model.ProductTastes = _mapper.Map<List<EditProductTasteCommand>>(productTastesDTO);
            model.ProductConcurrencyStamp = productDTO.ConcurrencyStamp;
            model.ProductId = productDTO.Id;
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = Roles.RoleAdmin + "," + Roles.RoleEmployee)]
        public async Task<IActionResult> EditQuantity(EditProductQuantityCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if(result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano magazyn", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }
            var productTastesDTO = await _mediator.QueryAsync(new GetProductTastesQuery() { ProductId = command.ProductTastes[0].ProductId });
            var model = new EditProductQuantityCommand();
            model.ProductTastes = _mapper.Map<List<EditProductTasteCommand>>(productTastesDTO);
            foreach (var item in model.ProductTastes)
                item.InStock = command.ProductTastes.FirstOrDefault(n => n.Id == item.Id).InStock;
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = Roles.RoleAdmin)]
        public async Task<IActionResult> Edit(EditProductViewModel command)
        {
            string previousImageUrlLarge = command.Product.ImageUrlLarge;
            string previousImageUrlMain = command.Product.ImageUrlMain;
            Tuple<string, string> fileName_filePath_ImageUrlLarge = null;
            Tuple<string, string> fileName_filePath_ImageUrlMain = null;
            if (command.ImageUrlLarge != null)
            {
                fileName_filePath_ImageUrlLarge = CreateFilePath(command.ImageUrlLarge);
                command.Product.ImageUrlLarge = fileName_filePath_ImageUrlLarge.Item1;
            }
            if (command.ImageUrlMain != null)
            {
                fileName_filePath_ImageUrlMain = CreateFilePath(command.ImageUrlMain);
                command.Product.ImageUrlMain = fileName_filePath_ImageUrlMain.Item1;
            }        
            var result = await _mediator.CommandAsync(command.Product);
            if (result.IsSuccess)
            {
                if (fileName_filePath_ImageUrlLarge!= null)
                {
                    string uploadedFilePath = Path.Combine(_env.WebRootPath, "images", "products", previousImageUrlLarge ?? "");
                    if (System.IO.File.Exists(uploadedFilePath))
                        System.IO.File.Delete(uploadedFilePath);
                    CopyImageToFilePath(fileName_filePath_ImageUrlLarge.Item2, command.ImageUrlLarge);
                }
                if (fileName_filePath_ImageUrlMain != null)
                {
                    string uploadedFilePath = Path.Combine(_env.WebRootPath, "images", "products", previousImageUrlMain ?? "");
                    if (System.IO.File.Exists(uploadedFilePath))
                        System.IO.File.Delete(uploadedFilePath);
                    CopyImageToFilePath(fileName_filePath_ImageUrlMain.Item2, command.ImageUrlMain);
                }
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano produkt", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }
            else
            {
                command.Categories = (await _mediator.QueryAsync(new GetCategoriesQuery())).Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() });
                command.Tastes = (await _mediator.QueryAsync(new GetTastesQuery())).Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() });
                command.Manufacturers = (await _mediator.QueryAsync(new GetManufacturersQuery())).Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() });
                ModelState.PopulateValidation(result.Errors, result.Message, "Product.", new List<string>() { "ImageUrlLarge", "ImageUrlMain" });
                return View(command);
            }
        }
        [HttpPost]
        [Authorize(Roles = Roles.RoleAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productDTO = await _mediator.QueryAsync(new GetProductQuery() { Id = id });
            string uploadedImageUrlLarge = Path.Combine(_env.WebRootPath, "images", productDTO.ImageUrlLarge);
            string uploadedImageUrlMain = Path.Combine(_env.WebRootPath, "images", productDTO.ImageUrlMain);
            var result = await _mediator.CommandAsync(new DeleteProductCommand() { Id = id });
            if (result.IsSuccess)
            {
                if (System.IO.File.Exists(uploadedImageUrlLarge))
                    System.IO.File.Delete(uploadedImageUrlLarge);
                if (System.IO.File.Exists(uploadedImageUrlMain))
                    System.IO.File.Delete(uploadedImageUrlMain);                
            }
            TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = result.IsSuccess ? "Pomyślnie usunięto kategorię" : result.Message, ActionSuccessfull = result.IsSuccess });
            return RedirectToAction(nameof(Index));

        }
    }
}
