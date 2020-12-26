using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Data.EntityConfigurations
{
    internal class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.
                Property(a => a.FirstName)
                .IsUnicode()
                .IsRequired(false);

            builder.
                Property(a => a.LastName)
                .IsUnicode()
                .IsRequired();

            
        }
    }
}
