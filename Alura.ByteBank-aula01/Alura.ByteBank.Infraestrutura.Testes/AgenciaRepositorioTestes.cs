﻿using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _repositorio;

        public AgenciaRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IAgenciaRepositorio, AgenciaRepositorio>();
            var provider = servico.BuildServiceProvider();
            _repositorio = provider.GetService<IAgenciaRepositorio>();
        }

        [Fact]
        public void TestaObterTodasAgencias()
        {
            //Arrange
            //Act
            List<Agencia> lista = _repositorio.ObterTodos();

            //Assert
            Assert.NotNull(lista);
        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            //Arrange
            //Act
            var agencia = _repositorio.ObterPorId(1);

            //Assert
            Assert.NotNull(agencia);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestaObterAgenciasPorVariosId(int id)
        {
            //Arrange
            //Act
            var agencia = _repositorio.ObterPorId(id);

            //Assert
            Assert.NotNull(agencia);

        }

        [Fact]
        public void TestaAtualizacaoInformacaoDeterminadaAgencia()
        {
            //Arrange
            var agencia = _repositorio.ObterPorId(2);
            var nomeNovo = "Agencia Nova";
            agencia.Nome = nomeNovo;

            //Act
            var atualizado = _repositorio.Atualizar(2, agencia);

            //Assert
            Assert.True(atualizado);
        }


        // Testes com Mock
        [Fact]
        public void TestaObterAgenciasMock()
        {
            //Arrange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object;

            //Act
            var lista = mock.BuscarAgencias();

            //Assert
            bytebankRepositorioMock.Verify(b => b.BuscarAgencias());
        }

        [Fact]
        public void TestaAdiconarAgenciaMock()
        {
            // Arrange
            var agencia = new Agencia()
            {
                Nome = "Agência Amaral",
                Identificador = Guid.NewGuid(),
                Id = 4,
                Endereco = "Rua Arthur Costa",
                Numero = 6497
            };

            var repositorioMock = new ByteBankRepositorio();

            //Act
            var adicionado = repositorioMock.AdicionarAgencia(agencia);

            //Assert
            Assert.True(adicionado);
        }
    }
}
