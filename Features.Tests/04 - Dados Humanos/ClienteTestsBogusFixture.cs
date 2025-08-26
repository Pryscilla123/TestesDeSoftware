using Bogus.DataSets;
using Bogus;
using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteBogusCollection))]
    public class ClienteBogusCollection : ICollectionFixture<ClienteTestsBogusFixture>
    {
        // Esta classe é usada apenas para marcar a coleção de testes e não contém código.
        // A coleção de testes permite que o fixture seja compartilhado entre vários testes.
    }
    public class ClienteTestsBogusFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            //var email = new Faker().Internet.Email(firstName:"eduardo", lastName:"pires", provider:"gmail");
            //var clientefaker = new Faker<Cliente>();
            //clientefaker.RuleFor(c => c.Nome, setter: (f, c) => f.Name.FirstName(genero));

            var cliente = new Faker<Cliente>(locale: "pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    nome: f.Name.FirstName(genero),
                    sobrenome: f.Name.LastName(genero),
                    dataNascimento: f.Date.Past(yearsToGoBack: 80, refDate: DateTime.Now.AddYears(-18)),
                    email: "",
                    ativo: true,
                    dataCadastro: DateTime.Now))
                .RuleFor(property: c => c.Email, setter: (f, c) =>
                    f.Internet.Email(firstName: c.Nome.ToLower(), lastName: c.Nome.ToLower()));

            return cliente;
        }

        public Cliente GerarClienteInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            return new Faker<Cliente>(locale: "pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    nome: f.Name.FirstName(genero),
                    sobrenome: f.Name.LastName(genero),
                    dataNascimento: f.Date.Past(yearsToGoBack: 1, refDate: DateTime.Now.AddYears(1)),
                    email: "",
                    ativo: false,
                    dataCadastro: DateTime.Now));
        }

        public IEnumerable<Cliente> GerarClientesValidos(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var clientes = new Faker<Cliente>(locale: "pt_BR").CustomInstantiator(f => new Cliente(
                Guid.NewGuid(),
                nome: f.Name.FirstName(genero),
                sobrenome: f.Name.LastName(genero),
                dataNascimento: f.Date.Past(yearsToGoBack: 80, refDate: DateTime.Now.AddYears(-18)),
                email: f.Internet.Email(),
                ativo: ativo,
                dataCadastro: f.Date.Past(yearsToGoBack: 4, refDate: DateTime.Now)))
                .RuleFor(property: c => c.Email, setter: (f, c) =>
                    f.Internet.Email(firstName: c.Nome.ToLower(), lastName: c.Sobrenome.ToLower()));

            return clientes.Generate(quantidade);
        }

        public IEnumerable<Cliente> ObterClientesVariados()
        {
            var clientes = new List<Cliente>();

            clientes.AddRange(GerarClientesValidos(50, true).ToList());
            clientes.AddRange(GerarClientesValidos(50, false).ToList());

            return clientes;
        }

        public void Dispose()
        {
            // Aqui você pode liberar recursos, se necessário.
        }
    }
}
