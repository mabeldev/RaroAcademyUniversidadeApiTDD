using demys_universidade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Infrastructure.Mappings
{
    public class DepartamentoMap : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder
                .HasOne(p => p.Endereco)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
