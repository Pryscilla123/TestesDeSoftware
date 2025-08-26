using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    public class OrdemTestes
    {
        public static bool Teste1Chamado;
        public static bool Teste2Chamado;
        public static bool Teste3Chamado;
        public static bool Teste4Chamado;

        [Fact(DisplayName = "Teste 04")]
        [Trait("Categoria", "Ordenacao Testes"), TestPriority(3)]
        public void Teste04()
        {
            // Arrange & Act
            Teste4Chamado = true;

            // Assert
            Assert.True(Teste3Chamado);
            Assert.True(Teste1Chamado);
            Assert.False(Teste2Chamado);
        }

        [Fact(DisplayName = "Teste 01"), TestPriority(2)]
        [Trait("Categoria", "Ordenacao Testes")]
        public void Teste01()
        {
            // Arrange & Act
            Teste1Chamado = true;

            // Assert
            Assert.True(Teste3Chamado);
            Assert.False(Teste4Chamado);
            Assert.False(Teste2Chamado);
        }

        [Fact(DisplayName = "Teste 03"), TestPriority(1)]
        [Trait("Categoria", "Ordenacao Testes")]
        public void Teste03()
        {
            // Arrange & Act
            Teste3Chamado = true;

            // Assert
            Assert.False(Teste1Chamado);
            Assert.False(Teste2Chamado);
            Assert.False(Teste4Chamado);
        }

        [Fact(DisplayName = "Teste 02"), TestPriority(4)]
        [Trait("Categoria", "Ordenacao Testes")]
        public void Teste02()
        {
            // Arrange & Act
            Teste2Chamado = true;

            // Assert
            Assert.True(Teste1Chamado);
            Assert.True(Teste2Chamado);
            Assert.True(Teste4Chamado);
        }

    }
}
