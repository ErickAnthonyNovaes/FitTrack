using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

[Route("api/usuario")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly DBFitTrackContext _context;

    public UsuarioController(DBFitTrackContext context)
    {
        _context = context;
    }

    // Endpoint para registro de usuário via POST
    [HttpPost("register")]
    public IActionResult Register([FromBody] Usuario usuario)
    {
        // Verifica se já existe um usuário com o mesmo email
        if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            return BadRequest(new { message = "Usuário já existe!" });

        // Criptografa a senha usando MD5 (para fins de demonstração)
        usuario.Senha = ComputeMD5Hash(usuario.Senha);

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        return Ok(new { message = "Usuário registrado com sucesso!" });
    }

    // Endpoint para login de usuário via GET
    [HttpGet("login")]
    public IActionResult Login([FromQuery] string email, [FromQuery] string senha)
    {
        string senhaHash = ComputeMD5Hash(senha);

        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Email == email && u.Senha == senhaHash);

        if (usuario == null)
            return Unauthorized(new { message = "Email ou senha incorretos!" });

        return Ok(new { message = "Login bem-sucedido!", usuario });
    }

    // Método para gerar o hash MD5 de uma string
    private string ComputeMD5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}
