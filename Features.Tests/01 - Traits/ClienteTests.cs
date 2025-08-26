using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    public class ClienteTests
    {
        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Trait Tests")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                nome:"Eduardo",
                sobrenome:"Pires",
                dataNascimento:DateTime.Now.AddYears(-30),
                email:"edu@edu.com",
                ativo:true,
                dataCadastro:DateTime.Now);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.True(result);
            Assert.Equal(expected: 0, actual: cliente.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Trait Tests")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                nome:"",
                sobrenome:"",
                dataNascimento:DateTime.Now,
                email:"edu2edu.com",
                ativo:true,
                dataCadastro:DateTime.Now);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(result);
            Assert.NotEqual(expected: 0, actual: cliente.ValidationResult.Errors.Count);
        }
    }
}
