


using Moq;
using RestaurantExtended;
using RestaurantExtended.Models;
using RestaurantExtended.Repositories;
using RestaurantExtended.Repositories.implementation;
using RestaurantExtended.Services.implementations;
using ServiceStack;
using TypeMock.ArrangeActAssert;

namespace RestaurantAPITests
{

    //checking stoc with  Comenzi Service and Produs Service and unit of work mocked
    public class UnitTest1
    {
        [Fact]
        public async void VerificareStoc()
         {
            //produse pentru testare 
            //creare date intrare in test


            Produs produs_create = new Produs();


            Produs produs_update = new Produs();
            produs_create.stoc = -2;
            produs_update.stoc = -3;




            Produs produs_creat = null;
            Produs produs_updatat = null;
            Comanda comanda_creata = null;






            Produs produs_comanda1 = new Produs();
            Produs produs_comanda2 = new Produs();
            Produs produs_comanda3 = new Produs();

            ComandaProdus comandaProdus1 = new ComandaProdus();
            ComandaProdus comandaProdus2 = new ComandaProdus();
            ComandaProdus comandaProdus3 = new ComandaProdus();
            ComandaProdus comandaProdus4 = new ComandaProdus();


            produs_comanda1.Id = 1;
            produs_comanda2.Id = 2;
            produs_comanda3.Id = 3;

            produs_comanda1.stoc = -1;
            produs_comanda2.stoc = 0;
            produs_comanda3.stoc = 5;

            comandaProdus1.ProductId = 1;
            comandaProdus1.quantity = -1;

            comandaProdus2.ProductId = 2;
            comandaProdus2.quantity = 2;

            comandaProdus3.ProductId = 3;
            comandaProdus3.quantity = 6;


            //prods comanda corecta
            comandaProdus4.ProductId = 3;
            comandaProdus4.quantity = 5;



            Comanda comanda1 = new Comanda();
            Comanda comanda2 = new Comanda();
            Comanda comanda3 = new Comanda();
            Comanda comanda4 = new Comanda();

            comanda1.produseComanda = new List<ComandaProdus>();
            comanda2.produseComanda = new List<ComandaProdus>();
            comanda3.produseComanda = new List<ComandaProdus>();
            comanda4.produseComanda = new List<ComandaProdus>();

            comanda1.produseComanda.Add(comandaProdus1);
            comanda1.produseComanda.Add(comandaProdus4);


            comanda2.produseComanda.Add(comandaProdus4);
            comanda2.produseComanda.Add(comandaProdus2);

            comanda3.produseComanda.Add(comandaProdus3);
            comanda3.produseComanda.Add(comandaProdus4);


            comanda4.produseComanda.Add(comandaProdus4);

            //mock Unit of work with 3 repositories
            var mockUnitOfRepoProduse = new Mock<IProdusRepository>();
            var mockUnitOfRepoUsers = new Mock<IUserRepository>();
            var mockUnitOfRepoComenzi = new Mock<IComenziRepository>();


           

            mockUnitOfRepoProduse.Setup(x => x.Get(produs_comanda1.Id)).Returns(Task.FromResult(produs_comanda1));
            mockUnitOfRepoProduse.Setup(x => x.Get(produs_comanda2.Id)).Returns(Task.FromResult(produs_comanda2));
            mockUnitOfRepoProduse.Setup(x => x.Get(produs_comanda3.Id)).Returns(Task.FromResult(produs_comanda3));
            mockUnitOfRepoProduse.Setup(x => x.Get(produs_update.Id)).Returns(Task.FromResult(produs_update));


            mockUnitOfRepoProduse.Setup(x => x.Update(It.IsAny<Produs>())).
               Callback((Produs produs) => produs_updatat =  produs);

            mockUnitOfRepoProduse.Setup(x => x.Add(It.IsAny<Produs>())).
             Callback((Produs produs) => produs_creat= produs);


            mockUnitOfRepoComenzi.Setup(x=>x.Add(It.IsAny<Comanda>())).
                Callback((Comanda comanda)=> comanda_creata=comanda);

       
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(unit => unit.Save()).Returns(Task.FromResult(1));
            mockUnitOfWork.Setup(unit => unit.ComenziRepository).Returns(mockUnitOfRepoComenzi.Object);
            mockUnitOfWork.Setup(unit => unit.ProdusRepository).Returns(mockUnitOfRepoProduse.Object);
            mockUnitOfWork.Setup(unit => unit.UserRepository).Returns(mockUnitOfRepoUsers.Object);


            IUnitOfWork moqunit = mockUnitOfWork.Object;


            //check you can't add comanda if product quantity>product stoc or product_stoc<=0
            ComenziService service1 = new ComenziService(moqunit);


            comanda_creata = null;
            await service1.CreazaComanda(comanda1);


            // Assert.True(comanda_creata != null && comanda_creata == comanda);
            Assert.True(comanda_creata == null);


            comanda_creata = null;
            await service1.CreazaComanda(comanda2);


            // Assert.True(comanda_creata != null && comanda_creata == comanda);
            Assert.True(comanda_creata == null);



            comanda_creata = null;
            await service1.CreazaComanda(comanda3);


            // Assert.True(comanda_creata != null && comanda_creata == comanda);
            Assert.True(comanda_creata == null);

            //check if everything alright comanda!=null


            comanda_creata = null;
            await service1.CreazaComanda(comanda4);


            // Assert.True(comanda_creata != null && comanda_creata == comanda);
            Assert.True(comanda_creata != null);


            //check add and update will use the repository
            //check you can add and update product and have non-negative stoc no matter what data was included
            ProductService service2 = new ProductService(moqunit);


           produs_creat = null;
           
            await service2.addProduct(produs_create);
            Assert.True(produs_creat!=null && produs_creat.stoc>=0);

            produs_updatat = null;

            await service2.editProduct(produs_update);
           
            Assert.True(produs_updatat != null && produs_updatat.stoc >= 0);




        }
    }
}