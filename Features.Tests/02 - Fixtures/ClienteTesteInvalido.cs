using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTesteInvalido
    {
        readonly ClienteTestsFixture _clienteTestsFixture;
        public ClienteTesteInvalido(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fixture Testes")]
        public void Cliente_NovoCliente_EhInvalido()
        {
            // Arrang
            var cliente = _clienteTestsFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(result);
            Assert.NotEqual(expected: 0, actual: cliente.ValidationResult.Errors.Count);
        }
    }
}
