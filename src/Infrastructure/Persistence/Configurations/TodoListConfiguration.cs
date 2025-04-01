using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo_App.Domain.Entities;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Infrastructure.Persistence.Configurations;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(t => t.Colour)
            .HasConversion(
                colour => colour.ToString(), // 'Colour' object'ü string'e dönüştür
                colourCode => Colour.From(colourCode)) // string'i 'Colour' object'üne dönüştür
            .HasMaxLength(7);
    }
}
