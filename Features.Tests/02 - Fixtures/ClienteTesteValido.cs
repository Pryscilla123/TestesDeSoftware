using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Clientes;

namespace Features.Tests
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTesteValido
    {
        readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteTesteValido(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Fixture Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrang
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.True(result);
            Assert.Equal(expected:0, actual:cliente.ValidationResult.Errors.Count);
        }
    }
}
