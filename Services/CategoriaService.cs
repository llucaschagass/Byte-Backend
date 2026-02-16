using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class CategoriaService
{
    private readonly ICategoriaRepository categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        this.categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<Categoria>> GetAllCategorias()
    {
        return await categoriaRepository.GetAllCategorias();
    }

    public async Task<Categoria?> GetCategoriaById(int id)
    {
        return await categoriaRepository.GetCategoriaById(id);
    }

    public async Task<(bool Success, string Message)> CreateCategoria(Categoria categoria)
    {
        var existeCategoria = await categoriaRepository.GetCategoriaByNome(categoria.Nome);
        if (existeCategoria != null)
        {
            return (false, $"Já existe uma categoria com o nome '{categoria.Nome}'.");
        }

        await categoriaRepository.CreateCategoria(categoria);
        return (true, "Categoria criada com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateCategoria(int id, Categoria categoria)
    {
        var categoriaExistente = await categoriaRepository.GetCategoriaById(id);
        if (categoriaExistente == null)
        {
            return (false, "Categoria não encontrada.");
        }
        
        // Verifica se o novo nome já existe e não pertence a esta categoria
        var nomeDuplicado = await categoriaRepository.GetCategoriaByNome(categoria.Nome);
        if (nomeDuplicado != null && nomeDuplicado.CategoriaId != id)
        {
            return (false, $"Já existe uma categoria com o nome '{categoria.Nome}'.");
        }

        categoriaExistente.Nome = categoria.Nome;
        categoriaExistente.Descricao = categoria.Descricao;

        await categoriaRepository.UpdateCategoria(categoriaExistente);
        return (true, "Categoria atualizada com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteCategoria(int id)
    {
        var categoria = await categoriaRepository.GetCategoriaById(id);
        if (categoria == null)
        {
            return (false, "Categoria não encontrada.");
        }

        await categoriaRepository.DeleteCategoria(id);
        return (true, "Categoria excluída com sucesso.");
    }
}
