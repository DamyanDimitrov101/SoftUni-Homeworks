import * as api from './data.js';
import {homePage, postCreate, createPage, detailsPage, deleteArticle, editPage, postEdit} from './controllers/catalog.js';
import {registerPage, loginPage, postRegister, postLogin, logout} from './controllers/user.js';
import { getUserData } from './util.js';

window.api = api;


const app = Sammy('#root',function (context) {
   
    //Template Engine setup
    this.use('Handlebars', 'hbs');

    const user = getUserData();
    context.userData = user;   
   
    //GET
    this.get('/',homePage);
    this.get('/home',homePage);

    this.get('/register',registerPage);
    this.get('/login',loginPage);
    this.get('/logout',logout);

    
    this.get('/create',createPage);

    this.get('/details/:id',detailsPage);
    

    this.get('/delete/:id',deleteArticle);

    this.get('/edit/:id',editPage);
    


    //POST
    
    this.post('/register',(ctx) => {postRegister(ctx); });
    this.post('/login',(ctx) => {postLogin(ctx); });
    
    
    this.post('/create',(ctx) => {postCreate(ctx); });
    this.post('/edit/:id',(ctx) => {postEdit(ctx); });

});


app.run();