﻿using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain.Entities
{
    public class Dimensoes
    {
        public decimal Altura { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Profundidade { get; private set; }

        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Validacoes.ValidarSeMenorQue(altura, 1, "O campo Altura não pode ser menor que 1");
            Validacoes.ValidarSeMenorQue(largura, 1, "O campo Largura não pode ser menor que 1");
            Validacoes.ValidarSeMenorQue(profundidade, 1, "O campo Profundidade não pode ser menor que 1");

            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }

        public string DescricaoFormatada() => $"LxAxP: {Largura} x {Altura} x {Profundidade}";

        public override string ToString() => DescricaoFormatada();
    }
}
