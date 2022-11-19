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
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.HasMany(p => p.Usuarios).WithOne(p => p.Curso);
            builder.HasOne(p => p.Departamento).WithOne().IsRequired();
        }
    }
}
