﻿using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    public class Categoria : Entity
    {
        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;

            Validar();
        }

        public override string ToString() => $"{Nome} - {Codigo}";

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do categoria não pode estar vazio.");
            Validacoes.ValidarSeIgual(Codigo, 0, "O campo Codigo da categoria não pode ser zero.");
        }
    }
}
