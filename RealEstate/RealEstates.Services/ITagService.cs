using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstates.Services
{
    public interface ITagService
    {
        IEnumerable<TagViewModel> GetTagViews();
    }
}
