using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    public class Dimensoes
    {
        public decimal Altura { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Profundidade { get; private set; }

        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Validacoes.ValidarSeMenorIgualMinimo(altura, 0, "O campo Altura deve ser maior que 0");
            Validacoes.ValidarSeMenorIgualMinimo(largura, 0, "O campo Largura deve ser maior que 0");
            Validacoes.ValidarSeMenorIgualMinimo(profundidade, 0, "O campo Profundidade deve ser maior que 0");

            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }

        public string DescricaoFormatada() => $"LxAxP: {Largura} x {Altura} x {Profundidade}";

        public override string ToString() => DescricaoFormatada();
    }
}
