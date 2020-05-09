using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Categories.Commands.AddCategory;
using Fixit.Application.Categories.Commands.AddSubcategory;
using Fixit.Application.Categories.Commands.RemoveCategory;
using Fixit.Application.Categories.Queries.GetCategories;
using Fixit.Application.Categories.Queries.GetCategory;
using Fixit.Application.Common.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fixit.WebApi.Controllers
{
    public class CategoriesController : BaseController
    {
        private const string GetCategoryAsyncRouteName = "GetCategoryAsync";
        private const string GetSubcategoryAsyncRouteName = "GetSubcategoryAsync";

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromBody] AddCategoryCommand command)
        {
            return await HandleCommandWithLocationResultAsync(command, GetCategoryAsyncRouteName);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<CategoryInfoForList>), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            return await HandleQueryAsync(new GetCategoriesQuery());
        }

        [HttpGet("{id}", Name = GetCategoryAsyncRouteName)]
        [ProducesResponseType(typeof(CategoryInfo), 200)]
        public async Task<IActionResult> GetCategoryAsync([FromRoute] int id)
        {
            return await HandleQueryAsync(new GetCategoryQuery { Id = id });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveCategoryAsync([FromRoute] int id)
        {
            return await HandleCommandAsync(new RemoveCategoryCommand { Id = id });
        }

        [HttpPost("{categoryId}/subcategories")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> AddSubcategoryAsync([FromRoute] int categoryId, [FromBody] AddSubcategoryCommand command)
        {
            command.CategoryId = categoryId;
            return await HandleCommandWithLocationResultAsync(command, GetSubcategoryAsyncRouteName);
        }

        [HttpGet("{categoryId}/subcategories/{id}", Name = GetSubcategoryAsyncRouteName)]
        public async Task<IActionResult> GetSubcategoryAsync([FromRoute] int categoryId, [FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        public CategoriesController(IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(mediator, mapper, currentUserService)
        {
        }
    }
}