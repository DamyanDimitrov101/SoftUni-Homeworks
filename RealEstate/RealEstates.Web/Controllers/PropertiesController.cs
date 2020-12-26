using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstates.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstates.Web.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertiesService propertiesService;

        public PropertiesController(IPropertiesService propertiesService)
        {
            this.propertiesService = propertiesService;
        }

        public IActionResult Search()
        {
            var tags = this.propertiesService.GetAllTags();
            return this.View(tags);
        }

        public IActionResult SearchBySize()
        {
            return this.View();
        }

        public IActionResult DoSearch(int minPrice, int maxPrice, string tag)
        {
            var properties = this.propertiesService.SearchByPrice(minPrice, maxPrice,tag);
            return this.View(properties);
        }

        public IActionResult DoSearchBySize(int minYear, int maxYear, int minSize, int maxSize)
        {
            var properties = this.propertiesService.Search(minYear, maxYear,minSize,maxSize);

            return this.View(properties);
        }

    }
}
