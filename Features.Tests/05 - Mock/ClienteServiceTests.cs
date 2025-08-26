using Features.Clientes;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    [Collection(nameof(ClienteTestsBogusFixture))]
    public class ClienteServiceTests
    {
        readonly ClienteTestsBogusFixture _clienteTestsBogusFixture;

        public ClienteServiceTests()
        {
            _clienteTestsBogusFixture = new ClienteTestsBogusFixture();
        }

        [Fact(DisplayName = "Adicionar Cliente com sucesso")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteValido();
            var clienteRepository = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepository.Object, mediator.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            clienteRepository.Verify(r => r.Adicionar(cliente), Times.Once);
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com falha")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteInvalido();
            var clienteRepository = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepository.Object, mediator.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            clienteRepository.Verify(r => r.Adicionar(cliente), Times.Never);
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Todos Ativos")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            var clienteRepo = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            clienteRepo.Setup(c=>c.ObterTodos())
                .Returns(_clienteTestsBogusFixture.ObterClientesVariados());

            var clienteService = new ClienteService(clienteRepo.Object, mediator.Object);

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert
            clienteRepo.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c=>!c.Ativo) > 0);
        }
    }
}
