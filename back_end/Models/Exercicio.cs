using System.ComponentModel.DataAnnotations;

public class Exercicio
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Peso { get; set; }
    public int Repeticoes { get; set; }
    public DateTime Data { get; set; }
    
    // Chave estrangeira para o usuário
    public int UsuarioId { get; set; }  // Referência ao ID do usuário
}
