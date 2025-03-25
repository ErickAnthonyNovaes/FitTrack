using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/exercicios")]
[ApiController]
public class ExercicioController : ControllerBase
{
    private readonly DBFitTrackContext _context;

    public ExercicioController(DBFitTrackContext context)
    {
        _context = context;
    }

    // Endpoint para adicionar um novo exercício
    [HttpPost]
    public async Task<IActionResult> AdicionarExercicio([FromBody] Exercicio exercicio)
    {
        // Verifica se o exercício tem um usuário válido associado
        if (exercicio.UsuarioId <= 0)
        {
            return BadRequest(new { message = "Usuário inválido!" });
        }

        // Adiciona o exercício no banco de dados
        _context.Exercicios.Add(exercicio);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Exercício adicionado com sucesso!" });
    }

    // Endpoint para pegar os exercícios de um usuário específico
    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> ObterExercicios(int usuarioId)
    {
        // Pega todos os exercícios do usuário especificado
        var exercicios = await _context.Exercicios
            .Where(e => e.UsuarioId == usuarioId)
            .ToListAsync();

        if (exercicios.Count == 0)
        {
            return NotFound(new { message = "Nenhum exercício encontrado." });
        }

        return Ok(exercicios);
    }

    // Endpoint para remover um exercício pelo ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverExercicio(int id)
    {
        var exercicio = await _context.Exercicios.FindAsync(id);
        if (exercicio == null)
        {
            return NotFound(new { message = "Exercício não encontrado." });
        }

        _context.Exercicios.Remove(exercicio);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Exercício removido com sucesso!" });
    }
}
