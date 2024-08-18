using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

namespace ApiCrud.Estudantes
{
    public static class EstudantesRotas
    {
        public static void AddRotasEstudantes(this WebApplication app)
        {
            // Declaração correta da variável
            var rotasEstudantes = app.MapGroup(prefix: "estudantes");

            // Para criar se usa o Post
            rotasEstudantes.MapPost(pattern: "",
                handler: async (AddEstudanteRequest request, AppDbContext context) =>
            {
                var jaExiste = await context.Estudantes
                .AnyAsync(estudante => estudante.Nome == request.Nome);

                if (jaExiste)
                    return Results.Conflict("Ja existe!");

                var novoEstudante = new Estudante(request.Nome);
                await context.Estudantes.AddAsync(novoEstudante);
                await context.SaveChangesAsync();

                var estudanteRetorno = new EstudanteDto(novoEstudante.Id, novoEstudante.Nome);

                return Results.Ok(estudanteRetorno);


                
            });

            //RETORNAR TODOS OS ESTUDANTES CADASTRADOS 
            rotasEstudantes.MapGet("", async (AppDbContext context) =>
            {
                var estudantes = await context
                .Estudantes
                .Where(estudante => estudante.Ativo)
                .Select(estudante => new EstudanteDto(estudante.Id, estudante.Nome))
                .ToListAsync();

                return estudantes;
            });

            //ATUALIZAR NOME Estudante

            rotasEstudantes.MapPut("{id:guid}", async (Guid id, UpdateEstudanteRequest request, AppDbContext context) =>
            {
                var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == id);

                if (estudante == null)
                    return Results.NotFound();

                estudante.AtualizarNome(request.Nome);
                await context.SaveChangesAsync();

                return Results.Ok(new EstudanteDto(estudante.Id, estudante.Nome));
            });

            rotasEstudantes.MapDelete(pattern:"{id}",
                async (Guid id, AppDbContext context) =>
                {
                    var estudante = await context.Estudantes
                    .SingleOrDefaultAsync(estudante => estudante.Id == id);


                    if (estudante == null)
                        return Results.NotFound();

                    estudante.Desativar();

                    await context.SaveChangesAsync();
                    return Results.Ok();

                    

                });
              
        }
    }
}
