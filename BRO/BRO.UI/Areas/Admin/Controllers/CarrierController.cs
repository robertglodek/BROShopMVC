using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Carrier;
using BRO.Domain.Query.Carrier;
using BRO.Domain.Query.DTO.Carrier;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Utilities;
using BRO.Domain.Utilities.PaginationSortingRules;
using BRO.Domain.Utilities.StaticDetails;
using BRO.UI.Extensions;
using BRO.UI.Utilities;
using BRO.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static BRO.Domain.Utilities.Result;

namespace BRO.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.RoleAdmin)]
    public class CarrierController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public CarrierController(IMediator mediator, IMapper mapper, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _mapper = mapper;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Index(SearchCarriersQuery query)
        {
            return View(await Fetch(query));
        }
        private async Task<PagedResultViewModel<CarrierDTO>> Fetch(SearchCarriersQuery query)
        {
            var viewModel = new PagedResultViewModel<CarrierDTO>();
            viewModel.Result = await _mediator.QueryAsync(query);
            viewModel.PageSizes = Pagination.GetPageSizesAndSelected(query.PageSize);
            viewModel.Sorting = Pagination.GetSortRulesAndSelected(new CarrierSortingRules(), query.SortBy);
            viewModel.SearchName = query.SearchName;
            viewModel.SearchValue = query.SearchValue;
            return viewModel;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
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
            string uploadsFolder = Path.Combine(_env.WebRootPath, "images", "carriers");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + form.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            return new Tuple<string, string>(uniqueFileName, filePath);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCarrierViewModel model)
        {
            Tuple<string, string> fileName_filePath =null;
            if (model.Image != null)
            {
                fileName_filePath = CreateFilePath(model.Image);
                model.Command.Image = fileName_filePath.Item1;
            }
            var result = await _mediator.CommandAsync(model.Command);
            if (result.IsSuccess)
            {
                if(fileName_filePath != null)
                    CopyImageToFilePath(fileName_filePath.Item2, model.Image);
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie dodano dostawcę", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }
            ModelState.PopulateValidation(result.Errors, result.Message, "Command.", new List<string>() {"Image"});
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new EditCarrierViewModel();
            var categoryDTO = await _mediator.QueryAsync(new GetCarrierQuery() { Id = id });
            model.Command = _mapper.Map<EditCarrierCommand>(categoryDTO);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCarrierViewModel model)
        {

            string previousImageUrlLarge = model.Command.Image;
            Tuple<string, string> fileName_filePath = null;
            if (model.Image != null)
            {
                fileName_filePath = CreateFilePath(model.Image);
                model.Command.Image = fileName_filePath.Item1;
            }
            var result = await _mediator.CommandAsync(model.Command);
            if (result.IsSuccess)
            {
                if (fileName_filePath != null)
                {
                    string uploadedFilePath = Path.Combine(_env.WebRootPath, "images", "carriers", previousImageUrlLarge ?? "");
                    if (System.IO.File.Exists(uploadedFilePath))
                        System.IO.File.Delete(uploadedFilePath);
                    CopyImageToFilePath(fileName_filePath.Item2, model.Image); 
                }
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano dostawcę", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }

            
            ModelState.PopulateValidation(result.Errors, result.Message, "Command.", new List<string>() { "Image" });
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var carrier = await _mediator.QueryAsync(new GetCarrierQuery() { Id = id });
            var result = await _mediator.CommandAsync(new DeleteCarrierCommand() { Id = id });
            if (result.IsSuccess)
            {  
                string uploadedFilePath = Path.Combine(_env.WebRootPath, "images", "carriers", carrier.Image ?? "");
                if (System.IO.File.Exists(uploadedFilePath))
                    System.IO.File.Delete(uploadedFilePath);
            }
            TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = result.IsSuccess ? "Pomyślnie usunięto dostawcę" : result.Message, ActionSuccessfull = result.IsSuccess });
            return RedirectToAction(nameof(Index));
        }
    }
}