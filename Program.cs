using System;
using System.Collections;
using System.Collections.Generic;

namespace teste_delegate
{
    public class Post
    {
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string Categoria { get; set; }
        public int qtdLike {get; set;}
    }

    class Program
    {
        static private List<Post> posts;

        public class Pessoa
        {}
        public class PessoaFisica: Pessoa{}
        public class PessoaJuridica: Pessoa{}

        public class ClasseTeste
        {
            public ClasseTeste (Pessoa pessoa)=>Console.WriteLine("Entrou no construtor");
        }

        // Função que retorna um delegate
        static private CalculoEntreDoisNumeros criaDelegate()
        {
            return (x,y) => x + y;
        }

        static void Main(string[] args)
        {
            var delegateCriadoPorMetodo = criaDelegate();

            Console.WriteLine($"Resultado: {delegateCriadoPorMetodo(3,2)}");

            // Contravariância  
            ClasseTeste classeteste = new ClasseTeste(new PessoaFisica());
            ClasseTeste classeteste2 = new ClasseTeste(new PessoaJuridica());

            // Delegate de operação com dois números
            CalculoEntreDoisNumeros operacao = Soma;

            // Posso fazer utilizando new tb e passar a função hehhehe
            CalculoEntreDoisNumeros operacao4 = new CalculoEntreDoisNumeros(Soma);
            Console.WriteLine($"Soma 4: {operacao4(1, 2)}");

            // Não preciso especificar o tipo dos parâmetros de entrada pq o delegate
            // diz para o compilador quais são os tipos passados
            CalculoEntreDoisNumeros operacao3 = (numero1, numero2) => numero2 + numero1;

            CalculoEntreDoisNumerosGenerico<double> operacao2 = Divisao;
            
            Console.WriteLine($"Divisao:{operacao2(10,3)}");

            posts = new List<Post> {
                new Post
                {
                    Titulo = "Harry Potter",
                    Resumo = "Pedra Filosofal",
                    Categoria = "Filmes"
                },
                new Post
                {
                    Titulo = "Harry Potter2",
                    Resumo = "Pedra Filosofal2",
                    Categoria = "Filmes2"
                },
                new Post
                {
                    Titulo = "Harry Potter3",
                    Resumo = "Pedra Filosofal3",
                    Categoria = "Filmes3"
                }
            };

            ExecutaAcaoEmListaFiltradaDePosts(posts, 
                                    (post) => post.Categoria.Equals("Filmes"), 
                                    (post) => Console.WriteLine(post.Titulo));

            ExecutaAcaoEmListaFiltradaDePosts2(posts, 
                                    (post) => post.Categoria.Equals("Filmes"), 
                                    (post) => Console.WriteLine(post.Titulo));
        }

        static int Soma(int numero1, int numero2)
        {
            return numero1 + numero2;
        }

        static double Divisao(double numero1, double numero2)
        {
            return numero1 / numero2;
        }

        // Método que recebe como argumento dois delegates
        // sendo um com um delegate com tipo genérico (CondicaoPost<Post>)
        private static void ExecutaAcaoEmListaFiltradaDePosts(
        IEnumerable posts,
        CondicaoPost<Post> condicaoQualquer,
        AcaoEmUmPost acaoQualquer)
        {
            foreach (Post post in posts)
            {
                if (condicaoQualquer(post))
                    acaoQualquer(post);
            }
        }

        // Utilizando delegate tipado (CondicaoPost)
        private static void ExecutaAcaoEmListaFiltradaDePosts2(
        IEnumerable posts,
        CondicaoPost condicaoQualquer,
        AcaoEmUmPost acaoQualquer)
        {
            foreach (Post post in posts)
            {
                if (condicaoQualquer(post))
                    acaoQualquer(post);
            }
        }
    }
}
