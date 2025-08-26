using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
    {
        // Esta classe é usada apenas para marcar a coleção de testes e não contém código.
        // A coleção de testes permite que o fixture seja compartilhado entre vários testes.
    }
    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            return new Cliente(
                Guid.NewGuid(),
                nome: "Eduardo",
                sobrenome: "Pires",
                dataNascimento: DateTime.Now.AddYears(-30),
                email:"edu@edu.com",
                ativo: true,
                dataCadastro: DateTime.Now);
        }

        public Cliente GerarClienteInvalido()
        {
            return new Cliente(
                Guid.NewGuid(),
                nome: "",
                sobrenome: "",
                dataNascimento: DateTime.Now,
                email:"edu2edu.com",
                ativo: true,
                dataCadastro: DateTime.Now);
        }

        public void Dispose()
        { 
        }
    }
}
