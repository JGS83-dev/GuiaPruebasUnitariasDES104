using PersonasAPI.Models;
using PersonasAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PersonasAPI.Tests
{
    public class PersonasControllerTests
    {
        [Fact]
        public async Task PostAgregarPersona_DatosValidos()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var personaPrueba = new Persona 
            { 
                PrimerNombre = "Sandra",
                SegundoNombre = "Elizabeth",
                PrimerApellido = "Sanchez", 
                SegundoApellido = "Diaz",
                FechaNacimiento = new DateTime(1970,11, 27),
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(personaPrueba);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var persona = Assert.IsType<Persona>(createdResult.Value);
            Assert.Equal("Sandra", persona.PrimerNombre);
        }               

        [Fact]
        public async Task PosPersona_NoAgregar_PrimerNombreNulo()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var personaPrueba = new Persona
            {
                PrimerNombre = null,
                SegundoNombre = "Armando",
                PrimerApellido = "Garcia",
                SegundoApellido = "Canizalez",
                FechaNacimiento = new DateTime(1959, 4, 25),
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(personaPrueba);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostPersona_AgregaPersona_CamposOpcionales()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var personaPrueba = new Persona
            {
                PrimerNombre = "Victor",
                SegundoNombre = null,
                PrimerApellido = "Garcia",                
                SegundoApellido = null,
                FechaNacimiento = new DateTime(1998, 10, 10),
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(personaPrueba);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var persona = Assert.IsType<Persona>(createdResult.Value);
            Assert.Equal("Victor", persona.PrimerNombre);
        }

        [Fact]
        public async Task PosPersona_NoAgregar_LimiteNombre()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var personaPrueba = new Persona
            {
                PrimerNombre = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                SegundoNombre = "Elizabeth",
                PrimerApellido = "Sanchez",
                SegundoApellido = "Diaz",
                FechaNacimiento = new DateTime(1970, 11, 27),
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(personaPrueba);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostPersona_NoAgregar_FechaNula()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var personaPrueba = new Persona
            {
                PrimerNombre = "Sandra",
                SegundoNombre = "Elizabeth",
                PrimerApellido = "Sanchez",
                SegundoApellido = "Diaz",
                FechaNacimiento = null,
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(personaPrueba);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostPersona_NoAgregar_FechaNoValida()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var personaPrueba = new Persona
            {
                PrimerNombre = "Sandra",
                SegundoNombre = "Elizabeth",
                PrimerApellido = "Sanchez",
                SegundoApellido = null,
                FechaNacimiento = new DateTime(2040, 11, 27),
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(personaPrueba);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostPersona_NoAgregar_DuiInvalido()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var personaPrueba = new Persona
            {
                PrimerNombre = "Sandra",
                SegundoNombre = "Elizabeth",
                PrimerApellido = "Sanchez",
                SegundoApellido = "Diaz",
                FechaNacimiento = new DateTime(1970, 11, 27),
                Dui = "0-8"
            };

            //Act
            var result = await controller.PostPersona(personaPrueba);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
