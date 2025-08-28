using Bogus;
using Bogus.DataSets;
using Features.Clientes;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteTestsAutoMockerCollection))]
    public class ClienteTestsAutoMockerCollection : ICollectionFixture<ClienteTestsAutoMockerFixture>
    {
    }
    public class ClienteTestsAutoMockerFixture : IDisposable
    {

        public AutoMocker Mocker;

        public Cliente GerarClienteValido()
        {
            return GerarClientes(1, true).FirstOrDefault();
        }

        public IEnumerable<Cliente> ObterClientesVariados()
        {
            var clientes = new List<Cliente>();

            clientes.AddRange(GerarClientes(50, true).ToList());
            clientes.AddRange(GerarClientes(50, false).ToList());

            return clientes;
        }

        public IEnumerable<Cliente> GerarClientes(int quantidade, bool ativo)
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

        public ClienteService ObterClienteService()
        {
            Mocker = new AutoMocker();

            return Mocker.CreateInstance<ClienteService>();
        }

        public void Dispose()
        {
        }
    }
}
