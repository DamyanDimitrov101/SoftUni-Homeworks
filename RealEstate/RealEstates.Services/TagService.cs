using RealEstates.Data;
using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstates.Services
{
    public class TagService : ITagService
    {
        private readonly RealEstateDbContext db;

        public TagService(RealEstateDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<TagViewModel> GetTagViews()
        {
            return this.db.Tags
                .Select(x=> new TagViewModel
                {
                    Name = x.Name,
                    Description = x.Description
                })
                .ToList();
        }
    }
}
