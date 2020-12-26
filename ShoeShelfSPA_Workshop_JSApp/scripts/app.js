import {homePage} from './controllers/homePage.js';
import { registerPage, loginPage, logout,
         registerPost, loginPost} 
         from './controllers/user.js';
import {createOfferPage, detailsPage, 
        editPage, buy, deleteOffer,
        createOfferPost, editOfferPost} 
         from './controllers/offers.js';

const app = Sammy('#root', function () {
    this.use('Handlebars', 'hbs');

    //User

    ///Get
    this.get('/home', homePage);

    this.get('/register', registerPage);

    this.get('/login', loginPage);

    this.get('/logout', logout);

    ///Post

    this.post('/register', registerPost);

    this.post('/login', loginPost)


    // Products

    ///Get
    this.get('/create-offer',createOfferPage);

    this.get('/details/:id', detailsPage)

    this.get('/edit/:id', editPage);

    this.get('/buy/:id', buy)

    this.get('/delete/:id', deleteOffer)

    ///Post
    this.post('/create-offer', createOfferPost)

    this.post('/edit/:id', editOfferPost)
});


app.run('/home');
