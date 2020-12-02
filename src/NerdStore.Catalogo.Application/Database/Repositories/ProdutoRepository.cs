using System;
using NerdStore.Core.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.Repositories;

namespace NerdStore.Catalogo.Application.Database.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Adicionar(Produto produto) => throw new NotImplementedException();
        public void Adicionar(Categoria categoria) => throw new NotImplementedException();
        public void Atualizar(Produto produto) => throw new NotImplementedException();
        public void Atualizar(Categoria categoria) => throw new NotImplementedException();
        public void Dispose() => throw new NotImplementedException();
        public Task<IEnumerable<Categoria>> ObterCategorias() => throw new NotImplementedException();
        public Task<IEnumerable<Produto>> ObterPorCategoria(int categoriaId) => throw new NotImplementedException();
        
        public Task<Produto> ObterPorId(Guid id) 
        {
            var produto = new Produto("teste", "produto teste do trapp", true, 10m, new Guid("6cf914d3-7dd6-49b0-8dca-ec799abe6b40"), DateTime.Now, 
                "imagem", dimensoes: new Dimensoes(1m, 1m, 1m));

            produto.AlterarCategoria(new Categoria("produto", 1));
            produto.ReporEstoque(9);
            
            return Task.FromResult(produto);
        }
        
        public Task<IEnumerable<Produto>> ObterTodos() => throw new NotImplementedException();
    }
}
