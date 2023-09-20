using Microsoft.EntityFrameworkCore;
using PruebaApi.Data;
using PruebaApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<UsuariosDB>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/usuarios/", async (Usuarios u, UsuariosDB db) =>
{
    db.Usuarios.Add(u);
    await db.SaveChangesAsync();

    return Results.Created($"/usuarios/{u.Id}", u);
});

app.MapGet("/usuarios/{id:int}", async (int id, UsuariosDB db) =>
{
    return await db.Usuarios.FindAsync(id)
        is Usuarios u
        ? Results.Ok(u)
        : Results.NotFound();
});

app.MapGet("/usuarios", async (UsuariosDB db) =>
{
    return await db.Usuarios.ToListAsync();  
});

app.MapPut("/usuarios/{id:int}", async (int id, Usuarios u ,UsuariosDB db) =>
{
    if (u.Id != id)
        return Results.BadRequest();

    var usuario = await db.Usuarios.FindAsync(id);

    if(usuario is null) return Results.NotFound();

    usuario.Nombre = u.Nombre;
    usuario.Apellido = u.Apellido;
    usuario.Edad = u.Edad;
    usuario.Email = u.Email;
    usuario.Telefono = u.Telefono;

    await db.SaveChangesAsync();

    return Results.Ok(usuario);
});

app.MapDelete("/usuarios/{id:int}", async (int id, UsuariosDB db) =>
{
    var usuario = await db.Usuarios.FindAsync(id);
    if (usuario is not null)
    {
        db.Usuarios.Remove(usuario);
        await db.SaveChangesAsync();

        return Results.NoContent();
    }
    else
    {
        return Results.NotFound();
    }
});

app.UseAuthorization();

app.MapControllers();

app.Run();
