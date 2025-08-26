using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Tests
{
    public class CalculadoraTests
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(2, 2);

            // Assert
            Assert.Equal(4, resultado);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(3, 5, 8)]
        [InlineData(10, 20, 30)]
        [InlineData(0, 0, 0)]
        [InlineData(-1, -1, -2)]
        [InlineData(-5, 5, 0)]
        public void Calculadora_Somar_RetornarValoresCorretos(double a, double b, double total)
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(a, b);

            // Assert
            Assert.Equal(total, resultado);
        }
    }
}
